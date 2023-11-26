using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;

namespace TgBot.BotEndpoints.Receiveds;

public interface IReceivedStrategy
{
    public abstract Task HandleAsync(Update request, CancellationToken cancellationToken);

    public void Register(Dictionary<string, Type> endpoints);
}
