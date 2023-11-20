using LibraryClasses.Commands;
using LibraryClasses.Ioc;

namespace LibraryClasses.Scope
{
    public class RegisterDependencyCommand : ICommand
    {
        readonly string _dependency;
        readonly Func<object[], object> _dependencyResolverStrategy;

        public RegisterDependencyCommand(string dependency, Func<object[], object> dependencyResolverStrategy)
        {
            _dependency = dependency;
            _dependencyResolverStrategy = dependencyResolverStrategy;
        }

        public void Execute()
        {
            var currentScope = IoC.Resolve<IDictionary<string, Func<object[], object>>>("IoC.Scope.Current");
            currentScope.Add(_dependency, _dependencyResolverStrategy);
        }
    }
}
