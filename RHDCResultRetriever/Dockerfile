
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /RHDC
COPY ["RHDCResultRetriever/RHDCResultRetriever.csproj", "/"]
COPY ["Infrastructure/Infrastructure.csproj", "/Infrastructure/"]
COPY ["Core/Core.csproj", "/Core/"]


RUN dotnet restore "/RHDCResultRetriever.csproj"
COPY . .
WORKDIR /RHDC
RUN dotnet build "RHDCResultRetriever/RHDCResultRetriever.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RHDCResultRetriever/RHDCResultRetriever.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RHDCResultRetriever.dll"]