#!/usr/bin/env python3
"""Validate locale JSON syntax, duplicate keys, and extension separation."""

from pathlib import Path
import json
import re


ROOT = Path(__file__).resolve().parents[1]
LOCALE = ROOT / "resources" / "locales" / "pt-BR"
PATCH = ROOT / "patches" / "modlist-backup" / "index.js"


class DuplicateKeyError(ValueError):
    pass


def object_without_duplicate_keys(pairs):
    result = {}
    for key, value in pairs:
        if key in result:
            raise DuplicateKeyError(f"duplicate key: {key}")
        result[key] = value
    return result


def read_json(path: Path):
    return json.loads(
        path.read_text(encoding="utf-8-sig"),
        object_pairs_hook=object_without_duplicate_keys,
    )


def placeholders(value: str) -> list[str]:
    return sorted(re.findall(r"\{\{\s*[^{}]+\s*\}\}", value))


def main() -> None:
    locale_files = sorted(LOCALE.glob("*.json"))
    if not locale_files:
        raise SystemExit(f"No locale JSON files found in {LOCALE}")

    parsed = {}
    for path in locale_files:
        try:
            parsed[path.name] = read_json(path)
        except Exception as error:
            raise SystemExit(f"Invalid {path.relative_to(ROOT)}: {error}") from error

    info = parsed.get("info.json", {})
    if info.get("type") != "translation":
        raise SystemExit("info.json must declare type=translation")
    if info.get("name") != "Tradução Português do Brasil p/ Vortex by Rikintosh":
        raise SystemExit("Unexpected translation display name in info.json")

    common = parsed.get("common.json", {})
    extension = parsed.get("modlist-backup.json", {})
    overlap = sorted(set(common) & set(extension))
    if overlap:
        raise SystemExit("Modlist Backup keys must stay out of common.json: " + ", ".join(overlap))
    for key, translation in extension.items():
        if placeholders(key) != placeholders(translation):
            raise SystemExit(f"Placeholder mismatch in modlist-backup.json: {key}")

    if not PATCH.is_file():
        raise SystemExit(f"Missing extension patch: {PATCH}")
    patch_text = PATCH.read_text(encoding="utf-8")
    if "loadNamespaces" not in patch_text or "modlist-backup" not in patch_text:
        raise SystemExit("Modlist Backup patch does not load its separate namespace")

    print(f"Validated {len(locale_files)} locale JSON files.")
    print(f"Vortex core strings: {len(common)}")
    print(f"Modlist Backup strings: {len(extension)}")


if __name__ == "__main__":
    main()
