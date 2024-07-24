using Kolyteon.Common;
using Reqnroll.Assist.ValueRetrievers;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal sealed class ColourValueRetriever : StructRetriever<Colour>
{
    protected override Colour GetNonEmptyValue(string value) => Colour.FromName(value);
}
