using CheckBikeThefts.Interfaces;
using CheckBikeThefts.UseCases.Cache;

namespace CheckBikeThefts.UseCases;

/// <summary>
/// Base class for all cacheable use cases.
/// Contains logic to cache and retrieve previous results
/// </summary>
/// <typeparam name="TIn">Type of the input parameter</typeparam>
/// <typeparam name="TOut">Type of the output parameter (result)</typeparam>
public abstract class CacheableUseCaseBaseHandler<TIn, TOut> :
    UseCaseBaseHandler<TIn, TOut>,
    ICacheableUseCase<TIn, TOut>
    where TIn : CacheableUseCaseInput
{
    private readonly ICacheService _cacheService;

    protected CacheableUseCaseBaseHandler(ICacheService cacheService, IApplicationLogger logger)
        : base(logger)
    {
        _cacheService = cacheService;
    }

    /// <summary>
    /// Cache expiry period. Default 10 minutes
    /// </summary>
    public TimeSpan CacheExpiry { get; init; } = TimeSpan.FromMinutes(10);

    public override async Task<Result<TOut>> Execute(TIn input)
    {
        var key = GetCacheKey(input);
        var value = (TOut?) _cacheService.Get(key);
        if (value == null || input.ForceUpdate)
        {
            var result = await base.Execute(input);
            if (result.Validation.IsValid && result.Value != null)
            {
                _cacheService.Set(key, result.Value, CacheExpiry);
            }

            return result;
        }

        return Result<TOut>.Success(value);
    }

    /// <summary>
    /// Implemented by inherited class. Returns cache key for the given input
    /// </summary>
    /// <param name="input">Use case input</param>
    /// <returns>Key to cache and retrieve result for the given input</returns>
    protected abstract string GetCacheKey(TIn input);
}