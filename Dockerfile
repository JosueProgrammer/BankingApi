# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files first for layer caching
COPY BankingApi.slnx ./
COPY Banking.Api/Banking.Api.csproj Banking.Api/
COPY Banking.Application/Banking.Application.csproj Banking.Application/
COPY Banking.Domain/Banking.Domain.csproj Banking.Domain/
COPY Banking.Infrastructure/Banking.Infrastructure.csproj Banking.Infrastructure/

# Restore dependencies
RUN dotnet restore Banking.Api/Banking.Api.csproj

# Copy all source code
COPY . .

# Publish Banking.Api
RUN dotnet publish Banking.Api/Banking.Api.csproj -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Create non-root user for security
RUN groupadd -r appgroup && useradd -r -g appgroup appuser

# Copy published output
COPY --from=build /app/publish .

# Set ownership
RUN chown -R appuser:appgroup /app
USER appuser

# Expose port
EXPOSE 8080

# Set environment
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Banking.Api.dll"]
