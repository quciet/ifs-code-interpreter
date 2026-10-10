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

Explore Code includes an embedded Companion screen, authenticated loopback service,
background indexing and questions, cancellation, and saved answers with source
excerpts. The `.ifstool` includes its runtime and bundled code. Install it using
Companion > Settings > Manage tools > Add from file. Companion 0.1.2 or later provides
shared AI settings and folder browsing. API keys are protected with Windows DPAPI;
they are not stored in the tool or returned to the browser. The tool reads the shared
settings file supplied by Companion, including changes made while it is open.

Select bundled code or up to four local folders, prepare the index, then ask a
question or search locally. Stop preserves the previous complete index and saved
answers. Updating or uninstalling the tool preserves user data. The source version
is shown with each saved answer. If a new tool version bundles different commits,
Explore Code prompts you to prepare its index again.

Retrieval is lexical, not compiler-resolved dependency analysis. Semantic search,
iterative investigation, and wiki integration remain future work. Citation validation
checks reference existence, not claim correctness. Live model answer quality has
not yet been evaluated; integration tests use a local mock provider. No IFs execution
or numerical results analysis is performed.

## Build a Companion package

On Windows with Python and PyInstaller installed:

```powershell
python scripts/build_tool.py --python 'path\to\packaging-python.exe'
```

The build verifies tests and snapshot hashes, then produces
`release/ifs-code-reader-0.2.0-win-x64.ifstool` and its SHA256 file. Runtime protocol:
`IFS_TOOL_TOKEN`, `IFS_TOOL_DATA_DIR`, `IFS_TOOL_READY_FILE`, `IFS_COMPANION_PID`, and
`IFS_AI_SETTINGS_FILE`. Every service route requires `X-IFs-Tool-Token`. User indexes,
source selection, and answers remain outside the installation. Parent exit and stdin
EOF close the service and its worker. Source snapshots retain upstream notices.

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
