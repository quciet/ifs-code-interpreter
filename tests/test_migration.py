import json
from pathlib import Path
import sys
import tempfile
import unittest
from unittest.mock import patch
sys.path.insert(0,str(Path(__file__).resolve().parents[1]/'tool'))
from ifs_code_interpreter import index_sources
from research import Research, prepare_research, evidence_from_hits
from investigation import investigate
from ifs_research.common import database

ROOT=Path(__file__).resolve().parents[1]


class MigrationTests(unittest.TestCase):
    def setUp(self):
        self.temp=tempfile.TemporaryDirectory();self.root=Path(self.temp.name)
        self.source=self.root/'source';self.source.mkdir()
        (self.source/'Forecast.vb').write_text('Sub Forecast()\nIf YEAR = FIRSTYEAR Then\nGDP = POP * GDPPC\nElse\nGDP += INVEST\nEnd If\nEnd Sub\n',encoding='utf-8')
        self.index=self.root/'index.db'
        index_sources([self.source],self.index)
        self.meta=prepare_research(self.index,ROOT/'data')
        self.r=Research(self.index,ROOT/'data')

    def tearDown(self):self.temp.cleanup()

    def test_offline_wiki_provenance_and_code_context(self):
        self.assertEqual(self.meta['wiki_pages'],20)
        with patch('urllib.request.OpenerDirector.open',side_effect=AssertionError('Offline retrieval must not connect')):
            rows=self.r.wiki.search('scenario',limit=2)
            self.assertTrue(rows)
            for row in rows:
                self.assertEqual(row['cache_status'],'offline')
                self.assertIn('oldid=',row['citation'])
                self.assertIn('checked_at',row['approval'])
            evidence,_=evidence_from_hits(self.r,'scenario interpolation')
            self.assertTrue(any(e['kind']=='wiki' and e['cache_status']=='offline' for e in evidence))
            hits=self.r.structure.lookup('GDP',kind='assignment')['hits']
            self.assertEqual(len(hits),2)
            self.assertTrue(hits[0]['conditions'])
            context=self.r.structure.context(hits[0]['citation']['path'],3)
            self.assertIn('FIRSTYEAR',context['source']['text'])
        (self.source/'Forecast.vb').write_text('changed',encoding='utf-8')
        result=self.r.code.read(hits[0]['citation']['path'],1,7)
        self.assertFalse(result['local_source_matches_snapshot'])
        self.assertIn('GDP = POP',result['text'])

    def test_multistep_research_saves_ledger_and_limits(self):
        turns=[]
        def model(settings,messages,allow):
            turns.append(messages)
            if len(turns)==1:
                return {'tool_calls':[{'id':'c1','type':'function','function':{'name':'symbol_lookup','arguments':json.dumps({'symbol':'GDP','kind':'assignment'})}}]},None
            evidence=json.loads(messages[-1]['content'])
            self.assertTrue(evidence['successful'])
            self.assertEqual(evidence['result']['hits'][0]['kind'],'assignment')
            return {'content':f"GDP depends on population and GDP per capita under an initialization condition [{evidence['id']}]."}, {'completion_tokens':12}
        result=investigate(self.r,'GDP',{},requester=model)
        self.assertEqual(result['metrics']['model_turns'],2)
        self.assertEqual(result['metrics']['tool_calls'],1)
        self.assertEqual(result['ledger'][-1]['tool'],'symbol_lookup')
        self.assertEqual(result['ledger'][-1]['result']['total'],2)

    def test_failed_and_fabricated_evidence_cannot_be_cited(self):
        with self.assertRaisesRegex(ValueError,'citations'):
            investigate(self.r,'GDP',{},requester=lambda *a:({'content':'Unsupported [E999].'},None))
        step=0
        def model(*args):
            nonlocal step
            step+=1
            if step==1:return {'tool_calls':[{'id':'bad','type':'function','function':{'name':'execute_shell','arguments':'{}'}}]},None
            identity=json.loads(args[1][-1]['content'])['id']
            return {'content':f'Unsupported [{identity}].'},None
        with self.assertRaisesRegex(ValueError,'citations'):investigate(self.r,'GDP',{},requester=model)

    def test_semantic_vectors_and_explicit_offline_fallback(self):
        import numpy as np
        class Encoder:
            identity='fixture-v1'
            def embed(self,texts,query=False):return np.asarray([[1.,len(t)%7+1] for t in texts])
        result=self.r.retrieval.prepare(encoder=Encoder())
        self.assertGreater(result['chunks'],10)
        self.assertEqual(self.r.retrieval.search('GDP')['semantic_status'],'local embeddings active')
        self.r.retrieval.encoder.identity='changed'
        self.assertIn('unavailable',self.r.retrieval.search('GDP')['semantic_status'])

    def test_wiki_integrity_failure_does_not_pass(self):
        bundle=self.root/'bundle';(bundle/'wiki').mkdir(parents=True)
        (bundle/'wiki/manifest.json').write_text(json.dumps({'pages':[{'file':'bad.json','sha256':'bad'}]}),encoding='utf-8')
        (bundle/'wiki/bad.json').write_text('{}',encoding='utf-8')
        index=self.root/'bad.db';index_sources([self.source],index)
        with self.assertRaisesRegex(ValueError,'integrity'):prepare_research(index,bundle)
