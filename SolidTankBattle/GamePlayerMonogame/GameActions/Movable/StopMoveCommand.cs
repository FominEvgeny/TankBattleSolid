using System.Collections.Concurrent;
using System.Linq;
using LibraryClasses.Commands;
using LibraryClasses.Entity;
using LibraryClasses.GameObjects;
using LibraryClasses.Ioc;

namespace GamePlayerMonogame.GameActions.Movable;

public class StopMoveCommand : ICommand
{
    private readonly UObject _gameUobject;

    public StopMoveCommand(UObject gameUobject)
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

        var movableGameObj = IoC.Resolve<MovableAdapterUObject>("MovableAdapter", _gameUobject);

        var moveCommand = IoC.Resolve<ICommand>("MoveCommand", movableGameObj);

        queueCommandsObject.TryTake(out moveCommand);
    }
}