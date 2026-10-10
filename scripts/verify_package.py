"""Exercise a packaged tool through the real Companion manager, with a mock AI."""
import argparse
from http.server import BaseHTTPRequestHandler,ThreadingHTTPServer
import json
from pathlib import Path
import sys
import subprocess
import tempfile
import threading
import time
from urllib.error import HTTPError
from urllib.request import urlopen


def main():
    parser=argparse.ArgumentParser()
    parser.add_argument('--companion',required=True)
    parser.add_argument('--package',required=True)
    args=parser.parse_args()
    sys.path.insert(0,str(Path(args.companion).resolve()))
    from tool_manager import ToolManager
    import ai_settings
    class MockAI(BaseHTTPRequestHandler):
        def log_message(self,*args):pass
        def do_POST(self):
            payload=json.loads(self.rfile.read(int(self.headers['Content-Length'])))
            assert payload['model']=='integration-fixture'
            if payload['messages'][-1]['role']=='tool':
                last=json.loads(payload['messages'][-1]['content'])
                if isinstance(last.get('result'),dict) and 'hits' in last['result']:
                    hit=last['result']['hits'][0]
                    message={'tool_calls':[{'id':'read-1','type':'function','function':{'name':'code_read','arguments':json.dumps({'path':hit['path'],'start':int(hit['line']),'end':int(hit['line'])+8})}}]}
                else:message={'content':f"The retrieved implementation is available [{last['id']}]. Initial source evidence is also retained [E1]."}
            else:message={'tool_calls':[{'id':'search-1','type':'function','function':{'name':'code_search','arguments':json.dumps({'query':'GDP','references':True,'limit':2})}}]}
            body=json.dumps({'choices':[{'finish_reason':'tool_calls' if message.get('tool_calls') else 'stop','message':message}]}).encode()
            self.send_response(200);self.send_header('Content-Length',str(len(body)));self.end_headers();self.wfile.write(body)
    ai=ThreadingHTTPServer(('127.0.0.1',0),MockAI)
    threading.Thread(target=ai.serve_forever,daemon=True).start()
    try:
        with tempfile.TemporaryDirectory(prefix='ifs-code-package-') as temporary:
            manager=ToolManager(temporary)
            identity='ifs-code-reader'
            def request(path,body=None):
                with manager.request(identity,path,'GET' if body is None else 'POST',None if body is None else json.dumps(body).encode()) as response:
                    return json.load(response)
            def wait():
                deadline=time.monotonic()+600
                while time.monotonic()<deadline:
                    state=request('api/state')
                    if state['job']['status']!='running':return state
                    time.sleep(.2)
                raise AssertionError('Packaged worker timed out')
            try:
                manager.install(args.package)
                print('Installed package',flush=True)
                ai_settings.save(manager.root/'ai-settings.json',{'endpoint':f'http://127.0.0.1:{ai.server_port}/v1','model':'integration-fixture'})
                manager.start(identity)
                print('Started service',flush=True)
                # Locate the executable from the validated install, not from source Python.
                executables=list(manager.root.rglob('ExploreCode.exe'))
                assert len(executables)==1,executables
                check=subprocess.run([str(executables[0]),'--check-offline-model'],capture_output=True,text=True,timeout=90)
                assert check.returncode==0,(check.stdout,check.stderr)
                assert 'passed' in check.stdout
                print('Packaged offline model passed',flush=True)
                with manager.request(identity,'/') as response:assert b'Explore the model' in response.read()
                port=manager.processes[identity]['port']
                try:urlopen(f'http://127.0.0.1:{port}/api/state')
                except HTTPError as error:assert error.code==403;error.close()
                else:raise AssertionError('Missing-token request was accepted')
                request('api/index',{'kind':'bundled'})
                print('Indexing bundle',flush=True)
                state=wait();assert state['job']['status']=='complete',state
                assert state['active']['indexed_files']>900
                assert state['active']['wiki_pages']==20
                request('api/ask',{'question':'GDP'})
                state=wait();assert state['job']['status']=='complete',state
                answer_id=state['job']['answer_id']
                answer=request('api/answer?id='+answer_id)
                assert '[E1]' in answer['answer'] and answer['evidence']
                assert answer['metrics']['tool_calls']==2 and answer['ledger'][-1]['tool']=='code_read'
                request('api/search',{'question':'scenario interpolation'})
                state=wait();assert state['job']['status']=='complete',state
                wiki_answer=request('api/answer?id='+state['job']['answer_id'])
                assert any(e.get('kind')=='wiki' and e['cache_status']=='offline' for e in wiki_answer['evidence'])
                request('api/index',{'kind':'bundled'})
                request('api/stop',{})
                assert request('bridge/status')=={'running':False}
                manager.stop(identity)
                manager.start(identity)
                assert request('api/answer?id='+answer_id)['id']==answer_id
                assert request('api/state')['active']['indexed_files']>900
                manager.stop(identity)
                manager.uninstall(identity)
                assert (manager.data/identity/'answers'/f'{answer_id}.json').exists()
                print('PASS: packaged install, auth, offline model, bundled index/wiki, multi-step AI research, ledger/citations, cancel, restart, shutdown, uninstall preservation')
            finally:manager.close()
    finally:ai.shutdown();ai.server_close()


if __name__=='__main__':main()
