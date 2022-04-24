namespace CheckBikeThefts.Interfaces;

/// <summary>
/// Configuration of the running application
/// </summary>
public interface IApplicationConfiguration
{
    /// <summary>
    /// Base url for Bike Index API
    /// </summary>
    string BikeIndexBaseUrl { get; }

    /// <summary>
    /// Path to CSV file with cities 
    /// </summary>
    string CityCsvFilePath { get; }
}