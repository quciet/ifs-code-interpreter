import importlib.util
import json
import os
from pathlib import Path
import sys
import tempfile
import threading
import time
import unittest
from urllib.request import Request,urlopen
from urllib.error import HTTPError
from http.server import BaseHTTPRequestHandler,ThreadingHTTPServer

sys.path.insert(0,str(Path(__file__).resolve().parents[1]/'tool'))
from companion_service import create_server
import ai_settings


class ServiceTests(unittest.TestCase):
    def setUp(self):
        self.temp=tempfile.TemporaryDirectory()
        self.root=Path(self.temp.name)
        bundle=self.root/'bundle'
        for name in ('ifs-dev','ifs-web'):
            (bundle/name).mkdir(parents=True)
            (bundle/name/'Forecast.vb').write_text('Sub Forecast()\nGDP = POP * GDPPC\nEnd Sub\n')
        (bundle/'sources.json').write_text(json.dumps({'sources':[{'repository':'repo','commit':'a'*40}]}))
        self.server=create_server(self.root/'work','test-token',bundle)
        self.thread=threading.Thread(target=self.server.serve_forever,daemon=True);self.thread.start()
        self.url=f'http://127.0.0.1:{self.server.server_port}'

    def tearDown(self):
        self.server.workspace.close();self.server.shutdown();self.server.server_close();self.thread.join()
        # Give cancelled worker monitor time to release its temporary index.
        time.sleep(.15)
        self.temp.cleanup()

    def request(self,path,body=None,token='test-token'):
        request=Request(self.url+path,headers={'X-IFs-Tool-Token':token},data=None if body is None else json.dumps(body).encode())
        with urlopen(request,timeout=10) as response:return json.load(response)

    def wait(self):
        end=time.monotonic()+15
        while time.monotonic()<end:
            state=self.request('/api/state')
            if state['job']['status']!='running':return state
            time.sleep(.05)
        self.fail('Worker timed out')

    def test_every_route_requires_token(self):
        for path,body in [('/',None),('/api/state',None),('/bridge/status',None),('/api/stop',{}),('/bridge/shutdown',{})]:
            with self.assertRaises(HTTPError) as error:self.request(path,body,token='wrong')
            self.assertEqual(error.exception.code,403);error.exception.close()

    def test_index_search_persist_and_failed_replacement(self):
        self.request('/api/index',{'kind':'bundled'})
        state=self.wait();self.assertEqual(state['job']['status'],'complete')
        self.assertEqual(state['active']['indexed_files'],2)
        self.request('/api/search',{'question':'GDP'})
        state=self.wait();identity=state['job']['answer_id']
        record=self.request('/api/answer?id='+identity)
        self.assertIn('GDP = POP',record['evidence'][0]['text'])
        empty=self.root/'empty';empty.mkdir()
        self.request('/api/index',{'kind':'local','roots':[str(empty)]})
        self.assertEqual(self.wait()['job']['status'],'failed')
        self.assertEqual(self.request('/api/state')['active']['kind'],'bundled')
        self.assertEqual(self.request('/api/answer?id='+identity)['id'],identity)
        self.assertEqual(self.request('/bridge/status'),{'running':False})

    def test_cancel_preserves_existing_source(self):
        self.request('/api/index',{'kind':'bundled'});self.wait()
        self.request('/api/index',{'kind':'bundled'})
        self.request('/api/stop',{})
        self.assertFalse(self.request('/bridge/status')['running'])
        self.assertEqual(self.request('/api/state')['active']['indexed_files'],2)

    def test_question_uses_shared_provider_and_saves_citations(self):
        class Provider(BaseHTTPRequestHandler):
            def log_message(self,*args):pass
            def do_POST(self):
                body=json.loads(self.rfile.read(int(self.headers['Content-Length'])))
                self.server.received=body
                reply=json.dumps({'choices':[{'finish_reason':'stop','message':{'content':'GDP is POP multiplied by GDPPC [E1].'}}]}).encode()
                self.send_response(200);self.send_header('Content-Length',str(len(reply)));self.end_headers();self.wfile.write(reply)
        provider=ThreadingHTTPServer(('127.0.0.1',0),Provider)
        thread=threading.Thread(target=provider.serve_forever,daemon=True);thread.start()
        try:
            ai_settings.save(self.server.workspace.settings_file,{'endpoint':f'http://127.0.0.1:{provider.server_port}/v1','model':'fixture'})
            self.request('/api/index',{'kind':'bundled'});self.wait()
            self.request('/api/ask',{'question':'How is GDP calculated?'})
            state=self.wait();self.assertEqual(state['job']['status'],'complete')
            answer=self.request('/api/answer?id='+state['job']['answer_id'])
            self.assertIn('[E1]',answer['answer']);self.assertTrue(answer['evidence'])
            self.assertEqual(provider.received['model'],'fixture')
            self.assertIn('GDP = POP',provider.received['messages'][1]['content'])
        finally:provider.shutdown();provider.server_close();thread.join()


class SharedAISettingsTests(unittest.TestCase):
    @unittest.skipUnless(os.name=='nt','Windows DPAPI')
    def test_key_encrypted_masked_and_not_transferred(self):
        with tempfile.TemporaryDirectory() as temp:
            path=Path(temp)/'ai.json'
            result=ai_settings.save(path,{'endpoint':'https://example.com/v1','model':'fixture','api_key':'test-only-sensitive-value'})
            self.assertTrue(result['has_key']);self.assertNotIn('api_key',result)
            self.assertNotIn('test-only-sensitive-value',path.read_text())
            self.assertEqual(ai_settings.load(path,secret=True)['api_key'],'test-only-sensitive-value')
            ai_settings.save(path,{'endpoint':'https://other.example/v1','model':'fixture'})
            self.assertFalse(ai_settings.load(path)['has_key'])
