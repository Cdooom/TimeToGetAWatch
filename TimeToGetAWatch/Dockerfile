
FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app

COPY . ./TimeToGetAWatch
WORKDIR /app/TimeToGetAWatch
RUN dotnet publish "TimeToGetAWatch.csproj" -c Release -o out

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
ENV TZ=America/Los_Angeles
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
WORKDIR /app
COPY --from=build /app/TimeToGetAWatch/out ./
ENTRYPOINT ["dotnet", "TimeToGetAWatch.dll"]
EXPOSE 80