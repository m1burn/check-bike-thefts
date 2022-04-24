namespace CheckBikeThefts.Interfaces.Repositories;

/// <summary>
/// Interface for retrieving number of stolen bikes
/// </summary>
public interface IStolenBikeRepository
{
    /// <summary>
    /// Retrieving number of stolen bikes for the given city name (in English)
    /// </summary>
    /// <param name="city">City name (e.g. Amsterdam, Berlin, etc.)</param>
    /// <returns>Number of stolen bikes</returns>
    Task<int> GetStolenBikes(string city);
}