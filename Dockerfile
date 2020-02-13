## Base Image
FROM stivik/altv:beta as runner



## Builder Server
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as builder_server
WORKDIR /app

# Restore nuget packages and build binaries
RUN mkdir -p server
ADD server/Exo.Rp.Core                      server/Exo.Rp.Core
ADD server/Exo.Rp.Models                    server/Exo.Rp.Models 
ADD server/Exo.Rp.Serialization             server/Exo.Rp.Serialization
RUN dotnet restore                          server/Exo.Rp.Core
RUN dotnet build --no-restore -c Release    server/Exo.Rp.Core -o /app/bin

## Builder Client
FROM node as builder_client
WORKDIR /app

# Add client files
RUN mkdir -p client
ADD client/cef          client/cef/
ADD client/src          client/src/
ADD client/*.cfg        client/
ADD client/*.json       client/

# Install typescript and compile project
WORKDIR /app/client
RUN npm install -g typescript
RUN npm install
RUN npm run build && \
    npm run clean



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
RUN mkdir -p resources/exov/ && \
    mkdir -p resources/exov-client/
COPY --from=builder_server  /app/bin                resources/exov/
COPY --from=configpatcher   /config/config.json     resources/exov/
COPY --from=builder_client  /app/client             resources/exov-client/

# Cleanup some files
RUN rm resources/exov/*.runtimeconfig.dev.json

# Add server config
ADD build/server.cfg config/

# Overwrite entrypoint with our own
#RUN mv _build/entrypoint.sh entrypoint.sh
#RUN chmod +x entrypoint.sh

# Entrypoint
ENTRYPOINT ["/bin/sh", "entrypoint.sh"]