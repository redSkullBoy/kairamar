using BotTelegramEndpoints.Endpoint;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot.Types;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;

namespace BotTelegramEndpoints
{
    public class CallbackQueryContext
    {
        private readonly Dictionary<string, CallbackQueryEndpoint> _registryDict = new Dictionary<string, CallbackQueryEndpoint>();

        public CallbackQueryContext()
        {

        }

        public async Task<Message> HandleAsync(CallbackQuery callbackQuery, CancellationToken ct)
        {
            if (callbackQuery.Data == null)
                return new Message();

            if (_registryDict.TryGetValue(callbackQuery.Data.Split(' ')[0], out var endpoint))
                return await endpoint.HandleAsync(callbackQuery, ct);

            return new Message();
        }

        public void RegisterEndpoints(CallbackQueryEndpoint[] endpoints)
        {
            foreach (var endpoint in endpoints)
            {
                foreach (var route in endpoint.Definition.Routes!)
                    _registryDict.Add(route, endpoint);
            }
        }

        public void RegisterEndpoint(CallbackQueryEndpoint endpoint)
        {
            foreach (var route in endpoint.Definition.Routes!)
                _registryDict.Add(route, endpoint);
        }

        public void RegisterEndpoint(Type type, Assembly assembly)
        {

            // Находим все типы, наследующиеся от MyServiceBase
            var serviceTypes = assembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(CallbackQueryEndpoint))).ToList();

            // Регистрируем каждый найденный тип
            //foreach (var serviceType in serviceTypes)
            //{
            //    services.AddScoped(typeof(MyServiceBase), serviceType);
            //}

            //foreach (var route in endpoint.Definition.Routes!)
            //    _registryDict.Add(route, endpoint);
        }
    }
}
