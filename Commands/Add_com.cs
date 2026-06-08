
using System.Xml.Linq;
using Microsoft.Data.Sqlite;
using Telegram.Bot;
using Telegram.Bot.Types;


namespace TelegramBotApp
{
    public class Add_com : IBotCommand
    {
        // Command class
        public string Name => "/add";

        // Function
        public async Task ExecuteAsync(ITelegramBotClient client, Update update)
        {

            long chatId = update.Message?.Chat.Id ?? 0;
            string? messageText = update.Message?.Text;
        
        // Parsing
        try {
        
        if (messageText.StartsWith("/add")) {
        
        // Parsing command
        string parsing0 = messageText.Substring(5);
        int spaceIndex = parsing0.IndexOf(' ');

        string date = parsing0.Substring(0,spaceIndex);
        string text = parsing0.Substring(spaceIndex + 1);

        // Parsing protaction

        if (!DateTime.TryParse(date,out DateTime remindDate))
        {
            await client.SendMessage(chatId,"❌ Неверны формат нужен: ГГГГ.ММ.ДД");
            return;
        }

        await client.SendMessage(chatId,$"DATA: {date}" + $"TEXT: {text}");

        // DB Path
        string connectDB = "Data Source=reminders.db";

        //DB Createing
        try{
            using (var connect = new SqliteConnection(connectDB))
            {
                connect.Open();
                var write = connect.CreateCommand();
                write.CommandText = @"
                    INSERT INTO reminders (chat_id,text,remind_date)
                    VALUE (@chatid, @text,@date)
                ";
                write.Parameters.AddWithValue("@chatid",chatId);
                write.Parameters.AddWithValue("@text",text);
                write.Parameters.AddWithValue("@date",date);

                await client.SendMessage(chatId,$"Напоминание сохранено !!!");
            }
        } catch (Exception ex) {
            await client.SendMessage(chatId,$"❌ Ошибка сохранения базы данных!! {ex}");
        }


        }

        } catch {
            await client.SendMessage(chatId,"❌ Неверный формат: /add ГГГГ.ММ.ДД ТЕКСТ");
        }

        }
    }
}
