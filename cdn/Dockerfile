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
#RUN npm audit fix


## Justpack
FROM eisengrind/altv-server:release-js as justpack

# Add binaries
RUN mkdir -p resources/exov-client/
COPY --from=builder_client  /app/client resources/exov-client/

# Add config file
ADD cdn/server.cfg server.cfg

# Build the CDN Package
ENTRYPOINT [ "./altv-server", "--justpack" ]
