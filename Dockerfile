## Config Patcher
FROM alpine as configpatcher
WORKDIR /config

# Install jq
RUN apk add jq

# Add and patch config
ARG DSN="https://invalid"
ARG SENTRY_ENVIRONMENT="invalid"
ARG SENTRY_RELEASE="invalid"

ADD config.json.example config.unpatched.json
RUN cat config.unpatched.json | \
    jq --arg data "$DSN" '.Sentry.DSN |= $data' | \
    jq --arg data "$SENTRY_ENVIRONMENT" '.Sentry.Environment |= $data' | \
    jq --arg data "$RELEASENTRY_RELEASESE" '.Sentry.Release |= $data' >> config.json

## Builder
FROM mcr.microsoft.com/dotnet/core/sdk:3.0 as builder
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

## Runner
FROM stivik/altv:stable

# Add binaries
COPY --from=builder         /app/bin                _build/
COPY --from=configpatcher   /config/config.json     _build/
RUN rm _build/*.runtimeconfig.dev.json
RUN cat _build/config.json

# Add client resources
# TODO

# Overwrite entrypoint with our own
#RUN mv _build/entrypoint.sh entrypoint.sh
#RUN chmod +x entrypoint.sh

# Entrypoint
ENTRYPOINT ["/bin/sh", "entrypoint.sh"]