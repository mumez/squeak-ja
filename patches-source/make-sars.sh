#!/usr/bin/env bash

set -eu

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

if [ "$#" -ne 1 ]; then
    echo "Usage: $0 VERSION" >&2
    exit 1
fi

MAX_VERSION="$1"

case "$MAX_VERSION" in
    ''|*[!0-9.]*|.*|*.)
        echo "Invalid version: $MAX_VERSION" >&2
        exit 1
        ;;
esac

if ! command -v zip >/dev/null 2>&1; then
    echo "zip command is required" >&2
    exit 1
fi

VERSION_DIR="$SCRIPT_DIR/$MAX_VERSION"
OUTPUT_DIR="$SCRIPT_DIR/../patches"

if [ ! -d "$VERSION_DIR" ]; then
    echo "Version directory not found: $VERSION_DIR" >&2
    exit 1
fi

if [ ! -d "$OUTPUT_DIR" ]; then
    echo "Output directory not found: $OUTPUT_DIR" >&2
    exit 1
fi

for patch_dir in "$VERSION_DIR"/PatchesJa*-"$MAX_VERSION"/; do
    [ -d "$patch_dir" ] || continue
    patch_name="$(basename "$patch_dir")"
    archive="$OUTPUT_DIR/${patch_name}.sar"

    echo "Creating $archive"
    rm -f "$archive" "$archive.zip"
    (
        cd "$patch_dir"
        zip -qr "$archive.zip" .
    )
    mv "$archive.zip" "$archive"
done
