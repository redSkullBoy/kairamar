using System.Collections.Concurrent;
using TgBot.BotEndpoints.Services;
using TgBot.Constants;

namespace TgBot.Services;

public class UserBotService : IUserBotService
{
    private ConcurrentDictionary<long, int> _userStateIndex = new ConcurrentDictionary<long, int>();
    private ConcurrentDictionary<long, string> _userProcesses = new ConcurrentDictionary<long, string>();

    public UserBotService()
    {

    }

    public bool PreviousState(long userId)
    {
        if (_userProcesses.TryGetValue(userId, out var process)
            && _userStateIndex.TryGetValue(userId, out var currentStateIndex))
        {
            if (currentStateIndex <= 1)
                return false;

            var preStateIndex = currentStateIndex - 1;
            _userStateIndex.AddOrUpdate(userId, preStateIndex,
                (key, oldValue) =>
                {
                    return preStateIndex;
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
            var nextStateIndex = 1;
            if (_userStateIndex.TryGetValue(userId, out var currentStateIndex))
            {
                if (states.Count >= currentStateIndex + 1)
                {
                    nextStateIndex = currentStateIndex + 1;
                }
            }
            _userStateIndex.AddOrUpdate(userId, nextStateIndex,
                (key, oldValue) =>
                {
                    return nextStateIndex;
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
            //если есть состояние, добавляем первое
            _userStateIndex.AddOrUpdate(userId, 1,
                (key, oldValue) =>
                {
                    return 1;
                }
            );
        }
    }

    public string? GetStateOrNull(long userId)
    {
        if (_userProcesses.TryGetValue(userId, out var process) 
            && UserProcesses.ProcessStates.TryGetValue(process, out var states)
            && _userStateIndex.TryGetValue(userId, out var currentStateIndex))
        {
            return states[currentStateIndex];
        }

        return null;
    }
}
