using Reqnroll.Assist;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal static partial class ValueRetrievers
{
    internal sealed class ColourArray : IValueRetriever
    {
        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) =>
            keyValuePair.Key.EndsWith("Colours");

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) =>
            keyValuePair.Value.Split(", ").Select(Common.Colour.FromName).ToArray();
    }
}
