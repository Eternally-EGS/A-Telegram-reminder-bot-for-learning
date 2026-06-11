using Telegram.Bot;
using Telegram.Bot.Types;

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


            try {
            // Parsing
            string parsing0 = messageText.Substring(8);
            int parsing1 = parsing0.IndexOf(' ');
            string predparsing = parsing0[(parsing1 + 1)..];
            int parsing2 = predparsing.IndexOf(':');

            Console.WriteLine("Пояс" + parsing0[..parsing1]);
            Console.WriteLine($"Время " + $"часов: {predparsing[..parsing2]}" + $"минут: {predparsing[(parsing2+ 1)..]}");
            } catch 
            {
                await client.SendTextMessageAsync(chatId,$"❌ Ошибка формата");
            }


        }
    }
}
