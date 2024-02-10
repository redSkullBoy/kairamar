using System.Collections.Concurrent;
using TgBot.BotEndpoints.Services;
using TgBot.Constants;

namespace TgBot.Services;

public class UserBotService : IUserBotService
{
    private ConcurrentDictionary<long, string> _userState = new ConcurrentDictionary<long, string>();
    private ConcurrentDictionary<long, string> _userProcesses = new ConcurrentDictionary<long, string>();

    public UserBotService()
    {

    }

    public bool PreviousState(long userId)
    {
        if (_userProcesses.TryGetValue(userId, out var process)
            && _userState.TryGetValue(userId, out var currentState)
            && UserProcesses.ProcessStates.TryGetValue(process, out var states))
        {
            var index = states.IndexOf(currentState);

            if (index < 1)
                return false;

            var preState = states[index - 1];

            _userState.AddOrUpdate(userId, preState,
                (key, oldValue) =>
                {
                    return preState;
                }
            );

            return true;
        }

        return false;
    }

    public bool NetxState(long userId)
    {
        if (_userProcesses.TryGetValue(userId, out var process) 
            && UserProcesses.ProcessStates.TryGetValue(process, out var states))
        {
            var nextState = states[0];

            if (_userState.TryGetValue(userId, out var currentState))
            {
                var index = states.IndexOf(currentState);

                if (index < 0 && index > states.Count)
                    return false;

                nextState = states[index + 1];
            }

            _userState.AddOrUpdate(userId, nextState,
                (key, oldValue) =>
                {
                    return nextState;
                }
            );

            return true;
        }
        

        return false;
    }

    public void SetProcess(long userId, string process)
    {
        _userProcesses.AddOrUpdate(userId, process,
            (key, oldValue) =>
            {
                return process;
            }
        );

        if (UserProcesses.ProcessStates.TryGetValue(process, out var states))
        {
            _userState.AddOrUpdate(userId, states[0],
                (key, oldValue) =>
                {
                    return states[0];
                }
            );
        }
    }

    public string? GetStateOrNull(long userId)
    {
        if (_userState.TryGetValue(userId, out var state))
            return state;

        return null;
    }
}
