using Telegram.Bot.Types.Enums;

namespace TgBot.BotEndpoints.Receiveds;

public class ReceivedDefinition
{
    private Dictionary<string, Type> _endpoints;
    private Dictionary<string, Type> _userStates;

    public ReceivedDefinition()
    {
        _endpoints = new Dictionary<string, Type>();
        _userStates = new();
    }

    public Dictionary<string, Type> UserStates { get { return _userStates; } }

    public void AddEndpoints(Dictionary<string, (Type type, bool isPreRoute)> endpoints, UpdateType type)
    {
        foreach (var endpoint in endpoints)
        {
            _endpoints.Add(KeyGenerating(endpoint.Key, type, endpoint.Value.isPreRoute), endpoint.Value.type);
        }
    }

    public void AddStates(Dictionary<string, Type> states)
    {
        foreach (var state in states)
        {
            _userStates.Add(state.Key, state.Value);
        }
    }

    public bool TryGetValueEndpoint(string key, UpdateType type, bool isPreRoute, out Type? value)
    {
        if (_endpoints!.TryGetValue(KeyGenerating(key, type, isPreRoute), out var result))
        {
            value = result;
            return true;
        }

        value = default;
        return false;
    }

    public bool TryGetValueState(string key, out Type? value)
    {
        if (_userStates!.TryGetValue(key, out var result))
        {
            value = result;
            return true;
        }

        value = default;
        return false;
    }

    private string KeyGenerating(string key, UpdateType type, bool isPreRoute)
    {
        return $"{key}_{type}_{isPreRoute}";
    }
}
