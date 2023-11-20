namespace LibraryClasses.Scope;

public interface IDependencyResolver
{
    object Resolve(string dependency, object[] args);
}