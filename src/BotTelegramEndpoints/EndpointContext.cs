using BotTelegramEndpoints.Endpoint;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace BotTelegramEndpoints
{
    public class EndpointContext
    {
        private readonly CallbackQueryContext _queryContext;
        private readonly ILogger<EndpointContext> _logger;

        public EndpointContext(CallbackQueryContext queryContext, ILogger<EndpointContext> logger)
        {
            _queryContext = queryContext;
        }

        public async Task ExecAsync(Update update, CancellationToken ct)
        {
            var handler = update switch
            {
                // UpdateType.Unknown:
                // UpdateType.ChannelPost:
                // UpdateType.EditedChannelPost:
                // UpdateType.ShippingQuery:
                // UpdateType.PreCheckoutQuery:
                // UpdateType.Poll:
                //{ Message: { } message } => BotOnMessageReceived(message, ct),
                //{ EditedMessage: { } message } => BotOnMessageReceived(message, ct),
                { CallbackQuery: { } callbackQuery } => new CallbackQueryContext().HandleAsync(callbackQuery, ct),
                //{ InlineQuery: { } inlineQuery } => BotOnInlineQueryReceived(inlineQuery, ct),
                //{ ChosenInlineResult: { } chosenInlineResult } => BotOnChosenInlineResultReceived(chosenInlineResult, ct),
                _ => UnknownUpdateHandlerAsync(update, ct)
            };

            await handler;
        }

        private Task UnknownUpdateHandlerAsync(Update update, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
            return Task.CompletedTask;
        }
    }
}
