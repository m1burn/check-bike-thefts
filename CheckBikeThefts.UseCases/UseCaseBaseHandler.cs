using System.Diagnostics;
using CheckBikeThefts.Interfaces;

namespace CheckBikeThefts.UseCases;

/// <summary>
/// Base class for all use cases.
/// Contains common execution steps
/// </summary>
/// <typeparam name="TIn">Type of the input parameter</typeparam>
/// <typeparam name="TOut">Type of the output parameter (result)</typeparam>
public abstract class UseCaseBaseHandler<TIn, TOut> : IUseCase<TIn, TOut>
{
    protected UseCaseBaseHandler(IApplicationLogger logger)
    {
        Logger = logger;
    }

    protected IApplicationLogger Logger { get; }

    public virtual async Task<Result<TOut>> Execute(TIn input)
    {
        var stopwatch = new Stopwatch();
        try
        {
            if (ReferenceEquals(input, null))
            {
                throw new ArgumentNullException(nameof(input));
            }

            stopwatch.Start();
            return await OnExecute(input);
        }
        catch (Exception ex)
        {
            Logger.Error(ex);
            throw;
        }
        finally
        {
            stopwatch.Stop();
            Logger.Debug($"{GetType().Name} has been executed in {stopwatch.ElapsedMilliseconds}ms");
        }
    }

    /// <summary>
    /// Implemented by inherited classes and contains use case execution logic 
    /// </summary>
    /// <param name="input">Use case input</param>
    /// <returns>Use case result</returns>
    protected abstract Task<Result<TOut>> OnExecute(TIn input);
}