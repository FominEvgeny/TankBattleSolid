using System.Collections.Concurrent;
using GamePlayerMonogame.Controller;
using LibraryClasses.Commands;
using LibraryClasses.GameObjects;
using LibraryClasses.Inits;
using LibraryClasses.Ioc;
using LibraryClasses.Scope;
using SolidTankBattleInitCommand = GamePlayerMonogame.Inits.SolidTankBattleInitCommand;

// Инициализируем команды с зависимостями
new InitCommand().Execute();
new SolidTankBattleInitCommand().Execute();

// Создадим и зададим текущий скоуп для игры
var iocScope = IoC.Resolve<object>("IoC.Scope.Create");
IoC.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

// Зарегистрируем коллекцию всех игровых объектов 
var allObjectsGame = IoC.Resolve<BlockingCollection<UObject>>("UObjectCollection");

// Зарегистрируем контроллер с инъекцией в конструктор всех игровых объектов 
var controllerGamePlayer = IoC.Resolve<IControllerGamePlayer>("ControllerGamePlayer", allObjectsGame);

// Стандартный механизм запуска игрового окна
using var game = new GamePlayerMonogame.Game1(allObjectsGame);
game.Run();
