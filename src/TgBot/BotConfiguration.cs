namespace TgBot;

public class BotConfiguration
{
    public static readonly string Configuration = "BotConfiguration";

    public string BotToken { get; init; } = default!;
    public string HostAddress { get; init; } = default!;
    public string Route { get; init; } = default!;
    public string SecretToken { get; init; } = default!;
    /// <summary>If this time has been passed since the user enter event, the event won't be processed.</summary>
    /// <remarks>
    /// Useful for cases when the bot goes offline for a significant amount of time, and receives outdated events
    /// after getting back online.
    /// </remarks>
    public TimeSpan ProcessEventTimeout { get; set; } = TimeSpan.FromMinutes(4.0);
}
