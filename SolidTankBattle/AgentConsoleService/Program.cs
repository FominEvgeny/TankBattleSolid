using AgentConsoleService.DTO;

namespace AgentConsoleService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Я - агент. Я получаю сигналы от тебя. Управляй танком, как собой!");

            var eventBus = new AgentService();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            eventBus.Publish("move", new Message { Direction = "up", Action = "StartMove", ObjectId = "1", TypeObject = "TankOneType" });
                            break;
                        case ConsoleKey.DownArrow:
                            eventBus.Publish("move", new Message { Direction = "down", Action = "StartMove", ObjectId = "1" });
                            break;
                        case ConsoleKey.LeftArrow:
                            eventBus.Publish("move", new Message { Direction = "left", Action = "StartMove", ObjectId = "1" });
                            break;
                        case ConsoleKey.RightArrow:
                            eventBus.Publish("move", new Message { Direction = "right", Action = "StartMove", ObjectId = "1" });
                            break;
                        case ConsoleKey.Enter:
                            eventBus.Publish("shoot", new Message { Action = "shoot", ObjectId = "1" });
                            break;

                        case ConsoleKey.W:
                            eventBus.Publish("move", new Message { Direction = "up", Action = "StartMove", ObjectId = "2" });
                            break;
                        case ConsoleKey.S:
                            eventBus.Publish("move", new Message { Direction = "down", Action = "StartMove", ObjectId = "2" });
                            break;
                        case ConsoleKey.A:
                            eventBus.Publish("move", new Message { Direction = "left", Action = "StartMove", ObjectId = "2" });
                            break;
                        case ConsoleKey.D:
                            eventBus.Publish("move", new Message { Direction = "right", Action = "StartMove", ObjectId = "2" });
                            break;
                    }
                }
            }
        }

    }
}
