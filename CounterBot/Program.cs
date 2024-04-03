using PRTelegramBot.Core;

const string exitCommand = "exit";

var telegramBot = new PRBot(options =>
{
    options.Token = "7106841221:AAE1hL7jgUKIeqAWIBxAjvzM_zbt2bjxuRY";
    options.ClearUpdatesOnStart = true;
    options.WhiteListUsers = new List<long>() { };
    options.Admins = new List<long>() { };
    options.BotId = 0;
});

telegramBot.OnLogCommon += TelegramBot_OnLogCommon;
telegramBot.OnLogError += TelegramBot_OnLogError;

await telegramBot.Start();

void TelegramBot_OnLogError(Exception ex, long? id)
{
    Console.ForegroundColor = ConsoleColor.Red;
    string ex_message = $"{DateTime.Now} : {ex}";
    Console.WriteLine(ex_message);
    Console.ResetColor();
}

void TelegramBot_OnLogCommon(string msg, Enum typeEvent, ConsoleColor color)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    string message = $"{DateTime.Now} : {msg}";
    Console.WriteLine(message);
    Console.ResetColor();
}

while (true)
{
    var result = Console.ReadLine();
    if (result.ToLower() == exitCommand)
    {
        Environment.Exit(0);
    }
}
