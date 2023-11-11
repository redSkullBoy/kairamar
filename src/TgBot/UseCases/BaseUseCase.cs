using Telegram.Bot.Types;
using Telegram.Bot;

namespace TgBot.Keyboard;

public abstract class BaseUseCase
{
    protected readonly ITelegramBotClient _botClient;

    public BaseUseCase(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public abstract Task<Message> Run(Message message, CancellationToken cancellationToken);
}
