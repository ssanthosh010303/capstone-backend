FROM mcr.microsoft.com/dotnet/sdk:8.0@sha256:35792ea4ad1db051981f62b313f1be3b46b1f45cadbaa3c288cd0d3056eefb83 AS build-env

EXPOSE 80

WORKDIR /app/

COPY ./WebApi/ ./

RUN dotnet restore

RUN dotnet publish -c Release -o ./out/

FROM mcr.microsoft.com/dotnet/aspnet:8.0@sha256:6c4df091e4e531bb93bdbfe7e7f0998e7ced344f54426b7e874116a3dc3233ff

WORKDIR /app/

COPY --from=build-env /app/out/ ./

ENTRYPOINT ["dotnet", "./WebApi.dll"]
