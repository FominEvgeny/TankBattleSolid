using LibraryClasses.Commands;
using LibraryClasses.Ioc;
using LibraryClasses.Scope;
using Xunit;

namespace TestsTankBattle;

public class ScopeTests : IDisposable
{
    public ScopeTests()
    {
        new InitCommand().Execute();
        var iocScope = IoC.Resolve<object>("IoC.Scope.Create");
        IoC.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void Paret_Scope_Can_Be_Set_Manually_For_Creating_Scope()
    {
        var scope1 = IoC.Resolve<object>("IoC.Scope.Create");
        var scope2 = IoC.Resolve<object>("IoC.Scope.Create", scope1);

        IoC.Resolve<ICommand>("IoC.Scope.Current.Set", scope1);
        IoC.Resolve<ICommand>("IoC.Register", "someDependency", (object[] args) => (object)2).Execute();
        IoC.Resolve<ICommand>("IoC.Scope.Current.Set", scope2);

        Assert.Equal(2, IoC.Resolve<int>("someDependency"));
    }

    [Fact]
    public void Ioc_Should_Resolve_Registered_Dependency_In_CurrentScope()
    {
        IoC.Resolve<ICommand>("IoC.Register", "someDependency", (object[] args) => (object)1).Execute();

        Assert.Equal(1, IoC.Resolve<int>("someDependency"));
    }

    [Fact]
    public void Ioc_Should_Throw_Exception_On_Unregistered_Dependency_In_CurrentScope()
    {
        Assert.ThrowsAny<Exception>(() => IoC.Resolve<int>("someDependency"));
    }

    [Fact]
    public void Ioc_Should_Use_Parent_Scope_If_Resolving_Dependency_Is_Not_Defined_In_Current_Scope()
    {
        IoC.Resolve<ICommand>("IoC.Register", "someDependency", (object[] args) => (object)1).Execute();

        var parentIoCScope = IoC.Resolve<object>("IoC.Scope.Current");

        var iocScope = IoC.Resolve<object>("IoC.Scope.Create");
        IoC.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

        Assert.Equal(iocScope, IoC.Resolve<object>("IoC.Scope.Current"));
        Assert.Equal(1, IoC.Resolve<int>("someDependency"));
    }

    public void Dispose()
    {
        IoC.Resolve<ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}