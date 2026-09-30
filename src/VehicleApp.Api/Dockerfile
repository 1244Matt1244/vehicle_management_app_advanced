FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["src/VehicleApp.Api/VehicleApp.Api.csproj", "src/VehicleApp.Api/"]
COPY ["src/VehicleApp.Application/VehicleApp.Application.csproj", "src/VehicleApp.Application/"]
COPY ["src/VehicleApp.Domain/VehicleApp.Domain.csproj", "src/VehicleApp.Domain/"]
COPY ["src/VehicleApp.Infrastructure/VehicleApp.Infrastructure.csproj", "src/VehicleApp.Infrastructure/"]
RUN dotnet restore "src/VehicleApp.Api/VehicleApp.Api.csproj"

COPY . .
RUN dotnet publish "src/VehicleApp.Api/VehicleApp.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "VehicleApp.Api.dll"]
