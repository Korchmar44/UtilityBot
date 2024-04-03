using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Threading;

namespace CountCharBot.Controllers
{
    internal class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private CountCharController _countCharController;

        public TextMessageController(ITelegramBotClient telegramBotClient, CountCharController countCharController)
        {
            _telegramClient = telegramBotClient;
            _countCharController = countCharController;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Подсчёт количества символов" , $"ru"),
                        InlineKeyboardButton.WithCallbackData($"Вычисление суммы чисел" , $"en")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Наш бот считает буквы и складывает числа.</b> {Environment.NewLine}", 
                        cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
                default:
                    await _countCharController.Handle(message, ct);//await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Отправьте аудио для превращения в текст.", cancellationToken: ct);
                    break;
            }
        }
    }
}
