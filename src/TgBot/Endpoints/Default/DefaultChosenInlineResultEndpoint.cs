using Telegram.Bot.Types;
using Telegram.Bot;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Constants;

namespace TgBot.Endpoints.Default;

public class DefaultChosenInlineResultEndpoint : ChosenInlineResultEndpoint
{
    private readonly ITelegramBotClient _botClient;

    public DefaultChosenInlineResultEndpoint(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public override void Configure()
    {
        Routes(BaseEndpointConst.DEFAULT);
    }

    public override async Task DefaultHandleAsync(ChosenInlineResult message, string userState, CancellationToken cancellationToken)
    {
        const string text = "Данная функция не добавлена\n";

        await _botClient.SendTextMessageAsync(
                        chatId: message.ResultId,
                        text: text,
                        cancellationToken: cancellationToken);
    }
}
