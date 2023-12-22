# #!/bin/bash
set -e
# export PGPASSWORD=$POSTGRES_PASSWORD;

echo "SqlInit"
psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
	CREATE USER docker;
	CREATE DATABASE docker;
	GRANT ALL PRIVILEGES ON DATABASE docker TO docker;
EOSQL
# createuser -P $POSTGRES_USER
# createdb -U $POSTGRES_USER $APP_DB_NAME
# # echo "Script"
# psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$APP_DB_NAME" \
# -f /tmp/database/tables/NovaTabela.sql \