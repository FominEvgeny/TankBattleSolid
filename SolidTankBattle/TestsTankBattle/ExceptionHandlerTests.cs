using System.Collections.Concurrent;
using LibraryClasses.Commands;
using LibraryClasses.ExceptionsHandle;
using LibraryClasses.Inits;
using NSubstitute;
using Serilog;
using Xunit;

namespace TestsTankBattle;

public class ExceptionHandlerTests 
{
    // п.4
    [Fact]
    public void Execute_WritesExpectedMessageToLog()
    {
        // Arrange
        var typeException = typeof(ArgumentException);
        var logger = Substitute.For<ILogger>();
        var command = new LogWriteCommand(typeException);
        command.SetLogger(logger);

        var expectedMessage = $"Выводим информацию в лог о типе ошибки: {typeException}";

        // Act
        command.Execute();

        // Assert
        logger.Received().Information(expectedMessage);
    }

    // п. 5
    [Fact]
    public void Execute_InitializesExceptionDictionary()
    {
        // Arrange
        var storeExceptionDictionary = new ConcurrentDictionary<Type, Dictionary<Type, ICommand>>();
        var command = Substitute.For<ICommand>();
        var assertCommand = new LogWriteCommand(typeof(ArgumentException));

        var initsExceptionHandlerCommand = new InitsExceptionHandlerCommand(storeExceptionDictionary, command, assertCommand );

        // Act
        initsExceptionHandlerCommand.Execute();

        // Assert
        Assert.NotEmpty(storeExceptionDictionary);
    }

    // п.6
    [Fact]
    public void ExecuteRepeatCommandExceptionCommand_WhenCalled_ShouldExecuteCommand()
    {
        // Arrange
        var mockCommand = Substitute.For<ICommand>();
        var repeatCommandExceptionCommand = new RepeatCommandExceptionCommand(mockCommand);

        // Act
        repeatCommandExceptionCommand.Execute();

        // Assert
        mockCommand.Received(1).Execute();
    }

    // п.7
    [Fact]
    public void CountQueueRepeatExceptionHandler()
    {
        // Arrange
        var mockCommand = Substitute.For<ICommand>();
        var commandRepeaterWithLoggingCommand = new CommandRepeaterWithLoggingCommand(mockCommand, 1);

        mockCommand.When(x => x.Execute()).Do(_ => throw new Exception());

        // Act
        commandRepeaterWithLoggingCommand.Execute();

        // Assert
        mockCommand.Received(1).Execute();
    }

    // п.8 (countRepeat == 1 - один раз повторение, затем запись в лог)
    [Fact]
    public void Execute_WithCountRepeatEqualsToOneAndFirstException_ExecutesCommandOnceAndLogsException()
    {
        // Arrange
        var mockCommand = Substitute.For<ICommand>();
        var commandRepeaterWithLoggingCommand = new CommandRepeaterWithLoggingCommand(mockCommand, 1);

        mockCommand.When(x => x.Execute()).Do(_ => throw new Exception());

        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        // Act
        commandRepeaterWithLoggingCommand.Execute();

        // Assert
        mockCommand.Received(1).Execute();
        var output = consoleOutput.ToString();
        Assert.Contains("Неудачное исполнение команды", output);
    }

    // п.9 (countRepeat == 2 - два раза повторение, затем запись в лог)
    [Fact]
    public void Execute_WithCountRepeatEqualsToTwoAndException_ExecutesCommandTwiceAndLogsException()
    {
        // Arrange
        var mockCommand = Substitute.For<ICommand>();
        var commandRepeaterWithLoggingCommand = new CommandRepeaterWithLoggingCommand(mockCommand, 2);

        mockCommand.When(x => x.Execute()).Do(_ => throw new Exception());

        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        // Act
        commandRepeaterWithLoggingCommand.Execute();

        // Assert
        mockCommand.Received(2).Execute();
        var output = consoleOutput.ToString();
        Assert.Contains("Неудачное исполнение команды", output);
    }
}