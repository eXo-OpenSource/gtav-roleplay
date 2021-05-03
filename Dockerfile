## Base Images
ARG CI_REGISTRY_IMAGE
FROM $CI_REGISTRY_IMAGE/internal/server as server
FROM $CI_REGISTRY_IMAGE/internal/client as client

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
FROM eisengrind/altv-server:rc

# Add binaries
RUN mkdir -p resources/exov/
COPY --from=server        /app/bin                resources/exov/
COPY --from=client        /app/client             resources/exov-client/
COPY --from=configpatcher /config/config.json     resources/exov/

# Add DLC Packs
COPY dlcs resources/

# Add server config
ADD build/server.cfg server.cfg
ADD build/entrypoint.sh /root/entrypoint.sh

ENTRYPOINT [ "/root/entrypoint.sh" ]
