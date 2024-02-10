using Telegram.Bot.Types;
using Telegram.Bot;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Constants;

namespace TgBot.Endpoints.Default;

public class DefaultInlineQueryEndpoint : InlineQueryEndpoint
{
    private readonly ITelegramBotClient _botClient;

    public DefaultInlineQueryEndpoint(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public override void Configure()
    {
        Routes(BaseEndpointConst.DEFAULT);
    }

    public override async Task DefaultHandleAsync(InlineQuery inlineQuery, string userState, CancellationToken cancellationToken)
    {
        const string text = "Данная функция не добавлена\n";

        await _botClient.SendTextMessageAsync(
                        chatId: inlineQuery.Id,
                        text: text,
                        cancellationToken: cancellationToken);
    }
}
