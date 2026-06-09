﻿using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;
using Microsoft.Data.Sqlite;

namespace TelegramBotApp
{

    internal class Program
    {

        // Command list
        private static readonly List<IBotCommand> _commands = new()
        {
            new Start_com(), 
            new Add_com(),
            new ShowList_com() 
        
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
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    chat_id INTEGER NOT NULL,
                    text TEXT NOT NULL,
                    remind_date TEXT NOT NULL
                    )";
                    command.ExecuteNonQuery();
                }

            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в процессе создания базы данных: {ex.Message}");
                return;
            }

            // Get bot token from environment variables
            string? botToken = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN");

            // If bot token not exist protection
            if (string.IsNullOrEmpty(botToken))
            {
                throw new Exception("Ошибка: Переменная окружения TELEGRAM_BOT_TOKEN не найдена!");
            }

            // Base start
            Host Remindbot = new Host(botToken);
            Remindbot.Onmessage += OnMessege;
            Remindbot.Start();
            Console.WriteLine("Бот успешно запусщен нажмите enter для выхода");
            
            // Ending 
            while (true) {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter){
                Console.WriteLine("wait..."); break;}            
            }
        }

        // On massage void
        private static async void OnMessege(ITelegramBotClient client, Update update)
        {
            // Get message text
            string? messageText = update.Message?.Text;

            // Protection
            if (string.IsNullOrEmpty(messageText)) return;

            // Searching command
            var command = _commands.FirstOrDefault(c => 
                messageText == c.Name || messageText.StartsWith(c.Name + " "));

            if (command != null)
            {
                await command.ExecuteAsync(client, update);
            }
        }

    }
}






