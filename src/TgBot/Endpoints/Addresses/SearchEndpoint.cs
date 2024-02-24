using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.BotEndpoints.Endpoints;
using TgBot.BotEndpoints.Services;
using TgBot.Constants;
using UseCases.Handlers.Addresses.Commands;

namespace TgBot.Endpoints.Addresses;

public class SearchEndpoint :  MessageEndpoint
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<SearchEndpoint> _logger;
    private readonly IMediator _mediator;
    private readonly IUserBotService _userBotService;

    public SearchEndpoint(ITelegramBotClient botClient, ILogger<SearchEndpoint> logger, IMediator mediator, IUserBotService userBotService)
    {
        _botClient = botClient;
        _logger = logger;
        _mediator = mediator;
        _userBotService = userBotService;
    }

    public override void Configure()
    {
        State(UserStates.TripSearchAddress);
    }

    public override async Task HandleAsync(Message message, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(message.Text)) return;

        var result = await _mediator.Send(new AutoFillingCommand { Text = message.Text }, cancellationToken);

        if (result.IsSuccess)
        {
            var keyboardButtons = result.Value.Select(item =>
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData(item.Note, item.Id.ToString())
                }
            ).ToArray();

            var inlineKeyboard = new InlineKeyboardMarkup(keyboardButtons);

            string text = "Выберите пункт отправления из списка";

            await _botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: text,
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);

            _userBotService.NetxState(message.From!.Id);

            return;
        }

        var error = "Адресс не найден, повторите попытку";

        await _botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: error,
            cancellationToken: cancellationToken);
    }
}
