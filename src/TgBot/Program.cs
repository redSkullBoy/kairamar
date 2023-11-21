using Ardalis.Result;
using BotTelegramEndpoints;
using BotTelegramEndpoints.Endpoint;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Endpoints.Initiator;
using TgBot.Services;
using BotTelegramEndpoints.Main;
using System.Reflection;

namespace TgBot;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Setup Bot configuration
        var botConfigurationSection = builder.Configuration.GetSection(BotConfiguration.Configuration);
        builder.Services.Configure<BotConfiguration>(botConfigurationSection);

        var botConfiguration = botConfigurationSection.Get<BotConfiguration>();

        builder.Services.AddHttpClient("telegram_bot_client")
                .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
                {
                    BotConfiguration? botConfig = sp.GetService<IOptions<BotConfiguration>>()?.Value;
                    TelegramBotClientOptions options = new(botConfig.BotToken);
                    return new TelegramBotClient(options, httpClient);
                });

        // business-logic service
        builder.Services.AddScoped<UpdateHandlers>();
        //

        builder.Services.AddBotEndpoints();
        builder.Services.AddSingleton<StartEndpoint>();

        builder.Services.AddHostedService<ConfigureWebhook>();

        builder.Services
                .AddControllers()
                .AddNewtonsoftJson();

        var app = builder.Build();

        var callback = app.Services.GetService<CallbackQueryContext>();


        // Получаем текущую сборку (или другие сборки, если требуется)
        var assembly = Assembly.GetExecutingAssembly();

        callback!.RegisterEndpoint(typeof(StartEndpoint), assembly);

        app.UseBotEndpoints();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Bot}/{action=Post}");

        app.Run();
    }
}
