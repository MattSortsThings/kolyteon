using Kolyteon.GraphColouring;
using Reqnroll.Assist;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal sealed class NodeArrayValueRetriever : IValueRetriever
{
    public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) =>
        keyValuePair.Key.EndsWith("Nodes");

    public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) =>
        keyValuePair.Value.Split(", ").Select(Node.FromName).ToArray();
}
