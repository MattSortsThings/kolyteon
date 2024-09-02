using Reqnroll.Assist.ValueRetrievers;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal static partial class ValueRetrievers
{
    internal sealed class Block : StructRetriever<Common.Block>
    {
        protected override Common.Block GetNonEmptyValue(string value) => Common.Block.Parse(value);
    }
}
