using Kolyteon.Common;
using Reqnroll.Assist;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal sealed class ColourArrayValueRetriever : IValueRetriever
{
    public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) =>
        keyValuePair.Key.EndsWith("Colours");

    public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) =>
        keyValuePair.Value.Split(", ").Select(Colour.FromName).ToArray();
}
