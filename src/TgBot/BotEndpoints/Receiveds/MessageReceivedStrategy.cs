using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.Keyboard;
using TgBot.UseCases;

namespace TgBot.BotEndpoints.Receiveds;

public class MessageReceivedStrategy : IReceivedStrategy
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<MessageReceivedStrategy> _logger;

    public MessageReceivedStrategy(ITelegramBotClient botClient, ILogger<MessageReceivedStrategy> logger)
    {
        _botClient = botClient;
        _logger = logger;
    }

    public async Task<Message> HandleAsync(Update update, CancellationToken cancellationToken)
    {
        var message = update.Message!;

        _logger.LogInformation("Receive message type: {MessageType}", message.Type);

        if (message.Text is not { } messageText)
            return null;

        var useCases = new Dictionary<string, BaseUseCase>()
        {
            { "/start", new LoginUseCase(_botClient) },
            { "/toChooseRole", new ToChooseUseCase(_botClient) }
        };

        Message sentMessage = null;

        if (useCases.TryGetValue(messageText.Split(' ')[0], out var keyboard))
        {
            sentMessage = await keyboard.Run(message, cancellationToken);
        }
        else
            sentMessage = await Usage(_botClient, message, cancellationToken);

        _logger.LogInformation("The message was sent with id: {SentMessageId}", sentMessage.MessageId);

        return sentMessage;

        static async Task<Message> Usage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            const string usage = "Usage:\n" +
                                 "/toChooseRole - send inline keyboard\n" +
                                 "/keyboard    - send custom keyboard\n" +
                                 "/remove      - remove custom keyboard\n" +
                                 "/photo       - send a photo\n" +
                                 "/request     - request location or contact\n" +
                                 "/inline_mode - send keyboard with Inline Query";

            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: usage,
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);
        }
    }

    public void Register(Dictionary<string, Type> endpoints)
    {
        throw new NotImplementedException();
    }
}
