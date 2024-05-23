using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;
using TgBot.Constants;

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

        string info = $"""
                            Для поиска поездки укажите следующую информацию:
                            - Пункт отправления
                            - Пункт назначения
                            - Дату и время отправления

                            Введите - Пункт отправления
                            """;

        await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: info,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);

        _userBotService.SetProcess(callbackQuery.From.Id, UserProcesses.TripFind);
    }
}
