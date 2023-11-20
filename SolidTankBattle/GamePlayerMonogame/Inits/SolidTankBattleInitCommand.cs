using System.Collections.Concurrent;
using System.Linq;
using GamePlayerMonogame.Controller;
using GamePlayerMonogame.DTO;
using GamePlayerMonogame.GamableMg;
using GamePlayerMonogame.GameActions.Movable;
using GamePlayerMonogame.GameActions.Rotatable;
using GamePlayerMonogame.GameActions.Shootable;
using LibraryClasses.Commands;
using LibraryClasses.Entity;
using LibraryClasses.GameObjects;
using LibraryClasses.Ioc;
using Microsoft.Xna.Framework.Graphics;

namespace GamePlayerMonogame.Inits;

public class SolidTankBattleInitCommand : ICommand
{
    public void Execute()
    {
        var iocScope = IoC.Resolve<object>("IoC.Scope.Current");
        IoC.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "UObjectCollection",
                (object[] args) => new BlockingCollection<UObject>())
            .Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register", 
                "ControllerGamePlayer", 
                (object[] args) => new ControllerGamePlayer((BlockingCollection<UObject>)args[0]))
            .Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "InterpretCommand",
                (object[] args) => new InterpretCommand((BlockingCollection<UObject>)args[0], (Message)args[1]))
            .Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "TexturesGameObjectsInitCommand",
                (object[] args) => new TexturesGameObjectsInitCommand((GraphicsDevice)args[0], (SpriteBatch)args[1]))
            .Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "MovableAdapter",
            (object[] args) => new MovableAdapterUObject((UObject)args[0])).Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "MoveCommand",
                (object[] args) => new MoveCommand((IMovable)args[0]))
            .Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "RotatableAdapter",
            (object[] args) => new RotatableAdapterUObject((UObject)args[0])).Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "RotateCommand",
                (object[] args) => new RotateCommand((IRotatable)args[0]))
            .Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "FreshObjectGame",
                (object[] args) =>
                {
                    var freshObject = new UObject();
                    freshObject.SetProperty("ObjectId", (string)args[0]);
                    freshObject.SetProperty("GamableAdapter", new GamableAdapter(freshObject));
                    freshObject.SetProperty("Position", new Coord2d(100,100));
                    freshObject.SetProperty("Velocity", new Coord2d(1,0));
                    freshObject.SetProperty("Direction", 1);
                    freshObject.SetProperty("Angular", 90);

                    var gamableAdapterFreshObject = (GamableAdapter)freshObject.GetProperty("GamableAdapter");
                    if (gamableAdapterFreshObject != null)
                    {
                        gamableAdapterFreshObject.SetTexture((Texture2D)TexturesObjects.Get((string)args[1]));
                        gamableAdapterFreshObject.SetSpriteBatch((SpriteBatch)TexturesObjects.Get("SpriteBatch"));
                    }

                    return freshObject;
                })
            .Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "FireShoot",
                (object[] args) =>
                {
                    var shootObject = new UObject();
                    var parentObject = (UObject)args[0];
                    var velocityParentObj = (Coord2d)parentObject.GetProperty("Velocity");

                    shootObject.SetProperty("ObjectId", $"shoot-{parentObject.GetProperty("ObjectId")}");
                    shootObject.SetProperty("GamableAdapter", new GamableAdapter(shootObject));

                    shootObject.SetProperty("Position", (Coord2d)parentObject.GetProperty("Position"));
                    shootObject.SetProperty("Velocity", new Coord2d(velocityParentObj.X * 5, velocityParentObj.Y * 5));
                    shootObject.SetProperty("Direction", (int)parentObject.GetProperty("Direction"));
                    shootObject.SetProperty("Angular", (int)parentObject.GetProperty("Angular"));

                    var gamableAdapterFreshObject = (GamableAdapter)shootObject.GetProperty("GamableAdapter");
                    if (gamableAdapterFreshObject != null)
                    {
                        gamableAdapterFreshObject.SetTexture((Texture2D)TexturesObjects.Get("FireShoot"));
                        gamableAdapterFreshObject.SetSpriteBatch((SpriteBatch)TexturesObjects.Get("SpriteBatch"));
                    }

                    return shootObject;
                })
            .Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "StartMove",
            (object[] args) => new StartMoveCommand((UObject)args[0]))
            .Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "StopMove",
            (object[] args) => new StopMoveCommand((UObject)args[0]))
            .Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "StartRotateClockwise",
                (object[] args) => new StartRotateClockwise((UObject)args[0]))
            .Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "StopRotateClockwise",
                (object[] args) => new StopRotateClockwise((UObject)args[0]))
            .Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "StartRotateCounterClockwise",
                (object[] args) => new StartRotateCounterClockwise((UObject)args[0]))
            .Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "StopRotateCounterClockwise",
                (object[] args) => new StopRotateCounterClockwise((UObject)args[0]))
            .Execute();

        IoC.Resolve<ICommand>(
                "IoC.Register",
                "ShootCommand",
                (object[] args) => new ShootCommand((UObject)args[0], (BlockingCollection<UObject>)args[1]))
            .Execute();
    }
}