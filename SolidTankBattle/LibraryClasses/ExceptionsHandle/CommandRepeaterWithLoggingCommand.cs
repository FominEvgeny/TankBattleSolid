using LibraryClasses.Commands;

namespace LibraryClasses.ExceptionsHandle;

public class CommandRepeaterWithLoggingCommand : ICommand
{
    private readonly ICommand _command;
    private readonly int _countRepeat;

    public CommandRepeaterWithLoggingCommand(ICommand command, int countRepeat)
    {
        _command = command;
        _countRepeat = countRepeat;
    }

    public void Execute()
    {
        for (int i = 1; i <= _countRepeat; i++)
        {
            try
            {
                _command.Execute();
            }
            catch (Exception e)
            {
                if (i == _countRepeat)
                {
                    Console.WriteLine($"Неудачное исполнение команды {_command.GetType()} {_countRepeat} раз. Вызвано: {e}");
                    return;
                }
            }
        }
    }
}