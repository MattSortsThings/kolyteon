using Kolyteon.Common;
using Kolyteon.Futoshiki;
using Kolyteon.Modelling;
using Kolyteon.Tests.Acceptance.TestUtils;
using Reqnroll;
using Reqnroll.Assist;
using Reqnroll.BoDi;

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
        Service.Instance.ValueRetrievers.Register<NumberedSquareValueRetriever>();
        Service.Instance.ValueRetrievers.Register<NodeValueRetriever>();
        Service.Instance.ValueRetrievers.Register<NodeArrayValueRetriever>();
    }

    [BeforeTestRun]
    internal static void RegisterBinaryCspFactories(IObjectContainer objectContainer) =>
        objectContainer.RegisterFactoryAs(CreateSudokuBinaryCsp);

    [AfterScenario]
    internal static void ClearBinaryCsp(IBinaryCsp<Square, int, FutoshikiProblem> binaryCsp) => binaryCsp.Clear();

    private static IBinaryCsp<Square, int, FutoshikiProblem> CreateSudokuBinaryCsp(IObjectContainer _) =>
        new FutoshikiConstraintGraph(17);
}
