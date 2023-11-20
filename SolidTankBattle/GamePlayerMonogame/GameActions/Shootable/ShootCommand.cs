using System.Collections.Concurrent;
using LibraryClasses.Commands;
using LibraryClasses.GameObjects;
using LibraryClasses.Ioc;

namespace GamePlayerMonogame.GameActions.Shootable;

public class ShootCommand : ICommand
{
    private readonly BlockingCollection<UObject> _objectsGame;
    private readonly UObject _gameUObject;

    public ShootCommand(UObject gameUObject, BlockingCollection<UObject> objectsGame)
    {
        _objectsGame = objectsGame;
        _gameUObject = gameUObject;
    }

    public void Execute()
    {
        // Создать объект пули
        var shootFire = IoC.Resolve<UObject>("FireShoot", _gameUObject, _objectsGame);

        var param = shootFire.Properties;
        _objectsGame.Add(shootFire);

        var commStartMoveShoot = IoC.Resolve<ICommand>("StartMove", shootFire);
        commStartMoveShoot.Execute();

        // Добавить в коллекцию игровых объектов на отображение
        // Удалить эту команду из очереди, чтобы не спамить пулями
    }
}