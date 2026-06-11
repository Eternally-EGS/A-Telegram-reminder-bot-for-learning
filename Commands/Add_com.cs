using Microsoft.Data.Sqlite;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotApp;

namespace TG_BOT_1.Commands
{
    public class Add_com : IBotCommand
    {
        // Command class
        public string Name => "/add";

        // Function
        public async Task ExecuteAsync(ITelegramBotClient client, Update update)
        {

            // Get chatid and message
            long chatId = update.Message?.Chat.Id ?? 0;
            string messageText = update.Message?.Text ?? "";
        
        // Parsing and save DB
        try {
            
            if (messageText.StartsWith("/add")) {
            
            // Parsing command
            string parsing0 = messageText[5..];
            int spaceIndex = parsing0.IndexOf(' ');

            // Parsing date and text in message
            string date = parsing0[..spaceIndex];
            string text = parsing0[(spaceIndex + 1)..];

            // Parsing protaction
            if (!DateTime.TryParse(date,out DateTime remindDate))
            {
                await client.SendTextMessageAsync(chatId,"❌ Неверны формат нужен: ГГГГ.ММ.ДД");
                return;
            }
            
            if (remindDate < DateTime.Today)
            {
                await client.SendTextMessageAsync(chatId,"❌ Нельзя добавить событие в прошлом!");
                return;
            }

            // await client.SendMessage(chatId,$"DATA: {date}" + $"TEXT: {text}");

            // DB Path
            string connectDB = "Data Source=/data/reminders.db";

            //DB Writing
            try {

                        using var connect = new SqliteConnection(connectDB);
                        connect.Open();

                        var write = connect.CreateCommand();
                        write.CommandText = @"
                        INSERT INTO reminders (chat_id,text,remind_date)
                        VALUES (@chatid, @text,@date)
                    ";

                        write.Parameters.AddWithValue("@chatid", chatId);
                        write.Parameters.AddWithValue("@text", text);
                        write.Parameters.AddWithValue("@date", remindDate.ToString("yyyy-MM-dd"));
                        write.ExecuteNonQuery();

                        await client.SendTextMessageAsync(chatId, $"✅ Напоминание сохранено !!!");
                    } catch (Exception ex) {
                await client.SendTextMessageAsync(chatId,$"❌ Ошибка сохранения базы данных!! {ex}");
            }
                }

        } catch {
            await client.SendTextMessageAsync(chatId,"❌ Неверный формат: /add ГГГГ.ММ.ДД ТЕКСТ");
        }

        
        }

    }
}
