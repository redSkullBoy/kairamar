using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace TgBot.Templates;

public static class StartTemplates
{
    public static InlineKeyboardMarkup Menu()
    {
        InlineKeyboardMarkup inlineKeyboard = new(
                new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Поездки", "activeTrips"),
                        InlineKeyboardButton.WithCallbackData("Создать поездку", "addTrip"),
                    },
                });

        return inlineKeyboard;
    }
}
