using LibraryClasses.Commands;

namespace LibraryClasses.ExceptionsHandle;

public class RepeatCommandExceptionCommand : ICommand
{
    private readonly ICommand _command;

    public RepeatCommandExceptionCommand(ICommand command)
    {
        _command = command;
    }

    public void Execute()
    {
        _command.Execute();
    }
}