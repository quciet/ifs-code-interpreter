"""Deterministic real-source retrieval checks; this does not score live AI answer quality."""
import argparse
import json
from pathlib import Path
import sys
import time
sys.path.insert(0,str(Path(__file__).resolve().parents[1]/'tool'))
from ifs_code_interpreter import index_sources
from research import Research,prepare_research

ROOT=Path(__file__).resolve().parents[1]


def main():
    parser=argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--index',type=Path,default=ROOT/'workspace/evaluation.sqlite3')
    args=parser.parse_args();started=time.monotonic()
    if not args.index.exists():
        index_sources([ROOT/'data/ifs-dev',ROOT/'data/ifs-web'],args.index)
        prepare_research(args.index,ROOT/'data')
    research=Research(args.index,ROOT/'data');checks=[]
    for symbol,suffix in [('YL','Agriculture.Forecast.vb'),('AGP','Agriculture.Forecast.vb'),('INCOMELT190LN','Economy.Forecast.vb')]:
        result=research.structure.lookup(symbol,kind='assignment',limit=10000)
        matches=[x for x in result['hits'] if x['citation']['path'].endswith(suffix)]
        assert matches,(symbol,result)
        assert all('TmpCode' not in x['citation']['path'] for x in result['hits'])
        c=matches[0]['citation'];source=research.code.read(c['path'],c['start'],c['end'])
        assert symbol.lower() in source['text'].lower()
        checks.append({'case':symbol,'matches':result['total'],'citation':c,'result':'pass'})
    for query in ('scenario','agriculture','population'):
        result=research.retrieval.search(query,kind='wiki')
        assert result['hits'],query
        page=research.wiki.read(result['hits'][0]['section_id'])
        assert page['cache_status']=='offline' and page['approval']['checked_at'] and 'oldid=' in page['citation']
        checks.append({'case':query,'citation':page['citation'],'result':'pass'})
    report={'checks':checks,'elapsed_seconds':round(time.monotonic()-started,2),
            'scope':'Deterministic navigation/retrieval only; no simulation or live model answer-quality score.'}
    (ROOT/'workspace/research-evaluation.json').write_text(json.dumps(report,indent=2),encoding='utf-8')
    print(json.dumps(report,indent=2))


if __name__=='__main__':main()
