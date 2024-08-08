using Kolyteon.GraphColouring;
using Kolyteon.MapColouring;
using Kolyteon.Sudoku;
using Reqnroll;
using Reqnroll.BoDi;

namespace Kolyteon.Tests.Acceptance.Hooks;

[Binding]
internal static class ProblemGeneratorHooks
{
    [BeforeTestRun]
    internal static void RegisterProblemGenerators(IObjectContainer objectContainer)
    {
        objectContainer.RegisterFactoryAs(CreateGraphColouringGenerator);
        objectContainer.RegisterFactoryAs(CreateMapColouringGenerator);
        objectContainer.RegisterFactoryAs(CreateSudokuGenerator);
    }

    private static IGraphColouringGenerator CreateGraphColouringGenerator(IObjectContainer _) => new GraphColouringGenerator();

    private static IMapColouringGenerator CreateMapColouringGenerator(IObjectContainer _) => new MapColouringGenerator();

    private static ISudokuGenerator CreateSudokuGenerator(IObjectContainer _) => new SudokuGenerator();
}
