using ApplicationServices.Implementation;
using DataAccess.Sqlite;
using Infrastructure.Implementation;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using TgBot.BotEndpoints.Main;
using TgBot.BotEndpoints.Services;
using TgBot.Services;
using UseCases;
using Utils.Modules;

namespace TgBot;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Setup Bot configuration
        var botConfigurationSection = builder.Configuration.GetSection(BotConfiguration.Configuration);
        builder.Services.Configure<BotConfiguration>(botConfigurationSection);
        // Add project modules
        builder.Services.RegisterModule<DataAccessModule>(builder.Configuration);
        builder.Services.RegisterModule<InfrastructureModule>(builder.Configuration);
        builder.Services.RegisterModule<ApplicationServicesModule>(builder.Configuration);
        builder.Services.RegisterModule<UseCasesModule>(builder.Configuration);

        var botConfiguration = botConfigurationSection.Get<BotConfiguration>();

        builder.Services.AddHttpClient("telegram_bot_client")
                .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
                {
                    BotConfiguration? botConfig = sp.GetService<IOptions<BotConfiguration>>()?.Value;
                    TelegramBotClientOptions options = new(botConfig.BotToken);
                    return new TelegramBotClient(options, httpClient);
                });

        builder.Services.AddSingleton<IUserBotService, UserBotService>();

        //BotEndpoints
        builder.Services.AddBotEndpoint();

        builder.Services.AddHostedService<ConfigureWebhook>();

        builder.Services
                .AddControllers()
                .AddNewtonsoftJson();

        var app = builder.Build();

        app.UseBotEndpoint();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Bot}/{action=Post}");

        app.Run();
    }
}
