using Telegram.Bot.Types;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;

namespace TgBot.BotEndpoints.Receiveds;

public class InlineQueryReceivedStrategy : BaseReceivedStrategy<InlineQuery, InlineQueryEndpoint>, IReceivedStrategy
{
    private readonly ILogger<InlineQueryReceivedStrategy> _logger;
    private readonly IUserBotService _botUserService;

    public InlineQueryReceivedStrategy(ILogger<InlineQueryReceivedStrategy> logger, IServiceScopeFactory serviceScopeFactory
        , IUserBotService botUserService, ReceivedDefinition receivedDef) : base(serviceScopeFactory, receivedDef)
    {
        _logger = logger;
        _botUserService = botUserService;
    }

    public string? GetUserState(Update update)
    {
        var userState = _botUserService.GetStateOrNull(update.CallbackQuery!.From!.Id);

        return userState;
    }

    public async Task HandleEndpointAsync(Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline keyboard callback from: {requestDataId}", update.InlineQuery!.Id);

        await HandleEndpointAsync(update.InlineQuery!, update.InlineQuery!.Query!, update.Type, cancellationToken);
    }

    public async Task HandleUserStateAsync(Update update, Type userStateEndpoint, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline keyboard callback from: {requestDataId}", update.InlineQuery!.Id);

        await HandleUserStateAsync(update.InlineQuery!, userStateEndpoint, cancellationToken);
    }
}
