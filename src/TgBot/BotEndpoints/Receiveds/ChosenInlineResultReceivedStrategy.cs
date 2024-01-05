using Telegram.Bot.Types;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;

namespace TgBot.BotEndpoints.Receiveds;

public class ChosenInlineResultReceivedStrategy : IReceivedStrategy
{
    private Dictionary<string, Type> _endpoints = new Dictionary<string, Type>();
    private Dictionary<Type, string> _userStates = new();

    private readonly ILogger<ChosenInlineResultReceivedStrategy> _logger;
    private readonly IEnumerable<ChosenInlineResultEndpoint> _endpointServices;
    private readonly IUserBotService _botUserService;

    public ChosenInlineResultReceivedStrategy(ILogger<ChosenInlineResultReceivedStrategy> logger
        , IEnumerable<ChosenInlineResultEndpoint> endpointServices, IUserBotService botUserService)
    {
        _logger = logger;
        _endpointServices = endpointServices;
        _botUserService = botUserService;
    }

    public async Task HandleAsync(Update update, CancellationToken cancellationToken)
    {
        var requestData = update.ChosenInlineResult!;
        var userId = update.ChosenInlineResult!.From.Id;

        _logger.LogInformation("Received inline result: {ChosenInlineResultId}", requestData.ResultId);

        var userState = _botUserService.GetStateOrNull(update.ChosenInlineResult!.From!.Id);
        //если нет статуса то ищем по endpoints
        if (string.IsNullOrWhiteSpace(userState))
        {
            if (_endpoints!.TryGetValue(requestData.Query!, out var endpointWithoutStatus))
            {
                // Выберите конкретную реализацию, связанную с endpoint
                var implementation = _endpointServices.First(impl => impl.GetType() == endpointWithoutStatus);

                await implementation.HandleAsync(requestData, cancellationToken);
            }
            return;
        }
        //Проверка на состояние
        var keyValue = _userStates!.Single(x => x.Value == userState);

        if (keyValue.Key != null)
        {
            // Выберите конкретную реализацию, связанную с endpoint
            var implementation = _endpointServices.First(impl => impl.GetType() == keyValue.Key);

            await implementation.HandleAsync(requestData, cancellationToken);
        }

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
