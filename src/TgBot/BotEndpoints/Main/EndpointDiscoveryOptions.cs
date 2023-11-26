namespace TgBot.BotEndpoints.Main;

/// <summary>
/// defines how endpoint discovery and registration should be done at startup
/// </summary>
public sealed class EndpointDiscoveryOptions
{
    public string? HttpClientName { get; set; }
}
