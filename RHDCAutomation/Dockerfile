##﻿See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /Race-Horse-Data-Capture
COPY ["RHDCAutomation/RHDCAutomation.csproj", "/"]
COPY ["Infrastructure/Infrastructure.csproj", "/Infrastructure/"]
COPY ["Core/Core.csproj", "/Core/"]


RUN dotnet restore "/RHDCAutomation.csproj"
COPY . .
WORKDIR /Race-Horse-Data-Capture
RUN dotnet build "//RHDCAutomation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "/RHDCAutomation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=base /app/publish .
ENTRYPOINT ["dotnet", "RHDCAutomation.dll"]