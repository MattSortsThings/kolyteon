using System.Text.RegularExpressions;

namespace Kolyteon.Common.Internals;

internal static class MatchExtensions
{
    internal static Block ToBlock(this Match match) =>
        Square.FromColumnAndRow(int.Parse(match.Groups["column"].Value),
                int.Parse(match.Groups["row"].Value))
            .ToBlock(Dimensions.FromWidthAndHeight(int.Parse(match.Groups["width"].Value),
                int.Parse(match.Groups["height"].Value)));

    internal static Dimensions ToDimensions(this Match match) =>
        Dimensions.FromWidthAndHeight(int.Parse(match.Groups["width"].Value),
            int.Parse(match.Groups["height"].Value));

    internal static NumberedSquare ToNumberedSquare(this Match match) =>
        Square.FromColumnAndRow(int.Parse(match.Groups["column"].Value),
                int.Parse(match.Groups["row"].Value))
            .ToNumberedSquare(int.Parse(match.Groups["number"].Value));

    internal static Square ToSquare(this Match match) =>
        Square.FromColumnAndRow(int.Parse(match.Groups["column"].Value),
            int.Parse(match.Groups["row"].Value));
}
