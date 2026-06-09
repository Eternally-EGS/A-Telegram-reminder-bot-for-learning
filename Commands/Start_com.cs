using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotApp
{
    public class Start_com : IBotCommand
    {
        // Command class
        public string Name => "/start";

        public async Task ExecuteAsync(ITelegramBotClient client, Update update)
        {
            // Command class
            long chatId = update.Message?.Chat.Id ?? 0;
    
            await client.SendMessage(chatId, @"Привет я бот напоминалка !!
            
            что я умею:
            пока ничего лол ждем обнов(((");
        }
    }
}
