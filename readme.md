# Product Manager

A distributed-applications lab: .NET Core backend, Angular frontend, Kafka queues, and Prometheus/Grafana metrics, all running in containers to simulate a production environment.

![Architecture](https://github.com/dgmodesto/mangager_products/blob/main/Arquitetura.JPG?raw=true)

## Grafana and Prometheus

```bash
# Prometheus image
docker build -t register-product-prometheus --no-cache -f prometheus.qa.Dockerfile .

# Grafana image
docker build -t register-product-grafana --no-cache -f grafana.qa.Dockerfile .
```

## Microservices

```bash
# API gateway
docker build -t register-product-api-gateway --no-cache -f ./ApiGatewayProduct/Dockerfile .

# Register product service
docker build -t register-product-service --no-cache -f ./src/Api/Dockerfile .

# Register product consumer
docker build -t register-product-consumer --no-cache -f ./src/Consumer/Dockerfile .
```

## Frontend

```bash
docker build -t product-frontend --no-cache .
```

## Running the containers

`docker-compose` also starts Redis, Kafka and ZooKeeper, so the queue-based concepts can be exercised.

```bash
docker-compose up --build
```

> Note: make sure the ports used in this example are not already in use by another application on your machine.
