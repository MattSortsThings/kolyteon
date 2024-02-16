using Mjt85.Kolyteon.MapColouring;
using TechTalk.SpecFlow.Assist;

namespace Mjt85.Kolyteon.FeatureTests.ValueRetrievers;

internal class ColourArrayValueRetriever : IValueRetriever
{
    public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) =>
        propertyType == typeof(Colour[]) || keyValuePair.Key.EndsWith("Colours", StringComparison.Ordinal);

    public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) => keyValuePair.Value
        .Split(",")
        .Select(Colour.FromName)
        .ToArray();
}
