
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotApp
{
    public class Start_com : IBotCommand
    {
        // Привязываем класс конкретно к этой команде
        public string Name => "/start";

        public async Task ExecuteAsync(ITelegramBotClient client, Update update)
        {
            long chatId = update.Message?.Chat.Id ?? 0;
            
            // Твоя логика, которая раньше была в Main
            await client.SendMessage(chatId, "GET OUT");
        }
    }
}
