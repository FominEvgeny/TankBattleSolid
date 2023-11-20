using LibraryClasses.Commands;

namespace LibraryClasses.Scope;

public class ClearCurrentScopeCommand : ICommand
{
    public void Execute()
    {
        InitCommand.CurrentScopes.Value = null;
    }
}