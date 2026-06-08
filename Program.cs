﻿using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;
using Microsoft.Data.Sqlite;

namespace TelegramBotApp
{

 

    




internal class Program
{

    private static readonly List<IBotCommand> _commands = new()
    {
        new Start_com(), 
        new Add_com() 
      
    };

    private static void Main() {

        // DB Path
        string connectDB = "Data Source=reminders.db";

        //DB Createing
        try{
            using (var connect = new SqliteConnection(connectDB))
            {
                connect.Open();

                var command = connect.CreateCommand();
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS reminders (
                id INTAGER PRIMARY KEY AUTOINCREMENT,
                chat_id INTEGER NOT NULL,
                text TEXT NOT NULL,
                remind_date TEXT NOT NULL
                )";
                Console.WriteLine("База данных была создана");

            }

        } catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в процессе создания базы данных: {ex.Message}");
            return;
        }


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

       
        var command = _commands.FirstOrDefault(c => 
            messageText == c.Name || messageText.StartsWith(c.Name + " "));

        if (command != null)
        {
            await command.ExecuteAsync(client, update);
        }
    }

}


}






