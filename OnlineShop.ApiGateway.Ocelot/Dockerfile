FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OnlineShop.ApiGateway.Ocelot/OnlineShop.ApiGateway.Ocelot.csproj", "OnlineShop.ApiGateway.Ocelot/"]
RUN dotnet restore "./OnlineShop.ApiGateway.Ocelot/./OnlineShop.ApiGateway.Ocelot.csproj"
COPY . .
WORKDIR "/src/OnlineShop.ApiGateway.Ocelot"
RUN dotnet build "./OnlineShop.ApiGateway.Ocelot.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OnlineShop.ApiGateway.Ocelot.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineShop.ApiGateway.Ocelot.dll"]