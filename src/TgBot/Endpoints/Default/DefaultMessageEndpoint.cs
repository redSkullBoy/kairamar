using Telegram.Bot.Types;
using Telegram.Bot;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Constants;

namespace TgBot.Endpoints.Default;

public class DefaultMessageEndpoint : MessageEndpoint
{
    private readonly ITelegramBotClient _botClient;

    public DefaultMessageEndpoint(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public override void Configure()
    {
        Routes(BaseEndpointConst.DEFAULT);
    }

    public override async Task DefaultHandleAsync(Message message, string userState, CancellationToken cancellationToken)
    {
        string text = $"{message.Text} - команда не распознана";

        await _botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: text,
                        cancellationToken: cancellationToken);
    }
}
