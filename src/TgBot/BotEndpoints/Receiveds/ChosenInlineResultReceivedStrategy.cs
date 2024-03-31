using Telegram.Bot.Types;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;

namespace TgBot.BotEndpoints.Receiveds;

public class ChosenInlineResultReceivedStrategy : BaseReceivedStrategy<ChosenInlineResult, ChosenInlineResultEndpoint>, IReceivedStrategy
{
    private readonly ILogger<ChosenInlineResultReceivedStrategy> _logger;
    private readonly IUserBotService _botUserService;

    public ChosenInlineResultReceivedStrategy(ILogger<ChosenInlineResultReceivedStrategy> logger, IServiceScopeFactory serviceScopeFactory
        , IUserBotService botUserService, ReceivedDefinition receivedDef) : base(serviceScopeFactory, receivedDef)
    {
        _logger = logger;
        _botUserService = botUserService;
    }

    public string? GetUserState(Update update)
    {
        var userState = _botUserService.GetStateOrNull(update.ChosenInlineResult!.From!.Id);

        return userState;
    }

    public void ResetUserState(Update update)
    {
        _botUserService.SetProcess(update.ChosenInlineResult!.From!.Id, string.Empty);
    }

    public async Task<bool> HandlePreEndpointAsync(Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline result: {ChosenInlineResultId}", update.ChosenInlineResult!.ResultId);

        var completed = await HandlePreEndpointAsync(update.ChosenInlineResult!, update.ChosenInlineResult!.Query!, update.Type, cancellationToken);

        if (completed)
            _botUserService.SetProcess(update.CallbackQuery!.From!.Id, string.Empty);

        return completed;
    }

    public async Task HandleEndpointAsync(Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline result: {ChosenInlineResultId}", update.ChosenInlineResult!.ResultId);

        await HandleEndpointAsync(update.ChosenInlineResult!, update.ChosenInlineResult!.Query!, update.Type, cancellationToken);
    }

    public async Task HandleUserStateAsync(Update update, Type userStateEndpoint, string userState, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline result: {ChosenInlineResultId}", update.ChosenInlineResult!.ResultId);

        await HandleUserStateAsync(update.ChosenInlineResult!, userStateEndpoint, update.Type, userState, cancellationToken);
    }

    public async Task HandleDefaultUserStateAsync(Update update, string userState, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Receive message type: {MessageType}", update.Message!.Type);

        await HandleDefaultUserStateAsync(update.ChosenInlineResult!, update.Type, userState, cancellationToken);
    }
}
