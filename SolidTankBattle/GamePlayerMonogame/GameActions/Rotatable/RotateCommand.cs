using System;
using GamePlayerMonogame.GameActions.Movable;
using LibraryClasses.Commands;
using LibraryClasses.Entity;

namespace GamePlayerMonogame.GameActions.Rotatable;

public class RotateCommand : ICommand
{
    private readonly IRotatable _rotatable;

    public RotateCommand(IRotatable rotatable)
    {
        _rotatable = rotatable;
    }

    public void Execute()
    {
        var position = _rotatable.GetDirection();
        var velocity = _rotatable.GetAngularVelocity();

        if (position == null
            || velocity == null)
        {
            throw new ArgumentException();
        }

        _rotatable.SetDirection((int)(position + velocity));
    }
}