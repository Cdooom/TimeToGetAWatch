
FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app

COPY . ./TimeToGetAWatch
WORKDIR /app/TimeToGetAWatch
RUN dotnet publish "TimeToGetAWatch.csproj" -c Release -o out

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/TimeToGetAWatch/out ./
ENTRYPOINT ["dotnet", "TimeToGetAWatch.dll"]
EXPOSE 80