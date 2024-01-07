using DataAccess.Sqlite;
using Domain.Entities.Model;
using Microsoft.AspNetCore.Identity;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;
using TgBot.Services;

namespace TgBot.Endpoints.Trips;

public class AddDeparturePointEndpoint : MessageEndpoint
{
    private readonly IUserBotService _userBotService;
    private readonly ITelegramBotClient _botClient;
    private readonly MemoryCacheService _cache; 
    private readonly UserManager<AppUser> _userManager;

    public AddDeparturePointEndpoint(IUserBotService userBotService, ITelegramBotClient botClient, MemoryCacheService cache, UserManager<AppUser> userManager)
    {
        _userBotService = userBotService;
        _botClient = botClient;
        _cache = cache;
        _userManager = userManager;
    }

    public override void Configure()
    {
        State("AddDeparturePoint");
    }

    public override async Task HandleAsync(Message message, CancellationToken cancellationToken)
    {
        _cache.SetTrip(message.From.Id, new Trip(), TimeSpan.FromMinutes(5));

        await _botClient.SendTextMessageAsync(
            chatId: message!.Chat.Id,
            text: "Введите - Пункт назначения",
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);

        _userBotService.SetState(message.From!.Id, "AddDestinationPoint");
    }
}
