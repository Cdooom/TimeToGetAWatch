
FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app

COPY . ./ItsTestingTime
WORKDIR /app/ItsTestingTime
RUN dotnet publish "ItsTestingTime.csproj" -c Release -o out

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/ItsTestingTime/out ./
ENTRYPOINT ["dotnet", "ItsTestingTime.dll"]
EXPOSE 80