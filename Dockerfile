FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BdRegionalDataApi/BdRegionalDataApi.csproj", "BdRegionalDataApi/"]
RUN dotnet restore "BdRegionalDataApi/BdRegionalDataApi.csproj"
COPY . .
WORKDIR "/src/BdRegionalDataApi"
RUN dotnet build "BdRegionalDataApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BdRegionalDataApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BdRegionalDataApi.dll"]
