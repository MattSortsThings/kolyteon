using System.Text.RegularExpressions;
using Kolyteon.Common;
using Reqnroll.Assist.ValueRetrievers;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal sealed class SquareValueRetriever : StructRetriever<Square>
{
    private static readonly Regex SquareRegex = new(@"\((?<col>[0-9]+),(?<row>[0-9]+)\)");

    protected override Square GetNonEmptyValue(string value)
    {
        Match match = SquareRegex.Match(value);

        int column = int.Parse(match.Groups["col"].Value);
        int row = int.Parse(match.Groups["row"].Value);

        return Square.FromColumnAndRow(column, row);
    }
}
