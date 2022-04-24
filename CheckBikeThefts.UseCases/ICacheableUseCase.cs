namespace CheckBikeThefts.UseCases;

/// <summary>
/// Common interface for use cases that caches a result and re-uses it for the same input 
/// </summary>
/// <typeparam name="TIn">Type of the input parameter</typeparam>
/// <typeparam name="TOut">Type of the output parameter (result)</typeparam>
public interface ICacheableUseCase<TIn, TOut> : IUseCase<TIn, TOut>
    where TIn : CacheableUseCaseInput
{
}