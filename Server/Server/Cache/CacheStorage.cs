﻿namespace Server.Server.Cache;

internal static class CacheStorage
{
    //                          <SessionKey, DatabaseName>
    private static readonly Dictionary<Guid, string> Cache = new();

    public static string? Get(Guid key)
    {
        return Cache.TryGetValue(key, out string? session)
            ? session
            : null;
    }

    public static void Set(Guid key, string value)
    {
        Cache[key] = value;
    }

    public static void Clear()
    {
        Cache.Clear();
    }
}