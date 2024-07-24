using Kolyteon.Tests.Acceptance.TestUtils;
using Reqnroll;
using Reqnroll.Assist;

namespace Kolyteon.Tests.Acceptance.Hooks;

[Binding]
internal static class TestHooks
{
    [BeforeTestRun]
    internal static void RegisterValueRetrievers()
    {
        Service.Instance.ValueRetrievers.Register<BlockValueRetriever>();
        Service.Instance.ValueRetrievers.Register<SquareValueRetriever>();
        Service.Instance.ValueRetrievers.Register<ColourValueRetriever>();
        Service.Instance.ValueRetrievers.Register<ColourArrayValueRetriever>();
    }
}
