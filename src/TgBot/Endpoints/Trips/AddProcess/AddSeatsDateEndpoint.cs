using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;
using TgBot.Constants;
using TgBot.Services;

namespace TgBot.Endpoints.Trips.AddProcess;

public class AddSeatsDateEndpoint : CallbackQueryEndpoint
{
    private readonly IUserBotService _userBotService;
    private readonly ITelegramBotClient _botClient;
    private readonly MemoryCacheService _cache;

    public AddSeatsDateEndpoint(IUserBotService userBotService, ITelegramBotClient botClient, MemoryCacheService cache)
    {
        _userBotService = userBotService;
        _botClient = botClient;
        _cache = cache;
    }

    public override void Configure()
    {
        State(UserStates.TripAddSeats);
    }

    public override async Task HandleAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        await _botClient.AnswerCallbackQueryAsync(
            callbackQueryId: callbackQuery.Id,
            cancellationToken: cancellationToken);

        if(!int.TryParse(callbackQuery.Data, out var seatCount))
        {
            await _botClient.SendTextMessageAsync(
                chatId: callbackQuery.Message!.Chat.Id,
                text: "Ошибка!!!",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);

            return;
        }

        var trip = _cache.GetTripOrNull(callbackQuery.From!.Id)!;

        trip.RequestedSeats = seatCount;

        _cache.SetTrip(callbackQuery.From!.Id, trip, TimeSpan.FromMinutes(5));

        await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: "Введите - Стоимость поездки",
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);

        _userBotService.NetxState(callbackQuery.From!.Id);
    }
}
