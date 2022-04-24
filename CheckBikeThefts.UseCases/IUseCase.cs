namespace CheckBikeThefts.UseCases;

/// <summary>
/// Common interfaces for use cases
/// </summary>
/// <typeparam name="TIn">Type of the input parameter</typeparam>
/// <typeparam name="TOut">Type of the output parameter (result)</typeparam>
public interface IUseCase<TIn, TOut>
{
    /// <summary>
    /// Execute use case
    /// </summary>
    /// <param name="input">Input parameter</param>
    /// <returns>Use case result</returns>
    Task<Result<TOut>> Execute(TIn input);
}