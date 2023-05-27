FROM mcr.microsoft.com/dotnet/sdk AS build
WORKDIR /src
COPY . .
RUN dotnet restore && dotnet publish -c Release -o publish
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS prod
WORKDIR /app
RUN mkdir -p /var/sqlite # For the SQLite DB file
COPY --from=build /src/publish .
EXPOSE 5005
ENTRYPOINT ["dotnet", "RouteServiceApp.API.dll"]