using Telegram.Bot.Types;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;

namespace TgBot.BotEndpoints.Receiveds;

public class CallbackQueryReceivedStrategy : BaseReceivedStrategy<CallbackQuery, CallbackQueryEndpoint>, IReceivedStrategy
{
    private readonly ILogger<CallbackQueryReceivedStrategy> _logger;

    public CallbackQueryReceivedStrategy(ILogger<CallbackQueryReceivedStrategy> logger, IServiceScopeFactory serviceScopeFactory
        , IUserBotService botUserService, ReceivedDefinition receivedDef) : base(serviceScopeFactory, botUserService, receivedDef)
    {
        _logger = logger;
    }

    public async Task HandleAsync(Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline keyboard callback from: {CallbackQueryId}", update.CallbackQuery!.Id);

        await ProcessAsync(update.CallbackQuery!, update.CallbackQuery!.From.Id, update.CallbackQuery.Data!, update.Type, cancellationToken);

        _logger.LogInformation("Unknown CallbackQuery type: {MessageType}", update.Type);
    }
}
