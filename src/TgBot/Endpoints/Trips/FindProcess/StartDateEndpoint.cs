using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;
using TgBot.Constants;
using TgBot.Services;
using UseCases.Handlers.Trips.Dto;

namespace TgBot.Endpoints.Trips.FindProcess;

public class StartDateEndpoint : CallbackQueryEndpoint
{
    private readonly IUserBotService _userBotService;
    private readonly ITelegramBotClient _botClient;
    private readonly MemoryCacheService _cache;

    public StartDateEndpoint(IUserBotService userBotService, ITelegramBotClient botClient, MemoryCacheService cache)
    {
        _userBotService = userBotService;
        _botClient = botClient;
        _cache = cache;
    }

    public override void Configure()
    {
        State(UserStates.TripFindStartDate);
    }

    public override async Task HandleAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        await _botClient.AnswerCallbackQueryAsync(
            callbackQueryId: callbackQuery.Id,
            cancellationToken: cancellationToken);

        var startData = new DateTime();

        switch (callbackQuery.Data)
        {
            case "today":
                startData = DateTime.Now;
                break;
            case "tomorrow":
                startData = DateTime.Now.AddDays(1);
                break;
            case "afterTomorrow":
                startData = DateTime.Now.AddDays(2);
                break;
        }

        var filter = _cache.GetTripFilterOrNull(callbackQuery.From!.Id) ?? new TripFilter();

        filter.StartDateLocal = new DateTime(startData.Year, startData.Month, startData.Day, 0, 0, 0);

        _cache.SetTripFilter(callbackQuery.From!.Id, filter, TimeSpan.FromMinutes(5));

        await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: "Введите - время. Пример: 16 20",
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);

        _userBotService.NetxState(callbackQuery.From!.Id);
    }
}
