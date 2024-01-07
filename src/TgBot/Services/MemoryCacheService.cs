using Domain.Entities.Model;
using Microsoft.Extensions.Caching.Memory;

namespace TgBot.Services;

public class MemoryCacheService
{
    private const string trip = "trip";

    private readonly IMemoryCache _memoryCache;

    public MemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

    public Trip? GetTripOrNull(long userId)
    {
        var key = $"{trip}_{userId}";

        if (_memoryCache.TryGetValue(key, out Trip? cachedData))
        {
            return cachedData;
        }

        return null;
    }

    public void SetTrip(long userId, Trip data, TimeSpan expirationTime)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expirationTime,
            SlidingExpiration = expirationTime
        };

        var key = $"{trip}_{userId}";

        _memoryCache.Set(key, data, cacheEntryOptions);
    }
}
