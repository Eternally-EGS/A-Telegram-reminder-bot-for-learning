using Telegram.Bot;
using Telegram.Bot.Types;
using Microsoft.Data.Sqlite;

namespace TelegramBotApp
{
    public class Set_timezone_com : IBotCommand
    {
        // Command class
        public string Name => "/set_tz";

        public async Task ExecuteAsync(ITelegramBotClient client, Update update)
        {
            long chatId = update.Message?.Chat.Id ?? 0;
            string messageText = update.Message?.Text ?? "";

            // Parsing
            string parsing0 = messageText.Substring(8);
            int parsing1 = parsing0.IndexOf(' ');
            string predparsing = parsing0[(parsing1 + 1)..];
            int parsing2 = predparsing.IndexOf(':');

            Console.WriteLine("Пояс" + parsing0[..parsing1]);
            Console.WriteLine($"Время " + $"часов: {predparsing[..parsing2]}" + $"минут: {predparsing[(parsing2+ 1)..]}");
            
            if (parsing1 <= 0)
            {
                await client.SendTextMessageAsync(chatId,"❌ Неправельный формат");
                return;
            }

            if (parsing2 <= 0)
            {
                await client.SendTextMessageAsync(chatId,"❌ Неправельный формат");
                return;
            }

            try {

            using (var connect = new SqliteConnection(GlobalDB.connectDBstr)){

                connect.Open();

                


                        var write = connect.CreateCommand();
                        write.CommandText = @"
                        INSERT INTO user_setting (chat_id,time_zone,alarm_time)
                        VALUES (@chatid, @time_zone,@alarm_time)
                    ";

                        write.Parameters.AddWithValue("@chatid", chatId);
                        write.Parameters.AddWithValue("@time_zone", parsing0[..parsing1]);
                        write.Parameters.AddWithValue("@alarm_time", predparsing);
                        write.ExecuteNonQuery();
            }

            } catch (Exception ex) {

            }
            
            
         
            
        
        }
    }
}
