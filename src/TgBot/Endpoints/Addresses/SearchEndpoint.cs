using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.BotEndpoints.Endpoints;
using UseCases.Handlers.Addresses.Commands;

namespace TgBot.Endpoints.Addresses;

public class SearchEndpoint :  MessageEndpoint
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<SearchEndpoint> _logger;
    private readonly IMediator _mediator;

    public SearchEndpoint(ITelegramBotClient botClient, ILogger<SearchEndpoint> logger, IMediator mediator)
    {
        _botClient = botClient;
        _logger = logger;
        _mediator = mediator;
    }

    public override void Configure()
    {
        State("SearchAddress");
    }

    public override async Task HandleAsync(Message message, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(message.Text)) return;

        var result = await _mediator.Send(new AutoFillingCommand { Text = message.Text }, cancellationToken);

        if (result.IsSuccess)
        {
            var res = result.Value.Select(s => new { s.Note, s.FiasId, });

            var keyboardButtons = res.Select(item =>
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData(item.Note, item.FiasId)
                }
            ).ToArray();

            var inlineKeyboard = new InlineKeyboardMarkup(keyboardButtons);

            string text = "Выберите пункт отправления из списка";

            await _botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: text,
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);

            return;
        }
    }
}
