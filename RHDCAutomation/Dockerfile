#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Race-Horse-Data-Capture/RHDCAutomation/RHDCFetchAutomator.csproj", "Race-Horse-Data-Capture/RHDCAutomation/]
COPY ["Race-Horse-Data-Capture/Infrastructure/Race-Horse-Data-Capture.Infrastructure.csproj", "Race-Horse-Data-Capture/Infrastructure/"]
COPY ["Race-Horse-Data-Capture/Core/Race-Horse-Data-Capture.Core.csproj", "src/Race-Horse-Data-Capture/Core/"]

RUN dotnet restore "Race-Horse-Data-Capture/RHDCAutomation/RHDCAutomation.csproj" --configfile NuGet.Config && rm NuGet.Config
COPY . .
WORKDIR "Race-Horse-Data-Capture/RHDCAutomation"
RUN dotnet build "RHDCAutomation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RHDCAutomation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RHDCAutomation.dll"]