ARG REPO=mcr.microsoft.com
FROM ${REPO}/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish

ARG REPO=mcr.microsoft.com
FROM ${REPO}/dotnet/runtime:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TG-BOT-1.csproj.dll"]
