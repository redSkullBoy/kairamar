using Telegram.Bot.Types;
using Telegram.Bot;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Constants;

namespace TgBot.Endpoints.Default;

public class DefaultChosenInlineResultEndpoint : ChosenInlineResultEndpoint
{
    public DefaultChosenInlineResultEndpoint()
    {
    }

    public override void Configure()
    {
        Routes(BaseEndpointConst.DEFAULT);
    }

    public override async Task HandleAsync(ChosenInlineResult message, CancellationToken cancellationToken)
    {
        const string text = "Данная функция не добавлена\n";

        await BotClient.SendTextMessageAsync(
                        chatId: message.ResultId,
                        text: text,
                        cancellationToken: cancellationToken);
    }
}
