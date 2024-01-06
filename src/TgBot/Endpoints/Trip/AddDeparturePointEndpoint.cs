using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;

namespace TgBot.Endpoints.Trip;

public class AddDeparturePointEndpoint : MessageEndpoint
{
    private readonly IUserBotService _userBotService;
    private readonly ITelegramBotClient _botClient;

    public AddDeparturePointEndpoint(IUserBotService userBotService, ITelegramBotClient botClient)
    {
        _userBotService = userBotService;
        _botClient = botClient;
    }

    public override void Configure()
    {
        State("AddDeparturePoint");
    }

    public override async Task HandleAsync(Message message, CancellationToken cancellationToken)
    {
        await _botClient.SendTextMessageAsync(
            chatId: message!.Chat.Id,
            text: "Введите - Пункт назначения",
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);

        _userBotService.SetState(message.From!.Id, "AddDestinationPoint");
    }
}
