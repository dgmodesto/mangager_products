FROM prom/prometheus:v2.1.0 as register-product-prometheus
COPY ./Qa/prometheus/prometheus.yml /etc/prometheus

# Volumes (Host/Container)
VOLUME ./Qa/prometheus/data /prometheus
EXPOSE 9090
