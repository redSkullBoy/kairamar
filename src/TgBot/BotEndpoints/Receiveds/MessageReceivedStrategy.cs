using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;

namespace TgBot.BotEndpoints.Receiveds;

public class MessageReceivedStrategy : BaseReceivedStrategy<Message, MessageEndpoint>, IReceivedStrategy
{
    private readonly ILogger<MessageReceivedStrategy> _logger;
    private readonly IUserBotService _botUserService;

    public MessageReceivedStrategy(ILogger<MessageReceivedStrategy> logger, IServiceScopeFactory serviceScopeFactory
        , IUserBotService botUserService, ReceivedDefinition receivedDef) : base(serviceScopeFactory, receivedDef)
    {
        _logger = logger;
        _botUserService = botUserService;
    }

    public string? GetUserState(Update update)
    {
        var userState = _botUserService.GetStateOrNull(update.Message!.From!.Id);

        return userState;
    }

    public async Task HandleEndpointAsync(Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Receive message type: {MessageType}", update.Message!.Type);

        await HandleEndpointAsync(update.Message!, update.Message!.Text!, update.Type, cancellationToken);
    }

    public async Task HandleUserStateAsync(Update update, Type userStateEndpoint, string userState, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Receive message type: {MessageType}", update.Message!.Type);

        await HandleUserStateAsync(update.Message, userStateEndpoint, update.Type, userState, cancellationToken);
    }

    public async Task HandleDefaultUserStateAsync(Update update, string userState, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Receive message type: {MessageType}", update.Message!.Type);

        await HandleDefaultUserStateAsync(update.Message, update.Type, userState, cancellationToken);
    }
}
