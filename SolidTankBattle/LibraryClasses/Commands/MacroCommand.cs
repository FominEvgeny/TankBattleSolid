using LibraryClasses.ExceptionsHandle;

namespace LibraryClasses.Commands;

public class MacroCommand : ICommand
{
    #region Private fields

    private readonly ICommand[] _commands;

    #endregion

    #region Ctors

    public MacroCommand(ICommand[] commands)
    {
        _commands = commands;
    }

    #endregion

    #region Public methods

    public void Execute()
    {
        foreach (var command in _commands)
        {
            try
            {
                command.Execute();
            }
            catch
            {
                throw new CommandException(this);
            }
        }
    }

    #endregion
}