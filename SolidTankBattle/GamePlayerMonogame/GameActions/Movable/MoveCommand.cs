using System;
using LibraryClasses.Commands;
using LibraryClasses.Entity;

namespace GamePlayerMonogame.GameActions.Movable;

public class MoveCommand : ICommand
{
    
    private readonly IMovable _movable;

    public MoveCommand(IMovable movable)
    {
        _movable = movable;
    }

   
    public void Execute()
    {
        var position = _movable.GetPosition();
        var velocity = _movable.GetVelocity();

        if (position == null
            || velocity == null)
        {
            throw new ArgumentException();
        }

        _movable.SetPosition(Coord2d.Append(position, velocity));
    }
}