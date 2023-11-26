using Telegram.Bot.Types;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;

namespace TgBot.BotEndpoints.Receiveds;

public class CallbackQueryReceivedStrategy : IReceivedStrategy
{
    private Dictionary<string, Type> _endpoints = new();
    private Dictionary<Type, string> _userStates = new();

    private readonly ILogger<CallbackQueryReceivedStrategy> _logger;
    private readonly IEnumerable<CallbackQueryEndpoint> _endpointServices;
    private readonly IBotUserService _botUserService;

    public CallbackQueryReceivedStrategy(ILogger<CallbackQueryReceivedStrategy> logger
        , IEnumerable<CallbackQueryEndpoint> endpointServices, IBotUserService botUserService)
    {
        _logger = logger;
        _endpointServices = endpointServices;
        _botUserService = botUserService;
    }

    public async Task HandleAsync(Update update, CancellationToken cancellationToken)
    {
        var callbackQuery = update.CallbackQuery!;
        var userId = update.CallbackQuery!.From.Id;

        _logger.LogInformation("Received inline keyboard callback from: {CallbackQueryId}", callbackQuery.Id);

        if (_endpoints!.TryGetValue(callbackQuery.Data!, out var endpoint))
        {
            //Проверка на состояние
            if (_userStates!.TryGetValue(endpoint, out var state))
            {
                var userState = _botUserService.GetUserState(userId);

                if(userState == state)
                {
                    // Выберите конкретную реализацию, связанную с endpoint
                    var implementation = _endpointServices.First(impl => impl.GetType() == endpoint);

                    await implementation.HandleAsync(callbackQuery, cancellationToken);
                }
            }
        }

        _logger.LogInformation("Unknown CallbackQuery type: {MessageType}", update.Type);
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
