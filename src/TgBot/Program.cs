using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Services;

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

        // Dummy business-logic service
        builder.Services.AddScoped<UpdateHandlers>();

        builder.Services.AddHostedService<ConfigureWebhook>();

        builder.Services
                .AddControllers()
                .AddNewtonsoftJson();

        var app = builder.Build();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Bot}/{action=Post}");

        app.Run();
    }
}
