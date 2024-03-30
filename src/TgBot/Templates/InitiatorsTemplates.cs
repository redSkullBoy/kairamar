using Telegram.Bot.Types.ReplyMarkups;

namespace TgBot.Templates;

public static class InitiatorsTemplates
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
