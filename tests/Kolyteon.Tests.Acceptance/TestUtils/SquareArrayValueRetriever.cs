using Kolyteon.Common;
using Reqnroll.Assist;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal sealed class SquareArrayValueRetriever : IValueRetriever
{
    public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) =>
        propertyType == typeof(Square[]);

    public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) =>
        keyValuePair.Value.Length <= 1
            ? []
            : keyValuePair.Value.Split(", ").Select(Square.Parse).ToArray();
}
