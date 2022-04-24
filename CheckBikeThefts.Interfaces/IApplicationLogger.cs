namespace CheckBikeThefts.Interfaces;

/// <summary>
/// Common interface for application logger
/// </summary>
public interface IApplicationLogger
{
    /// <summary>
    /// Log debug message
    /// </summary>
    /// <param name="message">Message text</param>
    void Debug(string message);
    
    /// <summary>
    /// Log error message
    /// </summary>
    /// <param name="message">Message text</param>
    void Error(string message);
    
    /// <summary>
    /// Log exception
    /// </summary>
    /// <param name="ex">Exception to log</param>
    void Error(Exception ex);
}