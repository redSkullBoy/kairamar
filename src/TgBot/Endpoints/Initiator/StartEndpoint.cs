using BotTelegramEndpoints.Endpoint;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TgBot.Endpoints.Initiator;

public class StartEndpoint : CallbackQueryEndpoint
{
    public StartEndpoint(ITelegramBotClient botClient) : base(botClient)
    {
    }

    public override void Configure()
    {
        Route("userIsInitiator");
    }

    public override async Task<Message> HandleAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        const string text = "Вы стали водителем. Можете использовать следующие функции:\n";

        InlineKeyboardMarkup inlineKeyboard = new(
                new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Мои поездки", "myTrip"),
                        InlineKeyboardButton.WithCallbackData("Создать поездку", "addTrip"),
                    },
                });

        return await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: text,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);
    }
}
