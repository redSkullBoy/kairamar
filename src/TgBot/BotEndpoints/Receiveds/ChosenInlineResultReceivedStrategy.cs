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
        var userState = _botUserService.GetStateOrNull(update.CallbackQuery!.From!.Id);

        return userState;
    }

    public async Task HandleEndpointAsync(Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline result: {ChosenInlineResultId}", update.ChosenInlineResult!.ResultId);

        await HandleEndpointAsync(update.ChosenInlineResult!, update.ChosenInlineResult!.Query!, update.Type, cancellationToken);
    }

    public async Task HandleUserStateAsync(Update update, Type userStateEndpoint, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline result: {ChosenInlineResultId}", update.ChosenInlineResult!.ResultId);

        await HandleUserStateAsync(update.ChosenInlineResult!, userStateEndpoint, cancellationToken);
    }
}
