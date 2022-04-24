namespace CheckBikeThefts.UseCases.RiskAssessment;

/// <summary>
/// Returns all cities
/// </summary>
public interface IGetAllCities : IUseCase<IGetAllCities.In, IGetAllCities.Out>
{
    public record In;

    /// <summary>
    /// <see cref="IGetAllCities"/> result
    /// </summary>
    /// <param name="CurrentlyOperate">List of cities where we currently operate</param>
    /// <param name="NotCurrentlyOperate">List of cities where we don't current operate</param>
    public record Out(ICollection<Out.CityDto> CurrentlyOperate, ICollection<Out.CityDto> NotCurrentlyOperate)
    {
        /// <summary>
        /// Data transfer object that contains information about a city
        /// </summary>
        /// <param name="Id">Id of the city</param>
        /// <param name="Name">Name of the city</param>
        /// <param name="Country">Country of the city</param>
        public record CityDto(int Id, string Name, string Country);
    }
}