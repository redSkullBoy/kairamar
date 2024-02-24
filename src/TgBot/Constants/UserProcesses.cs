using TgBot.Endpoints.Trips.AddProcess;

namespace TgBot.Constants;

public static class UserProcesses
{
    public const string AddTrip = "AddTrip";

    public static Dictionary<string, List<string>> ProcessStates = new()
    {
        [AddTrip] = new List<string>
        {
            UserStates.TripSearchAddress,
            UserStates.TripAddFromAddress,
            UserStates.TripSearchAddress,
            UserStates.TripAddToAddress,
            UserStates.TripAddStartDate,
            UserStates.TripAddStartTime,
            nameof(AddToAddressEndpoint.Definition.UserState)
        },
        // Другие процессы...
    };
}
