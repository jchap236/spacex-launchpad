# Overview 
Solution to SDC Space X .Net Core Code Challenge

## Setup with Docker

1. Install Docker (https://docs.docker.com/install/)
2. Run docker-compose
```docker-compose up --build```
3. Test Api http://localhost:5000/api/launchpads

## Setup without Docker
1. Install .Net Core 2.2 (https://dotnet.microsoft.com/download/dotnet-core/2.2)
2. Restore Nuget Packages
``` 
    cd /src
    dotnet restore
```
3. Build Solutions
``` 
    dotnet build
```
4. Run Solutions
```
    cd SpaceX.LaunchPads.Api
    dotnet run
```
5. Test Api http://localhost:5000/api/launchpads


## Filters
Filter the result set or limit the result set using query parameters

Examples:
http://localhost:5000/api/launchpads?filter=status,name&limit=3
http://localhost:5000/api/launchpads?limit=1
http://localhost:5000/api/launchpads?filter=id,name

