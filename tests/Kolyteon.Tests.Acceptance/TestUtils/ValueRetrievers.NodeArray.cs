using Reqnroll.Assist;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal static partial class ValueRetrievers
{
    internal sealed class NodeArray : IValueRetriever
    {
        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) =>
            keyValuePair.Key.EndsWith("Nodes");

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) =>
            keyValuePair.Value.Split(", ").Select(GraphColouring.Node.FromName).ToArray();
    }
}
