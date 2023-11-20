using System.Collections.Concurrent;
using System.Linq;
using LibraryClasses.Commands;
using LibraryClasses.Entity;
using LibraryClasses.GameObjects;
using LibraryClasses.Ioc;

namespace GamePlayerMonogame.GameActions.Movable;

public class StartMoveCommand : ICommand
{
    private readonly UObject _gameUobject;

    public StartMoveCommand(UObject gameUobject)
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

        var movableGameObj = IoC.Resolve<MovableAdapterUObject>("MovableAdapter", _gameUobject);

        //movableGameObj.SetVelocity(new Coord2d(1, 1));

        var moveCommand = IoC.Resolve<ICommand>("MoveCommand", movableGameObj);

        if (queueCommandsObject != null
            && !queueCommandsObject.Contains(moveCommand))
        {
            queueCommandsObject.Add(moveCommand);
        }
    }
}