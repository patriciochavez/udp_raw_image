#!/bin/bash
sudo docker ps --all | grep webcamstream | awk {'print $1}' | xargs sudo docker rm -f
sudo docker build --no-cache -t patriciochavez/controller-webcamstream .
sudo docker run -p 8090:8090 -d --name webcamstream patriciochavez/controller-webcamstream
