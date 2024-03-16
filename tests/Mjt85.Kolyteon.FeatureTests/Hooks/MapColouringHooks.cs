using BoDi;
using Mjt85.Kolyteon.FeatureTests.ValueRetrievers;
using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Common;
using Mjt85.Kolyteon.Solving.Silent;
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
        MapColouringBinaryCsp binaryCsp = MapColouringBinaryCsp.WithInitialCapacity(10);
        objectContainer.RegisterInstanceAs<IModellingBinaryCsp<MapColouringPuzzle, Region, Colour>>(binaryCsp);
    }

    [BeforeFeature]
    [Scope(Feature = "Map Colouring")]
    public static void RegisterBinaryCspSolver(IObjectContainer objectContainer)
    {
        SilentBinaryCspSolver<Region, Colour> binaryCspSolver = CreateBinaryCspSolver
            .WithInitialCapacity(1)
            .AndInitialSearchStrategy(Search.Backtracking)
            .AndInitialOrderingStrategy(Ordering.None)
            .Silent()
            .Build<Region, Colour>();

        objectContainer.RegisterInstanceAs<ISilentBinaryCspSolver<Region, Colour>>(binaryCspSolver);
    }

    [BeforeScenario]
    [AfterScenario]
    [Scope(Feature = "Map Colouring")]
    public static void ClearBinaryCsp(IModellingBinaryCsp<MapColouringPuzzle, Region, Colour> binaryCsp)
    {
        binaryCsp.Clear();
    }
}
