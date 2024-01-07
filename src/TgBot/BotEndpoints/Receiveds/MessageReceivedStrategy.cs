using Telegram.Bot.Types;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;

namespace TgBot.BotEndpoints.Receiveds;

public class MessageReceivedStrategy : BaseReceivedStrategy<Message, MessageEndpoint>, IReceivedStrategy
{
    private readonly ILogger<MessageReceivedStrategy> _logger;

    public MessageReceivedStrategy(ILogger<MessageReceivedStrategy> logger, IServiceScopeFactory serviceScopeFactory
        , IUserBotService botUserService, ReceivedDefinition receivedDef) : base(serviceScopeFactory, botUserService, receivedDef)
    {
        _logger = logger;
    }

    public async Task HandleAsync(Update update, CancellationToken cancellationToken)
    {
        var message = update.Message!;

        _logger.LogInformation("Receive message type: {MessageType}", message.Type);

        await ProcessAsync(update.Message!, update.Message!.From!.Id, update.Message.Text!, update.Type, cancellationToken);

        _logger.LogInformation("Unknown message type: {MessageType}", update.Type);
    }
}
