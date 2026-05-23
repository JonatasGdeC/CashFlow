FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env

WORKDIR /app

COPY src/ .

WORKDIR /app/CashFlow.API

RUN dotnet restore

RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=build-env /app/out .

ENTRYPOINJonatas@2003
T ["dotnet", "CashF low.API.dll"]