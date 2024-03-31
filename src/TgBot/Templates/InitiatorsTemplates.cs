using Telegram.Bot;
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

    public static async Task SendInitiatorMenu(this ITelegramBotClient botClient, long chatId, CancellationToken ctn)
    {

        InlineKeyboardMarkup inlineKeyboard = new(
                new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Перейти в меню", "menuInitiator"),
                    },
                });

        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Навигация",
            replyMarkup: inlineKeyboard,
            cancellationToken: ctn);
    }
}
