#!/usr/bin/env python3
"""Build the Vortex translation ZIP and the optional extension patch artifact."""

from pathlib import Path
from zipfile import ZIP_DEFLATED, ZipFile
import json
import shutil


ROOT = Path(__file__).resolve().parents[1]
LOCALE = ROOT / "resources" / "locales" / "pt-BR"
DIST = ROOT / "dist"
PACKAGE = DIST / "package"
PATCH = ROOT / "patches" / "modlist-backup" / "index.js"
README = ROOT / "resources" / "read-me.txt"


def main() -> None:
    info_path = LOCALE / "info.json"
    info = json.loads(info_path.read_text(encoding="utf-8-sig"))
    version = str(info["version"])
    if not version or any(char in version for char in "/\\"):
        raise SystemExit("Invalid version in resources/locales/pt-BR/info.json")
    if not PATCH.is_file():
        raise SystemExit(f"Missing extension patch: {PATCH}")
    if not README.is_file():
        raise SystemExit(f"Missing package readme: {README}")

    if DIST.exists():
        shutil.rmtree(DIST)
    package_locale = PACKAGE / "pt-BR"
    package_locale.mkdir(parents=True)
    shutil.copytree(LOCALE, package_locale, dirs_exist_ok=True)
    shutil.copy2(README, PACKAGE / "read-me.txt")

    archive = DIST / f"Vortex_PT-BR_{version}.zip"
    with ZipFile(archive, "w", ZIP_DEFLATED) as zip_file:
        for path in sorted(PACKAGE.rglob("*")):
            if path.is_file():
                zip_file.write(path, path.relative_to(PACKAGE).as_posix())

    shutil.copy2(PATCH, DIST / f"Modlist_Backup_index_{version}.js")
    print(f"Created {archive.relative_to(ROOT)}")
    print(f"Created {(DIST / f'Modlist_Backup_index_{version}.js').relative_to(ROOT)}")


if __name__ == "__main__":
    main()
