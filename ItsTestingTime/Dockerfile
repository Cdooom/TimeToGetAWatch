
FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app

COPY . ./ItsTestingTime
WORKDIR /app/ItsTestingTime
RUN dotnet publish "ItsTestingTime.csproj" -c Release -o out

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
ENV TZ=America/Los_Angeles
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
WORKDIR /app
COPY --from=build /app/ItsTestingTime/out ./
ENTRYPOINT ["dotnet", "ItsTestingTime.dll"]
EXPOSE 80