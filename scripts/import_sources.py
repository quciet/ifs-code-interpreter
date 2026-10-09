"""Export reviewed source snapshots from local Git checkouts, without Git history."""
import argparse
from datetime import datetime, timezone
import hashlib
import json
from pathlib import Path
import re
import subprocess

ROOT = Path(__file__).resolve().parents[1]
ALLOWED = {'.vb','.cs','.aspx','.ascx','.asax','.js','.ts','.css','.html','.htm','.md','.txt','.config','.json','.xml','.sql','.resx','.vbproj','.csproj','.sln','.props','.targets','.settings','.myapp','.xsd','.xslt','.xsl','.csv','.yml','.yaml','.svg'}
EXCLUDED = {'bin','obj','packages','node_modules','.git','.vs','.vscode'}


def git(root, *args):
    return subprocess.check_output(['git','-c',f'safe.directory={root.as_posix()}','-C',str(root),*args])


def sanitize(text):
    # Preserve line positions. Include commented-out credentials in the review.
    assignment = re.compile(r'(?i)(\b(?:password|pwd|DataPW\$?|DatabasePassword|api_key|client_secret)\s*(?:As\s+String\s*)?=[ \t]*")[^"\r\n]+"')
    output = []
    for line in text.splitlines(keepends=True):
        original = line
        line = assignment.sub(lambda m: m[1]+'[REDACTED]"' if original[:m.start()].count('"') % 2 == 0 else m[0], line)
        # Only replace a connection-string value inside a quoted literal.
        line = re.sub(r'"[^"\r\n]*"', lambda literal: re.sub(
            r'(?i)((?:password|pwd)\s*=\s*)([^;"\s<>]+)',
            lambda m: m[1]+'[REDACTED]', literal[0]), line)
        output.append(line)
    return ''.join(output)


def export(root, name, repository):
    root = Path(root).resolve(strict=True)
    destination = ROOT/'data'/name
    if destination.exists():
        raise ValueError(f'{destination} exists; review and move the previous snapshot before importing.')
    commit = git(root,'rev-parse','HEAD').decode().strip()
    entries = git(root,'ls-tree','-r','-z','--full-tree','HEAD').split(b'\0')
    included, excluded, redacted = [], [], []
    for entry in entries:
        if not entry:
            continue
        meta, raw_path = entry.split(b'\t',1)
        mode, kind, blob = meta.decode().split()
        relative = raw_path.decode('utf-8')
        path = Path(relative)
        if kind != 'blob' or mode == '120000' or any(p.lower() in EXCLUDED for p in path.parts) or (path.suffix.lower() not in ALLOWED and not path.name.lower().startswith(('license','copying','notice'))):
            excluded.append(relative)
            continue
        raw = git(root,'cat-file','blob',blob)
        if len(raw)>8_000_000:
            excluded.append(relative)
            continue
        encoding = 'utf-16' if raw.startswith((b'\xff\xfe',b'\xfe\xff')) else 'utf-8-sig'
        try:
            body = raw.decode(encoding)
        except UnicodeDecodeError:
            encoding = 'cp1252'
            body = raw.decode(encoding, errors='replace')
        if '\x00' in body:
            excluded.append(relative)
            continue
        cleaned = sanitize(body)
        if cleaned != body:
            redacted.append(relative)
            output = cleaned.encode(encoding)
        else:
            output = raw
        target = destination/path
        target.parent.mkdir(parents=True,exist_ok=True)
        target.write_bytes(output)
        included.append({'path':relative,'sha256':hashlib.sha256(output).hexdigest(),'upstream_sha256':hashlib.sha256(raw).hexdigest()})
    return {'repository':repository,'commit':commit,'branch':git(root,'branch','--show-current').decode().strip(),
            'imported_at':datetime.now(timezone.utc).isoformat(),'directory':f'data/{name}',
            'files':included,'excluded':excluded,'redacted':redacted}


if __name__ == '__main__':
    parser=argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--ifs-dev',required=True)
    parser.add_argument('--ifs-web',required=True)
    args=parser.parse_args()
    sources=[export(args.ifs_dev,'ifs-dev','https://github.com/PardeeCenterDU/IFs-Dev'),
             export(args.ifs_web,'ifs-web','https://github.com/PardeeCenterDU/IFsWeb')]
    (ROOT/'data/sources.json').write_text(json.dumps({'schema_version':1,'sources':sources},indent=2)+'\n',encoding='utf-8')
    for source in sources:
        print(source['directory'],source['commit'],len(source['files']),'files;',len(source['redacted']),'redacted')
