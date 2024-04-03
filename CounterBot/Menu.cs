using PRTelegramBot.Attributes;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using PRTelegramBot.Utils;
using PRTelegramBot.Models;
using PRTelegramBot.Interface;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.InlineButtons;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Helpers;

namespace CounterBot
{
    public class Menu
    {
        #region Reply
        [ReplyMenuHandler("Меню")]
        public static async Task Example(ITelegramBotClient botClient, Update update)
        {
            var message = "Меню";
            var menuList = new List<KeyboardButton>();

            menuList.Add(new KeyboardButton("Меню 1"));
            menuList.Add(new KeyboardButton("Меню 2"));
            menuList.Add(new KeyboardButton("Меню 3"));
            menuList.Add(new KeyboardButton("Меню 4"));
            menuList.Add(new KeyboardButton("Меню 5"));
            menuList.Add(new KeyboardButton("Меню 6"));

            var menu = MenuGenerator.ReplyKeyboard(1, menuList);

            var option = new OptionMessage();
            option.MenuReplyKeyboardMarkup = menu;


            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message, option);
        }
        #endregion

        #region Slash
        [ReplyMenuHandler("get")]
        public static async Task Get(ITelegramBotClient botClient, Update update)
        {
            var message = "Пример /get и /get_1";
            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        }

        [SlashHandler("/get")]
        public static async Task GetSlash(ITelegramBotClient botClient, Update update)
        {
            if (update.Message.Text.Contains("_"))
            {
                var spl = update.Message.Text.Split('_');
                if (spl.Length > 1)
                {
                    var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, $"Команда /get и параметр {spl[1]}");
                }
                else
                {
                    var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, "Команда /get");
                }
            }
            else
            {
                var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, "Команда /get");
            }
        }
        #endregion

        #region InLine
        [ReplyMenuHandler("inline")]
        public static async Task Inline(ITelegramBotClient botClient, Update update)
        {
            var message = "Пример inline";
            List<IInlineContent> menu = new List<IInlineContent>();
            var exampleOne = new InlineCallback("Пример 1", CustomTHeader.ExampleOne);
            var url = new InlineURL("Google", "https://google.com");
            var webapp = new InlineWebApp("ФБДА", "https://fbda.srvdev.ru");

            var exampleTwo = new InlineCallback<EntityTCommand<long>>("Название кнопки 2", CustomTHeader.ExampleTwo, new EntityTCommand<long>(5));
            var exampleThree = new InlineCallback<EntityTCommand<long>>("Название кнопки 3", CustomTHeader.ExampleThree, new EntityTCommand<long>(3));

            menu.Add(exampleOne);
            menu.Add(url);
            menu.Add(webapp);
            menu.Add(exampleTwo);
            menu.Add(exampleThree);

            var menuItems = MenuGenerator.InlineKeyboard(1, menu);

            var options = new OptionMessage();
            options.MenuInlineKeyboardMarkup = menuItems;

            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message, options);
        }
        #endregion

        [InlineCallbackHandler<CustomTHeader>(CustomTHeader.ExampleOne)]
        public static async Task InlineExample(ITelegramBotClient botClient, Update update)
        {
            var message = "Пример ExampleOne";

            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        }

        [InlineCallbackHandler<CustomTHeader>(CustomTHeader.ExampleTwo, CustomTHeader.ExampleThree)]
        public static async Task InlineExampleTwoThree(ITelegramBotClient botClient, Update update)
        {
            var callBackData = InlineCallback<EntityTCommand<long>>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
            if (callBackData != null)
            {
                var message = $"Данные {callBackData.Data.EntityId}";

                var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
            }

            
        }
    }
}
