using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBot.BotEndpoints.Endpoints
{
    public abstract class MessageEndpoint : BaseEndpoint<Message>
    {
        public MessageEndpoint(ITelegramBotClient botClient) : base(botClient)
        {
        }
    }
}
