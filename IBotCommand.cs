using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotApp
{
    public interface IBotCommand
    {
        // Command name
        string Name { get; }
        Task ExecuteAsync(ITelegramBotClient client, Update update);
    }
}
