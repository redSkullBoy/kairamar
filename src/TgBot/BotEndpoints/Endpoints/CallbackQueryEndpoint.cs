using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBot.BotEndpoints.Endpoints
{
    public abstract class CallbackQueryEndpoint : BaseEndpoint<CallbackQuery>
    {
        public CallbackQueryEndpoint(ITelegramBotClient botClient) : base(botClient)
        {
        }
    }
}
