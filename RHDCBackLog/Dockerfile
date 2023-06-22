
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /RHDC
COPY ["RHDCCleaner/RHDCCleaner.csproj", "/"]
COPY ["Infrastructure/Infrastructure.csproj", "/Infrastructure/"]
COPY ["Core/Core.csproj", "/Core/"]


RUN dotnet restore "/RHDCCleaner.csproj"
COPY . .
WORKDIR /RHDC
RUN dotnet build "RHDCCleaner/RHDCCleaner.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RHDCCleaner/RHDCCleaner.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RHDCCleaner.dll"]