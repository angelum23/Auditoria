FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
RUN apt update && apt install curl -y
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Auditoria.Api/Auditoria.Api.csproj", "Auditoria.Api/"]
RUN dotnet restore "Auditoria.Api/Auditoria.Api.csproj"
COPY . .
WORKDIR "/src/Auditoria.Api"
RUN dotnet build "Auditoria.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Auditoria.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auditoria.Api.dll"]
