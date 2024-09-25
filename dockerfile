# Use the official .NET SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app


# Copy the remaining files and build the project
COPY ./src ./
RUN dotnet publish jaranilla_09252024/*.csproj -c Debug -o out

# Use the official .NET Runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=80
WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_ENVIRONMENT=Development

# Expose the port the app runs on
EXPOSE 80


# Set the entry point for the container
ENTRYPOINT ["dotnet", "jaranilla_09252024.dll"]
