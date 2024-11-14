namespace DressedUp.Domain.Interfaces.Logging;

public interface ILoggerService<T>
{
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(string message, Exception ex);
    void LogCritical(string message, Exception ex);
}