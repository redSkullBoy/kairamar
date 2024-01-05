using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;

namespace TgBot.Endpoints.Trip;

public class AddDeparturePointEndpoint : MessageEndpoint
{
    private readonly IUserBotService _userBotService;

    public AddDeparturePointEndpoint(IUserBotService userBotService)
    {
        _userBotService = userBotService;
    }

    public override void Configure()
    {
        State("AddDeparturePoint");
    }

    public override async Task HandleAsync(Message message, CancellationToken cancellationToken)
    {
        await BotClient.SendTextMessageAsync(
            chatId: message!.Chat.Id,
            text: "Введите - Пункт назначения",
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);

        _userBotService.SetState(message.From!.Id, "AddDestinationPoint");
    }
}
