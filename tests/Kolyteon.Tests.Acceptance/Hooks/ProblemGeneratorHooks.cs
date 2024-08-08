using Kolyteon.Sudoku;
using Reqnroll;
using Reqnroll.BoDi;

namespace Kolyteon.Tests.Acceptance.Hooks;

[Binding]
internal static class ProblemGeneratorHooks
{
    [BeforeTestRun]
    internal static void RegisterProblemGenerators(IObjectContainer objectContainer) =>
        objectContainer.RegisterFactoryAs(CreateSudokuGenerator);

    private static ISudokuGenerator CreateSudokuGenerator(IObjectContainer _) => new SudokuGenerator();
}
