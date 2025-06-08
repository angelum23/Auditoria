# Guia de Desenvolvimento Local

Este documento fornece instruções para configurar, implantar, iniciar e desmontar o ambiente de desenvolvimento utilizando Docker Compose e comandos `make`.

## Pré-requisitos

Certifique-se de que você tem as seguintes ferramentas instaladas:

- [Docker](https://docs.docker.com/get-docker/)
- [Docker Compose](https://docs.docker.com/compose/install/)
- [Make](https://www.gnu.org/software/make/)

## 1. Docker Compose

Docker Compose é uma ferramenta para definir e gerenciar multi-containers Docker. Ele utiliza um arquivo YAML (`docker-compose.yml`) para configurar os serviços, redes e volumes necessários para o ambiente de desenvolvimento. Com Docker Compose, você pode iniciar e parar todos os contêineres de forma coordenada com um único comando.

Subir ambiente local: `docker compose up`

## 2. Configuração, Deploy e Início do Ambiente de Desenvolvimento com Kind

Para configurar, implantar e iniciar o ambiente de desenvolvimento, execute o seguinte comando:

* Cria o cluster:
```bash
make setup-dev
```
* Build do container e faz deploy no cluster:
```bash
make deploy-dev
```

* Destruir o Cluster
```bash
make teardown-dev
```

## 3. Executar container da aplicação
```bash
docker buildx build -t auditoria:latest -f Dockerfile .

docker run \
--name auditoria-container \
--rm \
-p 8080:8080 \
-e "ConnectionStrings__Conexao=Server=HOST;Port=6432;Database=sandbox;User Id=sa_sandbox;Password=DBPASSWORD;Enlist=true;ApplicationName=Auditoria;" \
-e "ConnectionStrings__Replica=Server=HOST;Port=6432;Database=sandbox;User Id=sa_sandbox;Password=DBPASSWORD;Enlist=true;ApplicationName=Auditoria;" \
-e "ConnectionStrings__Mongo=mongodb://mongodb:27017/Auditoria" \
auditoria:latest
```