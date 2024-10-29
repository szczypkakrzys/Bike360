#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/API/Bike360.Api/Bike360.Api.csproj", "src/API/Bike360.Api/"]
COPY ["src/Core/Bike360.Application/Bike360.Application.csproj", "src/Core/Bike360.Application/"]
COPY ["src/Core/Bike360.Domain/Bike360.Domain.csproj", "src/Core/Bike360.Domain/"]
COPY ["src/Infrastructure/Bike360.Infrastructure/Bike360.Infrastructure.csproj", "src/Infrastructure/Bike360.Infrastructure/"]
COPY ["src/Infrastructure/Bike360.Persistence/Bike360.Persistence.csproj", "src/Infrastructure/Bike360.Persistence/"]
RUN dotnet restore "./src/API/Bike360.Api/./Bike360.Api.csproj"
COPY . .
WORKDIR "/src/src/API/Bike360.Api"
RUN dotnet build "./Bike360.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Bike360.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bike360.Api.dll"]