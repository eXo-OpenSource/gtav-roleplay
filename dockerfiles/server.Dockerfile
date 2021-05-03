## Builder Server
FROM mcr.microsoft.com/dotnet/sdk
WORKDIR /

# Restore nuget packages and build binaries
RUN mkdir -p server
ADD server/Exo.Rp.Core          server/Exo.Rp.Core
ADD server/Exo.Rp.Models        server/Exo.Rp.Models
ADD server/Exo.Rp.Serialization server/Exo.Rp.Serialization
ADD server/Exo.Rp.Sdk           server/Exo.Rp.Sdk
RUN dotnet restore              server/Exo.Rp.Core
RUN dotnet publish              server/Exo.Rp.Core \
    --no-restore \
    --runtime linux-x64 \
    -c Release \
    -o /app/bin