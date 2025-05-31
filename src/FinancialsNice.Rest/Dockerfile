# ---------- Build stage ----------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 0) Copia o .env da raiz da solução  ➜  /src/.env
COPY .env .

# 1) Copia só os .csproj (sem duplicar /src)
COPY src/FinancialsNice.Rest/FinancialsNice.Rest.csproj               ./FinancialsNice.Rest/
COPY src/FinancialsNice.Application/FinancialsNice.Application.csproj ./FinancialsNice.Application/
COPY src/FinancialsNice.Domain/FinancialsNice.Domain.csproj           ./FinancialsNice.Domain/
COPY src/FinancialsNice.Infrastructure/FinancialsNice.Infrastructure.csproj ./FinancialsNice.Infrastructure/

RUN dotnet restore ./FinancialsNice.Rest/FinancialsNice.Rest.csproj

# 2) Copia todo o código
COPY src/ .

# 3) Build & publish
RUN dotnet build   ./FinancialsNice.Rest/FinancialsNice.Rest.csproj -c Release -o /app/build
RUN dotnet publish ./FinancialsNice.Rest/FinancialsNice.Rest.csproj -c Release -o /app/publish

# 4) Instala ferramenta EF Core
RUN dotnet tool install --global dotnet-ef --version 8.0.5
ENV PATH="$PATH:/root/.dotnet/tools"

# ---------- Runtime stage --------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Ferramentas extras
RUN apt-get update \
 && apt-get install -y postgresql-client dos2unix \
 && rm -rf /var/lib/apt/lists/*

COPY src/FinancialsNice.Rest/entrypoint.sh .
RUN dos2unix entrypoint.sh && chmod +x entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]
