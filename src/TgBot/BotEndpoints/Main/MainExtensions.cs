using System.Reflection;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Receiveds;

namespace TgBot.BotEndpoints.Main;

public static class MainExtensions
{
    public static IServiceCollection AddBotEndpoint(this IServiceCollection services)
    {
        // Получаем текущую сборку (или другие сборки, если требуется)
        var assembly = Assembly.GetExecutingAssembly();

        services.AddSingleton<ReceivedContext>();
        services.AddSingleton<MessageReceivedStrategy>();
        services.AddSingleton<CallbackQueryReceivedStrategy>();

        // Находим все типы, наследующиеся от CallbackQueryEndpoint
        var endpoints = assembly.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(CallbackQueryEndpoint)));

        // Регистрируем каждый найденный тип
        foreach (var serviceType in endpoints)
        {
            services.AddSingleton(typeof(CallbackQueryEndpoint), serviceType);
        }

        return services;
    }

    public static IApplicationBuilder UseBotEndpoint(this IApplicationBuilder app)
    {
        var regEndpoints = new Dictionary<string, Type>();

        var queryReceivedStrategy = app.ApplicationServices.GetRequiredService<CallbackQueryReceivedStrategy>();

        var endpointServices = (IEnumerable<CallbackQueryEndpoint>)app.ApplicationServices.GetServices(typeof(CallbackQueryEndpoint));

        foreach (var endpointService in endpointServices)
        {
            endpointService.Configure();
            var routes = endpointService.Definition.Routes;

            if (routes != null)
            {
                throw new ArgumentNullException(nameof(routes));
            }

            foreach (var route in routes)
            {
                regEndpoints.Add(route, endpointService.GetType());
            }

        }

        queryReceivedStrategy.Register(regEndpoints);

        RegisterStrategy<MessageReceivedStrategy, MessageEndpoint>(app);

        return app;
    }

    public static void RegisterStrategy<TStrategy, TEndpoint>(IApplicationBuilder app) where TStrategy : notnull, IReceivedStrategy where TEndpoint : notnull
    {
        var regEndpoints = new Dictionary<string, Type>();

        var queryReceivedStrategy = app.ApplicationServices.GetRequiredService<TStrategy>();

        var endpointServices = (IEnumerable<BaseEndpoint>)app.ApplicationServices.GetServices(typeof(TEndpoint));

        foreach (var endpointService in endpointServices)
        {
            endpointService.Configure();
            var routes = endpointService.Definition.Routes;

            if (routes != null)
            {
                throw new ArgumentNullException(nameof(routes));
            }

            foreach (var route in routes)
            {
                regEndpoints.Add(route, endpointService.GetType());
            }

        }

        queryReceivedStrategy.Register(regEndpoints);
    }
}
