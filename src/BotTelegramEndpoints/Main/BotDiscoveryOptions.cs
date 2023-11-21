using System.Collections.Generic;
using System.Reflection;

namespace BotTelegramEndpoints.Main
{
    public class BotDiscoveryOptions
    {
        /// <summary>
        /// an optional collection of assemblies to discover endpoints from in addition to the auto discovered ones.
        /// if <see cref="DisableAutoDiscovery" /> is set to true, this must be provided.
        /// <para>NOTE: not applicable when using FastEndpoints.Generator package</para>
        /// </summary>
        public IEnumerable<Assembly>? Assemblies { internal get; set; }

        /// <summary>
        /// set to true if only the provided Assemblies should be scanned for endpoints.
        /// if <see cref="Assemblies" /> is null and this is set to true, an exception will be thrown.
        /// <para>NOTE: not applicable when using FastEndpoints.Generator package</para>
        /// </summary>
        public bool DisableAutoDiscovery { internal get; set; }
    }
}
