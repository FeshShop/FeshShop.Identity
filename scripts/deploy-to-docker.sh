#!/bin/bash
if [ "$TRAVIS_BRANCH" == "main" ]; then
  docker login -u $DOCKER_USERNAME -p $DOCKER_PASSWORD docker.io
  docker build .
  docker push $DOCKER_USERNAME/feshshop.identity:latest
fi
