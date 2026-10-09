import importlib.util
import io
import json
from pathlib import Path
import tempfile
import unittest
from unittest.mock import patch

spec = importlib.util.spec_from_file_location('interpreter', Path(__file__).resolve().parents[1]/'tool/ifs_code_interpreter.py')
module = importlib.util.module_from_spec(spec)
spec.loader.exec_module(module)


class InterpreterTests(unittest.TestCase):
    def test_snapshot_and_reindex(self):
        with tempfile.TemporaryDirectory() as folder:
            base = Path(folder)
            source = base/'source'
            source.mkdir()
            file = source/'Forecast.vb'
            file.write_text('Sub Forecast()\nGDP = POP * GDPPC\nEnd Sub\n')
            (source/'bin').mkdir()
            (source/'bin/ignored.vb').write_text('SECRET = 1')
            index = base/'index.sqlite3'
            self.assertEqual(module.index_sources([source],index)['indexed_files'],1)
            file.write_text('replacement')
            hits = module.search(index, 'How is GDP calculated?')
            self.assertEqual(hits[0]['path'],'Forecast.vb')
            self.assertIn('GDP = POP',hits[0]['text'])
            self.assertEqual(module.search(index,'SECRET'),[])
            module.index_sources([source],index)
            self.assertEqual(module.search(index,'GDP'),[])

    def test_index_must_not_write_into_source(self):
        with tempfile.TemporaryDirectory() as folder:
            with self.assertRaises(ValueError):
                module.index_sources([folder], Path(folder)/'index.sqlite3')

    def test_no_evidence_does_not_call_provider(self):
        with patch.object(module,'build_opener',side_effect=AssertionError('network called')):
            self.assertIn('No matching',module.answer('q',[],'https://example.com','model','key'))

    def test_remote_plaintext_rejected(self):
        with self.assertRaises(ValueError):
            module.answer('q',[{'id':'E1'}],'http://example.com','model','key')

    def test_answer_citations_and_incomplete_responses(self):
        for content, finish, valid in [('GDP uses POP [E1]', 'stop', True),
                                       ('Unsupported [E99]', 'stop', False),
                                       ('Uncited answer', 'stop', False),
                                       ('Truncated [E1]', 'length', False)]:
            response = io.BytesIO(json.dumps({'choices':[{'finish_reason':finish,'message':{'content':content}}]}).encode())
            with patch.object(module,'build_opener') as opener:
                opener.return_value.open.return_value = response
                if valid:
                    self.assertEqual(module.answer('q',[{'id':'E1'}],'https://example.com','model','key'),content)
                else:
                    with self.assertRaises(ValueError):
                        module.answer('q',[{'id':'E1'}],'https://example.com','model','key')


if __name__ == '__main__':
    unittest.main()
