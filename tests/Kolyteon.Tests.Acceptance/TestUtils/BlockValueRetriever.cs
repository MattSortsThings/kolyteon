using System.Text.RegularExpressions;
using Kolyteon.Common;
using Reqnroll.Assist.ValueRetrievers;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal sealed partial class BlockValueRetriever : StructRetriever<Block>
{
    private static readonly Regex BlockRegex = GeneratedBlockRegex();

    protected override Block GetNonEmptyValue(string value)
    {
        Match match = BlockRegex.Match(value);

        int originColumn = int.Parse(match.Groups["col"].Value);
        int originRow = int.Parse(match.Groups["row"].Value);
        int width = int.Parse(match.Groups["width"].Value);
        int height = int.Parse(match.Groups["height"].Value);

        return Square.FromColumnAndRow(originColumn, originRow).ToBlock(Dimensions.FromWidthAndHeight(width, height));
    }

    [GeneratedRegex(@"\((?<col>[0-9]+),(?<row>[0-9]+)\) \[(?<width>[0-9]+)x(?<height>[0-9]+)\]", RegexOptions.Compiled)]
    private static partial Regex GeneratedBlockRegex();
}
