using Kolyteon.Common;
using Kolyteon.Futoshiki;
using Kolyteon.GraphColouring;
using Kolyteon.MapColouring;
using Kolyteon.Modelling;
using Kolyteon.NQueens;
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
    internal static void RegisterBinaryCspFactories(IObjectContainer objectContainer)
    {
        objectContainer.RegisterFactoryAs(CreateFutoshikiBinaryCsp);
        objectContainer.RegisterFactoryAs(CreateGraphColouringBinaryCsp);
        objectContainer.RegisterFactoryAs(CreateMapColouringBinaryCsp);
        objectContainer.RegisterFactoryAs(CreateNQueensBinaryCsp);
    }

    [AfterScenario]
    internal static void ClearBinaryCsp(IBinaryCsp<Square, int, FutoshikiProblem> binaryCsp) => binaryCsp.Clear();

    [AfterScenario]
    internal static void ClearBinaryCsp(IBinaryCsp<Node, Colour, GraphColouringProblem> binaryCsp) => binaryCsp.Clear();

    [AfterScenario]
    internal static void ClearBinaryCsp(IBinaryCsp<Block, Colour, MapColouringProblem> binaryCsp) => binaryCsp.Clear();

    [AfterScenario]
    internal static void ClearBinaryCsp(IBinaryCsp<int, Square, NQueensProblem> binaryCsp) => binaryCsp.Clear();

    private static IBinaryCsp<Square, int, FutoshikiProblem> CreateFutoshikiBinaryCsp(IObjectContainer _) =>
        new FutoshikiConstraintGraph(10);

    private static IBinaryCsp<Node, Colour, GraphColouringProblem> CreateGraphColouringBinaryCsp(IObjectContainer _) =>
        new GraphColouringConstraintGraph(8);

    private static IBinaryCsp<Block, Colour, MapColouringProblem> CreateMapColouringBinaryCsp(IObjectContainer _) =>
        new MapColouringConstraintGraph(10);

    private static IBinaryCsp<int, Square, NQueensProblem> CreateNQueensBinaryCsp(IObjectContainer _) =>
        new NQueensConstraintGraph(5);
}
