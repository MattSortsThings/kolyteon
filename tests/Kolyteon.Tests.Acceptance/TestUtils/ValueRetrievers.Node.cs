using Reqnroll.Assist.ValueRetrievers;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal static partial class ValueRetrievers
{
    internal class Node : StructRetriever<GraphColouring.Node>
    {
        protected override GraphColouring.Node GetNonEmptyValue(string value) => GraphColouring.Node.FromName(value);
    }
}
