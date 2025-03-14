#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Data.Microservice.API/Data.Microservice.API.csproj", "Data.Microservice.API/"]
COPY ["Data.Microservice.App/Data.Microservice.App.csproj", "Data.Microservice.App/"]
COPY ["Data.Microservice/Data.Microservice.Domain.csproj", "Data.Microservice/"]
COPY ["Data.Microservice.Infrastructure/Data.Microservice.Infrastructure.csproj", "Data.Microservice.Infrastructure/"]
RUN dotnet restore "Data.Microservice.API/Data.Microservice.API.csproj"
COPY . .
WORKDIR "/src/Data.Microservice.API"
RUN dotnet build "Data.Microservice.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Data.Microservice.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Data.Microservice.API.dll"]