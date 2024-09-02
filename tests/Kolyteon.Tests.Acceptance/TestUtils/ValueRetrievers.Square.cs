using Reqnroll.Assist.ValueRetrievers;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal static partial class ValueRetrievers
{
    internal sealed class Square : StructRetriever<Common.Square>
    {
        protected override Common.Square GetNonEmptyValue(string value) => Common.Square.Parse(value);
    }
}
