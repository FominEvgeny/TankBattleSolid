using System.Collections.Concurrent;
using LibraryClasses.Commands;

namespace LibraryClasses.Inits;

public class InitsExceptionHandlerCommand : ICommand
{
    #region Private fields

    /// <summary>
    /// Сигнатура словаря, хранящего ключи и значения исключения -> команды
    /// TypeOfCommand, new Dictionary (TypeOfException, CommandObject)
    /// </summary>
    private readonly ConcurrentDictionary<Type, Dictionary<Type, ICommand>> _storeExceptionDictionary;

    private readonly ICommand _callingCommand;
    private ICommand _assertCommand;

    #endregion

    #region Public methods

    public InitsExceptionHandlerCommand(ConcurrentDictionary<Type, Dictionary<Type, ICommand>> storeExceptionDictionary,
        ICommand callingCommand, ICommand assertCommand)
    {
        _storeExceptionDictionary = storeExceptionDictionary;
        _callingCommand = callingCommand;
        _assertCommand = assertCommand;
    }

    public void SetAssertCommand(ICommand command)
    {
        _assertCommand = command;
    }

    public void Execute()
    {
        List<Type> exceptionTypes = new List<Type>();

        _storeExceptionDictionary.Clear();

        // Сформировать список именований всех исключений
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(Exception)))
                {
                    exceptionTypes.Add(type);
                }
            }
        }

        // Сформировать список именования всех команд
        var typesImplementingICommand = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => typeof(ICommand).IsAssignableFrom(p) && !p.IsInterface)
            .ToList();

        var dictionaryExceptionsCommand = new Dictionary<Type, ICommand>();

        foreach (var typeException in exceptionTypes)
        {
            ICommand assertCommand = _assertCommand;

            dictionaryExceptionsCommand.TryAdd(typeException, assertCommand);
        }

        foreach (var typeCommand in typesImplementingICommand)
        {
            _storeExceptionDictionary.TryAdd(typeCommand, dictionaryExceptionsCommand);
        }
    }

    #endregion
}