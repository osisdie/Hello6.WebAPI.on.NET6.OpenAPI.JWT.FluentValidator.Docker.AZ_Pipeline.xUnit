## Prerequisite
- [Download VS Preview](https://visualstudio.microsoft.com/vs/preview/)
  - The latest Visual Studio
- [Download .NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)
  - Windows x64 and Linux for WSL/WSL2
- [Setup minikube](https://minikube.sigs.k8s.io/docs/start/)
  - Skip if you have you own k8s environment
- [nginx for Windows](https://nginx.org/en/docs/windows.html)
  - Skip if you don't need reverse proxy server
- [memurai-Redis for Windows alternative In-Memory Datastore](https://www.memurai.com/)
  - Skip if you have your own Redis server

## Features
- Use NET Standard 2.1 for library projects
- Use NET 6 for Web Applications (compatible with .NET Core 3.1 and .NET 5.0)
- Use Built-In Microsoft ConfigurationBuilder to config (appsettings.json, hellosettings.json)
- Use Secret Storage to protect sensitive configs
- Use Log4net Logging on console, file and AWS CloudWatch on demand
- Use Middleware for logging Request/Response
- Use Built-In Microsoft DI to inject most service classes
- Use Data Annotations to validate Request data format
- Follow HTTP Status Code for 2xx, 3xx, 4xx
- Async for HTTP and Database communications
- CamelCase property format
- Use Built-In Microsoft IDistributedCache for Redis Cache (Cache for Database)
- Use Built-In Microsoft ILogger for Log4net Adapter
- Use Built-In Microsoft OpenAPI for Swagger UI (v1 and v2)
- Use Built-In HealthCheck (~/health)
- Use **.editorconfig** to align coding style 
- Switch deployment environment by runtime system environment variable
  - ASPNETCORE_ENVIRONMENT: **Debug**
  - ASPNETCORE_ENVIRONMENT: **Development**
  - ASPNETCORE_ENVIRONMENT: **Testing**
  - ASPNETCORE_ENVIRONMENT: **Staging**
  - ASPNETCORE_ENVIRONMENT: **Production**
- Use xUnit UnitTest Projects  


## Projects
**Hello6.Domain.Endpoint** is the primary project wraps all the other dependent projects, such as
- CoreFX: Including abstraction design, common utilites
  - CoreFX.Abstractions
  - CoreFX.Common
  - CoreFX.Hosting
  - CoreFX.Logging.Log4net
  - CoreFX.Caching.Redis
  
- Hello6: Including domain-driven design, services
  - Hello6.Domain.Common
  - Hello6.Domain.Contract
  - Hello6.Domain.DataAccess.Database
  - Hello6.Domain.SDK

## Versioning
Whenever any feature, bugfix or necessary to rebuild a new image, make sure you or your builder modify **.version** and **ChangeLog.md** files. 
- Version File: `./src/Endpoint/Hello6/.version`
- Version Format: `#.#.#.#-###`
  - [major version].[minor version].[build version].[revision version]  (ex: `1.0.0.100`)
  - Adding AZ pipeline's build-id in the suffix
    - #**.**#**.**#**.**#**-**###  (ex: `1.0.0.0-100`)

- ChangeLog File: `./ChangeLog.md`
- ChangeLog Format: Markdown with date and version number, such as
  ```markdown
  ### 2021-04-01
  * ** Hello6.Domain.Contract (1.0.0)**
    * Created
  ```

- Git Version Tags: `hello6-api/v1.0.0.0-100`


## UnitTesting

### End to end integration Test
- Necessary Environment Variables
  - **ASPNETCORE_ENVIRONMENT**: ex: `Development`
  - **CI_TEST_ENDPOINT**: ex: `http://localhost:5006`

- Expect test results
  - Passed!
  - Duration: less then 5s

```powershell
$env:ASPNETCORE_ENVIRONMENT = 'Development'
$env:CI_TEST_ENDPOINT = 'http://localhost:5006'

dotnet test tests/IntegrationTest/Hello6/IntegrationTest.Hello6.csproj -c Release --filter FullyQualifiedName=IntegrationTest.Hello6.CI_Test.Integration_Test

#  Determining projects to restore...
#  Restored .\tests\IntegrationTest\Hello6\IntegrationTest.Hello6.csproj (in 475 ms).
#  5 of 6 projects are up-to-date for restore.
#  CoreFX.Abstractions -> .\src\Library\CoreFX\Abstractions\bin\Release\netstandard2.1\CoreFX.Abstractions.dll
#  CoreFX.Common -> .\src\Library\CoreFX\Common\bin\Release\netstandard2.1\CoreFX.Common.dll
#  Hello6.Domain.Common -> .\src\Common\bin\Release\netstandard2.1\Hello6.Domain.Common.dll
#  Hello6.Domain.Contract -> .\src\Endpoint\Contracts\Hello6\bin\Release\netstandard2.1\Hello6.Domain.Contract.dll
#  TestAbstractions -> .\tests\TestAbstractions\bin\Release\net6.0\TestAbstractions.dll
#  IntegrationTest.Hello6 -> .\tests\IntegrationTest\Hello6\bin\Release\net6\IntegrationTest.Hello6.dll

# Test run for .\tests\IntegrationTest\Hello6\bin\Release\net6\IntegrationTest.Hello6.dll (.NETCoreApp,Version=v5.0)
# Microsoft (R) Test Execution Command Line Tool Version 16.8.0
# Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:     1, Skipped:     0, Total:     1, Duration: 874 ms - IntegrationTest.Hello6.dll (net6.0)
```

### UnitTest

- Necessary Environment Variables
  - **ASPNETCORE_ENVIRONMENT**: ex: `Debug`
  - **HELLO_REDIS_CACHE_CONN**: ex: `127.0.0.1:6379`
- Expect test results
  - Passed!
  - Duration: less then 1m

```powershell
$env:ASPNETCORE_ENVIRONMENT = 'Debug'
$env:HELLO_REDIS_CACHE_CONN = '127.0.0.1:6379'

dotnet test tests/UnitTest/CoreFX/Caching/Redis/UnitTest.CoreFX.Caching.Redis.csproj -c Release --filter FullyQualifiedName=UnitTest.CoreFX.Caching.Redis.Tests.Cache_Test.Integration_Test

#  Determining projects to restore...
#  All projects are up-to-date for restore.
#  CoreFX.Abstractions -> .\src\Library\CoreFX\Abstractions\bin\Release\netstandard2.1\CoreFX.Abstractions.dll
#  CoreFX.Common -> .\src\Library\CoreFX\Common\bin\Release\netstandard2.1\CoreFX.Common.dll
#  TestAbstractions -> .\tests\TestAbstractions\bin\Release\net6.0\TestAbstractions.dll
#  CoreFX.Caching.Redis -> .\src\Library\CoreFX\Caching\Redis\bin\Release\netstandard2.1\CoreFX.Caching.Redis.dll
#  UnitTest.CoreFX.Caching.Redis -> .\tests\UnitTest\CoreFX\Caching\Redis\bin\Release\net6.0\UnitTest.CoreFX.Caching.Redis.dll
# Test run for .\tests\UnitTest\CoreFX\Caching\Redis\bin\Release\net6.0\UnitTest.CoreFX.Caching.Redis.dll (.NETCoreApp,Version=v5.0)
# Microsoft (R) Test Execution Command Line Tool Version 16.8.0
# Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:     1, Skipped:     0, Total:     1, Duration: 40 s - UnitTest.CoreFX.Caching.Redis.dll (net6.0)
```

K8s/Minikube 
---

### Build docker image

#### Review `Dockerfile`

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /build
COPY . .
RUN bash -c 'cat nuget.xml > ./nuget.config'

# --------------------------
# COPY Dependency Files
# --------------------------
COPY data/ src/Endpoint/Hello6/App_Data/

# --------------------------
# Build & Publish
# --------------------------
RUN dotnet restore "src/Endpoint/Hello6/Hello6.Domain.Endpoint.csproj" --configfile ./nuget.config
RUN dotnet publish "src/Endpoint/Hello6/Hello6.Domain.Endpoint.csproj" -c Release -o /app --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "Hello6.Domain.Endpoint.dll"]
```



#### Run `dockerbuild.sh`

```bash
export VERSION=$(cat src/Endpoint/Hello6/.version | head -n1)
#   Your docker image's version, ex:
#   - default: the content in the file of ./src/Endpoint/Hello6/.version
#   - or any specific such as 1.0.0.1

export IMAGE_HOST=docker.io/[ACCOUNT-ID]
#   Your Container Registry, ex:
#   - docker.io: docker.io/[ACCOUNT-ID]
#   - or AWS format: [ACCOUNT-ID].dkr.ecr.[REGION].amazonaws.com
#   - or GCP format: gcr.io/[PROJECT-ID]

# bash ./dockerbuild.hello6.sh
#================================================================
# Variables
#================================================================
REPO_NAME=hello6-api
VERSION=${VERSION:-1.0.0.0}
IMAGE_HOST_WITH_TAG=${IMAGE_HOST}/${REPO_NAME}:${VERSION}

#================================================================
# docker commands
#================================================================
docker build . -t $IMAGE_HOST_WITH_TAG -f Dockerfile 
# [+] Building 25.3s (17/17) FINISHED
# => => naming to docker.io/[ACCOUNT-ID]/hello6-api:1.0.0.1

docker push $IMAGE_HOST_WITH_TAG
```



### Minikube deployment  (For Example)

#### Review `deployment.yaml`

- Version: `1.0.0.1`
- Replicas: `2`
- Necessary environment variables
  - **IMAGE_HOST**: ex `docker.io/[ACCOUNT-ID]`
  - **HELLO_HELLODB_CONN**: ex `Data Source=.\\sqlexpress;Initial Catalog=HelloDB;user id=demouser;password=demopwd;Connection Timeout=5`
  - **HELLO_REDIS_CACHE_CONN**: ex `127.0.0.1:6379`

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: hello6
spec:
  replicas: 2
  selector:
    matchLabels:
      app: hello6
  template:
    metadata:
      labels:
        app: hello6
    spec:
      containers:
      - name: hello6-api
        image: ${IMAGE_HOST}/hello6-api:1.0.0.1
        ports:
        - containerPort: 80
        env:
        - name: ASPNETCORE_ENVIRONMENT
          value: Development
        - name: COREFX_DEPLOY_NAME
          value: hello6
        - name: COREFX_API_NAME
          value: hello6-dev
        - name: HELLO_HELLODB_CONN
          value: ${HELLO_HELLODB_CONN}
        - name: HELLO_REDIS_CACHE_CONN
          value: ${HELLO_REDIS_CACHE_CONN}
```



#### Review `service.yaml`

```yaml
apiVersion: v1
kind: Service
metadata:
  name: hello6-svc
  labels:
    app: hello6-svc
spec:
  ports:
  - port: 80
    targetPort: 80
    protocol: TCP
    name: http
  selector:
    app: hello6
```



#### Run `minikube.sh`

```bash
export VERSION=$(cat src/Endpoint/Hello6/.version | head -n1)
#   Your docker image's version, ex:
#   - default: the content in the file of ./src/Endpoint/Hello6/.version
#   - or any specific such as 1.0.0.1

export KUBE_DEPLOY_FILE='minikube/deployment.yaml'
#   Your deployment yaml file, ex:
#   - local folder/path
#   - or git repo/folder/path
#   - or azure devops artifacts/folder/path

# bash ./minikube/minikube.sh
#================================================================
# Variables
#================================================================
REPO_NAME=hello6
KUBE_DEPLOY_FILE='minikube/deployment.yaml'
IMAGE_TAG_REGEX='[0-9]\+.[0-9]\+.[0-9]\+[.-]\(build\)\?[0-9]\+'

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
# | default   | hello6-svc |             | http://127.0.0.1:38311 |
# |-----------|------------|-------------|------------------------|
# http://127.0.0.1:38311
# ❗  Because you are using a Docker driver on linux, the terminal needs to be open to run it.
# ✋  Stopping tunnel for service hello6-svc.
# 
# ❌  Exiting due to SVC_TUNNEL_STOP: stopping ssh tunnel: os: process already finished
# 
# 😿  If the above advice does not help, please let us know:
# 👉  https://github.com/kubernetes/minikube/issues/new/choose

#================================================================
# Expose endpoint
#================================================================
curl http://127.0.0.1:38311/health
# Healthy
```



## HealthChecks

### Set testing base url

```bash
# You can get exposed hello6-svc base url by the above command such as
# minikube service hello6-svc --url
BASE_URL=http://localhost:38311
```



### Is accessable ?

```bash
curl ${BASE_URL}/health
# Healthy
```



### Check service's version

```bash
curl ${BASE_URL}/api/echo/ver
# or curl ${BASE_URL}/api/echo/version

# Expect HTTP 200 application/json response
# {"data":"1.0.0.1","code":1,"msg":"Success","msgId":"abc788a5-298d-45f1-b757-4ab7a413e6e8","isSuccess":true,"subCode":"","subMsg":"","extMap":{}}

curl ${BASE_URL}/api/echo/ver | jq '.data'
# Expect result: "1.0.0.1"
```

 

### Check config exists

```bash
curl ${BASE_URL}/api/echo/config
# Expect HTTP 200 application/json response
# {"data":{"appSettingConfig":true,"helloSettingConfig":false,"logConfig":true},"code":0,"msg":"Error","msgId":"247d492e-0861-471c-8c35-c4a48432e65d","isSuccess":false,"subCode":"","subMsg":"","extMap":{}}

curl ${BASE_URL}/api/echo/config | jq '.data'
# Expect result: 
# {
#   "appSettingConfig": true,
#   "helloSettingConfig": true,
#   "logConfig": true
# }
```



### Check database's connection

- Unhealthy
  - Check your environment variable **HELLO_HELLODB_CONN** before deployment

```bash
curl ${BASE_URL}/api/echo/db
# Expect HTTP 503 application/json response
# {"code":0,"msg":"The ConnectionString property has not been initialized.","msgId":"2b9575aa-82de-449d-92e7-6568ccb02355","isSuccess":false,"subCode":"","subMsg":"","extMap":{}}

curl ${BASE_URL}/api/echo/db | jq '.isSuccess'
# false
```



- Healthy
  - Check your environment variable **HELLO_HELLODB_CONN** before deployment

```bash
curl ${BASE_URL}/api/echo/db
# Expect HTTP 200 application/json response
# {"data":"Microsoft SQL Server 2019 (RTM) - 15.0.2000.5 (X64) \n\tSep 24 2019 13:48:23 \n\tCopyright (C) 2019 Microsoft Corporation\n\tExpress Edition (64-bit) on Windows 10 Pro 10.0 <X64> (Build 19041: ) (Hypervisor)\n","code":1,"msg":"Success","msgId":"6851f5e2-60f0-45e6-b6d3-83f276439587","isSuccess":true,"subCode":"","subMsg":"","extMap":{}}

curl ${BASE_URL}/api/echo/db | jq '.isSuccess'
# true
```



### Check cache's connection

- Unhealthy

  - Check your environment variable **HELLO_REDIS_CACHE_CONN** before deployment

  - Or force stop your cache service `services > memurai > stop`
    > memurai-cli
    > Could not connect to Memurai at 127.0.0.1:6379: Unknown error (10061)
    > not connected>

```bash
curl ${BASE_URL}/api/echo/cache
# Expect HTTP 503 application/json response
# {"code":0,"msg":"It was not possible to connect to the redis server(s). UnableToConnect on 127.0.0.1:6379/Interactive, Initializing/NotStarted, last: NONE, origin: BeginConnectAsync, outstanding: 0, last-read: 2s ago, last-write: 2s ago, keep-alive: 60s, state: Connecting, mgr: 10 of 10 available, last-heartbeat: never, global: 7s ago, v: 2.0.593.37019","msgId":"4431e3c4-8685-449f-991e-45ff05e2e567","isSuccess":false,"subCode":"","subMsg":"","extMap":{}}

curl ${BASE_URL}/api/echo/cache | jq '.isSuccess'
# false
```



- Healthy
  - Check your environment variable **HELLO_REDIS_CACHE_CONN** before deployment

```bash
curl ${BASE_URL}/api/echo/cache
# Expect HTTP 200 application/json response
# {"data":"hello6-debug-Hello6.Domain.Endpoint.Controllers.EchoController__172.18.128.1","code":1,"msg":"Success","msgId":"cb5544c7-069c-4925-bd48-3956ea823dc9","isSuccess":true,"subCode":"","subMsg":"","extMap":{}}

curl ${BASE_URL}/api/echo/cache | jq '.isSuccess'
# true
```



### System information dump

```bash
curl ${BASE_URL}/api/echo/dump
# Expect HTTP 200 application/json response
# 200 {"code":1,"msg":"Success","msgId":"80b1b009-1e42-46c9-92c4-5e232e2938f0","isSuccess":true,"subCode":"","subMsg":"","extMap":{"ver":"1.0.0.1","api-name":"hello6-debug","deploy":"hello6-debug","env":"Debug","_ip":"172.18.128.1","_host":"RDKEVINWU","_os":"Win32NT","_ts":"2020-11-30T05:06:32","_up":"2020-11-30T04:21:17"}}

curl ${BASE_URL}/api/echo/dump | jq '.extMap'
# {
#   "ver": "1.0.0.1",
#   "api-name": "hello6-dev",
#   "deploy": "hello6",
#   "env": "Development",
#   "_ip": "172.17.0.2",
#   "_host": "hello6-6c7d69c76-4wxjr",
#   "_os": "Unix",
#   "_ts": "2020-11-30T09:14:59",
#   "_up": "2020-11-30T07:02:09"
# }
```



