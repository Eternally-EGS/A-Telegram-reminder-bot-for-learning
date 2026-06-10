using Microsoft.Data.Sqlite;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotApp
{
    public class Delete_com : IBotCommand
    {
        // Command class
        public string Name => "/delete";

        public async Task ExecuteAsync(ITelegramBotClient client, Update update)
        {
            // Command class
            long chatId = update.Message?.Chat.Id ?? 0;
            string messageText = update.Message?.Text ?? "";

            try {

            if (messageText.StartsWith("/delete")) {
            
            // Parsing command
            int spaceIndex = messageText.IndexOf(' ');

            // Parsing date and text in message
            string ids = (messageText.Substring(spaceIndex)).Trim();

            // DB Path
            string connectDB = "Data Source=/data/reminders.db";

            try {
            // Reading DB
            using (var connect = new SqliteConnection(connectDB))
            {
            await connect.OpenAsync();

                

                var command = connect.CreateCommand();
                command.CommandText = "DELETE FROM reminders WHERE id = @id AND chat_id = @chat_id";
                command.Parameters.AddWithValue("@id",ids);
                command.Parameters.AddWithValue("@chat_id",chatId);
                int delete = await command.ExecuteNonQueryAsync();

                if (delete > 0) 
                    await client.SendMessage(chatId,$"✅ Напоминание {ids} удалено.");
                else 
                    await client.SendMessage(chatId,$"❌ Напоминание с номером: {ids} не найдено.");
            }
            } catch (Exception ex) {
                 await client.SendMessage(chatId,$"❌ Ошибка базы данных {ex}");
            }
                }
            } catch {
                await client.SendMessage(chatId,"❌ Неверный формат: /delete номер_напоминания (номер_напоминания можно узнать из /list)");
            }
    
        }
    }
}
