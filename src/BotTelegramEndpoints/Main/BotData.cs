using BotTelegramEndpoints.Endpoint;
using BotTelegramEndpoints.Endpoint.Auxiliary;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace BotTelegramEndpoints.Main
{
    //lives as a singleton in each DI container instance
    public class BotData
    {
        internal Stopwatch Stopwatch { get; } = new Stopwatch();

        internal BotDefinition[] Found { get; }

        internal BotData(BotDiscoveryOptions options) 
        {
            Stopwatch.Start();
            Found = BuildEndpointDefinitions(options);

            if (Found.Length == 0)
                throw new InvalidOperationException("FastEndpoints was unable to find any endpoint declarations!");
        }

        static BotDefinition[] BuildEndpointDefinitions(BotDiscoveryOptions options)
        {
            if (options.DisableAutoDiscovery && options.Assemblies?.Any() is false)
                throw new InvalidOperationException("If 'DisableAutoDiscovery' is true, a collection of `Assemblies` must be provided!");

            return new BotDefinition[] { };
        }
    }
}
