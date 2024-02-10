using Telegram.Bot.Types;

namespace TgBot.BotEndpoints.Services;

public interface IUserBotService
{
    public string? GetStateOrNull(long userId);

    public bool PreviousState(long userId);

    public bool NetxState(long userId);

    public void SetProcess(long userId, string process);
}
