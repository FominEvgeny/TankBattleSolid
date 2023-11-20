using System.Collections.Concurrent;
using LibraryClasses.Commands;
using LibraryClasses.ExceptionsHandle;
using NSubstitute;
using Xunit;

namespace TestsTankBattle;

public class RunnerCommandsQueueCommandTests
{
    [Fact]
    public void Execute_WhenQueueIsNotEmpty_CommandExecuteIsCalled()
    {
        // Arrange
        var command = Substitute.For<ICommand>();
        var command2 = Substitute.For<ICommand>();

        var exceptionHandler = Substitute.For<ExceptionHandler>();

        var commandsQueue = new BlockingCollection<ICommand>();
        commandsQueue.TryAdd(command);
        commandsQueue.TryAdd(command2);

        var runnerCommands = new RunnerCommandsQueueCommand(exceptionHandler, commandsQueue);

        command.When(x => x.Execute()).Do(x => { }); 
        command2.When(x => x.Execute()).Do(x => { }); 

        // Act
        runnerCommands.Execute();

        // Assert
        command.Received(1).Execute(); // Проверка, что Execute вызывается один раз
        command2.Received(1).Execute(); // Проверка, что Execute вызывается один раз
    }
}
