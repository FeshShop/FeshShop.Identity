#!/bin/bash
CONFIGURATION=""

case "$TRAVIS_BRANCH" in
  "main")
    CONFIGURATION="Release"
    ;;
  "dev")
    CONFIGURATION="Debug"
    ;;    
esac

dotnet build -c $CONFIGURATION --no-cache