using BotTelegramEndpoints.Endpoint;
using System;

namespace BotTelegramEndpoints.Factories
{
    public interface IEndpointFactory
    {
        CallbackQueryEndpoint Create(string route);
        void Register(string route, Type type);
    }
}
