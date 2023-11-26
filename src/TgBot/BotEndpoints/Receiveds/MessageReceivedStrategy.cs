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
    private readonly IBotUserService _botUserService;

    public MessageReceivedStrategy(ILogger<MessageReceivedStrategy> logger
        , IEnumerable<MessageEndpoint> endpointServices, IBotUserService botUserService)
    {
        _logger = logger;
        _endpointServices = endpointServices;
        _botUserService = botUserService;
    }

    public async Task HandleAsync(Update update, CancellationToken cancellationToken)
    {
        var message = update.Message!;
        var userId = update.CallbackQuery!.From.Id;

        _logger.LogInformation("Receive message type: {MessageType}", message.Type);

        if (_endpoints!.TryGetValue(message.Text!, out var endpoint))
        {
            //Проверка на состояние
            if (_userStates!.TryGetValue(endpoint, out var state))
            {
                var userState = _botUserService.GetUserState(userId);

                if (userState == state)
                {
                    // Выберите конкретную реализацию, связанную с endpoint
                    var implementation = _endpointServices.First(impl => impl.GetType() == endpoint);

                    await implementation.HandleAsync(message, cancellationToken);
                }
            }
        }
        else
        {
            //Проверка на состояние
            var userState = _botUserService.GetUserState(userId);

            var keyValue = _userStates!.Single(x => x.Value == message.Text!);

            if (keyValue.Key != null && keyValue.Value == userState)
            {
                // Выберите конкретную реализацию, связанную с endpoint
                var implementation = _endpointServices.First(impl => impl.GetType() == endpoint);

                await implementation.HandleAsync(message, cancellationToken);
            }
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
