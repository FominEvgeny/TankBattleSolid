using GamePlayerMonogame.GameActions.Movable;
using LibraryClasses.GeneratorAdapters;
using NSubstitute;
using Xunit;

namespace TestsTankBattle;

public class AdaptorGeneratorIocTests
{
    private readonly AdapterGeneratorIoC _adapterGenerator;
    private readonly Type _mockType;

    public AdaptorGeneratorIocTests()
    {
        _mockType = Substitute.For<Type>();
        _adapterGenerator = new AdapterGeneratorIoC();
    }

    [Fact]
    public void GenerateAdapterMethodV2_ShouldReturnExpectedMethodCode_WhenMethodInfoWithParametersIsProvided()
    {
        // Arrange && Act
        var result = AdapterGeneratorIoC.GenerateAdapter(typeof(IMovable));

        // Assert
        Assert.Contains("return IoC.Resolve(\"IMovable:GetPosition\", _obj);", result);
        Assert.Contains("return IoC.Resolve(\"IMovable:SetPosition\", _obj, position).Execute();", result);
        Assert.Contains("return IoC.Resolve(\"IMovable:GetVelocity\", _obj);", result);
    }
}