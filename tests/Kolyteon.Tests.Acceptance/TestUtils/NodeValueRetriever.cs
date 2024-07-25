using Kolyteon.GraphColouring;
using Reqnroll.Assist.ValueRetrievers;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal class NodeValueRetriever : StructRetriever<Node>
{
    protected override Node GetNonEmptyValue(string value) => Node.FromName(value);
}
