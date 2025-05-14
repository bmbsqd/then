#!/bin/sh

# Check if NUGET_API_KEY is set
if [ -z "$NUGET_API_KEY" ]; then
  echo "Error: NUGET_API_KEY environment variable is not set."
  echo "Please set it before running this script."
  echo "Example: export NUGET_API_KEY=your_api_key_here"
  exit 1
fi

# Find and publish all .nupkg files
# Assumes the script is run from the project root (i.e. /Users/jonas/projects/then), and packages are in Then/bin/publish
# This script itself is located in the 'Then' directory.
PUBLISH_DIR="./bin/publish"

if ! ls "$PUBLISH_DIR"/*.{nupkg,snupkg} 1> /dev/null 2>&1; then
    echo "No .nupkg files found in $PUBLISH_DIR to publish."
    echo "Please run ./pack.sh first."
    exit 1
fi

for pkg in "$PUBLISH_DIR"/*.{nupkg,snupkg}; do
  if [ -f "$pkg" ]; then
    echo "Publishing $pkg..."
    dotnet nuget push "$pkg" --api-key "$NUGET_API_KEY" --source https://api.nuget.org/v3/index.json
  fi
  # No 'else' needed here because we check for existence of any .nupkg file upfront
done

echo "All packages published successfully."
