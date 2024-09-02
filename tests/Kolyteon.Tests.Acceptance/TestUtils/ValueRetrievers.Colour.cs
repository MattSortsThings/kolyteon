using Reqnroll.Assist.ValueRetrievers;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal static partial class ValueRetrievers
{
    internal sealed class Colour : StructRetriever<Common.Colour>
    {
        protected override Common.Colour GetNonEmptyValue(string value) => Common.Colour.FromName(value);
    }
}
