namespace CheckBikeThefts.UseCases;

/// <summary>
/// Base input class for cacheable use cases
/// </summary>
/// <param name="ForceUpdate">If true, then refresh cached value</param>
public record CacheableUseCaseInput(bool ForceUpdate);