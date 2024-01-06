using Telegram.Bot.Types;
using Telegram.Bot;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Constants;

namespace TgBot.Endpoints.Default;

public class DefaultInlineQueryEndpoint : InlineQueryEndpoint
{
    public DefaultInlineQueryEndpoint()
    {
    }

    public override void Configure()
    {
        Routes(BaseEndpointConst.DEFAULT);
    }

    public override async Task HandleAsync(InlineQuery inlineQuery, CancellationToken cancellationToken)
    {
        const string text = "Данная функция не добавлена\n";

        await BotClient.SendTextMessageAsync(
                        chatId: inlineQuery.Id,
                        text: text,
                        cancellationToken: cancellationToken);
    }
}
