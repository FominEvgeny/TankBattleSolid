using System.Collections.Concurrent;
using LibraryClasses.Commands;
using LibraryClasses.Inits;

namespace LibraryClasses.ExceptionsHandle
{
    public class ExceptionHandler
    {
        #region Private fields

        private static readonly ConcurrentDictionary<Type, Dictionary<Type, ICommand>> StoreExceptionDictionary = new();
        private static ICommand _initsExceptionHandlerCommand = null!;
        private static BlockingCollection<ICommand>? _queueCommands;
        private static readonly object LockObject = new();

        #endregion

        #region Public methods

        // Обработчик события, записывает информацию о выброшенном исключении в лог
        public static void Handle(Exception e, ICommand command)
        {
            Type exceptionType = e.GetType();
            Type commandType = command.GetType();

            _initsExceptionHandlerCommand = new InitsExceptionHandlerCommand(StoreExceptionDictionary, command, new LogWriteCommand(exceptionType));
            _initsExceptionHandlerCommand.Execute();

            if (StoreExceptionDictionary.TryGetValue(commandType, out var commandDictionary))
            {
                if (commandDictionary.TryGetValue(exceptionType, out var exceptionCommand))
                {
                    exceptionCommand.Execute();
                }
            }
        }

        // Обработчик события, ставит команду, записывающую информацию о выброшенном исключении, в очередь
        public static void Handle(Exception e, ICommand command, BlockingCollection<ICommand>? queueCommands)
        {
            Type exceptionType = e.GetType();
            Type commandType = command.GetType();
            _queueCommands = queueCommands;

            _initsExceptionHandlerCommand = new InitsExceptionHandlerCommand(StoreExceptionDictionary, command, new RepeatCommandExceptionCommand(command));
            _initsExceptionHandlerCommand.Execute();

            if (StoreExceptionDictionary.TryGetValue(commandType, out var commandDictionary))
            {
                if (commandDictionary.TryGetValue(exceptionType, out var exceptionCommand))
                {
                    AddQueue(exceptionCommand);
                }
            }
        }

        public static void AddQueue(ICommand command)
        {
            try
            {

                if (_queueCommands != null)
                    lock (_queueCommands)
                    {
                        _queueCommands.TryAdd(command);
                    }
            }
            catch (Exception e)
            {
                Handle(e, command, null);
            }
        }

        #endregion
    }
}
