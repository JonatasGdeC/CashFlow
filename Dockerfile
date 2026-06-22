FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore src/Backend/CashFlow.Api/CashFlow.Api.csproj
RUN dotnet publish src/Backend/CashFlow.Api/CashFlow.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE=false

ENTRYPOINT ["dotnet", "CashFlow.Api.dll"]
