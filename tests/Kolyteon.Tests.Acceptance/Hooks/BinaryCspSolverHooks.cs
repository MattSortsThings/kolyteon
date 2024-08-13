using Kolyteon.Common;
using Kolyteon.GraphColouring;
using Kolyteon.Solving;
using Reqnroll;
using Reqnroll.BoDi;

namespace Kolyteon.Tests.Acceptance.Hooks;

[Binding]
internal static class BinaryCspSolverHooks
{
    [BeforeTestRun]
    internal static void RegisterBinaryCspSolverFactories(IObjectContainer objectContainer)
    {
        objectContainer.RegisterFactoryAs(CreateFutoshikiAndSudokuSolver);
        objectContainer.RegisterFactoryAs(CreateGraphColouringSolver);
        objectContainer.RegisterFactoryAs(CreateMapColouringSolver);
        objectContainer.RegisterFactoryAs(CreateNQueensSolver);
        objectContainer.RegisterFactoryAs(CreateShikakuSolver);
    }

    private static IBinaryCspSolver<Square, int> CreateFutoshikiAndSudokuSolver(IObjectContainer _) =>
        BinaryCspSolver<Square, int>.Create()
            .WithCapacity(12)
            .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
            .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
            .Build();

    private static IBinaryCspSolver<Node, Colour> CreateGraphColouringSolver(IObjectContainer _) =>
        BinaryCspSolver<Node, Colour>.Create()
            .WithCapacity(12)
            .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
            .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
            .Build();

    private static IBinaryCspSolver<Block, Colour> CreateMapColouringSolver(IObjectContainer _) =>
        BinaryCspSolver<Block, Colour>.Create()
            .WithCapacity(12)
            .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
            .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
            .Build();

    private static IBinaryCspSolver<int, Square> CreateNQueensSolver(IObjectContainer _) =>
        BinaryCspSolver<int, Square>.Create()
            .WithCapacity(12)
            .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
            .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
            .Build();

    private static IBinaryCspSolver<NumberedSquare, Block> CreateShikakuSolver(IObjectContainer _) =>
        BinaryCspSolver<NumberedSquare, Block>.Create()
            .WithCapacity(12)
            .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
            .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
            .Build();
}
