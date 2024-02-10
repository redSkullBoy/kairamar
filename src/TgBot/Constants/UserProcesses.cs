namespace TgBot.Constants;

public static class UserProcesses
{
    public const string AddTrip = "AddTrip";

    public static Dictionary<string, List<string>> ProcessStates = new()
    {
        [AddTrip] = new List<string>
        {
            UserStates.SearchAddress,
            UserStates.AddFromAddress,
            UserStates.SearchAddress,
            UserStates.AddToAddress
        },
        // Другие процессы...
    };
}
