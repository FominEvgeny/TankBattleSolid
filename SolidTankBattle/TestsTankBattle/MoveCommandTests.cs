using GamePlayerMonogame.GameActions.Movable;
using LibraryClasses.Entity;
using NSubstitute;
using NSubstitute.Exceptions;
using Xunit;

namespace TestsTankBattle;

public class MoveCommandTests
{
    [Fact]
    public void Execute_ShouldChangePosition()
    {
        // Arrange
        var movable = Substitute.For<IMovable>();
        var moveCommand = new MoveCommand(movable);

        var position = new Coord2d(12, 5);
        var velocity = new Coord2d(-7, 3);

        movable.GetPosition().Returns(position);
        movable.GetVelocity().Returns(velocity);

        // Act
        moveCommand.Execute();

        // Assert
        movable.Received().SetPosition(Arg.Is<Coord2d>(c => c.X == 5 && c.Y == 8));
    }

    [Fact]
    public void Execute_ShouldThrowException_WhenGetPositionFails()
    {
        // Arrange
        var movable = Substitute.For<IMovable>();
        var moveCommand = new MoveCommand(movable);

        movable.GetPosition().Returns(x => throw new SubstituteException());

        // Act and Assert
        Assert.Throws<SubstituteException>(() => moveCommand.Execute());
    }

    [Fact]
    public void Execute_ShouldThrowException_WhenGetVelocityFails()
    {
        // Arrange
        var movable = Substitute.For<IMovable>();
        var moveCommand = new MoveCommand(movable);

        movable.GetVelocity().Returns(x => throw new SubstituteException());

        // Act and Assert
        Assert.Throws<SubstituteException>(() => moveCommand.Execute());
    }

    [Fact]
    public void Execute_ShouldThrowException_WhenSetPositionFails()
    {
        // Arrange
        var movable = Substitute.For<IMovable>();
        var moveCommand = new MoveCommand(movable);

        movable.GetPosition().Returns(new Coord2d(1, 1));
        movable.GetVelocity().Returns(new Coord2d(1, 1));
        movable.When(m => m.SetPosition(Arg.Any<Coord2d>())).Do(x => throw new SubstituteException());

        // Act and Assert
        Assert.Throws<SubstituteException>(() => moveCommand.Execute());
    }
}