# Etapa de Build
FROM mcr.microsoft.com/dotnet/nightly/sdk:9.0 AS build
WORKDIR /src

# Copiar apenas os arquivos necessários para restaurar dependências
COPY RinhaDeBackend.sln ./
COPY RinhaDeBackend/*.csproj RinhaDeBackend/
COPY RinhaDeBackend.Application/*.csproj RinhaDeBackend.Application/
COPY RinhaDeBackend.Infrastructure/*.csproj RinhaDeBackend.Infrastructure/
COPY RinhaDeBackend.Domain/*.csproj RinhaDeBackend.Domain/
COPY RinhaDeBackend.Tests/*.csproj RinhaDeBackend.Tests/

# Restaurar dependências
RUN dotnet restore

# Copiar o restante dos arquivos
COPY . .

# Publicar o projeto
RUN dotnet publish RinhaDeBackend/RinhaDeBackend.API.csproj -c Release -o /out

# Etapa de Runtime
FROM mcr.microsoft.com/dotnet/nightly/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "RinhaDeBackend.API.dll"]

# Etapa de Migrations
FROM mcr.microsoft.com/dotnet/nightly/sdk:9.0 AS migrations
WORKDIR /src
COPY --from=build /src .
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT ["dotnet", "ef"]
