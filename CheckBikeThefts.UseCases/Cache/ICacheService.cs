namespace CheckBikeThefts.UseCases.Cache;

/// <summary>
/// Interface to save object in internal cache and retrieve it later by key 
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Retrieve cached object for the given key
    /// </summary>
    /// <param name="key">Cache object key</param>
    /// <returns>Cached object. Null, if object for the given key doesn't exist</returns>
    public object? Get(string key);

    /// <summary>
    /// Save object in internal cache
    /// </summary>
    /// <param name="key">Key for later retrieval</param>
    /// <param name="value">Object to cache</param>
    /// <param name="expiry">Expiry timeout</param>
    public void Set(string key, object value, TimeSpan expiry);
}