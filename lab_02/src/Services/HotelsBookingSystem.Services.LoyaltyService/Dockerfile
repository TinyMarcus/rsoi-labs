FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /application

# Copy csproj and restore as distinct layers
COPY ./ ./
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o LoyaltyService

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /application
COPY --from=build-env /application/LoyaltyService .
ENTRYPOINT ["dotnet", "HotelsBookingSystem.Services.LoyaltyService.dll"]
