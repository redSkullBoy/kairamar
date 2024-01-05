using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TgBot.BotEndpoints.Receiveds;

public class ReceivedContext
{
    private readonly Dictionary<UpdateType, IReceivedStrategy> _strategies;
    private readonly ILogger<ReceivedContext> _logger;

    public ReceivedContext(ILogger<ReceivedContext> logger, MessageReceivedStrategy messageReceivedStrategy, CallbackQueryReceivedStrategy queryReceivedStrategy
        , InlineQueryReceivedStrategy inlineQueryReceivedStrategy, ChosenInlineResultReceivedStrategy chosenInlineResultStrategy)
    {
        _strategies = new Dictionary<UpdateType, IReceivedStrategy>
        {
            { UpdateType.Message , messageReceivedStrategy },
            { UpdateType.EditedMessage , messageReceivedStrategy },
            { UpdateType.CallbackQuery , queryReceivedStrategy },
            { UpdateType.InlineQuery , inlineQueryReceivedStrategy },
            { UpdateType.ChosenInlineResult , chosenInlineResultStrategy },
        };

        _logger = logger;
    }

    public async Task HandleAsync(Update update, CancellationToken cancellationToken)
    {
        if (_strategies.TryGetValue(update.Type, out var contextStrategy))
        {
            await contextStrategy.HandleAsync(update, cancellationToken);
        }

        _logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
    }
}
