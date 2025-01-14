# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY ./ ./

RUN dotnet restore /source/src/src.sln

# copy everything else and build app
COPY ./ ./
WORKDIR /source/src/
RUN dotnet publish /source/src/External/Presentation/BookShop.API/BookShop.API.csproj -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "BookShop.API.dll"]