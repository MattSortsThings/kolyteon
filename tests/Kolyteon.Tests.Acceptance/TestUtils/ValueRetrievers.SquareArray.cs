using Reqnroll.Assist;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal static partial class ValueRetrievers
{
    internal sealed class SquareArray : IValueRetriever
    {
        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) =>
            propertyType == typeof(Common.Square[]);

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) =>
            keyValuePair.Value.Length <= 1
                ? []
                : keyValuePair.Value.Split(", ").Select(Common.Square.Parse).ToArray();
    }
}
