# Этап 1: Сборка приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем файл проекта и восстанавливаем зависимости
COPY ["TG-BOT-1.csproj", "."]
RUN dotnet restore

# Копируем весь остальной код и собираем приложение
COPY . .
RUN dotnet publish "TG-BOT-1.csproj" -c Release -o /app/publish

# Этап 2: Запуск приложения (используем ASP.NET Core образ)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Токен будет передан через переменную окружения
ENTRYPOINT ["dotnet", "TG-BOT-1.dll"]