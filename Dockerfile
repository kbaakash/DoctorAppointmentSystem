# ── Stage 1: Publish ────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files first (for layer caching)
COPY DoctorAppointmentSystem.Domain/DoctorAppointmentSystem.Domain.csproj             DoctorAppointmentSystem.Domain/
COPY DoctorAppointmentSystem.Application/DoctorAppointmentSystem.Application.csproj   DoctorAppointmentSystem.Application/
COPY DoctorAppointmentSystem.Infrastructure/DoctorAppointmentSystem.Infrastructure.csproj DoctorAppointmentSystem.Infrastructure/
COPY DoctorAppointmentSystem.WebAPI/DoctorAppointmentSystem.WebAPI.csproj             DoctorAppointmentSystem.WebAPI/

# Copy the rest of the source code
COPY . .

# Clear Windows-generated cache files
RUN find . -name "project.assets.json" -delete && \
    find . -name "*.nuget.dgspec.json" -delete && \
    find . -name "*.nuget.g.props" -delete && \
    find . -name "*.nuget.g.targets" -delete && \
    find . -name "project.nuget.cache" -delete

# Restore + Build + Publish in one clean step
RUN dotnet publish DoctorAppointmentSystem.WebAPI/DoctorAppointmentSystem.WebAPI.csproj \
    -c Release \
    -o /app/publish

# ── Stage 2: Runtime ────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Create a non-root user for security
RUN adduser --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

# Copy published output
COPY --from=build /app/publish .

# Expose port
EXPOSE 8080

# Set environment
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "DoctorAppointmentSystem.WebAPI.dll"]
