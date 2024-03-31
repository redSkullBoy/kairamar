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
        var userState = _botUserService.GetStateOrNull(update.InlineQuery!.From!.Id);

        return userState;
    }

    public void ResetUserState(Update update)
    {
        _botUserService.SetProcess(update.InlineQuery!.From!.Id, string.Empty);
    }

    public async Task<bool> HandlePreEndpointAsync(Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline keyboard callback from: {InlineQuery}", update.InlineQuery!.Id);

        var completed = await HandlePreEndpointAsync(update.InlineQuery!, update.InlineQuery!.Query!, update.Type, cancellationToken);

        if (completed)
            _botUserService.SetProcess(update.CallbackQuery!.From!.Id, string.Empty);

        return completed;
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
