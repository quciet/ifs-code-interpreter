# IFs Code Interpreter

Code research backend for IFs Companion's Explore Code tool. Ask questions about
International Futures implementation and inspect the source evidence behind answers.

## Repository layout

- `tool/`: independent Python backend and command-line interface.
- `data/ifs-dev/`: source snapshot of PardeeCenterDU/IFs-Dev.
- `data/ifs-web/`: source snapshot of PardeeCenterDU/IFsWeb.
- `data/sources.json`: upstream commits, file hashes, exclusions, and redactions.
- `scripts/`: source import utilities.
- `tests/`: offline backend tests.

Snapshots contain source, documentation, and relevant text resources, not complete
buildable distributions. Git history, binaries, build outputs, and dependency
directories are excluded. Hard-coded password values are redacted without changing
line positions. Upstream source notices remain applicable; this repository does not
relicense the bundled IFs source or third-party files.

## Run locally

Requires Python 3.11+ with SQLite FTS5. Run from this repository's root. No Python
packages or IFs installation are required for indexing and search.

```powershell
python tool/ifs_code_interpreter.py index
python tool/ifs_code_interpreter.py search "GDP population"
```

To answer questions, configure a Chat Completions-compatible provider:

```powershell
$env:IFS_AI_ENDPOINT = 'https://your-provider.example/v1'
$env:IFS_AI_MODEL = 'your-model-name'
# Supply IFS_AI_API_KEY through your local environment; never commit it.
python tool/ifs_code_interpreter.py ask "Where is GDP calculated?"
```

Asking sends the question and retrieved source excerpts to the configured provider
and may incur usage charges. Search and indexing stay local. Credentials are read
from the environment and are not saved by this tool. API redirects are disabled.

## Use another code version

Obtain the source yourself, then select one or more local folders:

```powershell
python tool/ifs_code_interpreter.py --index workspace/older.sqlite3 index --source 'D:\My IFs Source'
python tool/ifs_code_interpreter.py --index workspace/older.sqlite3 ask "Where is GDP calculated?"
```

Original files are never modified or executed. The index stores a snapshot; rerun
indexing to pick up changes. Answers cite paths, line ranges, and file SHA256 hashes.
An IFs source snapshot is not proof of compatibility with an installed model or
saved run. See `data/sources.json` for bundled revision identities.

## Status and limitations

This initial backend provides lexical retrieval and model-generated cited answers.
It is not yet an installable `.ifstool` or integrated Companion screen. Companion
will own navigation and shared settings; the earlier research assistant's standalone
application layout is not copied. Semantic retrieval, iterative code investigation,
and wiki integration are future work. Lexical matches are not compiler-resolved
dependencies or proof of causality. Citation validation checks reference existence,
not whether every generated claim follows from the evidence. Live model quality has
not yet been evaluated. No model execution or results analysis is implemented here.

## Maintainer snapshot import

Clone or update the two upstream repositories using authorized Git credentials.
Review the selected commits before running:

```powershell
python scripts/import_sources.py --ifs-dev 'D:\IFs Dev Code\IFs-Dev' --ifs-web 'D:\IFs Dev Code\IFsWeb'
```

The importer refuses to overwrite an existing snapshot. Review and move previous
snapshots first. Review all imported files for publication suitability; automated
redaction is not a complete secret scanner. Never publish upstream history or tokens.
Keep both source snapshots pinned to the manifest commits for each tool release.

## Validation

```powershell
python -m unittest discover -s tests -v
```
