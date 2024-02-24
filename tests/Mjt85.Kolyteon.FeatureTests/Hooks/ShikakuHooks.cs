using BoDi;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Shikaku;
using Mjt85.Kolyteon.Solving;

namespace Mjt85.Kolyteon.FeatureTests.Hooks;

[Binding]
public sealed class ShikakuHooks
{
    [BeforeFeature]
    [Scope(Feature = "Shikaku")]
    public static void RegisterBinaryCsp(IObjectContainer objectContainer)
    {
        ShikakuBinaryCsp binaryCsp = new(10);
        objectContainer.RegisterInstanceAs<IModellingBinaryCsp<ShikakuPuzzle, Hint, Rectangle>>(binaryCsp);
    }

    [BeforeFeature]
    [Scope(Feature = "Shikaku")]
    public static void RegisterBinaryCspSolver(IObjectContainer objectContainer)
    {
        BinaryCspSolver<Hint, Rectangle> binaryCspSolver = BinaryCspSolver<Hint, Rectangle>.Create()
            .WithInitialCapacity(1)
            .AndInitialSearchStrategy(Search.Backtracking)
            .AndInitialOrderingStrategy(Ordering.None)
            .Build();
        objectContainer.RegisterInstanceAs<IBinaryCspSolver<Hint, Rectangle>>(binaryCspSolver);
    }

    [BeforeScenario]
    [AfterScenario]
    [Scope(Feature = "Shikaku")]
    public static void ClearBinaryCsp(IModellingBinaryCsp<ShikakuPuzzle, Hint, Rectangle> binaryCsp)
    {
        binaryCsp.Clear();
    }
}
