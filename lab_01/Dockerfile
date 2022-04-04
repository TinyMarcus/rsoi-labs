FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /application

# Copy csproj and restore as distinct layers
COPY src/ ./
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o rsoiHttpServer

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /application
COPY --from=build-env /application/rsoiHttpServer .
ENTRYPOINT ["dotnet", "rsoi.HttpServer.dll"]
