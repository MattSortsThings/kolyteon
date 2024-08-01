using Kolyteon.Common;
using Reqnroll.Assist.ValueRetrievers;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal sealed class BlockValueRetriever : StructRetriever<Block>
{
    protected override Block GetNonEmptyValue(string value) => Block.Parse(value);
}
