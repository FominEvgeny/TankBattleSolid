using LibraryClasses.Commands;

namespace LibraryClasses.ExceptionsHandle;

public class CommandException : Exception
{
    private readonly ICommand _command;

    public CommandException(ICommand command) : base("Error while executing command " + command)
    {
        _command = command;
    }

    public ICommand GetCommand()
    {
        return _command;
    }
}