using System.Collections.Concurrent;
using GamePlayerMonogame.DTO;
using GamePlayerMonogame.GamableMg;
using LibraryClasses.Commands;
using LibraryClasses.GameObjects;
using LibraryClasses.Ioc;

namespace GamePlayerMonogame.Controller;

public class InterpretCommand : ICommand
{
    private readonly BlockingCollection<UObject> _objectsGame;
    private readonly Message _message;

    public InterpretCommand(BlockingCollection<UObject> objectsGame, Message message)
    {
        _objectsGame = objectsGame;
        _message = message;
    }

    public void Execute()
    {
        // Проверим, есть ли объект из сообщения в коллекции? Если элемент есть, 
        // поставим в очередь действие
        foreach (var uObject in _objectsGame)
        {
            if ((string)uObject.GetProperty("ObjectId") == _message.ObjectId)
            {
                ActionMethod(uObject, _message);
                return;
            }
        }

        // Зарегистрируем объект, если его нет в списке игровых объектов
        if (_message.ObjectId != null
            && _message.TypeObject != null
            && _message.GameId != null)
        {
            var freshObject = IoC.Resolve<UObject>("FreshObjectGame", _message.ObjectId, _message.TypeObject);
            _objectsGame.Add(freshObject);
        }
    }

    private void ActionMethod(UObject uObject, Message message)
    {
        if (message.Action != null)
        {
            var comm = IoC.Resolve<ICommand>(message.Action, uObject, _objectsGame);
            IoC.Resolve<ICommand>(message.Action, uObject,_objectsGame).Execute();
        }
    }
}