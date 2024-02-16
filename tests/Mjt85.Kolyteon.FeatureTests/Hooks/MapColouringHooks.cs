using Mjt85.Kolyteon.FeatureTests.ValueRetrievers;
using TechTalk.SpecFlow.Assist;

namespace Mjt85.Kolyteon.FeatureTests.Hooks;

[Binding]
public sealed class MapColouringHooks
{
    [BeforeTestRun]
    public static void RegisterValueRetrievers()
    {
        Service.Instance.ValueRetrievers.Register(new ColourArrayValueRetriever());
        Service.Instance.ValueRetrievers.Register(new PresetMapValueRetriever());
    }
}
