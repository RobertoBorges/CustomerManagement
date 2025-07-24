#!/bin/bash
set -e

# Make sure script is executable
chmod +x "$0"

# Wait for SQL Server to be ready
for i in {1..50}; do
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "SELECT 1" &>/dev/null
    if [ $? -eq 0 ]; then
        echo "SQL Server is ready"
        break
    fi
    echo "Waiting for SQL Server to start... (attempt $i)"
    sleep 1
done

echo "Importing customer data..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -i /docker-entrypoint-initdb.d/init.sql

echo "Import completed successfully"
