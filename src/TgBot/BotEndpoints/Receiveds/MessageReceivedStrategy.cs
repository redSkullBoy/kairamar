using Telegram.Bot.Types;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;

namespace TgBot.BotEndpoints.Receiveds;

public class MessageReceivedStrategy : IReceivedStrategy
{
    private Dictionary<string, Type> _endpoints = new Dictionary<string, Type>();
    private Dictionary<Type, string> _userStates = new();

    private readonly ILogger<MessageReceivedStrategy> _logger;
    private readonly IEnumerable<MessageEndpoint> _endpointServices;
    private readonly IUserBotService _botUserService;

    public MessageReceivedStrategy(ILogger<MessageReceivedStrategy> logger
        , IEnumerable<MessageEndpoint> endpointServices, IUserBotService botUserService)
    {
        _logger = logger;
        _endpointServices = endpointServices;
        _botUserService = botUserService;
    }

    public async Task HandleAsync(Update update, CancellationToken cancellationToken)
    {
        var message = update.Message!;

        _logger.LogInformation("Receive message type: {MessageType}", message.Type);

        var userState = _botUserService.GetStateOrNull(update.Message!.From!.Id);
        //если нет статуса то ищем по endpoints
        if (string.IsNullOrWhiteSpace(userState))
        {
            if (_endpoints!.TryGetValue(message.Text!, out var endpointWithoutStatus))
            {
                // Выберите конкретную реализацию, связанную с endpoint
                var implementation = _endpointServices.First(impl => impl.GetType() == endpointWithoutStatus);

                await implementation.HandleAsync(message, cancellationToken);
            }
        }
        //Проверка на состояние
        var keyValue = _userStates!.Single(x => x.Value == userState);

        if (keyValue.Key != null)
        {
            // Выберите конкретную реализацию, связанную с endpoint
            var implementation = _endpointServices.First(impl => impl.GetType() == keyValue.Key);

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
