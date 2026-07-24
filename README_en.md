# squeak-ja

![Squeak Japanese Edition screenshot](docs/screenshots/squeak-ja-6.0.png)

Japanese localization patches and translations for [Squeak Smalltalk](https://squeak.org/).

## Installation

Drag and drop [`installers/InstallJa20260724.sar`](./installers/InstallJa20260724.sar) onto a running Squeak image window,
then choose "install SAR" from the menu that appears.

## Folder structure

- `installers/` — final, dated end-user installer archives (`InstallJa<YYYYMMDD>.sar`). Drag-and-drop one of these to install the patches, translations, fonts, and utilities in one step.
- `installerBuild/` — staging area used to build the installer bundle (fonts, package/font utility changesets, README text shown inside Squeak).
- `patches/` — released patch archives (`.sar`), per-Squeak-version listing files (`PatchList<version>-ja.txt`), and the remote-install bootstrap `PatchInstaller-ja.st`.
- `patches-source/<version>/` — unpacked changeset source behind each released patch archive.
- `translations/` — source data for Japanese UI translations (`trans.tsv`), generated translation files (`ja-<date>.translation`), per-version listing files, and the remote-install bootstrap `TransInstaller-ja.st`.
- `docs/` — documentation assets such as screenshots.
