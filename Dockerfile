FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /build
COPY . .
RUN bash -c 'cat nuget.xml > ./nuget.config'

# --------------------------
# COPY Dependency Files
# --------------------------
COPY data/ src/Endpoint/Hello6/App_Data/

# --------------------------
# Build & Publish
# --------------------------
RUN dotnet restore "src/Endpoint/Hello6/Hello6.Domain.Endpoint.csproj" --configfile ./nuget.config
RUN dotnet publish "src/Endpoint/Hello6/Hello6.Domain.Endpoint.csproj" -c Release -o /app --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app .
COPY --from=build /app/App_Data/openssl.modified.cnf.txt /etc/ssl/openssl.cnf

ENTRYPOINT ["dotnet", "Hello6.Domain.Endpoint.dll"]

