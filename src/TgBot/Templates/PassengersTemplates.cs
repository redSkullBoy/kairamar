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
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Актуальные поездки по адресу", "findTripsFrom"),
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
}
