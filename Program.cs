﻿using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotApp
{

internal class Program
{
    private static readonly List<IBotCommand> _commands = new()
    {
        new Start_com() 
      
    };

    private static void Main() {

        string? botToken = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN");

        if (string.IsNullOrEmpty(botToken))
        {
            throw new Exception("Ошибка: Переменная окружения TELEGRAM_BOT_TOKEN не найдена!");
        }

        Host Remindbot = new Host(botToken);
        Remindbot.Onmessage += OnMessege;
        Remindbot.Start();
        Console.WriteLine("Бот успешно запусщен нажмите enter для выхода");
        while (true) {
        var key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.Enter){
            Console.WriteLine("wait..."); break;}            
        }
    }

    private static async void OnMessege(ITelegramBotClient client, Update update)
    {
        string? messageText = update.Message?.Text;

        if (string.IsNullOrEmpty(messageText)) return;

        var command = _commands.FirstOrDefault(c => c.Name == messageText);

        if (command != null)
        {
            await command.ExecuteAsync(client, update);
        }
    }
}


}






