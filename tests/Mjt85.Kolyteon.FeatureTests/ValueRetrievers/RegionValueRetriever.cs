using Mjt85.Kolyteon.MapColouring;
using TechTalk.SpecFlow.Assist;

namespace Mjt85.Kolyteon.FeatureTests.ValueRetrievers;

internal class RegionValueRetriever : IValueRetriever
{
    public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) =>
        keyValuePair.Key.Equals("Region") || targetType == typeof(Region);

    public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) =>
        Region.FromId(keyValuePair.Value);
}
