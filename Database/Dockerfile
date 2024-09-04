FROM postgres:15


ENV POSTGRES_DB=mobitdb
ENV POSTGRES_USER=mobituser
ENV POSTGRES_PASSWORD=mobitpass

COPY init.sql /docker-entrypoint-initdb.d/

EXPOSE 5432
