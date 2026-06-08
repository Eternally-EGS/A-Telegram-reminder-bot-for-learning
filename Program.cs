﻿using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;

internal class Program
{
    private static void Main() {

        string? botToken = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN");

        if (string.IsNullOrEmpty(botToken))
        {
            throw new Exception("Ошибка: Переменная окружения TELEGRAM_BOT_TOKEN не найдена!");
        }

        Host Remindbot = new Host(botToken);
        Remindbot.Onmessage += OnMessege;
        Remindbot.Start();
        Console.ReadLine();
    }

    private static async void OnMessege(ITelegramBotClient client, Update update)
    {
        Console.WriteLine(update.Message?.Text ?? "is no text");

            if(update.Message?.Text == "/help") {
                await client.SendMessage(update.Message?.Chat.Id ?? 0,"GET OUT");
            }

    }
}









