using GamePlayerMonogame.GameActions.Movable;
using System.Collections.Concurrent;
using System.Linq;
using LibraryClasses.Commands;
using LibraryClasses.Entity;
using LibraryClasses.GameObjects;
using LibraryClasses.Ioc;
using Microsoft.Xna.Framework;
using Microsoft.CodeAnalysis;
using System;

namespace GamePlayerMonogame.GameActions.Rotatable;

public class StartRotateClockwise : ICommand
{
    private readonly UObject _gameUobject;

    public StartRotateClockwise(UObject gameUobject)
    {
        _gameUobject = gameUobject;
    }

    public void Execute()
    {
        var queueCommandsObject = (BlockingCollection<ICommand>)_gameUobject.GetProperty("QueueCommandsObject");

        if (queueCommandsObject == null)
        {
            _gameUobject.SetProperty("QueueCommandsObject", new BlockingCollection<ICommand>());
            queueCommandsObject = (BlockingCollection<ICommand>)_gameUobject.GetProperty("QueueCommandsObject");
        }

        var rotatableGameObj = IoC.Resolve<RotatableAdapterUObject>("RotatableAdapter", _gameUobject);

        rotatableGameObj.SetAngularVelocity(2);

        var rotateCommand = IoC.Resolve<ICommand>("RotateCommand", rotatableGameObj);

        var angle = rotatableGameObj.GetDirection();
        if (angle != null)
        {
            var radAngular = MathHelper.ToRadians((float)angle);

            _gameUobject.SetProperty("Velocity", new Coord2d((float)Math.Cos(radAngular),
                (float)Math.Sin(radAngular)));
        }

        if (queueCommandsObject != null
            && !queueCommandsObject.Contains(rotateCommand))
        {
            queueCommandsObject.Add(rotateCommand);
        }
    }
}