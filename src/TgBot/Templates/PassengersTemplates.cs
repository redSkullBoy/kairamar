using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace TgBot.Templates;

public static class PassengersTemplates
{
    public static InlineKeyboardMarkup Menu()
    {
        InlineKeyboardMarkup inlineKeyboard = new(
                new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Найти поездку", "findTrips"),
                    },
                });

        return inlineKeyboard;
    }

    public static async Task SendPassengerMenu(this ITelegramBotClient botClient, long chatId, CancellationToken ctn)
    {

        InlineKeyboardMarkup inlineKeyboard = new(
                new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Перейти в меню", "menuPassenger"),
                    },
                });

        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Навигация",
            replyMarkup: inlineKeyboard,
            cancellationToken: ctn);
    }

    public static async Task SendTripFindInfo(this ITelegramBotClient botClient, long chatId, CancellationToken ctn, string action, 
        string? fromAddress = null, string? toAddress = null, string? startDate = null, InlineKeyboardMarkup inlineKeyboard = null)
    {
        string info = $"""
                            Для поиска поездки укажите следующую информацию:
                            - Пункт отправления {fromAddress}
                            - Пункт назначения {toAddress}
                            - Дату и время отправления {startDate}

                            {action}
                            """;

        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: info,
            replyMarkup: inlineKeyboard,
            cancellationToken: ctn);
    }
}
