using Telegram.Bot.Types.Enums;
using TgBot.BotEndpoints.Constants;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;

namespace TgBot.BotEndpoints.Receiveds;

public class BaseReceivedStrategy<TRequest, TEndpoint> 
    where TEndpoint : notnull, BaseEndpoint<TRequest>
    where TRequest : notnull
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IUserBotService _botUserService;
    private readonly ReceivedDefinition _receivedDef;

    public BaseReceivedStrategy(IServiceScopeFactory serviceScopeFactory
        , IUserBotService botUserService, ReceivedDefinition receivedDef)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _botUserService = botUserService;
        _receivedDef = receivedDef;
    }

    public async Task ProcessAsync(TRequest request, long userId, string key, UpdateType type, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var endpointServices = (IEnumerable<TEndpoint>)scope.ServiceProvider.GetServices(typeof(TEndpoint));

        var userState = _botUserService.GetStateOrNull(userId);
        //если нет статуса то ищем по endpoints
        if (string.IsNullOrWhiteSpace(userState))
        {
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

            return;
        }
        //Проверка на состояние
        if (_receivedDef.TryGetValueState(userState, out var endpointWithoutState))
        {
            var baseType = typeof(BaseEndpoint<>);

            using var scope1 = _serviceScopeFactory.CreateScope();

            var matchingServices = scope.ServiceProvider.GetServices<object>()
                .Where(service => service.GetType().IsGenericType &&
                                  service.GetType().GetGenericTypeDefinition() == baseType)
                .ToList();

            var implementation = endpointServices.First(impl => impl.GetType() == endpointWithoutState);
            await implementation.HandleAsync(request, cancellationToken);
        }
        // Для DefaultEndpoint
        else if (_receivedDef.TryGetValueEndpoint(BaseEndpointConst.DEFAULT, type, out var endpoint))
        {
            var implementation = endpointServices.First(impl => impl.GetType() == endpoint);
            await implementation.HandleAsync(request, cancellationToken);
        }
    }
}
