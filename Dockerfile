## Base Image
FROM stivik/altv:rc as runner



## Builder Server
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as builder_server
WORKDIR /app

# Restore nuget packages and build binaries
RUN mkdir -p server
ADD server/Exo.Rp.Core                      server/Exo.Rp.Core
ADD server/Exo.Rp.Models                    server/Exo.Rp.Models
ADD server/Exo.Rp.Serialization             server/Exo.Rp.Serialization
ADD server/Exo.Rp.Plugins.Core              server/Exo.Rp.Plugins.Core
RUN dotnet restore                          server/Exo.Rp.Core
RUN dotnet build --no-restore -c Release    server/Exo.Rp.Core -o /app/bin

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

## Config Patcher
FROM alpine as configpatcher
WORKDIR /config

# Install jq
RUN apk add jq

# Add and patch config
ARG SENTRY_DSN
ARG SENTRY_ENVIRONMENT
ARG SENTRY_RELEASE
ARG DATABASE_SERVER
ARG DATABASE_USER_ID
ARG DATABASE_PASSWORD
ARG DATABASE_NAME
ARG DATABASE_PORT
ARG DATABASE_QUERYLOG
ARG METRICSCOLLECTOR_HOST
ARG METRICSCOLLECTOR_DATABASE
ARG METRICSCOLLECTOR_USER
ARG METRICSCOLLECTOR_PASSWORD
ARG WOTLABAPI_URL
ARG WOTLABAPI_SECRET
ARG WOTLABAPI_ONLYBETA
ARG WOTLABAPI_BETAGROUPID

ADD build/config.json.example config.unpatched.json
RUN cat config.unpatched.json | \
    # Patch Database section
    jq --arg data "$DATABASE_SERVER" '.Database.Server |= $data' | \
    jq --arg data "$DATABASE_USER_ID" '.Database.UserId |= $data' | \
    jq --arg data "$DATABASE_PASSWORD" '.Database.Password |= $data' | \
    jq --arg data "$DATABASE_NAME" '.Database.Database |= $data' | \
    jq --arg data "$DATABASE_PORT" '.Database.Port |= $data' | \
    jq --arg data "$DATABASE_QUERYLOG" '.Database.QueryLog |= $data' | \
    # Patch Logger section
    jq --arg data "63" '.Logger.LogFileFlags |= $data' | \
    jq --arg data "resources\\exov\\logs" '.Logger.PathToLogFolder |= $data' | \
    jq --arg data "\${0}.log" '.Logger.FileName |= $data' | \
    # Patch MetricsCollector section
    jq --arg data "600000.0" '.MetricsCollector.Interval |= $data' | \
    jq --arg data "$METRICSCOLLECTOR_HOST" '.MetricsCollector.host |= $data' | \
    jq --arg data "$METRICSCOLLECTOR_DATABASE" '.MetricsCollector.database |= $data' | \
    jq --arg data "$METRICSCOLLECTOR_USER" '.MetricsCollector.user |= $data' | \
    jq --arg data "$METRICSCOLLECTOR_PASSWORD" '.MetricsCollector.password |= $data' | \
    # Patch WotlabApi section
    jq --arg data "$WOTLABAPI_URL" '.WotlabApi.Url |= $data' | \
    jq --arg data "$WOTLABAPI_SECRET" '.WotlabApi.Secret |= $data' | \
    jq --arg data "$WOTLABAPI_ONLYBETA" '.WotlabApi.OnlyBeta |= $data' | \
    jq --arg data "$WOTLABAPI_BETAGROUPID" '.WotlabApi.BetaGroupId |= $data' | \
    # Patch Sentry section
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
ADD build/server.cfg config/
