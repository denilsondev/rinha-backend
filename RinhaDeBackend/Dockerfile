# Etapa de Build
FROM mcr.microsoft.com/dotnet/nightly/sdk:9.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /out

# Etapa de Runtime (para API)
FROM mcr.microsoft.com/dotnet/nightly/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "RinhaDeBackend.dll"]

# Etapa de Migrations (SDK necessário)
FROM mcr.microsoft.com/dotnet/nightly/sdk:9.0 AS migrations
WORKDIR /app
COPY . .
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT ["dotnet", "ef"]
