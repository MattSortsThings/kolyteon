namespace Mjt85.Kolyteon.FeatureTests;

[Feature]
public sealed class DummyFeatureTests
{
    [Fact]
    public void DoNothing()
    {
        true.Should().BeTrue();
    }
}
