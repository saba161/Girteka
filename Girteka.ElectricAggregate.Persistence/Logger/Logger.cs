using Girteka.ElectricAggregate.Domain.Logger;
using Microsoft.Extensions.Logging;

namespace Girteka.ElectricAggregate.Persistence.Logger;

public class Logger : ILogger
{
    private readonly ApplicationDbContext _db;

    public Logger(ApplicationDbContext db)
    {
        _db = db;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        var logMessage = formatter(state, exception);

        var log = new Log
        {
            Message = logMessage,
            Timestamp = DateTime.Now
        };

        _db.Logs.Add(log);
        _db.SaveChanges();
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }
}