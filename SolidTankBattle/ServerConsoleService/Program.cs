using AdapterPractice.Controller;

namespace ServerConsoleService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Я игровой сервер. Моя задача - координировать действия других частей. Просто сверни меня, но не выключай.");
            ServerControllerService mainControllerService = new ServerControllerService();

            Console.ReadKey();
        }
    }
}
