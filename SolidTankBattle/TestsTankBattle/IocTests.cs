using LibraryClasses.Commands;
using LibraryClasses.Ioc;
using Xunit;

namespace TestsTankBattle;

public class IocTests
{
    [Fact]
    public void IocShouldUpdateResolveDependencyStrategy()
    {
        bool wasCalled = false;

        IoC.Resolve<ICommand>(
            "Update Ioc Resolve Dependency Strategy",
            (Func<string, object[], object> args) =>
            {
                wasCalled = true;
                return args;
            }
        ).Execute();

        Assert.True(wasCalled);
    }

    [Fact]
    public void IocShouldThrowArgumentExceptionIfDependencyIsNotFound()
    {
        Assert.Throws<ArgumentException>(
            () => IoC.Resolve<object>("UnexistingDependency")
        );
    }

    [Fact]
    public void IocShouldThrowInvalidCastExceptionIfDependencyResolvesAnotherType()
    {
        Assert.Throws<InvalidCastException>(
            () => IoC.Resolve<string>(
                "Update Ioc Resolve Dependency Strategy",
                (Func<string, object[], object> args) => args
            )
        );
    }
}