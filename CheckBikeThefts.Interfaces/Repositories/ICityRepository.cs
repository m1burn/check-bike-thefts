namespace CheckBikeThefts.Interfaces.Repositories;

/// <summary>
/// Interface for city data storage
/// </summary>
public interface ICityRepository
{
    /// <summary>
    /// Retrieve all cities
    /// </summary>
    /// <returns>Collection of cities</returns>
    Task<ICollection<CityDto>> GetAll();

    /// <summary>
    /// Find a city for the given Id
    /// </summary>
    /// <param name="cityId">City Id for a search</param>
    /// <returns>Found city. Null, if city for the given Id doesn't exist</returns>
    Task<CityDto?> GetById(int cityId);

    /// <summary>
    /// Data transfer object that contains persisted information about a city
    /// </summary>
    /// <param name="Id">Id of the city</param>
    /// <param name="Name">Name of the city</param>
    /// <param name="Country">Country of the city</param>
    /// <param name="CurrentlyOperate">If true, then we already operate in the city. Otherwise, false</param>
    public record CityDto(int? Id, string? Name, string? Country, bool? CurrentlyOperate);
}