
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["TimeToGetAWatch/TimeToGetAWatch.csproj", "TimeToGetAWatch/"]
RUN dotnet restore "TimeToGetAWatch/TimeToGetAWatch.csproj"
COPY . .
WORKDIR "/src/TimeToGetAWatch"
RUN dotnet build "TimeToGetAWatch.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TimeToGetAWatch.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TimeToGetAWatch.dll"]