using CountCharBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CountCharBot.Controllers
{
    public class CountCharController
    {
        private readonly IStorage _memoryStorage; // Поле для хранения ссылки на объект, реализующий интерфейс IStorage
        private readonly ITelegramBotClient _telegramClient; // Поле для хранения ссылки на объект, реализующий интерфейс ITelegramBotClient

        public CountCharController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _memoryStorage = memoryStorage; // Присваивание переданного объекта memoryStorage полю _memoryStorage
            _telegramClient = telegramBotClient; // Присваивание переданного объекта telegramBotClient полю _telegramClient
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            var fileId = message.Text; // Получение текстового сообщения от пользователя

            if (fileId == null)
                return; // Выход из метода, если сообщение пустое

            string userOperationCode = _memoryStorage.GetSession(message.Chat.Id).OperationCode; // Получение кода операции из сессии пользователя

            switch (userOperationCode)
            {
                case "ru":
                    await _telegramClient.SendTextMessageAsync(message.From.Id, $"Длина сообщения: {message.Text.Length} знаков", cancellationToken: ct); // Отправка сообщения о длине текста на русском языке
                    break;
                case "en":
                    string input = message.Text; // Получение ввода пользователя
                    string[] numbers = input.Split(' '); // Разделение ввода на числа
                    int sum = 0; // Переменная для хранения суммы чисел
                    bool isValidInput = true; // Флаг для проверки правильности ввода

                    foreach (string number in numbers)
                    {
                        if (int.TryParse(number, out int parsedNumber)) // Попытка преобразования строки в число
                        {
                            sum += parsedNumber; // Если успешно, добавление числа к сумме
                        }
                        else
                        {
                            await _telegramClient.SendTextMessageAsync(message.From.Id, $"Ошибка: '{number}' не является числом.", cancellationToken: ct); // Отправка сообщения об ошибке неправильного ввода
                            isValidInput = false; // Установка флага неправильного ввода
                            break; // Выход из цикла
                        }
                    }

                    if (isValidInput)
                    {
                        await _telegramClient.SendTextMessageAsync(message.From.Id, $"Сумма чисел: {sum}", cancellationToken: ct); // Отправка сообщения с суммой чисел
                    }
                    else
                    {
                        await _telegramClient.SendTextMessageAsync(message.From.Id, "Пожалуйста, введите только числа через пробел.", cancellationToken: ct); // Отправка сообщения о необходимости вводить только числа
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
