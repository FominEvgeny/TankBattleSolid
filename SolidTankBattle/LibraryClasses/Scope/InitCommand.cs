using System.Collections.Concurrent;
using LibraryClasses.Commands;
using LibraryClasses.GeneratorAdapters;
using LibraryClasses.Ioc;

namespace LibraryClasses.Scope;

public class InitCommand : ICommand
{
    #region Fields

    public static ThreadLocal<object>? CurrentScopes =
        new ThreadLocal<object>(true);

    private static readonly ConcurrentDictionary<string, Func<object[], object>> RootScope =
        new ConcurrentDictionary<string, Func<object[], object>>();

    static bool _alreadyExecutesScuccessfully = false;

    #endregion

    #region Public methods

    public void Execute()
    {
        if (_alreadyExecutesScuccessfully)
            return;

        lock (RootScope)
        {
            RootScope.TryAdd(
                "IoC.Scope.Current.Set",
                (object[] args) => new SetCurrentScopeCommand(args[0])
            );

            RootScope.TryAdd(
                "IoC.Scope.Current.Clear",
                (object[] args) => new ClearCurrentScopeCommand()
            );

            RootScope.TryAdd(
                "IoC.Scope.Current",
                (object[] args) => CurrentScopes.Value != null ? CurrentScopes.Value! : RootScope
            );

            RootScope.TryAdd(
                "IoC.Scope.Parent",
                (object[] args) => throw new Exception("The root scope has no a parent scope.")
            );

            RootScope.TryAdd(
                "IoC.Scope.Create.Empty",
                (object[] args) => new Dictionary<string, Func<object[], object>>()
            );

            RootScope.TryAdd(
                "IoC.Scope.Create",
                (object[] args) =>
                {
                    var creatingScope = IoC.Resolve<IDictionary<string, Func<object[], object>>>("IoC.Scope.Create.Empty");

                    if (args.Length > 0)
                    {
                        var parentScope = args[0];
                        creatingScope.Add("IoC.Scope.Parent", (object[] args) => parentScope);
                    }
                    else
                    {
                        var parentScope = IoC.Resolve<object>("IoC.Scope.Current");
                        creatingScope.Add("IoC.Scope.Parent", (object[] args) => parentScope);
                    }
                    return creatingScope;
                }
            );

            RootScope.TryAdd(
                (string)"IoC.Register",
                (Func<object[], object>)((object[] args) => new RegisterDependencyCommand((string)args[0], (Func<object[], object>)args[1]))
            );

            RootScope.TryAdd(
                (string)"IoC.Adapter",
                (Func<object[], object>)((object[] args) => AdapterGeneratorIoC.GenerateAdapter((Type)args[0])));

            // Здесь реализация GetOperationCommand
            //RootScope.TryAdd(
            //    "IoC.GetOperationCommand",
            //    (object[] args) => (args)
            //);

            IoC.Resolve<ICommand>(
                "Update Ioc Resolve Dependency Strategy",
                (Func<string, object[], object> oldStrategy) =>
                    (string dependency, object[] args) =>
                    {
                        var scope = CurrentScopes.Value != null ? CurrentScopes.Value! : RootScope;
                        var dependencyResolver = new DependencyResolver(scope);

                        return dependencyResolver.Resolve(dependency, args);
                    }
            ).Execute();

            
            _alreadyExecutesScuccessfully = true;
        }
    }

    #endregion
}