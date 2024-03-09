using TgBot.Endpoints.Trips.AddProcess;

namespace TgBot.Constants;

public static class UserProcesses
{
    public const string AddTrip = "AddTrip";
    public const string Initiator = "Initiator";

    public static Dictionary<string, Dictionary<int, string>> ProcessStates = new()
    {
        [AddTrip] = new Dictionary<int, string>
        {
            { 1, UserStates.TripSearchAddress },
            { 2, UserStates.TripAddFromAddress },
            { 3, UserStates.TripSearchAddress },
            { 4, UserStates.TripAddToAddress },
            { 5, UserStates.TripAddStartDate },
            { 6, UserStates.TripAddStartTime },
            { 7, UserStates.TripAddEndDate },
            { 8, UserStates.TripAddSeats },
            { 9, UserStates.TripAddPrice },
            { 10, UserStates.TripAddDescription }

        },
        [Initiator] = new Dictionary<int, string>
        {
            { 1, UserStates.Initiator },
        }
        // Другие процессы...
    };
}
