using BoDi;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Shikaku;

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

    [BeforeScenario]
    [AfterScenario]
    [Scope(Feature = "Shikaku")]
    public static void ClearBinaryCsp(IModellingBinaryCsp<ShikakuPuzzle, Hint, Rectangle> binaryCsp)
    {
        binaryCsp.Clear();
    }
}
