using FluentValidation.Results;

namespace CheckBikeThefts.UseCases;

/// <summary>
/// Represents use case result
/// </summary>
/// <typeparam name="T"></typeparam>
public record Result<T>
{
    private Result(T value, ValidationResult validation)
    {
        Value = value;
        Validation = validation;
    }

    /// <summary>
    /// Result value. If contains validation errors, then returns default(T)
    /// </summary>
    public T Value { get; }

    /// <summary>
    /// Validation results. If contains no errors, then validation was successful
    /// </summary>
    public ValidationResult Validation { get; }

    /// <summary>
    /// Creates successful result
    /// </summary>
    /// <param name="value">Result value</param>
    /// <returns>Use case result</returns>
    public static Result<T> Success(T value) => new(value, new ValidationResult());

    /// <summary>
    /// Creates result with validation errors
    /// </summary>
    /// <param name="validation">Validation object with errors</param>
    /// <returns>Use case result</returns>
    public static Result<T> Fail(ValidationResult validation) => new(default!, validation);
}