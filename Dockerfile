# Use the SDK image to build and publish the website
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["src/ROCNSBE.Website.csproj", "."]

RUN dotnet restore "ROCNSBE.Website.csproj"

COPY src/ .

RUN dotnet publish "ROCNSBE.Website.csproj" -c Release -o /app/publish

# Copy the published output to the final running image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80

ENV ASPNETCORE_URLS=http://*:80


ENTRYPOINT ["dotnet", "ROCNSBE.Website.dll"]
