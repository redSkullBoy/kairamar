using System.Collections.Concurrent;
using TgBot.BotEndpoints.Services;

namespace TgBot.Services;

public class UserBotService : IUserBotService
{
    private ConcurrentDictionary<long, string> _userState = new ConcurrentDictionary<long, string>();

    public UserBotService()
    {

    }

    public string? GetStateOrNull(long userId)
    {
        if (_userState.TryGetValue(userId, out var state))
            return state;

        return null;
    }

    public void SetState(long userId, string state)
    {
        _userState.AddOrUpdate(userId, state, 
            (key, oldValue) => 
            {
                return state; 
            }
        );
    }
}
