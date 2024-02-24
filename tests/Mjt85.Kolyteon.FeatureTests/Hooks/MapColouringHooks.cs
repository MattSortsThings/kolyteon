using BoDi;
using Mjt85.Kolyteon.FeatureTests.ValueRetrievers;
using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving;
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

    [BeforeFeature]
    [Scope(Feature = "Map Colouring")]
    public static void RegisterBinaryCspSolver(IObjectContainer objectContainer)
    {
        BinaryCspSolver<Region, Colour> binaryCspSolver = BinaryCspSolver<Region, Colour>.Create()
            .WithInitialCapacity(1)
            .AndInitialSearchStrategy(Search.Backtracking)
            .AndInitialOrderingStrategy(Ordering.None)
            .Build();
        objectContainer.RegisterInstanceAs<IBinaryCspSolver<Region, Colour>>(binaryCspSolver);
    }

    [BeforeScenario]
    [AfterScenario]
    [Scope(Feature = "Map Colouring")]
    public static void ClearBinaryCsp(IModellingBinaryCsp<MapColouringPuzzle, Region, Colour> binaryCsp)
    {
        binaryCsp.Clear();
    }
}
