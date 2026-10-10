import unittest
from ifs_research.structure import parse_vb

class StructuralTests(unittest.TestCase):
    def test_multiline_assignment_indices_branch_and_compound(self):
        source='''Public Sub Forecast()
If Year = 1 Then
  Production(r, sector) = Yield(r) * _
      Land(r, sector)
Else
  Production(r, sector) += Stock(r)
End If
End Sub'''
        parsed=parse_vb(source,'A.Forecast.vb')
        assigns=[x for x in parsed['records'] if x['kind']=='assignment']
        self.assertEqual(len(assigns),2)
        self.assertEqual(assigns[0]['target'],'Production(r, sector)')
        self.assertEqual(assigns[0]['end'],4)
        self.assertIn('yield',assigns[0]['inputs'])
        self.assertNotIn('production',assigns[0]['inputs'])
        self.assertIn('production',assigns[1]['inputs'])
        self.assertEqual(assigns[1]['conditions'][0]['branches'],['If Year = 1 Then','Else'])
        self.assertEqual(assigns[0]['index_references'],['r','sector'])

    def test_comments_strings_inline_if_declarations_and_unresolved_calls(self):
        parsed=parse_vb('''Dim A(,), B As Single
Public Sub F()
' Fake = Wrong
If A = B Then A = 2
X = F(A) + B ' comment Z
Msg = "Secret = Y"
End Sub''','A.vb')
        self.assertTrue(any('Single-line If' in x['reason'] for x in parsed['warnings']))
        self.assertEqual([x['symbol'] for x in parsed['records'] if x['kind']=='declaration'],['a','b'])
        assigns=[x for x in parsed['records'] if x['kind']=='assignment']
        self.assertEqual([x['symbol'] for x in assigns],['x','msg'])
        self.assertEqual(assigns[-1]['inputs'],[])
        self.assertTrue(any(x['kind']=='call_candidate' and x['symbol']=='f' for x in parsed['records']))

    def test_scalar_type_suffix_and_declaration_initializer(self):
        parsed=parse_vb('Public Sub F()\nDim A As Single = B + 1\nCount% = Count% + 1\nEnd Sub','A.vb')
        rows=[x for x in parsed['records'] if x['kind']=='assignment']
        self.assertEqual(rows[0]['symbol'],'a');self.assertEqual(rows[0]['inputs'],['b'])
        self.assertTrue(rows[0]['declaration_initializer'])
        self.assertEqual(rows[1]['target'],'Count%');self.assertEqual(rows[1]['symbol'],'count')
