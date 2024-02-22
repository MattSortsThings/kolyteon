using BoDi;
using Mjt85.Kolyteon.FeatureTests.ValueRetrievers;
using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.Modelling;
using TechTalk.SpecFlow.Assist;

namespace Mjt85.Kolyteon.FeatureTests.Hooks;

[Binding]
public sealed class MapColouringHooks
{
    [BeforeTestRun]
    public static void RegisterValueRetrievers()
    {
        Service.Instance.ValueRetrievers.Register(new ColourArrayValueRetriever());
        Service.Instance.ValueRetrievers.Register(new ColourValueRetriever());
        Service.Instance.ValueRetrievers.Register(new PresetMapValueRetriever());
        Service.Instance.ValueRetrievers.Register(new RegionValueRetriever());
    }

    [BeforeFeature]
    [Scope(Feature = "Map Colouring")]
    public static void RegisterBinaryCsp(IObjectContainer objectContainer)
    {
        MapColouringBinaryCsp binaryCsp = new(10);
        objectContainer.RegisterInstanceAs<IModellingBinaryCsp<MapColouringPuzzle, Region, Colour>>(binaryCsp);
    }

    [BeforeScenario]
    [AfterScenario]
    [Scope(Feature = "Map Colouring")]
    public static void ClearBinaryCsp(IModellingBinaryCsp<MapColouringPuzzle, Region, Colour> binaryCsp)
    {
        binaryCsp.Clear();
    }
}
