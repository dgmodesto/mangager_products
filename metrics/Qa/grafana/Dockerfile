FROM grafana/grafana:7.3.1 as ap-grafana
COPY  ./grafana/config/grafana.ini /etc/grafana 
COPY  ./grafana/provisioning /etc/grafana/provisioning
COPY  ./grafana/dashboards /var/lib/grafana/dashboards
EXPOSE 3000
