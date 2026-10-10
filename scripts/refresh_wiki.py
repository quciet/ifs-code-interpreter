"""Maintainer-only approved-first wiki snapshot refresh; partial refresh never replaces a bundle."""
import argparse
import hashlib
import json
from pathlib import Path
import sys
import tempfile
sys.path.insert(0,str(Path(__file__).resolve().parents[1]/'tool'))
from ifs_research.common import database
from ifs_research.wiki_client import Transport, WikiClient
from ifs_research.wiki_store import WikiStore

ROOT=Path(__file__).resolve().parents[1]


def export(store,destination,scope):
    destination.mkdir(parents=True,exist_ok=True)
    with database(store.path) as db:
        rows=db.execute('SELECT s.payload FROM wiki_pages p JOIN wiki_snapshots s ON s.id=p.snapshot JOIN wiki_scope_members m ON m.page_id=p.page_id ORDER BY p.page_id').fetchall()
    pages=[]
    for row in rows:
        page=json.loads(row[0]);raw=(json.dumps(page,ensure_ascii=False,sort_keys=True,indent=2)+'\n').encode('utf-8')
        sha=hashlib.sha256(raw).hexdigest()
        name=f"{page['page_id']}-{page['revision']}-{sha[:16]}.json";pending=destination/(name+'.tmp');pending.write_bytes(raw);pending.replace(destination/name)
        pages.append({'file':name,'title':page['title'],'revision':page['revision'],'sha256':sha,
                      'fetched_at':page['fetched_at'],'selection':page['selection']})
    manifest={'schema':1,'source':'https://pardeewiki.du.edu','provenance':'Approved-first public wiki retrieval; each page records its own verification and selection.',
              'scope':scope,'pages':pages}
    pending=destination/'manifest.json.tmp';pending.write_text(json.dumps(manifest,ensure_ascii=False,indent=2)+'\n',encoding='utf-8');pending.replace(destination/'manifest.json')
    return len(pages)


def main():
    parser=argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--output',type=Path,default=ROOT/'data/wiki');args=parser.parse_args()
    (ROOT/'workspace').mkdir(exist_ok=True)
    with tempfile.TemporaryDirectory(dir=ROOT/'workspace') as temp:
        store=WikiStore(Path(temp)/'wiki.sqlite3')
        report=store.sync(WikiClient(Transport('https://pardeewiki.du.edu/api.php')),
                          ['Understand the Model','Guide to Scenario Analysis in International Futures (IFs)'],
                          max_pages=200,max_depth=10,scoped=True)
        if not report['scope_activated']:raise RuntimeError('Wiki refresh incomplete; existing bundle preserved. '+json.dumps(report))
        print('Exported pages:',export(store,args.output,report))


if __name__=='__main__':main()
