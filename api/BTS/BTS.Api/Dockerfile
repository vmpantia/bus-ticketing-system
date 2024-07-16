#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BTS.Api/BTS.Api.csproj", "BTS.Api/"]
COPY ["BTS.Core/BTS.Core.csproj", "BTS.Core/"]
COPY ["BTS.Domain/BTS.Domain.csproj", "BTS.Domain/"]
COPY ["BTS.Infrastructure/BTS.Infrastructure.csproj", "BTS.Infrastructure/"]
RUN dotnet restore "./BTS.Api/BTS.Api.csproj"
COPY . .
WORKDIR "/src/BTS.Api"
RUN dotnet build "./BTS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BTS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BTS.Api.dll"]