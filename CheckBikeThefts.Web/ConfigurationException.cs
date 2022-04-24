namespace CheckBikeThefts.Web;

public class ConfigurationException : Exception
{
    public ConfigurationException(string message)
        : base(message)
    {
    }
}