FROM postgres

ENV POSTGRES_PASSWORD=mysecretpassword
# ENV POSTGRES_USER=${POSTGRES_USER}
# ENV POSTGRES_PASSWORD=${POSTGRES_PASSWORD}
# ENV APP_DB_NAME=${POSTGRES_DB}

# COPY --chown=postgres:postgres ./postgres/database/ /tmp/database/
COPY --chown=postgres:postgres ./postgres/scripts/sqlinit.sh /docker-entrypoint-initdb.d/