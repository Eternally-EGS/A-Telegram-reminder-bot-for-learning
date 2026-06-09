# Глобальные переменные без двоеточий и слэшей
ARG REGISTRY=://microsoft.com
ARG SDK_IMAGE=dotnet/sdk:8.0
ARG RUNTIME_IMAGE=dotnet/runtime:8.0

# Сборка
FROM ${REGISTRY}/${SDK_IMAGE} AS build
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish

# Запуск
FROM ${REGISTRY}/${RUNTIME_IMAGE} AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TG-BOT-1.csproj.dll"]
