using Telegram.Bot.Types;
using TgBot.BotEndpoints.Constants;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;

namespace TgBot.BotEndpoints.Receiveds;

public class CallbackQueryReceivedStrategy : IReceivedStrategy
{
    private Dictionary<string, Type> _endpoints = new();
    private Dictionary<Type, string> _userStates = new();

    private readonly ILogger<CallbackQueryReceivedStrategy> _logger;
    private readonly IEnumerable<CallbackQueryEndpoint> _endpointServices;
    private readonly IUserBotService _botUserService;

    public CallbackQueryReceivedStrategy(ILogger<CallbackQueryReceivedStrategy> logger
        , IEnumerable<CallbackQueryEndpoint> endpointServices, IUserBotService botUserService)
    {
        _logger = logger;
        _endpointServices = endpointServices;
        _botUserService = botUserService;
    }

    public async Task HandleAsync(Update update, CancellationToken cancellationToken)
    {
        var callbackQuery = update.CallbackQuery!;

        _logger.LogInformation("Received inline keyboard callback from: {CallbackQueryId}", callbackQuery.Id);

        var userState = _botUserService.GetStateOrNull(update.CallbackQuery!.From!.Id);
        //если нет статуса то ищем по endpoints
        if (string.IsNullOrWhiteSpace(userState))
        {
            if (_endpoints!.TryGetValue(callbackQuery.Data!, out var endpointWithoutStatus))
            {
                var implementation = _endpointServices.First(impl => impl.GetType() == endpointWithoutStatus);
                await implementation.HandleAsync(callbackQuery, cancellationToken);
            }
            // Для DefaultEndpoint
            else if(_endpoints!.TryGetValue(BaseEndpointConst.DEFAULT, out var endpoint))
            {
                var implementation = _endpointServices.First(impl => impl.GetType() == endpoint);
                await implementation.HandleAsync(callbackQuery, cancellationToken);
            }

            return;
        }
        //Проверка на состояние
        var keyValue = _userStates!.Single(x => x.Value == userState);

        if (keyValue.Key != null)
        {
            var implementation = _endpointServices.First(impl => impl.GetType() == keyValue.Key);
            await implementation.HandleAsync(callbackQuery, cancellationToken);
        }
        // Для DefaultEndpoint
        else if (_endpoints!.TryGetValue(BaseEndpointConst.DEFAULT, out var endpoint))
        {
            var implementation = _endpointServices.First(impl => impl.GetType() == endpoint);
            await implementation.HandleAsync(callbackQuery, cancellationToken);
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
