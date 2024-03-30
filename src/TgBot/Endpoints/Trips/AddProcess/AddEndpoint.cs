using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;
using TgBot.Constants;

namespace TgBot.Endpoints.Trips.AddProcess;

public class AddEndpoint : CallbackQueryEndpoint
{
    private readonly IUserBotService _userBotService;
    private readonly ITelegramBotClient _botClient;

    public AddEndpoint(IUserBotService userBotService, ITelegramBotClient botClient)
    {
        _userBotService = userBotService;
        _botClient = botClient;
    }

    public override void Configure()
    {
        Routes("addTrip");
    }

    public override async Task HandleAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        await _botClient.AnswerCallbackQueryAsync(
            callbackQueryId: callbackQuery.Id,
            cancellationToken: cancellationToken);

        const string info = """
                            Для создания поездки укажите следующую информацию:
                            - Пункт отправления
                            - Пункт назначения
                            - Дату и время отправления
                            - Продолжительность поездки
                            - Количество свободных мест
                            - Стоимость поездки
                            - Описание
                            """;

        await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: info,
            cancellationToken: cancellationToken);

        await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: "Введите - Пункт отправления",
            cancellationToken: cancellationToken);

        _userBotService.SetProcess(callbackQuery.From.Id, UserProcesses.TripAdd);
    }
}
