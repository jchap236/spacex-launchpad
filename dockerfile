FROM microsoft/dotnet:sdk AS build-env

# Copy csproj and restore as distinct layers
WORKDIR /src

WORKDIR /src/SpaceX.LaunchPads
COPY /src/SpaceX.LaunchPads/SpaceX.LaunchPads.csproj .
RUN dotnet restore

WORKDIR /src/SpaceX.LaunchPads.Api
COPY /src/SpaceX.LaunchPads.Api/SpaceX.LaunchPads.Api.csproj .
RUN dotnet restore

# Copy everything else and build
COPY /src/SpaceX.LaunchPads/ //src/SpaceX.LaunchPads/
COPY /src/SpaceX.LaunchPads.Api /src/SpaceX.LaunchPads.Api
WORKDIR /src/SpaceX.LaunchPads.Api

RUN dotnet publish -c Release -o /out

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /out .
ENTRYPOINT ["dotnet", "SpaceX.LaunchPads.Api.dll"]