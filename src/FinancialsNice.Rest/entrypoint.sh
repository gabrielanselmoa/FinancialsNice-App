#!/bin/bash
set -e

# Aguarda o PostgreSQL estar pronto
wait_for_postgres() {
    echo "⏳ Aguardando o PostgreSQL iniciar..."
    until PGPASSWORD=postgres psql -h psql-db -U postgres -d FinNiceDB -c '\q' 2>/dev/null; do
        echo "PostgreSQL ainda não está disponível. Aguardando..."
        sleep 1
    done
    echo "✅ PostgreSQL está pronto."
}

# Executar EF Core migrations
#run_migrations() {
#    echo "⚙️ Executando migrations com EF Core..."
#    dotnet ef database update \
#        --project FinancialsNice.Infrastructure \
#        --startup-project FinancialsNice.Rest
#}

# Execução principal
wait_for_postgres
#run_migrations

# Inicia a aplicação
echo "🚀 Iniciando a aplicação..."
exec dotnet FinancialsNice.Rest.dll
