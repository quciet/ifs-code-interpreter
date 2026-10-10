"""Build the self-contained Windows Explore Code package with PyInstaller."""
import argparse
import hashlib
import json
from pathlib import Path
import shutil
import subprocess
import sys
import zipfile

ROOT=Path(__file__).resolve().parents[1]
VERSION='0.3.0'


def main():
    parser=argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--python',default=sys.executable,help='Python with PyInstaller installed')
    args=parser.parse_args()
    subprocess.run([args.python,'-m','unittest','discover','-s','tests','-v'],cwd=ROOT,check=True)
    model=json.loads((ROOT/'data/embedding-model.json').read_text(encoding='utf-8'))
    for name,sha in model['files'].items():
        path=ROOT/'workspace/model'/name
        if not path.is_file() or hashlib.sha256(path.read_bytes()).hexdigest()!=sha:
            raise ValueError('Missing or changed embedding model. Run scripts/prepare_model.py: '+name)
    manifest=json.loads((ROOT/'data/sources.json').read_text())
    for source in manifest['sources']:
        for item in source['files']:
            if hashlib.sha256((ROOT/source['directory']/item['path']).read_bytes()).hexdigest()!=item['sha256']:
                raise ValueError('Source snapshot differs from its manifest: '+item['path'])
    subprocess.run([args.python,'-m','PyInstaller','--noconfirm','--clean','--name','ExploreCode','--console',
                    '--distpath',str(ROOT/'dist'),'--workpath',str(ROOT/'build'),'--specpath',str(ROOT/'build'),
                    '--add-data',str(ROOT/'tool/static')+';static','--add-data',str(ROOT/'data')+';data',
                    '--add-data',str(ROOT/'workspace/model')+';data/embedding-model',
                    '--collect-all','fastembed','--collect-all','onnxruntime','--copy-metadata','fastembed',
                    str(ROOT/'tool/companion_service.py')],cwd=ROOT,check=True)
    stage=ROOT/'dist/ExploreCode'
    runtime=Path(subprocess.check_output([args.python,'-c','import sys;print(sys.base_prefix)'],text=True).strip())
    licenses=stage/'licenses';licenses.mkdir(exist_ok=True)
    for file in runtime.glob('LICENSE*'):
        shutil.copy2(file,licenses/file.name)
    # Preserve distribution metadata/licenses for bundled third-party dependencies.
    import importlib.metadata
    for package in ('fastembed','onnxruntime','numpy','tokenizers','huggingface-hub','requests','httpx','pillow','loguru','py-rust-stemmers'):
        try:
            distribution=importlib.metadata.distribution(package)
            for entry in distribution.files or []:
                if any(word in entry.name.lower() for word in ('license','copying','notice')) and 'dist-info' in str(entry):
                    shutil.copy2(distribution.locate_file(entry),licenses/(package+'-'+entry.name))
        except importlib.metadata.PackageNotFoundError:pass
    shutil.copy2(ROOT/'README.md',stage/'README.md')
    (stage/'tool.json').write_text(json.dumps({'id':'ifs-code-reader','name':'Explore Code','version':VERSION,
        'api_version':1,'platform':'win-x64','kind':'web-service','entrypoint':'ExploreCode.exe'},indent=2))
    release=ROOT/'release';release.mkdir(exist_ok=True)
    destination=release/f'ifs-code-reader-{VERSION}-win-x64.ifstool'
    pending=destination.with_suffix('.partial')
    with zipfile.ZipFile(pending,'w',zipfile.ZIP_DEFLATED,compresslevel=6) as archive:
        for file in sorted(stage.rglob('*')):
            if file.is_file(): archive.write(file,file.relative_to(stage).as_posix())
    with zipfile.ZipFile(pending) as archive:
        if archive.testzip(): raise RuntimeError('Package checksum validation failed.')
    pending.replace(destination)
    with destination.open('rb') as stream: digest=hashlib.file_digest(stream,'sha256').hexdigest()
    destination.with_suffix('.sha256').write_text(digest+'  '+destination.name+'\n')
    print(json.dumps({'package':str(destination),'sha256':digest,'size':destination.stat().st_size}))


if __name__=='__main__':main()
