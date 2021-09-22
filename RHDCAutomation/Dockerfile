##﻿See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /Race-Horse-Data-Capture
COPY ["RHDCAutomation/RHDCAutomation.csproj", "/RHDCAutomation/"]

COPY ["Infrastructure/Infrastructure.csproj", "/Infrastructure/"]
COPY ["Core/Core.csproj", "/Core/"]
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "RHDCAutomation.dll"]