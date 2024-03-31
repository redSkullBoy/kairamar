using Telegram.Bot.Types;
using Telegram.Bot;
using TgBot.BotEndpoints.Endpoints;
using TgBot.Templates;

namespace TgBot.Endpoints.Initiator;

public class MenuEndpoint : CallbackQueryEndpoint
{
    private readonly ITelegramBotClient _botClient;

    public MenuEndpoint(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public override void Configure()
    {
        PreRoutes("menuInitiator");
    }

    public override async Task HandleAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        await _botClient.AnswerCallbackQueryAsync(
            callbackQueryId: callbackQuery.Id,
            cancellationToken: cancellationToken);

        await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: "Меню для водителя",
            replyMarkup: InitiatorsTemplates.Menu(),
            cancellationToken: cancellationToken);
    }
}