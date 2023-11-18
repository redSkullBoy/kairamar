using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;

namespace TgBot.Services
{
    public interface IReceivedStrategy
    {
        public abstract Task<Message> HandleAsync(Update request, CancellationToken cancellationToken);
    }
}
