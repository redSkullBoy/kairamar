using Telegram.Bot.Types;

namespace TgBot.BotEndpoints.Services;

public interface IUserBotService
{
    public string? GetStateOrNull(long userId);

    public void SetState(long userId, string state);
}
