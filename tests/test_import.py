import importlib.util
from pathlib import Path
import unittest

spec=importlib.util.spec_from_file_location('importer',Path(__file__).resolve().parents[1]/'scripts/import_sources.py')
module=importlib.util.module_from_spec(spec)
spec.loader.exec_module(module)


class ImportTests(unittest.TestCase):
    def test_preserves_password_expressions(self):
        source='cnnStr &= ";Password=" & DatabasePassword & ";"\n.Password = password\n'
        self.assertEqual(module.sanitize(source),source)

    def test_redacts_literals_and_keeps_line_count(self):
        source='Dim password As String = "example-secret"\nconnection = "Server=x;Password=example-secret;"\n'
        result=module.sanitize(source)
        self.assertNotIn('example-secret',result)
        self.assertEqual(source.count('\n'),result.count('\n'))
