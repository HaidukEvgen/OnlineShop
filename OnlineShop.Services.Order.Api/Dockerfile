#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OnlineShop.Services.Order.Api/OnlineShop.Services.Order.Api.csproj", "OnlineShop.Services.Order.Api/"]
COPY ["OnlineShop.Services.Order.BusinessLayer/OnlineShop.Services.Order.BusinessLayer.csproj", "OnlineShop.Services.Order.BusinessLayer/"]
COPY ["OnlineShop.Services.Order.DataLayer/OnlineShop.Services.Order.DataLayer.csproj", "OnlineShop.Services.Order.DataLayer/"]
RUN dotnet restore "./OnlineShop.Services.Order.Api/./OnlineShop.Services.Order.Api.csproj"
COPY . .
WORKDIR "/src/OnlineShop.Services.Order.Api"
RUN dotnet build "./OnlineShop.Services.Order.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OnlineShop.Services.Order.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineShop.Services.Order.Api.dll"]