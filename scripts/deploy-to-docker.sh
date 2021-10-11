#!/bin/bash
if [ "$TRAVIS_BRANCH" == "main" ]; then
  echo "Pushes a local container image to Docker Hub"

  echo $DOCKER_PASSWORD | docker login -u $DOCKER_USERNAME --password-stdin docker.io
  docker build -t feshShop.identity:latest .
  docker tag feshShop.identity:$DOCKER_TAG $DOCKER_USERNAME/feshShop.identity:$DOCKER_TAG
  docker push $DOCKER_USERNAME/feshshop.identity:latest
else
    echo "not pushing an image, we are not on master"
fi
