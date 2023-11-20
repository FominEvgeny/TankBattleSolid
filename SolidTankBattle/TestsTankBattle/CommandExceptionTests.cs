using LibraryClasses.Commands;
using LibraryClasses.ExceptionsHandle;
using NSubstitute;
using Xunit;

namespace TestsTankBattle;

public class CommandExceptionTests
{
    [Fact]
    public void CommandException_Throws_Error()
    {
        // Arrange
        var command = Substitute.For<ICommand>();
        string expectedMessage = "Error while executing command " + command;

        // Act
        var sut = new CommandException(command);

        // Assert
        var exception = Assert.IsType<CommandException>(sut);
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Fact]
    public void GetCommand_Returns_The_Same_Command_Object()
    {
        // Arrange
        var command = Substitute.For<ICommand>();

        // Act
        var sut = new CommandException(command);
        var returnedCommand = sut.GetCommand();

        // Assert
        Assert.Same(command, returnedCommand);
    }
}