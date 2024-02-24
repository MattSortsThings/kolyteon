using BoDi;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.NQueens;
using Mjt85.Kolyteon.Solving;

namespace Mjt85.Kolyteon.FeatureTests.Hooks;

[Binding]
public sealed class NQueensHooks
{
    [BeforeFeature]
    [Scope(Feature = "N-Queens")]
    public static void RegisterBinaryCsp(IObjectContainer objectContainer)
    {
        NQueensBinaryCsp binaryCsp = new(5);
        objectContainer.RegisterInstanceAs<IModellingBinaryCsp<NQueensPuzzle, int, Queen>>(binaryCsp);
    }

    [BeforeFeature]
    [Scope(Feature = "N-Queens")]
    public static void RegisterBinaryCspSolver(IObjectContainer objectContainer)
    {
        BinaryCspSolver<int, Queen> binaryCspSolver = BinaryCspSolver<int, Queen>.Create()
            .WithInitialCapacity(1)
            .AndInitialSearchStrategy(Search.Backtracking)
            .AndInitialOrderingStrategy(Ordering.None)
            .Build();
        objectContainer.RegisterInstanceAs<IBinaryCspSolver<int, Queen>>(binaryCspSolver);
    }

    [BeforeScenario]
    [AfterScenario]
    [Scope(Feature = "N-Queens")]
    public static void ClearBinaryCsp(IModellingBinaryCsp<NQueensPuzzle, int, Queen> binaryCsp)
    {
        binaryCsp.Clear();
    }
}
