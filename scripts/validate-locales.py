#!/usr/bin/env python3
"""Validate locale JSON syntax, duplicate keys, and Vortex metadata."""

from pathlib import Path
import json


ROOT = Path(__file__).resolve().parents[1]
LOCALE = ROOT / "resources" / "locales" / "pt-BR"


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

    print(f"Validated {len(locale_files)} locale JSON files.")
    print(f"Vortex common.json strings: {len(parsed.get('common.json', {}))}")


if __name__ == "__main__":
    main()
