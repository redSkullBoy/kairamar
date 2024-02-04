using Telegram.Bot.Types.Enums;
using TgBot.BotEndpoints.Constants;
using TgBot.BotEndpoints.Endpoints;

namespace TgBot.BotEndpoints.Receiveds;

public class BaseReceivedStrategy<TRequest, TEndpoint> 
    where TEndpoint : notnull, BaseEndpoint<TRequest>
    where TRequest : notnull
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ReceivedDefinition _receivedDef;

    public BaseReceivedStrategy(IServiceScopeFactory serviceScopeFactory, ReceivedDefinition receivedDef)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _receivedDef = receivedDef;
    }

    public async Task HandleEndpointAsync(TRequest request, string key, UpdateType type, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var endpointServices = (IEnumerable<TEndpoint>)scope.ServiceProvider.GetServices(typeof(TEndpoint));

        if (_receivedDef.TryGetValueEndpoint(key, type, out var endpointWithoutStatus))
        {
            var implementation = endpointServices.First(impl => impl.GetType() == endpointWithoutStatus);
            await implementation.HandleAsync(request, cancellationToken);
        }
        // Для DefaultEndpoint
        else if (_receivedDef.TryGetValueEndpoint(BaseEndpointConst.DEFAULT, type, out var endpoint))
        {
            var implementation = endpointServices.First(impl => impl.GetType() == endpoint);
            await implementation.HandleAsync(request, cancellationToken);
        }
    }

    public async Task HandleUserStateAsync(TRequest request, Type userStateEndpoint, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var endpointServices = (IEnumerable<TEndpoint>)scope.ServiceProvider.GetServices(typeof(TEndpoint));

        var implementation = endpointServices.First(impl => impl.GetType() == userStateEndpoint);
        await implementation.HandleAsync(request, cancellationToken);
    }
}
