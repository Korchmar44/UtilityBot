using PRTelegramBot.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CounterBot
{
    public class Commands
    {
        [ReplyMenuHandler("Тест")]
        public static async Task Example(ITelegramBotClient botClient, Update update)
        {
            var message = "Hello World";
            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        }
    }
}
