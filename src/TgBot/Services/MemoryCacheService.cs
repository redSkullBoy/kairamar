﻿using Domain.Entities.Model;
using Microsoft.Extensions.Caching.Memory;
using UseCases.Handlers.Trips.Dto;

namespace TgBot.Services;

public class MemoryCacheService
{
    private const string trip = "trip";
    private const string tripFilter = "tripFilter";

    private readonly IMemoryCache _memoryCache;

    public MemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

    public CreateTripDto? GetTripOrNull(long userId)
    {
        var key = $"{trip}_{userId}";

        if (_memoryCache.TryGetValue(key, out CreateTripDto? cachedData))
        {
            return cachedData;
        }

        return null;
    }

    public void SetTrip(long userId, CreateTripDto data, TimeSpan expirationTime)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expirationTime,
            SlidingExpiration = expirationTime
        };

        var key = $"{trip}_{userId}";

        _memoryCache.Set(key, data, cacheEntryOptions);
    }

    public void SetTripFilter(long userId, TripFilter data, TimeSpan expirationTime)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expirationTime,
            SlidingExpiration = expirationTime
        };

        var key = $"{tripFilter}_{userId}";

        _memoryCache.Set(key, data, cacheEntryOptions);
    }

    public TripFilter? GetTripFilterOrNull(long userId)
    {
        var key = $"{tripFilter}_{userId}";

        if (_memoryCache.TryGetValue(key, out TripFilter? cachedData))
        {
            return cachedData;
        }

        return null;
    }
}
