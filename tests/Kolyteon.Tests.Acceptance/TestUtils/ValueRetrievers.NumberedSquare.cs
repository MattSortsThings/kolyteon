using Reqnroll.Assist.ValueRetrievers;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal static partial class ValueRetrievers
{
    internal sealed class NumberedSquare : StructRetriever<Common.NumberedSquare>
    {
        protected override Common.NumberedSquare GetNonEmptyValue(string value) => Common.NumberedSquare.Parse(value);
    }
}
