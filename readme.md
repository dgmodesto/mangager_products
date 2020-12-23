# Projeto Gerenciador de Produtos

![alt text](https://github.com/dgmodesto/mangager_products/blob/main/Arquitetura.JPG?raw=true)
  
## Grafana and Prometheus

#### Create a image for prometheus
  - docker build -t register-product-prometheus --no-cache -f prometheus.qa.Dockerfile .

#### Create a image for grafana
  - docker build -t register-product-grafana --no-cache -f grafana.qa.Dockerfile .


## Microservices

#### Create a image for register product api gateway
  - docker build -t register-product-api-gateway --no-cache -f ./ApiGatewayProduct/Dockerfile .


#### Create a image for register product service
  - docker build -t register-product-service --no-cache -f ./src/Api/Dockerfile .

#### Create a image for register product consumer
  - docker build -t register-product-consumer --no-cache -f ./src/Consumer/Dockerfile .

## Frontend

#### Create a image for product frontend
  - docker build -t product-frontend --no-cache .



## Run Images in containers
  #### dentro do docker-compose, iremos subir a imagem do redis, kafka e do zookeper para trabalharmos com o conceito de filas.
  - docker-compose up --build

##### ps: verifique se as portas utilizadas neste exemplos já estão sendo utilizadas por outra aplicação em seu computador.
