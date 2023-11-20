using LibraryClasses.Commands;
using Serilog;

namespace LibraryClasses.ExceptionsHandle;

public class LogWriteCommand : ICommand
{
    private readonly Type _typeException;
    private ILogger? _logger;

    public LogWriteCommand(Type typeException)
    {
        _typeException = typeException;

        _logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
    }

    public void SetLogger(ILogger logger)
    {
        _logger = logger;
    }

    public void Execute()
    {
        _logger?.Information($"Выводим информацию в лог о типе ошибки: {_typeException}");
    }
}