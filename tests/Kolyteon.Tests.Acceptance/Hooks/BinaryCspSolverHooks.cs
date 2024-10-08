using Kolyteon.Common;
using Kolyteon.GraphColouring;
using Kolyteon.Solving;
using Kolyteon.Tests.Acceptance.TestUtils;
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

    [BeforeTestRun]
    internal static void RegisterNQueensVerboseSolverAndReporter(IObjectContainer objectContainer)
    {
        IVerboseBinaryCspSolver<int, Square> verboseSolver = VerboseBinaryCspSolver<int, Square>.Create()
            .WithCapacity(4)
            .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
            .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
            .AndStepDelay(TimeSpan.Zero)
            .Build();

        objectContainer.RegisterInstanceAs(verboseSolver, typeof(IVerboseBinaryCspSolver<int, Square>));

        objectContainer.RegisterInstanceAs(new NQueensSolvingProgressReporter());
    }

    private static ISilentBinaryCspSolver<Square, int> CreateFutoshikiAndSudokuSolver(IObjectContainer _) =>
        SilentBinaryCspSolver<Square, int>.Create()
            .WithCapacity(12)
            .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
            .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
            .Build();

    private static ISilentBinaryCspSolver<Node, Colour> CreateGraphColouringSolver(IObjectContainer _) =>
        SilentBinaryCspSolver<Node, Colour>.Create()
            .WithCapacity(12)
            .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
            .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
            .Build();

    private static ISilentBinaryCspSolver<Block, Colour> CreateMapColouringSolver(IObjectContainer _) =>
        SilentBinaryCspSolver<Block, Colour>.Create()
            .WithCapacity(12)
            .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
            .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
            .Build();

    private static ISilentBinaryCspSolver<int, Square> CreateNQueensSolver(IObjectContainer _) =>
        SilentBinaryCspSolver<int, Square>.Create()
            .WithCapacity(12)
            .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
            .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
            .Build();

    private static ISilentBinaryCspSolver<NumberedSquare, Block> CreateShikakuSolver(IObjectContainer _) =>
        SilentBinaryCspSolver<NumberedSquare, Block>.Create()
            .WithCapacity(12)
            .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
            .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
            .Build();
}
