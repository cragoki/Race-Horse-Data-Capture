﻿
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /RHDC
COPY ["RHDCAutomation/RHDCAutomation.csproj", "/"]
COPY ["Infrastructure/Infrastructure.csproj", "/Infrastructure/"]
COPY ["Core/Core.csproj", "/Core/"]


RUN dotnet restore "/RHDCAutomation.csproj"
COPY . .
WORKDIR /RHDC
RUN dotnet build "RHDCAutomation/RHDCAutomation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RHDCAutomation/RHDCAutomation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RHDCAutomation.dll"]