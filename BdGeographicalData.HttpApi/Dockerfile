FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BdGeographicalData.HttpApi/BdGeographicalData.HttpApi.csproj", "BdGeographicalData.HttpApi/"]
RUN dotnet restore "BdGeographicalData.HttpApi/BdGeographicalData.HttpApi.csproj"
COPY . .
WORKDIR "/src/BdGeographicalData.HttpApi"
RUN dotnet build "BdGeographicalData.HttpApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BdGeographicalData.HttpApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BdGeographicalData.HttpApi.dll"]
