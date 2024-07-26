using System.Text.RegularExpressions;
using Kolyteon.Common;
using Reqnroll.Assist.ValueRetrievers;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal sealed partial class NumberedSquareValueRetriever : StructRetriever<NumberedSquare>
{
    private static readonly Regex NumberedSquareRegex = GeneratedRegex();

    protected override NumberedSquare GetNonEmptyValue(string value)
    {
        Match match = NumberedSquareRegex.Match(value);

        int col = int.Parse(match.Groups["col"].Value);
        int row = int.Parse(match.Groups["row"].Value);
        int num = int.Parse(match.Groups["num"].Value);

        return Square.FromColumnAndRow(col, row).ToNumberedSquare(num);
    }

    [GeneratedRegex(@"\((?<col>[0-9]+),(?<row>[0-9]+)\) \[(?<num>[0-9]+)\]", RegexOptions.Multiline)]
    private static partial Regex GeneratedRegex();
}
