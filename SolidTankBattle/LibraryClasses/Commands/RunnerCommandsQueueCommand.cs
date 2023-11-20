using System.Collections.Concurrent;
using LibraryClasses.ExceptionsHandle;

namespace LibraryClasses.Commands;

public class RunnerCommandsQueueCommand : ICommand
{
    #region Private fields

    private readonly BlockingCollection<ICommand> _queueCommands;

    private readonly ExceptionHandler _exceptionHandler;

    #endregion

    #region Ctors

    public RunnerCommandsQueueCommand(ExceptionHandler exceptionHandler, BlockingCollection<ICommand> queueCommands)
    {
        _exceptionHandler = exceptionHandler;
        _queueCommands = queueCommands;
    }

    #endregion

    #region Public methods

    public void Execute()
    {
        while (_queueCommands.Count > 0 && _queueCommands.TryTake(out var command))
        {
            try
            {
                command.Execute();
            }
            catch (Exception e)
            {
                ExceptionHandler.Handle(e, command, _queueCommands);
            }
        }
    }

    #endregion
}