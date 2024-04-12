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

COPY --from=build /src/entrypoint.sh /run/rocnsbe/entrypoint.sh

RUN chmod +x "/run/rocnsbe/entrypoint.sh"

EXPOSE 80

ENV ASPNETCORE_URLS=http://*:80
ENV ASPNETCORE_ENVIRONMENT=Production


ENTRYPOINT /run/rocnsbe/entrypoint.sh
