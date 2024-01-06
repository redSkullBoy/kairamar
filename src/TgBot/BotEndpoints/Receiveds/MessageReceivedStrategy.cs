using Telegram.Bot.Types;
using TgBot.BotEndpoints.Constants;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;

namespace TgBot.BotEndpoints.Receiveds;

public class MessageReceivedStrategy : IReceivedStrategy
{
    private Dictionary<string, Type> _endpoints = new Dictionary<string, Type>();
    private Dictionary<Type, string> _userStates = new();

    private readonly ILogger<MessageReceivedStrategy> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IUserBotService _botUserService;

    public MessageReceivedStrategy(ILogger<MessageReceivedStrategy> logger
        , IServiceScopeFactory serviceScopeFactory, IUserBotService botUserService)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _botUserService = botUserService;
    }

    public async Task HandleAsync(Update update, CancellationToken cancellationToken)
    {
        var message = update.Message!;

        _logger.LogInformation("Receive message type: {MessageType}", message.Type);

        using var scope = _serviceScopeFactory.CreateScope();
        var endpointServices = (IEnumerable<MessageEndpoint>)scope.ServiceProvider.GetServices(typeof(MessageEndpoint));

        var userState = _botUserService.GetStateOrNull(update.Message!.From!.Id);
        //если нет статуса то ищем по endpoints
        if (string.IsNullOrWhiteSpace(userState))
        {
            if (_endpoints!.TryGetValue(message.Text!, out var endpointWithoutStatus))
            {
                var implementation = endpointServices.First(impl => impl.GetType() == endpointWithoutStatus);
                await implementation.HandleAsync(message, cancellationToken);
            }
            // Для DefaultEndpoint
            else if (_endpoints!.TryGetValue(BaseEndpointConst.DEFAULT, out var endpoint))
            {
                var implementation = endpointServices.First(impl => impl.GetType() == endpoint);
                await implementation.HandleAsync(message, cancellationToken);
            }

            return;
        }
        //Проверка на состояние
        var keyValue = _userStates!.SingleOrDefault(x => x.Value == userState);

        if (keyValue.Key != null)
        {
            var implementation = endpointServices.First(impl => impl.GetType() == keyValue.Key);
            await implementation.HandleAsync(message, cancellationToken);
        }
        // Для DefaultEndpoint
        else if (_endpoints!.TryGetValue(BaseEndpointConst.DEFAULT, out var endpoint))
        {
            var implementation = endpointServices.First(impl => impl.GetType() == endpoint);
            await implementation.HandleAsync(message, cancellationToken);
        }

        _logger.LogInformation("Unknown message type: {MessageType}", update.Type);
    }

    public void Register(Dictionary<string, Type> endpoints)
    {
        foreach (var endpoint in endpoints)
        {
            _endpoints.Add(endpoint.Key, endpoint.Value);
        }
    }

    public void Register(Dictionary<Type, string> states)
    {
        foreach (var state in states)
        {
            _userStates.Add(state.Key, state.Value);
        }
    }
}
