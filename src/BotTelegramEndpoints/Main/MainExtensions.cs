using BotTelegramEndpoints.Endpoint;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace BotTelegramEndpoints.Main
{
    public static class MainExtensions
    {
        public static IServiceCollection AddBotEndpoints(this IServiceCollection services, Action<BotDiscoveryOptions>? options = null)
        {
            services.AddSingleton<EndpointContext>();
            services.AddSingleton<CallbackQueryContext>();

            var opts = new BotDiscoveryOptions();
            options?.Invoke(opts);

            if (opts.DisableAutoDiscovery && opts.Assemblies?.Any() is false)
                throw new InvalidOperationException("If 'DisableAutoDiscovery' is true, a collection of `Assemblies` must be provided!");

            foreach(var assemblie in opts.Assemblies)
            {
                var endpointTypes = Assembly.GetExecutingAssembly().GetTypes()
                                        .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(CallbackQueryEndpoint)));

                foreach (var endpointType in endpointTypes)
                {
                    services.AddSingleton(endpointType);
                }
            }

            return services;
        }

        public static IApplicationBuilder UseBotEndpoints(this IApplicationBuilder app)
        {
            var callback = app.ApplicationServices.GetService<CallbackQueryContext>();

            //callback!.RegisterEndpoint(typeof(StartEndpoint));
            //var types = typeof(CallbackQueryEndpoint).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(CallbackQueryEndpoint))).ToList();

            //var startEndpoints = app.ApplicationServices.GetServices<CallbackQueryEndpoint>();
            //foreach (var endpoint in startEndpoints)
            //{
            //    endpoint.Configure();
            //    callback!.RegisterEndpoint(endpoint);
            //}

            return app;
        }
    }
}
