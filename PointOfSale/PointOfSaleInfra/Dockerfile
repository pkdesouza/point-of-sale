FROM microsoft/mssql-server-linux:latest
RUN mkdir -p /opt/mssql-scripts
COPY ./PointOfSaleInfra/script.sql /opt/mssql-scripts
ENV ACCEPT_EULA Y
ENV sa_password P@sswo0RD!