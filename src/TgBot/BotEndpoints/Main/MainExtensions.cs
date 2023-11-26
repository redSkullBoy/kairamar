﻿using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Receiveds;

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
            services.AddSingleton(typeof(TEndpoint), serviceType);
        }

        return services;
    }

    public static IApplicationBuilder UseBotEndpoint(this IApplicationBuilder app)
    {
        RegisterStrategy<MessageReceivedStrategy, MessageEndpoint, Message>(app);
        RegisterStrategy<CallbackQueryReceivedStrategy, CallbackQueryEndpoint, CallbackQuery>(app);
        RegisterStrategy<InlineQueryReceivedStrategy, InlineQueryEndpoint, InlineQuery>(app);
        RegisterStrategy<ChosenInlineResultReceivedStrategy, ChosenInlineResultEndpoint, ChosenInlineResult>(app);

        return app;
    }

    private static void RegisterStrategy<TStrategy, TEndpoint, TBotType>(IApplicationBuilder app) where TStrategy : notnull, IReceivedStrategy 
        where TEndpoint : notnull 
        where TBotType : notnull
    {
        var regEndpoints = new Dictionary<string, Type>();
        var botClient = app.ApplicationServices.GetRequiredService<ITelegramBotClient>();

        var queryReceivedStrategy = app.ApplicationServices.GetRequiredService<TStrategy>();

        var endpointServices = (IEnumerable<BaseEndpoint<TBotType>>)app.ApplicationServices.GetServices(typeof(TEndpoint));

        foreach (var endpointService in endpointServices)
        {
            endpointService.BotClient = botClient;

            var routes = endpointService.Definition.Routes;

            if (routes == null)
                throw new ArgumentNullException(nameof(routes));

            foreach (var route in routes)
            {
                regEndpoints.Add(route, endpointService.GetType());
            }

        }

        queryReceivedStrategy.Register(regEndpoints);
    }
}