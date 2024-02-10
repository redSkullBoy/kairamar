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

    public async Task HandleUserStateAsync(Update update, Type userStateEndpoint, string userState, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline keyboard callback from: {requestDataId}", update.InlineQuery!.Id);

        await HandleUserStateAsync(update.InlineQuery!, userStateEndpoint, update.Type, userState, cancellationToken);
    }

    public async Task HandleDefaultUserStateAsync(Update update, string userState, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Receive message type: {MessageType}", update.Message!.Type);

        await HandleDefaultUserStateAsync(update.InlineQuery!, update.Type, userState, cancellationToken);
    }
}
