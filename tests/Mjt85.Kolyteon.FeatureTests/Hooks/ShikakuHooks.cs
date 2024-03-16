using BoDi;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Shikaku;
using Mjt85.Kolyteon.Solving.Common;
using Mjt85.Kolyteon.Solving.Silent;

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
        SilentBinaryCspSolver<Hint, Rectangle> binaryCspSolver = CreateBinaryCspSolver
            .WithInitialCapacity(1)
            .AndInitialSearchStrategy(Search.Backtracking)
            .AndInitialOrderingStrategy(Ordering.None)
            .Silent()
            .Build<Hint, Rectangle>();

        objectContainer.RegisterInstanceAs<ISilentBinaryCspSolver<Hint, Rectangle>>(binaryCspSolver);
    }

    [BeforeScenario]
    [AfterScenario]
    [Scope(Feature = "Shikaku")]
    public static void ClearBinaryCsp(IModellingBinaryCsp<ShikakuPuzzle, Hint, Rectangle> binaryCsp)
    {
        binaryCsp.Clear();
    }
}
