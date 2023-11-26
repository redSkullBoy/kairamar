using Telegram.Bot.Types;
using TgBot.BotEndpoints.Endpoints;

namespace TgBot.BotEndpoints.Receiveds;

public class ChosenInlineResultReceivedStrategy : IReceivedStrategy
{
    private Dictionary<string, Type> _endpoints = new Dictionary<string, Type>();

    private readonly ILogger<ChosenInlineResultReceivedStrategy> _logger;
    private readonly IEnumerable<ChosenInlineResultEndpoint> _endpointServices;

    public ChosenInlineResultReceivedStrategy(ILogger<ChosenInlineResultReceivedStrategy> logger
        , IEnumerable<ChosenInlineResultEndpoint> endpointServices)
    {
        _logger = logger;
        _endpointServices = endpointServices;
    }

    public async Task HandleAsync(Update request, CancellationToken cancellationToken)
    {
        var requestData = request.ChosenInlineResult!;

        _logger.LogInformation("Received inline result: {ChosenInlineResultId}", requestData.ResultId);

        if (_endpoints!.TryGetValue(requestData.Query!, out var endpoint))
        {
            // Выберите конкретную реализацию, связанную с endpoint
            var implementation = _endpointServices.First(impl => impl.GetType() == endpoint);

            await implementation.HandleAsync(requestData, cancellationToken);
        }

        throw new NotImplementedException("Для такого запроса не найден Endpoint");
    }

    public void Register(Dictionary<string, Type> endpoints)
    {
        foreach (var endpoint in endpoints)
        {
            _endpoints.Add(endpoint.Key, endpoint.Value);
        }
    }
}
