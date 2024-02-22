using BoDi;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Sudoku;

namespace Mjt85.Kolyteon.FeatureTests.Hooks;

[Binding]
public sealed class SudokuHooks
{
    [BeforeFeature]
    [Scope(Feature = "Sudoku")]
    public static void RegisterBinaryCsp(IObjectContainer objectContainer)
    {
        SudokuBinaryCsp binaryCsp = new(10);
        objectContainer.RegisterInstanceAs<IModellingBinaryCsp<SudokuPuzzle, EmptyCell, int>>(binaryCsp);
    }

    [BeforeScenario]
    [AfterScenario]
    [Scope(Feature = "Sudoku")]
    public static void ClearBinaryCsp(IModellingBinaryCsp<SudokuPuzzle, EmptyCell, int> binaryCsp)
    {
        binaryCsp.Clear();
    }
}
