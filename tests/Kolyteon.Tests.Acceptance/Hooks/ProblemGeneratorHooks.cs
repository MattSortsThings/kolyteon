using Kolyteon.Futoshiki;
using Kolyteon.GraphColouring;
using Kolyteon.MapColouring;
using Kolyteon.Shikaku;
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
        objectContainer.RegisterFactoryAs(CreateFutoshikiGenerator);
        objectContainer.RegisterFactoryAs(CreateGraphColouringGenerator);
        objectContainer.RegisterFactoryAs(CreateMapColouringGenerator);
        objectContainer.RegisterFactoryAs(CreateShikakuGenerator);
        objectContainer.RegisterFactoryAs(CreateSudokuGenerator);
    }

    private static IFutoshikiGenerator CreateFutoshikiGenerator(IObjectContainer _) => new FutoshikiGenerator();

    private static IGraphColouringGenerator CreateGraphColouringGenerator(IObjectContainer _) => new GraphColouringGenerator();

    private static IMapColouringGenerator CreateMapColouringGenerator(IObjectContainer _) => new MapColouringGenerator();

    private static IShikakuGenerator CreateShikakuGenerator(IObjectContainer _) => new ShikakuGenerator();

    private static ISudokuGenerator CreateSudokuGenerator(IObjectContainer _) => new SudokuGenerator();
}
