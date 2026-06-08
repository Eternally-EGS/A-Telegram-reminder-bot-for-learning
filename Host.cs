using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

class Host {
TelegramBotClient Bot;

    public Action <ITelegramBotClient,Update>? Onmessage;

    public Host(string token)
    {
        Bot = new TelegramBotClient(token);
    }

    public void Start(){
        Bot.StartReceiving(UpdateHead,ErrorHead);
        Console.WriteLine("Bot is started");
    }

    private async Task ErrorHead(ITelegramBotClient client, Exception exception, HandleErrorSource source, CancellationToken token)
    {
        Console.WriteLine($"Error: + {exception.Message}");
        await Task.CompletedTask;
    }

    private async Task UpdateHead(ITelegramBotClient client, Update update, CancellationToken token)
    {
        Console.WriteLine($"I received a message: {update.Message?.Text ?? "[Is no text]"}");
        Onmessage?.Invoke(client,update);
        await Task.CompletedTask;
    }
}