"""Maintainer-only download of the pinned public embedding model for release builds."""
import hashlib
import json
from pathlib import Path
from urllib.request import urlopen

ROOT=Path(__file__).resolve().parents[1]


def main():
    manifest=json.loads((ROOT/'data/embedding-model.json').read_text(encoding='utf-8'))
    dest=ROOT/'workspace/model';dest.mkdir(parents=True,exist_ok=True)
    base=f"https://huggingface.co/{manifest['distribution']}/resolve/{manifest['revision']}/"
    for name,sha in manifest['files'].items():
        path=dest/name
        if path.exists() and hashlib.sha256(path.read_bytes()).hexdigest()==sha:continue
        with urlopen(base+name,timeout=120) as response:raw=response.read(100_000_001)
        if hashlib.sha256(raw).hexdigest()!=sha:raise ValueError('Model checksum mismatch: '+name)
        path.write_bytes(raw)
    print('Pinned local embedding model verified:',dest)


if __name__=='__main__':main()
