using Telegram.Bot.Types;
using TgBot.BotEndpoints.Endpoints;

namespace TgBot.BotEndpoints.Receiveds;

public class MessageReceivedStrategy : IReceivedStrategy
{
    private Dictionary<string, Type> _endpoints = new Dictionary<string, Type>();

    private readonly ILogger<MessageReceivedStrategy> _logger;
    private readonly IEnumerable<MessageEndpoint> _endpointServices;

    public MessageReceivedStrategy(ILogger<MessageReceivedStrategy> logger
        , IEnumerable<MessageEndpoint> endpointServices)
    {
        _logger = logger;
        _endpointServices = endpointServices;
    }

    public async Task HandleAsync(Update update, CancellationToken cancellationToken)
    {
        var message = update.Message!;

        _logger.LogInformation("Receive message type: {MessageType}", message.Type);

        if (_endpoints!.TryGetValue(message.Text!, out var endpoint))
        {
            // Выберите конкретную реализацию, связанную с endpoint
            var implementation = _endpointServices.First(impl => impl.GetType() == endpoint);

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
}
