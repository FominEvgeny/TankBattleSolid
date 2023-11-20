using LibraryClasses.ExceptionsHandle;

namespace LibraryClasses.Commands;

public class BestMacroCommand : ICommand
{
    #region Private fields

    private readonly List<ICommand> _commands;

    #endregion

    #region Ctors

    public BestMacroCommand(List<ICommand> commands)
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