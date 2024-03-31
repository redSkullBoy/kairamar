using System.Reflection;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgBot.BotEndpoints.Constants;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Receiveds;
using Utils.Extensions;

namespace TgBot.BotEndpoints.Main;

public static class MainExtensions
{
    public static IServiceCollection AddBotEndpoint(this IServiceCollection services, Action<EndpointDiscoveryOptions>? options = null)
    {
        var opts = new EndpointDiscoveryOptions();
        options?.Invoke(opts);

        // Получаем текущую сборку (или другие сборки, если требуется)
        var assembly = Assembly.GetExecutingAssembly();

        var endpointData = new EndpointData(opts);

        services.AddSingleton(endpointData);
        services.AddSingleton<ReceivedContext>();
        services.AddSingleton<ReceivedDefinition>();

        services.AddSingleton<MessageReceivedStrategy>();
        services.AddSingleton<CallbackQueryReceivedStrategy>();
        services.AddSingleton<InlineQueryReceivedStrategy>();
        services.AddSingleton<ChosenInlineResultReceivedStrategy>();

        services.AddEndpoints<MessageEndpoint>(assembly);
        services.AddEndpoints<CallbackQueryEndpoint>(assembly);
        services.AddEndpoints<InlineQueryEndpoint>(assembly); 
        services.AddEndpoints<ChosenInlineResultEndpoint>(assembly);

        return services;
    }

    private static IServiceCollection AddEndpoints<TEndpoint>(this IServiceCollection services, Assembly assembly)
    {
        // Находим все типы, наследующиеся от TEndpoint
        var endpoints = assembly.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(TEndpoint)));

        // Регистрируем каждый найденный тип
        foreach (var serviceType in endpoints)
        {
            services.AddScoped(typeof(TEndpoint), serviceType);
        }

        return services;
    }

    public static IApplicationBuilder UseBotEndpoint(this IApplicationBuilder app)
    {
        var receivedDefinition = app.ApplicationServices.GetRequiredService<ReceivedDefinition>();

        RegisterStrategy<MessageEndpoint, Message>(app, receivedDefinition, UpdateType.Message);
        RegisterStrategy<CallbackQueryEndpoint, CallbackQuery>(app, receivedDefinition, UpdateType.CallbackQuery);
        RegisterStrategy<InlineQueryEndpoint, InlineQuery>(app, receivedDefinition, UpdateType.InlineQuery);
        RegisterStrategy<ChosenInlineResultEndpoint, ChosenInlineResult>(app, receivedDefinition, UpdateType.ChosenInlineResult);

        return app;
    }

    private static void RegisterStrategy<TEndpoint, TBotType>(IApplicationBuilder app, ReceivedDefinition receivedDefinition, UpdateType type) 
        where TEndpoint : notnull 
        where TBotType : notnull
    {
        var regEndpoints = new Dictionary<string, (Type type, bool isPreRoute)>();
        var userStatesEndpoints = new Dictionary<string, Type>();

        using var scope = app.ApplicationServices.CreateScope();
        var endpointServices = (IEnumerable<BaseEndpoint<TBotType>>)scope.ServiceProvider.GetServices(typeof(TEndpoint));

        foreach (var endpointService in endpointServices)
        {
            var routes = endpointService.Definition.Routes;
            var userState = endpointService.Definition.UserState;

            if (routes == null && userState == null)
                throw new ArgumentNullException(nameof(userState));

            foreach (var route in routes.OrEmptyIfNull())
            {
                regEndpoints.Add(route, (endpointService.GetType(), endpointService.Definition.IsPreRoute));
            }

            #region UserStates
            if (userState != null)
            {
                userStatesEndpoints.Add(userState, endpointService.GetType());
            }
            #endregion
        }

        if (!regEndpoints!.TryGetValue(BaseEndpointConst.DEFAULT, out var endpoint))
            throw new Exception($"добавьте endpoint по умолчанию для всех типов сообщений");

        receivedDefinition.AddEndpoints(regEndpoints, type);
        receivedDefinition.AddStates(userStatesEndpoints);
    }
}
