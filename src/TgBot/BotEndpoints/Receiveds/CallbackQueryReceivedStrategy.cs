using Telegram.Bot.Types;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;

namespace TgBot.BotEndpoints.Receiveds;

public class CallbackQueryReceivedStrategy : BaseReceivedStrategy<CallbackQuery, CallbackQueryEndpoint>, IReceivedStrategy
{
    private readonly ILogger<CallbackQueryReceivedStrategy> _logger;
    private readonly IUserBotService _botUserService;

    public CallbackQueryReceivedStrategy(ILogger<CallbackQueryReceivedStrategy> logger, IServiceScopeFactory serviceScopeFactory
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

    public void ResetUserState(Update update)
    {
        _botUserService.SetProcess(update.CallbackQuery!.From!.Id, string.Empty);
    }

    public async Task<bool> HandlePreEndpointAsync(Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline keyboard callback from: {CallbackQueryId}", update.CallbackQuery!.Id);

        var completed = await HandlePreEndpointAsync(update.CallbackQuery!, update.CallbackQuery!.Data!, update.Type, cancellationToken);

        if (completed)
            _botUserService.SetProcess(update.CallbackQuery!.From!.Id, string.Empty);

        return completed;
    }

    public async Task HandleEndpointAsync(Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline keyboard callback from: {CallbackQueryId}", update.CallbackQuery!.Id);

        await HandleEndpointAsync(update.CallbackQuery!, update.CallbackQuery!.Data!, update.Type, cancellationToken);
    }

    public async Task HandleUserStateAsync(Update update, Type userStateEndpoint, string userState, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline keyboard callback from: {CallbackQueryId}", update.CallbackQuery!.Id);

        await HandleUserStateAsync(update.CallbackQuery, userStateEndpoint, update.Type, userState, cancellationToken);
    }

    public async Task HandleDefaultUserStateAsync(Update update, string userState, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Receive message type: {MessageType}", update);

        await HandleDefaultUserStateAsync(update.CallbackQuery!, update.Type, userState, cancellationToken);
    }
}
