using Telegram.Bot.Types.Enums;

namespace TgBot.BotEndpoints.Endpoints.Auxiliary
{
    /// <summary>
    /// represents the configuration settings of an endpoint
    /// </summary>
    public class EndpointDefinition
    {
        public string? UserState { get; internal set; }

        public string[]? Routes { get; internal set; }

        public bool IsPreRoute { get; internal set; }

        public MessageType? MessageType { get; internal set; }
    }
}
