using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.BotEndpoints.Endpoints;

namespace TgBot.Endpoints.Initiator;

public class IsInitiatorEndpoint : CallbackQueryEndpoint
{
    public IsInitiatorEndpoint()
    {
    }

    public override void Configure()
    {
        Routes("userIsInitiator");
    }

    public override async Task HandleAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        const string text = "Поздравляю вы стали водителем. Можете использовать следующие функции:\n";

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

        await BotClient.AnswerCallbackQueryAsync(
            callbackQueryId: callbackQuery.Id,
            cancellationToken: cancellationToken);

        await BotClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: text,
            replyMarkup: inlineKeyboard,
            cancellationToken: cancellationToken);
    }
}
