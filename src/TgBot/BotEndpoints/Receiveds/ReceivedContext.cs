using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TgBot.BotEndpoints.Receiveds;

public class ReceivedContext
{
    private readonly Dictionary<UpdateType, IReceivedStrategy> _strategies;
    private readonly ILogger<ReceivedContext> _logger;
    private readonly ReceivedDefinition _receivedDef;

    public ReceivedContext(ILogger<ReceivedContext> logger, ReceivedDefinition receivedDef, MessageReceivedStrategy messageReceivedStrategy, 
        CallbackQueryReceivedStrategy queryReceivedStrategy, InlineQueryReceivedStrategy inlineQueryReceivedStrategy,
        ChosenInlineResultReceivedStrategy chosenInlineResultStrategy)
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
        _receivedDef = receivedDef;
    }

    public async Task HandleAsync(Update update, CancellationToken cancellationToken)
    {
        if (_strategies.TryGetValue(update.Type, out var contextStrategy))
        {
            var userState = contextStrategy.GetUserState(update);

            //для Предварительные маршруты
            var completed = await contextStrategy.HandlePreEndpointAsync(update, cancellationToken);
            if (completed)
            {
                return;
            }

            //Если соостояния нет, обрабатываеем через Endpoint
            if (string.IsNullOrWhiteSpace(userState))
            {
                await contextStrategy.HandleEndpointAsync(update, cancellationToken);
                return;
            }

            //Проверка на состояние
            if (_receivedDef.TryGetValueState(userState, out var userStateEndpoint))
            {
                await contextStrategy.HandleUserStateAsync(update, userStateEndpoint!, userState, cancellationToken);
                return;
            }
            else
            {
                await contextStrategy.HandleDefaultUserStateAsync(update, userState, cancellationToken);
            }
        }

        _logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
    }
}
