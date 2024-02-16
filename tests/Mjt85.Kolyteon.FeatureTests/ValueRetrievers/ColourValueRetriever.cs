using Mjt85.Kolyteon.MapColouring;
using TechTalk.SpecFlow.Assist;

namespace Mjt85.Kolyteon.FeatureTests.ValueRetrievers;

internal class ColourValueRetriever : IValueRetriever
{
    public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) =>
        keyValuePair.Key == "Colour" || targetType == typeof(Colour);

    public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) =>
        Colour.FromName(keyValuePair.Value);
}
