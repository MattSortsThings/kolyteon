using Kolyteon.Common;
using Reqnroll.Assist.ValueRetrievers;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal sealed class NumberedSquareValueRetriever : StructRetriever<NumberedSquare>
{
    protected override NumberedSquare GetNonEmptyValue(string value) => NumberedSquare.Parse(value);
}
