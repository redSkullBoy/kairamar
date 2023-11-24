using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.BotEndpoints.Endpoints.Auxiliary;

namespace TgBot.BotEndpoints.Endpoints
{
    public abstract class BaseEndpoint<TRequest> where TRequest : notnull
    {
        protected readonly ITelegramBotClient _botClient;

        /// <summary>
        /// gets the endpoint definition which contains all the configuration info for the endpoint
        /// </summary>
        public BotDefinition Definition { get; internal set; } = new BotDefinition();//also for extensibility

        public BaseEndpoint(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        /// <summary>
        /// the handler method for the endpoint. this method is called for each request received.
        /// </summary>
        /// <param name="req">the request dto</param>
        /// <param name="ct">a cancellation token</param>
        public virtual Task<Message> HandleAsync(TRequest req, CancellationToken ct) => throw new NotImplementedException();

        /// <summary>
        /// use this method to configure how the endpoint should be listening to incoming requests.
        /// <para>HINT: it is only called once during endpoint auto registration during app startup.</para>
        /// </summary>
        public virtual void Configure()
        => throw new NotImplementedException();

        /// <summary>
        /// specify one or more route patterns this endpoint should be listening for
        /// </summary>
        protected virtual void Routes(params string[] patterns)
            => Definition.Routes = patterns;
    }
}


