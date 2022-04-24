using CheckBikeThefts.Interfaces;

namespace CheckBikeThefts.Web;

public delegate bool CheckFileExists(string path);

public class WebApplicationConfiguration : IApplicationConfiguration
{
    private readonly CheckFileExists _checkFileExists;

    public WebApplicationConfiguration(IConfiguration configuration, CheckFileExists checkFileExists)
    {
        _checkFileExists = checkFileExists;
        
        BikeIndexBaseUrl = RequiredString(configuration, "BikeIndexBaseUrl");
        CityCsvFilePath = RequiredFilePath(configuration, "CityCsvFilePath");
    }

    public string BikeIndexBaseUrl { get; }

    public string CityCsvFilePath { get; }

    private string RequiredString(IConfiguration configuration, string key)
    {
        var value = configuration[key];
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ConfigurationException($"{key} configuration is required");
        }

        return value.Trim();
    }

    private string RequiredFilePath(IConfiguration configuration, string key)
    {
        var path = RequiredString(configuration, key);
        if (!_checkFileExists(path))
        {
            throw new ConfigurationException($"File \"{path}\" doesn't exist");
        }

        return path;
    }
}