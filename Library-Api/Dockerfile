# Etapa 1 - build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia arquivos de projeto
COPY ../LibraryApi.sln .
COPY ../Library-Api/Library-Api.csproj ./Library-Api/
COPY ../Library-Application/Library-Application.csproj ./Library-Application/
COPY ../Library-Domain/Library-Domain.csproj ./Library-Domain/
COPY ../Library-Infra/Library-Infra.csproj ./Library-Infra/

# Restaura dependências
RUN dotnet restore

# Copia o restante dos arquivos
COPY .. .

# Publica o projeto principal
WORKDIR /src/Library-Api
RUN dotnet publish -c Release -o /app/publish

# Etapa 2 - runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Library-Api.dll"]
