using LibraryClasses.Ioc.Impl;

namespace LibraryClasses.Ioc;

public class IoC
{
    public static Func<string, object[], object> Strategy { get; set; } = DefaultStrategy;

    private static object DefaultStrategy(string dependency, object[] args)
    {
        if (dependency == "Update Ioc Resolve Dependency Strategy")
        {
            return new UpdateIocResolveDependencyStrategyCommand(
                (Func<Func<string, object[], object>, Func<string, object[], object>>)args[0]
            );
        }
        else
        {
            throw new ArgumentException(@"Dependency {dependency} is not found.");
        }
    }

    public static T Resolve<T>(string dependency, params object[] args)
    {
        return (T)Strategy(dependency, args);
    }
}