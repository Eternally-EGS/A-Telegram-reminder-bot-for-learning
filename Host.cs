using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

class Host {
TelegramBotClient Bot;

    // OnMessage connection
    public Action <ITelegramBotClient,Update>? Onmessage;

    // Bot create
    public Host(string token)
    {
        Bot = new TelegramBotClient(token);
    }

    // Start funcion 
    public void Start(){
        Bot.StartReceiving(UpdateHead,ErrorHead);
    }

    // Error output
    private async Task ErrorHead(ITelegramBotClient client, Exception exception, HandleErrorSource source, CancellationToken token)
    {
        Console.WriteLine($"Error: + {exception.Message}");
        await Task.CompletedTask;
    }

    // Message output
    private async Task UpdateHead(ITelegramBotClient client, Update update, CancellationToken token)
    {
        Console.WriteLine($"I received a message: {update.Message?.Text ?? "[Is no text]"}");
        Onmessage?.Invoke(client,update);
        await Task.CompletedTask;
    }

}