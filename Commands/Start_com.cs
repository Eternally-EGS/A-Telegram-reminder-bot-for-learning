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

            string response = @"
Привет, я бот-напоминалка.
Автор: Eternally16

Команды:
/add ГГГГ-ММ-ДД Текст — добавить событие
/list — показать список событий
/delete номер — удалить событие из списка

Каждый день в 9 утра я присылаю отчёт, сколько дней осталось до каждого события.

Пример: /add 2025-12-31 Купить ёлку
";
    
            await client.SendTextMessageAsync(chatId, response);
        }
    }
}
