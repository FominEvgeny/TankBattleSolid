using GamePlayerMonogame.GameActions.Rotatable;
using NSubstitute;
using Xunit;

namespace TestsTankBattle;

public class RotateCommandTests
{
    [Fact]
    public void Execute_ShouldThrowException_WhenUnableToReadPosition()
    {
        // Arrange
        var rotatableMock = Substitute.For<IRotatable>();
        var rotateCommand = new RotateCommand(rotatableMock);

        rotatableMock.GetDirection().Returns(x => { throw new Exception(); });

        // Act and Assert
        Assert.Throws<Exception>(() => rotateCommand.Execute());
    }

    [Fact]
    public void Execute_ShouldThrowException_WhenUnableToReadAngularVelocity()
    {
        // Arrange
        var rotatableMock = Substitute.For<IRotatable>();
        var rotateCommand = new RotateCommand(rotatableMock);

        rotatableMock.GetAngularVelocity().Returns(x => { throw new Exception(); });

        // Act and Assert
        Assert.Throws<Exception>(() => rotateCommand.Execute());
    }
}