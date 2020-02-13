## Base Image
FROM stivik/altv:beta as runner

## Builder
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as builder
WORKDIR /app

# Restore nuget packages and build binaries
RUN mkdir -p server
ADD server/Exo.Rp.Core                      server/Exo.Rp.Core
ADD server/Exo.Rp.Models                    server/Exo.Rp.Models 
ADD server/Exo.Rp.Serialization             server/Exo.Rp.Serialization
RUN dotnet restore                          server/Exo.Rp.Core
RUN dotnet build --no-restore -c Release    server/Exo.Rp.Core -o /app/bin

## Client
# TODO

## Config Patcher
FROM alpine as configpatcher
WORKDIR /config

# Install jq
RUN apk add jq

# Add and patch config
ARG SENTRY_DSN="https://invalid"
ARG SENTRY_ENVIRONMENT="invalid"
ARG SENTRY_RELEASE="invalid"

ADD build/config.json.example config.unpatched.json
RUN cat config.unpatched.json | \
    # Patch Sentry section
    jq --arg data "$SENTRY_DSN" '.Sentry.DSN |= $data' | \
    jq --arg data "$SENTRY_ENVIRONMENT" '.Sentry.Environment |= $data' | \
    jq --arg data "$SENTRY_RELEASE" '.Sentry.Release |= $data' | \
    cat >> config.json

## Runner
FROM runner

# Add binaries
RUN mkdir -p resources/exov/
COPY --from=builder         /app/bin                resources/exov/
COPY --from=configpatcher   /config/config.json     resources/exov/
RUN rm resources/exov/*.runtimeconfig.dev.json

# Add server config
RUN rm config/server.cfg
ADD build/server.cfg config/

# Add client resources
# TODO

# Overwrite entrypoint with our own
#RUN mv _build/entrypoint.sh entrypoint.sh
#RUN chmod +x entrypoint.sh

# Entrypoint
ENTRYPOINT ["/bin/sh", "entrypoint.sh"]