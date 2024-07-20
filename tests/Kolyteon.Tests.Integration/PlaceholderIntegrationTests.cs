namespace Kolyteon.Tests.Integration;

[IntegrationTest]
[PlaceholderTest]
public sealed class PlaceholderIntegrationTests
{
    [Fact]
    public void DoNothing() => true.Should().BeTrue();
}
