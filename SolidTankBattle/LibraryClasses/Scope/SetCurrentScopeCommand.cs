using LibraryClasses.Commands;

namespace LibraryClasses.Scope;

public class SetCurrentScopeCommand : ICommand
{
    #region Private fields

    private readonly object _scope;

    #endregion

    #region Ctors

    public SetCurrentScopeCommand(object scope)
    {
        _scope = scope;
    }

    #endregion

    #region Public methods

    public void Execute()
    {
        InitCommand.CurrentScopes.Value = _scope;
    }

    #endregion
}