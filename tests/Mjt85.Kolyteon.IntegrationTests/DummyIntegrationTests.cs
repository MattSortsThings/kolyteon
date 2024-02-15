namespace Mjt85.Kolyteon.IntegrationTests;

[IntegrationTest]
public class DummyIntegrationTests
{
    [Fact]
    public void DoNothing()
    {
        true.Should().BeTrue();
    }
}
