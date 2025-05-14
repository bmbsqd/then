#!/bin/sh

# This script bumps the version in Then.csproj
# Usage: ./bump-version.sh [major|minor|patch]

PROJECT_FILE="./Then.csproj"
VERSION_TAG="Version"

if [ "$#" -ne 1 ]; then
    echo "Usage: $0 [major|minor|patch]"
    exit 1
fi

BUMP_TYPE=$1

if [ "$BUMP_TYPE" != "major" ] && [ "$BUMP_TYPE" != "minor" ] && [ "$BUMP_TYPE" != "patch" ]; then
    echo "Error: Invalid bump type. Must be 'major', 'minor', or 'patch'."
    echo "Usage: $0 [major|minor|patch]"
    exit 1
fi

# Read current version
# Using grep to find the line, then sed to extract the version number
CURRENT_VERSION_LINE=$(grep "<${VERSION_TAG}>" "$PROJECT_FILE")
if [ -z "$CURRENT_VERSION_LINE" ]; then
    echo "Error: Could not find <${VERSION_TAG}> tag in $PROJECT_FILE"
    exit 1
fi

CURRENT_VERSION=$(echo "$CURRENT_VERSION_LINE" | sed -n "s/.*<${VERSION_TAG}>\([^<]*\)<\/${VERSION_TAG}>.*/\1/p")
if [ -z "$CURRENT_VERSION" ]; then
    echo "Error: Could not extract version number from $PROJECT_FILE"
    echo "Line found: $CURRENT_VERSION_LINE"
    exit 1
fi

echo "Current version: $CURRENT_VERSION"

# Split version into parts
OLD_IFS=$IFS
IFS='.'
# shellcheck disable=SC2206 # Word splitting is intended here
VERSION_PARTS=($CURRENT_VERSION)
IFS=$OLD_IFS

MAJOR=${VERSION_PARTS[0]}
MINOR=${VERSION_PARTS[1]}
PATCH=${VERSION_PARTS[2]}

# Bump the specified part
case $BUMP_TYPE in
    major)
        MAJOR=$((MAJOR + 1))
        MINOR=0
        PATCH=0
        ;;
    minor)
        MINOR=$((MINOR + 1))
        PATCH=0
        ;;
    patch)
        PATCH=$((PATCH + 1))
        ;;
esac

NEW_VERSION="$MAJOR.$MINOR.$PATCH"
echo "New version: $NEW_VERSION"

# Replace the version in the project file
# Using a temporary file for sed to work reliably on macOS and Linux
TMP_FILE=$(mktemp)
sed "s#<${VERSION_TAG}>${CURRENT_VERSION}</${VERSION_TAG}>#<${VERSION_TAG}>${NEW_VERSION}</${VERSION_TAG}>#" "$PROJECT_FILE" > "$TMP_FILE" && mv "$TMP_FILE" "$PROJECT_FILE"

if [ $? -eq 0 ]; then
    echo "Version bumped successfully in $PROJECT_FILE"
else
    echo "Error: Failed to update version in $PROJECT_FILE"
    rm -f "$TMP_FILE" # Clean up temp file on error
    exit 1
fi

# Clean up temp file if it still exists (should have been moved)
rm -f "$TMP_FILE"
