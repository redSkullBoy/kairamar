using Microsoft.VisualBasic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TgBot.Services
{
    public class ReceivedContext
    {
        private readonly Dictionary<UpdateType, IReceivedStrategy> _strategies;

        public ReceivedContext(MessageReceivedStrategy _messageReceivedStrategy)
        {
            _strategies = new Dictionary<UpdateType, IReceivedStrategy>
            {
                { UpdateType.Message , _messageReceivedStrategy},
            };
        }

        public async Task<Message> HandleAsync(Update update, CancellationToken cancellationToken)
        {
            if (_strategies.TryGetValue(update.Type, out var contextStrategy))
            {
                return await contextStrategy.HandleAsync(update, cancellationToken);
            }

            throw new NotSupportedException($"Strategy {update} is not supported");
        }
    }
}
