#================================================================
# About API
#================================================================
# - Document  
#   - Development         http://localhost:28731/swagger
# - HealthCheck 
#   - Development         http://localhost:28731/health

#================================================================
# About minikube deployment
#================================================================
# export VERSION=$(cat src/Endpoint/Hello6/.version | head -n1)
#   Your docker image's version, ex:
#   - default: the content in the file of ./src/Endpoint/Hello6/.version
#   - or any specific such as 1.0.0.1

# export KUBE_DEPLOY_FILE='minikube/deployment.yaml'
#   Your deployment yaml file, ex:
#   - local folder/path
#   - or git repo/folder/path
#   - or azure devops artifacts/folder/path

#!/bin/bash
#================================================================
# Variables
#================================================================
if [ -z ${KUBE_DEPLOY_FILE+x} ]; then echo "KUBE_DEPLOY_FILE is unset" && exit 1; else echo "KUBE_DEPLOY_FILE is set to '$KUBE_DEPLOY_FILE'"; fi
if [ -z ${VERSION+x} ]; then echo "VERSION is unset" && exit 1; else echo "VERSION is set to '$VERSION'"; fi

REPO_NAME=hello6-api
KUBE_DEPLOY_FILE='minikube/deployment.yaml'
IMAGE_TAG_REGEX='[0-9]\+.[0-9]\+.[0-9]\+[.-]\(build\)\?[0-9]\+'

for i in {\
VERSION,REPO_NAME,KUBE_DEPLOY_FILE,IMAGE_TAG_REGEX\
}; do
  echo "$i = ${!i}"
done

#================================================================
# Update deployment version
#================================================================
sed -i "s/\/${REPO_NAME}:${IMAGE_TAG_REGEX}/\/${REPO_NAME}:${VERSION}/" ${KUBE_DEPLOY_FILE}

#================================================================
# Start deployment
#================================================================
envsubst < minikube/deployment.yaml | kubectl apply -f -
# deployment.apps/hello6 created

kubectl apply -f minikube/service.yaml -
# service/hello6-svc created

#================================================================
# Expose endpoint
#================================================================
minikube service hello6-svc --url
# service default/hello6-svc has no node port
# 🏃  Starting tunnel for service hello6-svc.
# |-----------|------------|-------------|------------------------|
# | NAMESPACE |    NAME    | TARGET PORT |          URL           |
# |-----------|------------|-------------|------------------------|
# | default   | hello6-svc |             | http://127.0.0.1:36883 |
# |-----------|------------|-------------|------------------------|
# http://127.0.0.1:36883
# ❗  Because you are using a Docker driver on linux, the terminal needs to be open to run it.
# ^C✋  Stopping tunnel for service hello6-svc.
# 
# ❌  Exiting due to SVC_TUNNEL_STOP: stopping ssh tunnel: os: process already finished
# 
# 😿  If the above advice does not help, please let us know:
# 👉  https://github.com/kubernetes/minikube/issues/new/choose


#================================================================
# HealthCheck
#================================================================
curl http://127.0.0.1:36883/health
# Healthy

#================================================================
# Trace kubectl commands
#================================================================
kubectl get deploy
# NAME             READY   UP-TO-DATE   AVAILABLE   AGE
# hello6           2/2     2            2           77s

kubectl get po
# NAME                              READY   STATUS    RESTARTS   AGE
# hello6-6c7d69c76-4wxjr            1/1     Running   0          9s
# hello6-6c7d69c76-skwhz            1/1     Running   0          9s

kubectl get svc
# NAME             TYPE        CLUSTER-IP       EXTERNAL-IP   PORT(S)          AGE
# hello6-svc       ClusterIP   10.110.246.31    <none>        28731/TCP        2m4s
