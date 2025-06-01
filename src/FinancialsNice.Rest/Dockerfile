# ---------- Build stage ----------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1) Copy only the .csproj files (to avoid duplicating /src)
COPY src/FinancialsNice.Rest/FinancialsNice.Rest.csproj               ./FinancialsNice.Rest/
COPY src/FinancialsNice.Application/FinancialsNice.Application.csproj ./FinancialsNice.Application/
COPY src/FinancialsNice.Domain/FinancialsNice.Domain.csproj           ./FinancialsNice.Domain/
COPY src/FinancialsNice.Infrastructure/FinancialsNice.Infrastructure.csproj ./FinancialsNice.Infrastructure/

# Restore project dependencies
RUN dotnet restore ./FinancialsNice.Rest/FinancialsNice.Rest.csproj

# 2) Copy all source code
COPY src/ .

# 3) Build and publish the application
RUN dotnet build   ./FinancialsNice.Rest/FinancialsNice.Rest.csproj -c Release -o /app/build
RUN dotnet publish ./FinancialsNice.Rest/FinancialsNice.Rest.csproj -c Release -o /app/publish

# 4) Install EF Core CLI tools
RUN dotnet tool install --global dotnet-ef --version 8.0.5
ENV PATH="$PATH:/root/.dotnet/tools"

# ---------- Runtime stage --------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published app from the build stage
COPY --from=build /app/publish .

# Install extra tools (PostgreSQL client, dos2unix)
RUN apt-get update \
 && apt-get install -y postgresql-client dos2unix \
 && rm -rf /var/lib/apt/lists/*

# Copy and prepare the entrypoint script
COPY src/FinancialsNice.Rest/entrypoint.sh .
RUN dos2unix entrypoint.sh && chmod +x entrypoint.sh

# Set entrypoint
ENTRYPOINT ["./entrypoint.sh"]
