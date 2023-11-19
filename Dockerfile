FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
LABEL name=bd-geographical-data-api
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . /src
RUN dotnet restore "BdGeographicalData.csproj"
RUN dotnet build "BdGeographicalData.csproj" -c Release -o /app/build

FROM build AS publish
COPY ./app.db .
RUN dotnet publish "BdGeographicalData.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY ./app.db .
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BdGeographicalData.dll"]
