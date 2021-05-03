## Builder Client
FROM node
WORKDIR /app

# Add client files
RUN mkdir -p client
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

# Compile UI
WORKDIR /app/ui
RUN npm install
RUN npm run build

# Add Phone files
WORKDIR /app
RUN mkdir -p phone
ADD phone/src    phone/src/
ADD phone/public phone/public/
ADD phone/*.js   phone/
ADD phone/*.json phone/

# Compile Phone
WORKDIR /app/phone
RUN npm install
RUN npm run build