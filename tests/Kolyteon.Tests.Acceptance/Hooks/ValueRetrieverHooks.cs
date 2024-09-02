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
        Service.Instance.ValueRetrievers.Register<ValueRetrievers.Block>();
        Service.Instance.ValueRetrievers.Register<ValueRetrievers.Colour>();
        Service.Instance.ValueRetrievers.Register<ValueRetrievers.ColourArray>();
        Service.Instance.ValueRetrievers.Register<ValueRetrievers.Node>();
        Service.Instance.ValueRetrievers.Register<ValueRetrievers.NodeArray>();
        Service.Instance.ValueRetrievers.Register<ValueRetrievers.NumberedSquare>();
        Service.Instance.ValueRetrievers.Register<ValueRetrievers.Square>();
        Service.Instance.ValueRetrievers.Register<ValueRetrievers.SquareArray>();
    }
}
