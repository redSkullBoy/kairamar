namespace TgBot.BotEndpoints.Main;

internal class EndpointData
{
    private readonly EndpointDiscoveryOptions _endpointDiscoveryOptions;

    internal EndpointData(EndpointDiscoveryOptions endpointDiscoveryOptions)
    {
        _endpointDiscoveryOptions = endpointDiscoveryOptions;
    }

    public EndpointDiscoveryOptions EndpointDiscoveryOptions { get {  return _endpointDiscoveryOptions; } }
}
