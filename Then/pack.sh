#!/bin/sh

# Define the output directory relative to this script's location
OUTPUT_DIR="./bin/publish"

# Create the output directory if it doesn't exist, and clear it
mkdir -p "$OUTPUT_DIR"
rm -rf "$OUTPUT_DIR"/*

echo "Packing NuGet package to $OUTPUT_DIR..."
dotnet build ./Then.csproj --configuration Release
dotnet pack ./Then.csproj --configuration Release --include-symbols --include-source --output "$OUTPUT_DIR"

echo "Package created in $OUTPUT_DIR"
