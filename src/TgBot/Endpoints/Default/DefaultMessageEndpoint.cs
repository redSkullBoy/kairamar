using Telegram.Bot.Types;
using Telegram.Bot;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Constants;

namespace TgBot.Endpoints.Default;

public class DefaultMessageEndpoint : MessageEndpoint
{
    public DefaultMessageEndpoint()
    {
    }

    public override void Configure()
    {
        Routes(BaseEndpointConst.DEFAULT);
    }

    public override async Task HandleAsync(Message message, CancellationToken cancellationToken)
    {
        const string text = "Данная функция не добавлена\n";

        await BotClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: text,
                        cancellationToken: cancellationToken);
    }
}
