using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;

namespace TgBot.Endpoints.Trip;

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
                            - Количество свободных мест
                            - Стоимость поездки
                            """;

        await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: info,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);

        await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: "Введите - Пункт отправления",
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);

        _userBotService.SetState(callbackQuery.From.Id, "AddDeparturePoint");
    }
}
