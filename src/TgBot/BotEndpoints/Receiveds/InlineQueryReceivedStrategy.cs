using Telegram.Bot.Types;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;

namespace TgBot.BotEndpoints.Receiveds;

public class InlineQueryReceivedStrategy : BaseReceivedStrategy<InlineQuery, InlineQueryEndpoint>, IReceivedStrategy
{
    private readonly ILogger<InlineQueryReceivedStrategy> _logger;

    public InlineQueryReceivedStrategy(ILogger<InlineQueryReceivedStrategy> logger, IServiceScopeFactory serviceScopeFactory, 
        IUserBotService botUserService, ReceivedDefinition receivedDef) : base(serviceScopeFactory, botUserService, receivedDef)
    {
        _logger = logger;
    }

    public async Task HandleAsync(Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline keyboard callback from: {requestDataId}", update.InlineQuery!.Id);

        await ProcessAsync(update.InlineQuery!, update.InlineQuery!.From.Id, update.InlineQuery.Query!, update.Type, cancellationToken);

        _logger.LogInformation("Для такого запроса не найден Endpoint: {requestDataId}", update.InlineQuery!.Id);
    }
}
