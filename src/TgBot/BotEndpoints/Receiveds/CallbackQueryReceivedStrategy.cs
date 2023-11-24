using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.BotEndpoints.Endpoints;

namespace TgBot.BotEndpoints.Receiveds;

public class CallbackQueryReceivedStrategy : IReceivedStrategy
{
    private Dictionary<string, Type> _endpoints = new Dictionary<string, Type>();

    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<CallbackQueryReceivedStrategy> _logger;
    private readonly IServiceProvider _serviceProvider;

    public CallbackQueryReceivedStrategy(ITelegramBotClient botClient, ILogger<CallbackQueryReceivedStrategy> logger, IServiceProvider serviceProvider)
    {
        _botClient = botClient;
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task<Message> HandleAsync(Update request, CancellationToken cancellationToken)
    {
        var callbackQuery = request.CallbackQuery!;

        _logger.LogInformation("Received inline keyboard callback from: {CallbackQueryId}", callbackQuery.Id);

        if (_endpoints!.TryGetValue(callbackQuery.Data!, out var endpoint))
        {
            using var scope = _serviceProvider.CreateScope();
            var endpointServices = (IEnumerable<CallbackQueryEndpoint>)scope.ServiceProvider.GetServices(typeof(CallbackQueryEndpoint));

            // Выберите конкретную реализацию, связанную с endpoint
            var implementation = endpointServices.FirstOrDefault(impl => impl.GetType() == endpoint);

            return await implementation!.HandleAsync(callbackQuery, cancellationToken);
        }

        throw new NotImplementedException();
    }

    public void Register(Dictionary<string, Type> endpoints)
    {
        foreach (var endpoint in endpoints)
        {
            _endpoints.Add(endpoint.Key, endpoint.Value);
        }
    }
}
