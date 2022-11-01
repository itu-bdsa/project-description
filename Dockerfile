FROM mcr.microsoft.com/dotnet/sdk:6.0-ubuntu AS build-env
WORKDIR /App

COPY . ./
ENTRYPOINT ["dotnet", "test"]