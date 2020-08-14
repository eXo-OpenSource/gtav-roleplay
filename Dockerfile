## Base Image
FROM eisengrind/altv-server:1280-js-dotnet as runner


## Builder Server
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as builder_server
WORKDIR /app

# Restore nuget packages and build binaries
RUN mkdir -p server
ADD server/Exo.Rp.Core          server/Exo.Rp.Core
ADD server/Exo.Rp.Models        server/Exo.Rp.Models
ADD server/Exo.Rp.Serialization server/Exo.Rp.Serialization
ADD server/Exo.Rp.Sdk           server/Exo.Rp.Sdk
RUN dotnet restore              server/Exo.Rp.Core
RUN dotnet build                server/Exo.Rp.Core \
    --no-restore \
    --runtime linux-x64 \
    -c Release \
    -o /app/bin

## Builder Client
FROM node as builder_client
WORKDIR /app

# Add client files
RUN mkdir -p client/cef
ADD client/src          client/src/
ADD client/*.cfg        client/
ADD client/*.json       client/
ADD client/*.mjs        client/

# Install typescript and compile project
WORKDIR /app/client
RUN npm install -g typescript
RUN npm install
RUN npm run build && \
    npm run clean
#    npm audit fix

# Add UI files
WORKDIR /app
RUN mkdir -p ui
ADD ui/src          ui/src/
ADD ui/*.babelrc    ui/
ADD ui/*.js         ui/
ADD ui/*.json       ui/
ADD ui/*.html       ui/

# Install typescript and compile project
WORKDIR /app/ui
RUN npm install
RUN npm run build
RUN npm audit fix


## Config Patcher
FROM alpine as configpatcher
WORKDIR /config

# Install jq
RUN apk add jq

# Add and patch config
ARG BASE_CONFIG
ARG SENTRY_DSN
ARG SENTRY_ENVIRONMENT
ARG SENTRY_RELEASE

RUN echo ${BASE_CONFIG} | \
    # # Patch Sentry section
    jq --arg data "$SENTRY_DSN" '.Sentry.DSN |= $data' | \
    jq --arg data "$SENTRY_ENVIRONMENT" '.Sentry.Environment |= $data' | \
    jq --arg data "$SENTRY_RELEASE" '.Sentry.Release |= $data' | \
    # Write to config.json
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
ADD build/server.cfg server.cfg
ADD build/entrypoint.sh /root/entrypoint.sh

ENTRYPOINT [ "/root/entrypoint.sh" ]
