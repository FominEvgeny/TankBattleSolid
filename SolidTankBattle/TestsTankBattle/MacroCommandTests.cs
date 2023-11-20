using LibraryClasses.Commands;
using LibraryClasses.ExceptionsHandle;
using NSubstitute;
using Xunit;

namespace TestsTankBattle;

public class MacroCommandTests
{
    [Fact]
    public void Execute_AllCommandsSucceed()
    {
        //Arrange 
        var command1 = Substitute.For<ICommand>();
        var command2 = Substitute.For<ICommand>();
        var macroCommand = new MacroCommand(new ICommand[] { command1, command2 });

        //Act
        macroCommand.Execute();

        //Assert
        command1.Received(1).Execute();
        command2.Received(1).Execute();
    }

    [Fact]
    public void Execute_AnyCommandThrowsException_ShouldThrowCommandException()
    {
        //Arrange 
        var command1 = Substitute.For<ICommand>();
        var command2 = Substitute.For<ICommand>();
        command2.When(x => x.Execute()).Do(x => throw new Exception());
        var macroCommand = new MacroCommand(new ICommand[] { command1, command2 });

        //Act
        void Act() => macroCommand.Execute();

        //Assert
        Assert.Throws<CommandException>((Action)Act);
        command1.Received(1).Execute();
        command2.Received(1).Execute();
    }

}