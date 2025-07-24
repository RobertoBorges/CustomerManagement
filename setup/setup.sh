#!/bin/bash
# Wait for SQL Server to be ready
echo "Waiting for SQL Server to start up..."
sleep 30
echo "Attempting to connect to SQL Server..."

# Run the initialization script
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -i /docker-entrypoint-initdb.d/init.sql
echo "Database initialization completed."
