
using Microsoft.Data.Sqlite;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotApp
{
    public class Add_com : IBotCommand
    {
        // Привязываем класс конкретно к этой команде
        public string Name => "/add";

        public async Task ExecuteAsync(ITelegramBotClient client, Update update)
        {

            long chatId = update.Message?.Chat.Id ?? 0;
            string? messageText = update.Message?.Text;
        
        if (messageText.StartsWith("/add")) {
        
        // Parsing command
        string parsing0 = messageText.Substring(5);

        int spaceIndex = parsing0.IndexOf(' ');
        
        
        string date = parsing0.Substring(0,spaceIndex);
        string text = parsing0.Substring(spaceIndex + 1);

         await client.SendMessage(chatId,$"DATA: {date}" + $"TEXT: {text}");

        }

        // DB Path
        string connectDB = "Data Source=reminders.db";

        //DB Createing
        try{
            using (var connect = new SqliteConnection(connectDB))
            {
                connect.Open();




            }
        } catch {

            
        }


        }
    }
}
