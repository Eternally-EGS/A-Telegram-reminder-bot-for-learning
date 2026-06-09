using Microsoft.Data.Sqlite;
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

    public void StartDailyAction() {
        // Auto message send

            _ = Task.Run(async () => {
                while(true) {

                    // Time now
                    DateTime now = DateTime.Now;
                    
                    // Time alarm
                    DateTime alarm = new DateTime(now.Year,now.Month,now.Day,15,37,0);
                    
                    // Alarm if mising
                    if (now > alarm) {
                        alarm = alarm.AddDays(1); }

                    await Task.Delay(alarm - now);

                    // Alarm action
                    Console.WriteLine($"Рассылка {alarm}");
                    
                    // DB Path
                    string connectDB = "Data Source=reminders.db";

                    try {

                        // Reading DB
                        using (var connect = new SqliteConnection(connectDB))
                        {
                        await connect.OpenAsync();

                            var command = connect.CreateCommand();
                        command.CommandText = "SELECT chat_id,text,remind_date FROM reminders";

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            // Message list from send
                            var usermessage = new Dictionary<long,string>();

                            while (await reader.ReadAsync())
                            {
                                long chat_id = reader.GetInt64(0);
                                string text = reader.GetString(1);
                                DateTime remindDate = DateTime.Parse(reader.GetString(2));
                                int dayleft = (int)(remindDate - DateTime.Today).TotalDays;

                                string line = "";
                                if (dayleft > 0)
                                    line = $"До события {text} осталось: {dayleft} дней";
                                else if (dayleft == 0) 
                                    line = $"Событие {text} наступило поздравляю!!!";
                                
                                if (usermessage.ContainsKey(chat_id))
                                    usermessage[chat_id] += "\n" + line;
                                else
                                    usermessage[chat_id] = $"События:\n{line}";
                            }

                            foreach(var user in usermessage){
                                await Bot.SendMessage(user.Key, user.Value);
                            }
                        }  
                    }

                    }  catch (Exception ex) {
                        Console.WriteLine("Ошибка" + ex);
                    }

                }
            });


    }

}