using Kolyteon.Common;
using Reqnroll.Assist.ValueRetrievers;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal sealed class SquareValueRetriever : StructRetriever<Square>
{
    protected override Square GetNonEmptyValue(string value) => Square.Parse(value);
}
