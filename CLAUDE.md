# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this repo is

squeak-ja distributes Japanese localization for Squeak Smalltalk: UI string translations, source-code patches (`.cs`/`.st` changesets), and installer packages (`.sar` files) that get loaded *into a running Squeak image* via Squeak's own fileIn/SAR mechanisms. There is no compiler, build system, package manager, or test suite in the conventional sense — "building" means assembling files into a zip-based `.sar` archive, and "running" means loading that archive inside Squeak itself.

## Repository layout

- `translations/` — Japanese UI string translations.
  - `trans.tsv` — master tab-separated `English phrase<TAB>Japanese phrase` source table (this is what maintainers actually edit).
  - `applyTranslations.st` — a Squeak doit that reads `trans.tsv`, feeds each pair into `NaturalLanguageTranslator current`, then files out the result as a dated `.translation` binary (e.g. `ja-20260724.translation`). Must be run from *inside* a Squeak image, not as a standalone script.
  - `ja-YYYYMMDD.translation` — generated translation files (one per release), loaded into Squeak via `NaturalLanguageTranslator`.
  - `TransList<version>-ja.txt` — per-Squeak-version array literals (`#(...)`) listing which `.translation` files apply to that Squeak version. `TransInstaller-ja.st` picks the right list at install time based on `Smalltalk version`.
  - `TransInstaller-ja.st` — the remote-fileIn bootstrap script end users `Do it` in Squeak; it downloads the correct `TransList<version>-ja.txt` from GitHub raw and loads only the not-yet-installed entries (tracked via the global `TransJa`).
- `patches/` — released patch `.sar` archives plus `PatchList<version>-ja.txt` (same array-of-filenames pattern as translations) and `PatchInstaller-ja.st` (same remote bootstrap pattern, tracked via global `PatchesJa`). One `.sar` may target multiple Squeak versions (see filename suffix, e.g. `-4.3.sar` vs `-6.0.sar`).
- `patches-source/<squeak-version>/PatchesJa<date>-<version>/` — unpacked source for each released patch archive, before zipping:
  - `install/preamble`, `install/postscript` — Smalltalk snippets run before/after the patch's members are filed in (see `SARInstaller`/`ChangeSet` "members installer" convention: `install/postscript` typically iterates `self membersMatching: '<version>-extra-patches/*'` and fileIns each one, then registers itself in `PatchesJa`).
  - `<version>-extra-patches/*.cs` or `*.st` — the actual changesets/method sources (Squeak fileIn format, with `stamp:` doit headers).
  - `patches-source/make-sars.sh <version>` — zips every `PatchesJa*-<version>/` directory under `patches-source/<version>/` into a `.sar` in `patches/`, replacing any existing archive of the same name. Run this after editing/adding patch source for a version.
- `installerBuild/` — staging area for the full installer bundle (fonts, package/font utility changesets, the JaEx Squeak-side package `.mcz`, README files shown inside Squeak). `make-sar.sh` copies `patches/` into here, zips the whole directory into `InstallJa<YYYYMMDD>.sar`, moves it to `installers/`, and cleans up the copied `patches/`.
- `installers/` — final, dated end-user installer archives (`InstallJa<YYYYMMDD>.sar`) — drag-and-drop these onto a running Squeak image to install everything (patches + translations + fonts + utilities) in one step.
- `docs/screenshots/` — repo documentation images.

## Common workflows

**Add/update a UI translation**
1. Edit `translations/trans.tsv` (tab-separated `English<TAB>Japanese`).
2. Inside a Squeak image, update the date in `applyTranslations.st` and run it as a doit — this regenerates the `ja-YYYYMMDD.translation` file from `trans.tsv`.
3. Add the new `.translation` filename to the relevant `TransList<version>-ja.txt`.

**Add a source patch for a Squeak version**
1. Create/update `patches-source/<version>/PatchesJa<date>-<version>/` with the changeset files under `<version>-extra-patches/` and the `install/preamble` + `install/postscript` scripts.
2. Run `patches-source/make-sars.sh <version>` to zip it into `patches/PatchesJa<date>-<version>.sar`.
3. Add the new `.sar` filename to `patches/PatchList<version>-ja.txt`.

**Cut a new full installer release**
1. Ensure `patches/` and `translations/` contain the latest files, and `installerBuild/` has the current supporting assets (fonts, package utility changesets, README text).
2. Run `./make-sar.sh` from the repo root — it produces `installers/InstallJa<YYYYMMDD>.sar`.

## Conventions to preserve

- Filenames encode dates (`YYYYMMDD`) and target Squeak versions (e.g. `-6.0`, `-5.3`) — keep both when adding new patches/translations, since installers pick files by matching `Smalltalk version` against the version suffix (see `TransInstaller-ja.st` / `PatchInstaller-ja.st`).
- The `TransList*`/`PatchList*` files are literal Smalltalk array-of-strings (`#(...)`) read back with `Array readFrom:` — keep the exact quoting/format when editing.
- Installed items are tracked idempotently via global `OrderedCollection`s (`TransJa`, `PatchesJa`) so re-running an installer doesn't reapply already-installed entries — new `install/postscript` scripts for patches should follow the same "check membership, then add" pattern shown in existing ones.
- Most prose files (READMEs shown inside Squeak, comments) are written in Japanese; keep new user-facing text consistent with that unless it's developer-facing tooling (shell scripts, this file).
