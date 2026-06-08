
using Microsoft.Data.Sqlite;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotApp
{
    public class ShowList_com : IBotCommand
    {
        // Привязываем класс конкретно к этой команде
        public string Name => "/list";

        public async Task ExecuteAsync(ITelegramBotClient client, Update update)
        {
            long chatId = update.Message?.Chat.Id ?? 0;
            // DB Path

            try{
                string connectDB = "Data Source=reminders.db";
                using (var connect = new SqliteConnection(connectDB))
                {
                    connect.Open();
                    var command = connect.CreateCommand();
                    command.CommandText = @"
                        SELECT id, text, remind_date 
                        FROM reminders 
                        WHERE chat_id = @chatId 
                        ORDER BY remind_date ASC
                    ";
                    command.Parameters.AddWithValue("@chatId", chatId);

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        await client.SendMessage(chatId, "📭 У тебя пока нет напоминаний.");
                        return;
                    }
                    
                    string response = "📋 Твои напоминания:\n\n";
                    int counter = 1;
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string text = reader.GetString(1);
                        string date = reader.GetString(2);
                        response += $"{counter}. {text} (на {date})\n";
                        counter++;
                    }
                    await client.SendMessage(chatId, response);
                        }
                        

                        }
                } catch (Exception ex) {
                await client.SendMessage(chatId,$"❌ Ошибка чтения базы данных!! {ex}");
                }



        }
    }
}
