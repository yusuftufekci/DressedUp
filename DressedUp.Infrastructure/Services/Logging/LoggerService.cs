using DressedUp.Domain.Interfaces.Logging;
using Microsoft.Extensions.Logging;

namespace DressedUp.Infrastructure.Services.Logging;

public class LoggerService<T> : ILoggerService<T>
{
    private readonly ILogger<T> _logger;

    public LoggerService(ILogger<T> logger)
    {
        _logger = logger;
    }

    public void LogInformation(string message)
    {
        _logger.LogInformation(message);
    }

    public void LogWarning(string message)
    {
        _logger.LogWarning(message);
    }

    public void LogError(string message, Exception ex)
    {
        _logger.LogError(ex, message);
    }

    public void LogCritical(string message, Exception ex)
    {
        _logger.LogCritical(ex, message);
    }
}