# Сборка приложения с помощью .NET SDK
FROM ://microsoft.com AS build
WORKDIR /src

# Копируем проект и восстанавливаем зависимости
COPY *.csproj ./
RUN dotnet restore

# Копируем остальные файлы и публикуем релиз
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Финальный легковесный образ для запуска бота
FROM ://microsoft.com AS final
WORKDIR /app
COPY --from=build /app/publish .

# Запуск бота (замените TG-BOT-1.dll на точное имя вашего файла, если оно отличается)
ENTRYPOINT ["dotnet", "TG-BOT-1.dll"]
