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

    public async Task<bool> HandlePreEndpointAsync(TRequest request, string key, UpdateType type, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var endpointServices = (IEnumerable<TEndpoint>)scope.ServiceProvider.GetServices(typeof(TEndpoint));

        if (_receivedDef.TryGetValueEndpoint(key, type, true, out var endpointWithoutStatus))
        {
            var implementation = endpointServices.First(impl => impl.GetType() == endpointWithoutStatus);
            await implementation.HandleAsync(request, cancellationToken);

            return true;
        }

        return false;
    }

    public async Task HandleEndpointAsync(TRequest request, string key, UpdateType type, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var endpointServices = (IEnumerable<TEndpoint>)scope.ServiceProvider.GetServices(typeof(TEndpoint));

        if (_receivedDef.TryGetValueEndpoint(key, type, false, out var endpointWithoutStatus))
        {
            var implementation = endpointServices.First(impl => impl.GetType() == endpointWithoutStatus);
            await implementation.HandleAsync(request, cancellationToken);
        }
        // Для DefaultEndpoint
        else if (_receivedDef.TryGetValueEndpoint(BaseEndpointConst.DEFAULT, type, false, out var endpoint))
        {
            var implementation = endpointServices.First(impl => impl.GetType() == endpoint);
            await implementation.DefaultHandleAsync(request, string.Empty, cancellationToken);
        }
    }

    public async Task HandleUserStateAsync(TRequest request, Type userStateEndpoint, UpdateType type, string userState, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var endpointServices = (IEnumerable<TEndpoint>)scope.ServiceProvider.GetServices(typeof(TEndpoint));
        //если для этого состояния другой тип сообщения
        var implementation = endpointServices.FirstOrDefault(impl => impl.GetType() == userStateEndpoint);

        if (implementation != null)
        {
            await implementation.HandleAsync(request, cancellationToken);
            return;
        }

        if (_receivedDef.TryGetValueEndpoint(BaseEndpointConst.DEFAULT, type, false, out var endpoint))
        {
            var implementationDef = endpointServices.First(impl => impl.GetType() == endpoint);
            await implementationDef.DefaultHandleAsync(request, userState, cancellationToken);
        }
    }

    public async Task HandleDefaultUserStateAsync(TRequest request, UpdateType type, string userState, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var endpointServices = (IEnumerable<TEndpoint>)scope.ServiceProvider.GetServices(typeof(TEndpoint));

        if (_receivedDef.TryGetValueEndpoint(BaseEndpointConst.DEFAULT, type, false, out var endpoint))
        {
            var implementation = endpointServices.First(impl => impl.GetType() == endpoint);
            await implementation.DefaultHandleAsync(request, userState, cancellationToken);
        }
    }
}
