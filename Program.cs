﻿using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using TG_BOT_1.Commands;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace TelegramBotApp
{
    internal class Program
    {

        // Command list
        private static readonly List<IBotCommand> _commands = new()
        {
            new Start_com(), 
            new Add_com(),
            new ShowList_com(),
            new Delete_com() ,
            new Set_timezone_com() 
        
        };

        private static async Task Main(string[] args)
        {
            // Db path create 
            if (Directory.Exists("/data")){
                GlobalDB.connectDBstr = "Data Source=/data/reminders.db";
            } else { GlobalDB.connectDBstr = "Data Source=reminders.db"; }
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => "Bot is running!");
            app.MapGet("/health", () => "OK");

            _ = Task.Run(() => app.RunAsync());

            //DB Createing
            try{

                using (var connect = new SqliteConnection(GlobalDB.connectDBstr))
                {
                    connect.Open();

                    // Creating table 1
                    var command = connect.CreateCommand();
                    command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS reminders (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    chat_id INTEGER NOT NULL,
                    text TEXT NOT NULL,
                    remind_date TEXT NOT NULL
                    )";
                    command.ExecuteNonQuery();
                    
                    // Cleanup old remind
                    var deleteOld = connect.CreateCommand();
                    deleteOld.CommandText = @"DELETE FROM reminders WHERE remind_date < date('now')";
                    deleteOld.ExecuteNonQuery();

                    // Creating table 2
                    var table2 = connect.CreateCommand();
                    table2.CommandText = @"
                    CREATE TABLE IF NOT EXISTS user_setting
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    chat_id INTEGER NOT NULL,
                    time_zone INTEGER NOT NULL,
                    alarm_time INTEGER NOT NULL
                    ";
                    table2.ExecuteNonQuery();
                    
                }

            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в процессе создания базы данных: {ex.Message}");
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
            Remindbot.StartDailyAction();
            Console.WriteLine("Бот успешно запусщен нажмите enter для выхода");

            // Ending 
            await Task.Delay(-1);
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

public static class GlobalDB {
    public static string connectDBstr {get; set;}
}






