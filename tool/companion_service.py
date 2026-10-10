"""Authenticated loopback service for Companion. Workers are killable processes."""
from __future__ import annotations
import ctypes
from datetime import datetime, timezone
from http.server import BaseHTTPRequestHandler, ThreadingHTTPServer
import json
import subprocess
import os
from pathlib import Path
import secrets
import sys
import threading
import time
from urllib.parse import urlsplit, parse_qs
import uuid

import ai_settings
from ifs_code_interpreter import index_sources, search, answer

ROOT = Path(__file__).resolve().parent
BUNDLE = ROOT.parent / 'data'
if getattr(sys, 'frozen', False):
    BUNDLE = Path(sys._MEIPASS) / 'data'


def write_json(path, value):
    pending = path.with_suffix('.tmp')
    pending.write_text(json.dumps(value, ensure_ascii=False), encoding='utf-8')
    pending.replace(path)


def worker(action, payload):
    try:
        if action == 'index':
            result = index_sources(payload['roots'], payload['destination'])
            if not result['indexed_files']:
                raise ValueError('No supported source files found. Choose a folder containing IFs code.')
        else:
            evidence = search(payload['index'], payload['question'])
            # Bound provider context even when source files contain minified lines.
            for item in evidence:
                lines = item['text'].splitlines()
                kept = []
                size = 0
                for line in lines:
                    if size + len(line) > 10000: break
                    kept.append(line); size += len(line)
                item['text'] = '\n'.join(kept)
                item['end'] = item['start'] + len(kept) - 1
                item['excerpt_truncated'] = len(kept) != len(lines)
            evidence = [item for item in evidence if item['text']]
            result = {'question': payload['question'], 'evidence': evidence}
            if action == 'ask':
                settings = ai_settings.load(payload['settings_file'], secret=True)
                if not settings['configured']:
                    raise ValueError('Configure the shared AI connection in Companion Settings first.')
                result['answer'] = answer(payload['question'], evidence, settings['endpoint'], settings['model'], settings['api_key'])
            else:
                result['answer'] = 'Matching source excerpts. Search is local and does not call an AI provider.'
        return {'ok': True, 'result': result}
    except ValueError as error:
        return {'ok': False, 'error': str(error)}
    except Exception:
        return {'ok': False, 'error': 'Operation failed. Check the source folder or AI connection and try again.'}


class Workspace:
    def __init__(self, data, bundle=BUNDLE, settings_file=None):
        self.data = Path(data).resolve()
        self.data.mkdir(parents=True, exist_ok=True)
        (self.data/'answers').mkdir(exist_ok=True)
        self.bundle = Path(bundle).resolve()
        self.settings_file = str(settings_file or self.data/'missing-ai-settings.json')
        self.lock = threading.RLock()
        self.process = None
        self.stopping = False
        self.job = {'status': 'idle', 'message': 'Choose a source and prepare its index.'}
        self.active = self.read_active()
        self.pending = None

    def read_active(self):
        try:
            active = json.loads((self.data/'source.json').read_text(encoding='utf-8'))
            return active if (self.data/'code.sqlite3').is_file() else None
        except (OSError, ValueError): return None

    def state(self):
        with self.lock:
            try:
                ai = ai_settings.load(self.settings_file)
            except (OSError, ValueError): ai = {'configured': False}
            manifest = json.loads((self.bundle/'sources.json').read_text(encoding='utf-8'))
            bundled = [{'repository': s['repository'], 'commit': s['commit']} for s in manifest['sources']]
            old = self.active and self.active['kind']=='bundled' and self.active.get('revisions') != bundled
            history = []
            for p in sorted((self.data/'answers').glob('*.json'), reverse=True)[:50]:
                try:
                    record = json.loads(p.read_text(encoding='utf-8'))
                    history.append({k:record[k] for k in ('id','question','created','mode')})
                except (OSError, ValueError, KeyError): continue
            return {'job': dict(self.job), 'active': self.active, 'bundled': bundled,
                    'bundle_changed': bool(old), 'ai': ai, 'history': history}

    def start(self, action, request):
        with self.lock:
            if self.stopping: raise ValueError('This tool is closing.')
            if self.process is not None: raise ValueError('Stop or finish the current task first.')
            if action == 'index':
                kind = request.get('kind')
                if kind == 'bundled':
                    roots = [str(self.bundle/name) for name in ('ifs-dev','ifs-web')]
                elif kind == 'local':
                    roots = request.get('roots')
                    if not isinstance(roots, list) or not 1 <= len(roots) <= 4 or any(not isinstance(p,str) or not p.strip() for p in roots):
                        raise ValueError('Select one to four source folders.')
                    roots = [str(Path(p).resolve(strict=True)) for p in roots]
                else: raise ValueError('Choose bundled code or local folders.')
                for root in roots:
                    path = Path(root).resolve()
                    if not path.is_dir() or path.is_relative_to(self.data) or self.data.is_relative_to(path):
                        raise ValueError('Choose source folders separate from the tool data directory.')
                candidate = {'kind': kind, 'roots': roots, 'revisions': self.state()['bundled'] if kind=='bundled' else [],
                             'created': datetime.now(timezone.utc).isoformat()}
                temporary = self.data/('index-'+uuid.uuid4().hex+'.sqlite3')
                payload = {'roots': roots, 'destination': str(temporary)}
            elif action in ('ask', 'search'):
                if not self.active: raise ValueError('Prepare the source index first.')
                question = request.get('question', '')
                if not isinstance(question,str) or not 1 <= len(question.strip()) <= 6000:
                    raise ValueError('Enter a question up to 6,000 characters.')
                if action == 'ask' and not ai_settings.load(self.settings_file)['configured']:
                    raise ValueError('Configure the shared AI connection in Companion Settings first.')
                payload = {'question': question.strip(), 'index': str(self.data/'code.sqlite3'), 'settings_file': self.settings_file}
                candidate = dict(self.active)
                temporary = None
            else: raise ValueError('Unknown action.')
            prefix = [sys.executable] if getattr(sys, 'frozen', False) else [sys.executable, str(Path(__file__).resolve())]
            self.job = {'status': 'running', 'action': action, 'message': 'Preparing code index…' if action=='index' else 'Reading source evidence…'}
            self.pending = temporary
            try:
                process = subprocess.Popen(prefix+['--worker'], stdin=subprocess.PIPE, stdout=subprocess.PIPE, stderr=subprocess.DEVNULL,
                    env=dict(os.environ, IFS_CODE_WORKER_PARENT=str(os.getpid())),
                    creationflags=subprocess.CREATE_NO_WINDOW if os.name=='nt' else 0)
            except Exception:
                self.job = {'status':'failed','message':'Could not start the worker.'}
                raise
            self.process = process
            threading.Thread(target=self.watch, args=(process,action,payload,candidate,temporary), daemon=True).start()
            return dict(self.job)

    def watch(self, process, action, payload, candidate, temporary):
        try:
            output, _ = process.communicate(json.dumps({'action':action,'payload':payload}).encode(), timeout=600)
            with self.lock:
                if self.process is not process: return
                response = json.loads(output) if output else {}
                if process.returncode or not response.get('ok'):
                    self.job = {'status':'failed','message':response.get('error','Worker stopped before finishing.')}
                elif action == 'index':
                    temporary.replace(self.data/'code.sqlite3')
                    candidate['indexed_files'] = response['result']['indexed_files']
                    write_json(self.data/'source.json', candidate)
                    self.active = candidate
                    self.job = {'status':'complete','action':action,'message':f"Ready · {candidate['indexed_files']} source files indexed"}
                else:
                    now = datetime.now(timezone.utc)
                    record = {**response['result'], 'id': now.strftime('%Y%m%dT%H%M%S')+'-'+uuid.uuid4().hex[:12],
                              'created': now.isoformat(), 'source': candidate, 'mode': action}
                    write_json(self.data/'answers'/f"{record['id']}.json", record)
                    self.job = {'status':'complete','action':action,'message':'Answer saved.' if action=='ask' else 'Source search saved.', 'answer_id':record['id']}
                self.process = None
        except Exception:
            with self.lock:
                if self.process is process:
                    if process.poll() is None: process.terminate(); process.wait(timeout=5)
                    self.process = None
                    self.job = {'status':'failed','message':'Could not finish the task. Your saved answers are preserved.'}
        finally:
            for stream in (process.stdin, process.stdout):
                if stream: stream.close()
            if temporary:
                temporary.unlink(missing_ok=True)
                Path(str(temporary)+'-journal').unlink(missing_ok=True)

    def cancel(self):
        with self.lock:
            process = self.process
            if process:
                self.process = None
                if process.poll() is None: process.terminate()
                process.wait(timeout=5)
                if process.poll() is None:
                    self.process = process
                    raise ValueError('The worker is still stopping. Try Stop again.')
            self.job = {'status':'idle','message':'Stopped. Source selection and saved answers are preserved.'}
            return dict(self.job)

    def close(self):
        with self.lock:
            self.stopping = True
            self.cancel()


class Handler(BaseHTTPRequestHandler):
    def log_message(self, *args): pass

    def send(self, value, status=200, content='application/json'):
        data = value if isinstance(value,bytes) else json.dumps(value).encode()
        self.send_response(status)
        self.send_header('Content-Type', content+'; charset=utf-8')
        self.send_header('Content-Length', str(len(data)))
        self.send_header('Cache-Control','no-store')
        self.send_header('X-Content-Type-Options','nosniff')
        self.end_headers(); self.wfile.write(data)

    def authorized(self):
        return secrets.compare_digest(self.headers.get('X-IFs-Tool-Token',''), self.server.token)

    def do_GET(self):
        if not self.authorized(): return self.send({'error':'Unauthorized'},403)
        path = urlsplit(self.path).path
        try:
            if path == '/bridge/status':
                with self.server.workspace.lock:
                    return self.send({'running': self.server.workspace.process is not None})
            if path == '/api/state': return self.send(self.server.workspace.state())
            if path == '/api/answer':
                identity = parse_qs(urlsplit(self.path).query).get('id',[''])[0]
                import re
                if not re.fullmatch(r'\d{8}T\d{6}-[a-f0-9]{12}',identity): raise ValueError('Invalid answer ID.')
                return self.send(json.loads((self.server.workspace.data/'answers'/f'{identity}.json').read_text(encoding='utf-8')))
            if path in ('/','/style.css','/app.js'):
                name = 'index.html' if path=='/' else path[1:]
                return self.send((ROOT/'static'/name).read_bytes(),content={'index.html':'text/html','style.css':'text/css','app.js':'text/javascript'}[name])
            return self.send({'error':'Not found'},404)
        except (ValueError,OSError): return self.send({'error':'Unable to read this item.'},400)

    def do_POST(self):
        if not self.authorized(): return self.send({'error':'Unauthorized'},403)
        try:
            size = int(self.headers.get('Content-Length','0'))
            if not 0 < size <= 32768: raise ValueError('Invalid request size.')
            request = json.loads(self.rfile.read(size))
            if not isinstance(request,dict): raise ValueError('Invalid request.')
            if self.path == '/bridge/shutdown':
                self.server.workspace.close()
                self.send({'ok':True})
                threading.Thread(target=self.server.shutdown,daemon=True).start(); return
            if self.path == '/api/stop': return self.send(self.server.workspace.cancel())
            if self.path in ('/api/index','/api/ask','/api/search'):
                return self.send(self.server.workspace.start(self.path.rsplit('/',1)[1],request))
            return self.send({'error':'Not found'},404)
        except (ValueError,OSError) as error: return self.send({'error':str(error)},400)


def create_server(data, token, bundle=BUNDLE, settings_file=None):
    if not token: raise ValueError('A tool authentication token is required.')
    server = ThreadingHTTPServer(('127.0.0.1',0),Handler)
    server.token = token
    server.workspace = Workspace(data,bundle,settings_file)
    return server


def parent_alive(pid):
    if os.name == 'nt':
        kernel = ctypes.windll.kernel32
        kernel.OpenProcess.restype = ctypes.c_void_p
        kernel.CloseHandle.argtypes = [ctypes.c_void_p]
        kernel.WaitForSingleObject.argtypes = [ctypes.c_void_p,ctypes.c_ulong]
        handle = kernel.OpenProcess(0x100000,False,pid)
        if not handle: return False
        try: return kernel.WaitForSingleObject(handle,0) == 0x102
        finally: kernel.CloseHandle(handle)
    try: os.kill(pid,0); return True
    except OSError: return False


def main():
    server = create_server(os.environ['IFS_TOOL_DATA_DIR'],os.environ['IFS_TOOL_TOKEN'],settings_file=os.environ.get('IFS_AI_SETTINGS_FILE'))
    ready = Path(os.environ['IFS_TOOL_READY_FILE'])
    write_json(ready, {'port':server.server_port})
    def stop():
        server.workspace.close(); server.shutdown()
    def watch_parent():
        pid = int(os.environ['IFS_COMPANION_PID'])
        while parent_alive(pid): time.sleep(1)
        stop()
    def watch_input():
        if sys.stdin is not None:
            sys.stdin.read(); stop()
    threading.Thread(target=watch_parent,daemon=True).start()
    threading.Thread(target=watch_input,daemon=True).start()
    try: server.serve_forever()
    finally: server.workspace.close(); server.server_close()


if __name__ == '__main__':
    if '--worker' in sys.argv:
        def watch_worker_parent():
            pid = int(os.environ['IFS_CODE_WORKER_PARENT'])
            while parent_alive(pid): time.sleep(1)
            os._exit(1)
        threading.Thread(target=watch_worker_parent, daemon=True).start()
        request = json.load(sys.stdin)
        sys.stdout.write(json.dumps(worker(request['action'], request['payload'])))
    else:
        main()
