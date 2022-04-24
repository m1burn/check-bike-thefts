namespace CheckBikeThefts.UseCases.RiskAssessment;

/// <summary>
/// Returns number of stolen bikes for the given city Id
/// </summary>
public interface IGetCityBikeThefts : ICacheableUseCase<IGetCityBikeThefts.In, IGetCityBikeThefts.Out>
{
    /// <summary>
    /// <see cref="IGetCityBikeThefts"/> input
    /// </summary>
    /// <param name="CityId">Id of the city to find number of stolen bikes</param>
    /// <param name="ForceUpdate">If true, then refresh cached value</param>
    public record In(int CityId, bool ForceUpdate) : CacheableUseCaseInput(ForceUpdate);

    /// <summary>
    /// <see cref="IGetCityBikeThefts"/> result
    /// </summary>
    /// <param name="CityId">Id of the city</param>
    /// <param name="StolenBikes">Number of stolen bikes in the city</param>
    public record Out(int CityId, int StolenBikes);
}