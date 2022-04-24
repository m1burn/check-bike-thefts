using CheckBikeThefts.Interfaces;
using NLog;

namespace CheckBikeThefts.Web;

public class NLogApplicationLogger : IApplicationLogger
{
    private readonly Logger _logger = LogManager.GetLogger("MainLogger");
    
    public void Debug(string message) => _logger.Debug(message);

    public void Error(string message) => _logger.Error(message);

    public void Error(Exception ex) => _logger.Error(ex);
}