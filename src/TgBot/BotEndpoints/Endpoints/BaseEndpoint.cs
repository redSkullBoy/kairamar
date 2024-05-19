using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.BotEndpoints.Endpoints.Auxiliary;

namespace TgBot.BotEndpoints.Endpoints
{
    public abstract class BaseEndpoint<TRequest> where TRequest : notnull
    {
        /// <summary>
        /// gets the endpoint definition which contains all the configuration info for the endpoint
        /// </summary>
        public EndpointDefinition Definition { get; internal set; } = new EndpointDefinition();//also for extensibility

        public BaseEndpoint()
        {
            Configure();
        }

        /// <summary>
        /// Для вызова основного хендлера
        /// </summary>
        /// <param name="preBotClient"></param>
        /// <param name="chatId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task PreparationAsync(ITelegramBotClient preBotClient, long chatId, CancellationToken cancellationToken)
            => throw new NotImplementedException();

        /// <summary>
        /// the handler method for the endpoint. this method is called for each request received.
        /// </summary>
        /// <param name="req">the request dto</param>
        /// <param name="ct">a cancellation token</param>
        public virtual Task HandleAsync(TRequest req, CancellationToken ct) => throw new NotImplementedException();

        public virtual Task DefaultHandleAsync(TRequest req, string userState, CancellationToken ct) => throw new NotImplementedException();

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

        protected virtual void State(string state)
            => Definition.UserState = state;
        /// <summary>
        /// Срабатывает не смотря на состояние пользователя
        /// </summary>
        /// <param name="patterns"></param>
        protected virtual void PreRoutes(params string[] patterns)
        {
            Definition.Routes = patterns;
            Definition.IsPreRoute = true;
        }

        protected virtual void SetMessageType(MessageType type)
            => Definition.MessageType = type;
    }
}


