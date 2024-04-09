using TgBot.Endpoints.Trips.AddProcess;

namespace TgBot.Constants;

public static class UserProcesses
{
    public const string TripAdd = "TripAdd";
    public const string TripList = "TripList";
    public const string TripFind = "TripFind";

    public const string Initiator = "Initiator";

    public static Dictionary<string, Dictionary<int, string>> ProcessStates = new()
    {
        [TripAdd] = new Dictionary<int, string>
        {
            { 1, UserStates.SearchAddress },
            { 2, UserStates.TripAddFromAddress },
            { 3, UserStates.SearchAddress },
            { 4, UserStates.TripAddToAddress },
            { 5, UserStates.TripAddStartDate },
            { 6, UserStates.TripAddStartTime },
            { 7, UserStates.TripAddEndDate },
            { 8, UserStates.TripAddSeats },
            { 9, UserStates.TripAddPrice },
            { 10, UserStates.TripAddDescription }

        },
        [TripList] = new Dictionary<int, string>
        {
            { 1, UserStates.TripActive },
        },
        [Initiator] = new Dictionary<int, string>
        {
            { 1, UserStates.Initiator },
        },
        [TripFind] = new Dictionary<int, string>
        {
            { 1, UserStates.SearchAddress },
            { 2, UserStates.TripFindFromAddress },
            { 3, UserStates.SearchAddress },
            { 4, UserStates.TripFindToAddress },
            { 5, UserStates.TripFindStartDate },
            { 6, UserStates.TripFindStartTime },
            { 7, UserStates.TripFindList },
        }
         // Другие процессы...
    };
}
