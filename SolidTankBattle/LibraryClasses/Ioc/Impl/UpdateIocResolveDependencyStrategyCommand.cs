using LibraryClasses.Commands;

namespace LibraryClasses.Ioc.Impl;

public class UpdateIocResolveDependencyStrategyCommand : ICommand
{
    #region Private fields

    private readonly Func<Func<string, object[], object>, Func<string, object[], object>> _strategy;

    #endregion

    #region Ctors

    public UpdateIocResolveDependencyStrategyCommand(
        Func<Func<string, object[], object>, Func<string, object[], object>> newStrategy)
    {
        _strategy = newStrategy;
    }

    #endregion

    #region Public methods

    public void Execute()
    {
        IoC.Strategy = _strategy(IoC.Strategy);
    }

    #endregion
}