# Use the SDK image to build and publish the website
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ROCNSBE.Website.csproj", "."]
RUN dotnet restore "ROCNSBE.Website.csproj"
COPY . .
RUN dotnet publish "ROCNSBE.Website.csproj" -c Release -o /app/publish

# Copy the published output to the final running image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ROCNSBE.Website.dll"]
