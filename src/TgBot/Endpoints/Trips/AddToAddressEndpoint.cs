using DataAccess.Sqlite;
using Domain.Entities.Model;
using Microsoft.AspNetCore.Identity;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;
using TgBot.Constants;
using TgBot.Services;

namespace TgBot.Endpoints.Trips;

public class AddToAddressEndpoint : CallbackQueryEndpoint
{
    private readonly IUserBotService _userBotService;
    private readonly ITelegramBotClient _botClient;
    private readonly MemoryCacheService _cache; 
    private readonly UserManager<AppUser> _userManager;

    public AddToAddressEndpoint(IUserBotService userBotService, ITelegramBotClient botClient, MemoryCacheService cache, UserManager<AppUser> userManager)
    {
        _userBotService = userBotService;
        _botClient = botClient;
        _cache = cache;
        _userManager = userManager;
    }

    public override void Configure()
    {
        State(UserStates.AddToAddress);
    }

    public override async Task HandleAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        await _botClient.AnswerCallbackQueryAsync(
            callbackQueryId: callbackQuery.Id,
            cancellationToken: cancellationToken);

        var trip = _cache.GetTripOrNull(callbackQuery.From!.Id) ?? new Trip();

        trip.ToAddressId = int.Parse(callbackQuery.Data);

        _cache.SetTrip(callbackQuery.From!.Id, trip, TimeSpan.FromMinutes(5));

        await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: "Введите - Пункт назначения",
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);

        _userBotService.NetxState(callbackQuery.From!.Id);
    }
}
