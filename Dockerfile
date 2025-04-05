# Usa una imagen base de ASP.NET 8.0 para la fase final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 7154
EXPOSE 5084

# ------------------------------------------------------------
# Fase de compilación
# ------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY . .

RUN dotnet restore "API/GestorFacturas.API.csproj"

RUN dotnet build "API/GestorFacturas.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ------------------------------------------------------------
# Fase de publicación
# ------------------------------------------------------------
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "API/GestorFacturas.API.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false

# ------------------------------------------------------------
# Fase final
# ------------------------------------------------------------
FROM base AS final
WORKDIR /app

# Copiamos los archivos publicados desde la fase 'publish'
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:5084

ENTRYPOINT ["dotnet", "GestorFacturas.API.dll"]
