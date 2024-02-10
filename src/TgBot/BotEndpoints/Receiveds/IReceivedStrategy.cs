﻿using Telegram.Bot.Types;

namespace TgBot.BotEndpoints.Receiveds;

public interface IReceivedStrategy
{
    public string? GetUserState(Update update);

    public Task HandleEndpointAsync(Update update, CancellationToken cancellationToken);

    public Task HandleUserStateAsync(Update update, Type userStateEndpoint, string userState, CancellationToken cancellationToken);

    public Task HandleDefaultUserStateAsync(Update update, string userState, CancellationToken cancellationToken);
}
