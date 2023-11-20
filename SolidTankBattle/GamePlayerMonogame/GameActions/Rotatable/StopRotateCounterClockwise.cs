using GamePlayerMonogame.GameActions.Movable;
using System.Collections.Concurrent;
using LibraryClasses.Commands;
using LibraryClasses.Entity;
using LibraryClasses.GameObjects;
using LibraryClasses.Ioc;
using Microsoft.Xna.Framework;
using System;

namespace GamePlayerMonogame.GameActions.Rotatable;

public class StopRotateCounterClockwise : ICommand
{
    private readonly UObject _gameUobject;

    public StopRotateCounterClockwise(UObject gameUobject)
    {
        _gameUobject = gameUobject;
    }

    public void Execute()
    {
        var queueCommandsObject = (BlockingCollection<ICommand>)_gameUobject.GetProperty("QueueCommandsObject");

        if (queueCommandsObject == null)
        {
            return;
        }

        var rotatableGameObj = IoC.Resolve<RotatableAdapterUObject>("RotatableAdapter", _gameUobject);

        var angle = rotatableGameObj.GetDirection();
        if (angle != null)
        {
            var radAngular = MathHelper.ToRadians((float)angle);

            _gameUobject.SetProperty("Velocity", new Coord2d((float)Math.Cos(radAngular),
                (float)Math.Sin(radAngular)));
        }

        var rotateCommand = IoC.Resolve<ICommand>("RotateCommand", rotatableGameObj);

        queueCommandsObject.TryTake(out rotateCommand);
    }
}