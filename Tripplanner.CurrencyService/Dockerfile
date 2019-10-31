FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["Tripplanner.CurrencyService/Tripplanner.CurrencyService.csproj", "Tripplanner.CurrencyService/"]
RUN dotnet restore "Tripplanner.CurrencyService/Tripplanner.CurrencyService.csproj"
COPY . .
WORKDIR "/src/Tripplanner.CurrencyService"
RUN dotnet build "Tripplanner.CurrencyService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Tripplanner.CurrencyService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Tripplanner.CurrencyService.dll"]