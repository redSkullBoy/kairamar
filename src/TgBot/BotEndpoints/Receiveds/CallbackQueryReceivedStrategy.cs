﻿using Telegram.Bot.Types;
using TgBot.BotEndpoints.Endpoints;

namespace TgBot.BotEndpoints.Receiveds;

public class CallbackQueryReceivedStrategy : IReceivedStrategy
{
    private Dictionary<string, Type> _endpoints = new Dictionary<string, Type>();

    private readonly ILogger<CallbackQueryReceivedStrategy> _logger;
    private readonly IEnumerable<CallbackQueryEndpoint> _endpointServices;

    public CallbackQueryReceivedStrategy(ILogger<CallbackQueryReceivedStrategy> logger
        , IEnumerable<CallbackQueryEndpoint> endpointServices)
    {
        _logger = logger;
        _endpointServices = endpointServices;
    }

    public async Task HandleAsync(Update request, CancellationToken cancellationToken)
    {
        var callbackQuery = request.CallbackQuery!;

        _logger.LogInformation("Received inline keyboard callback from: {CallbackQueryId}", callbackQuery.Id);

        if (_endpoints!.TryGetValue(callbackQuery.Data!, out var endpoint))
        {
            // Выберите конкретную реализацию, связанную с endpoint
            var implementation = _endpointServices.First(impl => impl.GetType() == endpoint);

            await implementation.HandleAsync(callbackQuery, cancellationToken);
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