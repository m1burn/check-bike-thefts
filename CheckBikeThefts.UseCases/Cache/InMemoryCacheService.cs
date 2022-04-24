using System.Collections.Concurrent;

namespace CheckBikeThefts.UseCases.Cache;

public class InMemoryCacheService : ICacheService
{
    private readonly ConcurrentDictionary<string, (object Value, DateTime Expiry)> _cache = new();

    protected virtual DateTime UtcNow => DateTime.UtcNow;

    public object? Get(string key)
    {
        if (_cache.TryGetValue(key, out (object Value, DateTime Expiry) item) && item.Expiry >= UtcNow)
        {
            return item.Value;
        }

        return null;
    }

    public void Set(string key, object value, TimeSpan expiry)
    {
        var newItem = (value, UtcNow.Add(expiry));
        _cache.AddOrUpdate(key, newItem, (_, _) => newItem);
    }
}