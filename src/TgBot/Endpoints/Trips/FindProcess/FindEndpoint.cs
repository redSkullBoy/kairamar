using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;
using TgBot.Constants;
using TgBot.Templates;

namespace TgBot.Endpoints.Trips.FindProcess;

public class FindEndpoint : CallbackQueryEndpoint
{
    private readonly IUserBotService _userBotService;
    private readonly ITelegramBotClient _botClient;

    public FindEndpoint(IUserBotService userBotService, ITelegramBotClient botClient)
    {
        _userBotService = userBotService;
        _botClient = botClient;
    }

    public override void Configure()
    {
        Routes("findTrips");
    }

    public override async Task HandleAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        await _botClient.AnswerCallbackQueryAsync(
            callbackQueryId: callbackQuery.Id,
            cancellationToken: cancellationToken);

        await _botClient.SendTripFindInfo(callbackQuery.Message!.Chat.Id, cancellationToken, "Введите - Пункт отправления");

        _userBotService.SetProcess(callbackQuery.From.Id, UserProcesses.TripFind);
    }
}
