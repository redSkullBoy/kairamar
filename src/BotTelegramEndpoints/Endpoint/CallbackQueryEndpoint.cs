using Telegram.Bot;
using Telegram.Bot.Types;

namespace BotTelegramEndpoints.Endpoint
{
    public abstract class CallbackQueryEndpoint : BaseEndpoint<CallbackQuery>
    {
        public CallbackQueryEndpoint(ITelegramBotClient botClient) : base(botClient)
        {
        }
    }
}
