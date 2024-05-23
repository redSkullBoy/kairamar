using Domain.Entities;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;
using TgBot.Constants;
using TgBot.Services;

namespace TgBot.Endpoints.Trips.AddProcess;

public class AddStartDateEndpoint : CallbackQueryEndpoint
{
    private readonly IUserBotService _userBotService;
    private readonly ITelegramBotClient _botClient;
    private readonly MemoryCacheService _cache;
    private readonly IDateTime _dateTime;
    private readonly UserManager<AppUser> _userManager;

    public AddStartDateEndpoint(IUserBotService userBotService, ITelegramBotClient botClient, MemoryCacheService cache, 
        IDateTime dateTime, UserManager<AppUser> userManager)
    {
        _userBotService = userBotService;
        _botClient = botClient;
        _cache = cache;
        _dateTime = dateTime;
        _userManager = userManager;
    }

    public override void Configure()
    {
        State(UserStates.TripAddStartDate);
    }

    public override async Task HandleAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        await _botClient.AnswerCallbackQueryAsync(
            callbackQueryId: callbackQuery.Id,
            cancellationToken: cancellationToken);
        var startData = new DateTime();

        var user = await _userManager.FindByNameAsync(callbackQuery.From!.Username!);

        switch (callbackQuery.Data)
        {
            case "today":
                startData = _dateTime.TimeZoneNow(user!.TimeZoneId);
                break;
            case "tomorrow":
                startData = _dateTime.TimeZoneNow(user!.TimeZoneId).AddDays(1);
                break;
            case "afterTomorrow":
                startData = _dateTime.TimeZoneNow(user!.TimeZoneId).AddDays(2);
                break;
        }

        var trip = _cache.GetTripOrNull(callbackQuery.From!.Id)!;

        trip.StartDateLocal = new DateTime(startData.Year, startData.Month, startData.Day, 0, 0, 0);

        _cache.SetTrip(callbackQuery.From!.Id, trip, TimeSpan.FromMinutes(5));

        await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: "Введите - время. Пример: 16 20",
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);

        _userBotService.NetxState(callbackQuery.From!.Id);
    }
}
