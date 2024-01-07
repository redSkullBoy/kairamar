using Telegram.Bot.Types;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;

namespace TgBot.BotEndpoints.Receiveds;

public class ChosenInlineResultReceivedStrategy : BaseReceivedStrategy<ChosenInlineResult, ChosenInlineResultEndpoint>, IReceivedStrategy
{
    private readonly ILogger<ChosenInlineResultReceivedStrategy> _logger;

    public ChosenInlineResultReceivedStrategy(ILogger<ChosenInlineResultReceivedStrategy> logger, IServiceScopeFactory serviceScopeFactory, 
        IUserBotService botUserService, ReceivedDefinition receivedDef) : base(serviceScopeFactory, botUserService, receivedDef)
    {
        _logger = logger;
    }

    public async Task HandleAsync(Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline result: {ChosenInlineResultId}", update.ChosenInlineResult!.ResultId);

        await ProcessAsync(update.ChosenInlineResult!, update.ChosenInlineResult!.From.Id, update.ChosenInlineResult.Query!, update.Type, cancellationToken);
    }
}
