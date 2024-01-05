using Telegram.Bot.Types;
using Telegram.Bot;
using TgBot.BotEndpoints.Endpoints;

namespace TgBot.Endpoints.Default;

public class DefaultCallbackQueryEndpoint : CallbackQueryEndpoint
{
    public DefaultCallbackQueryEndpoint()
    {
    }

    public override void Configure()
    {
        Routes("default");
    }

    public override async Task HandleAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        const string text = "Данная функция не добавлена\n";

        await BotClient.AnswerCallbackQueryAsync(
            callbackQueryId: callbackQuery.Id,
            cancellationToken: cancellationToken);

        await BotClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: text,
            cancellationToken: cancellationToken);
    }
}
