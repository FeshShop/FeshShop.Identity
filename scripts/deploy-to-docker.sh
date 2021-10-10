#!/bin/bash
if [ "$TRAVIS_BRANCH" == "main" ]; then
  echo "${$DOCKER_PASSWORD}" | docker login -u "${$DOCKER_USERNAME}" --password-stdin docker.io
  docker build .
  docker push $DOCKER_USERNAME/feshshop.identity:latest
fi
