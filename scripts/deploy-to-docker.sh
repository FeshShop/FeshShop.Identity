#!/bin/bash
if [ "$TRAVIS_BRANCH" == "main" ]; then
  echo "Pushes a local container image to Docker Hub"

  echo $DOCKER_PASSWORD | docker login -u $DOCKER_USERNAME --password-stdin docker.io
  docker build -t feshshop.identity:latest .
  docker tag feshshop.identity:latest $DOCKER_USERNAME/feshshop.identity:latest
  docker push $DOCKER_USERNAME/feshshop.identity:latest
else
    echo "not pushing an image, we are not on main"
fi
