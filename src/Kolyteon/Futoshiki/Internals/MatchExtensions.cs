using System.Text.RegularExpressions;
using Kolyteon.Common;

namespace Kolyteon.Futoshiki.Internals;

internal static class MatchExtensions
{
    internal static GreaterThanSign ToGreaterThanSign(this Match match) =>
        GreaterThanSign.Between(
            Square.FromColumnAndRow(int.Parse(match.Groups["column1"].Value), int.Parse(match.Groups["row1"].Value)),
            Square.FromColumnAndRow(int.Parse(match.Groups["column2"].Value), int.Parse(match.Groups["row2"].Value))
        );

    internal static LessThanSign ToLessThanSign(this Match match) =>
        LessThanSign.Between(
            Square.FromColumnAndRow(int.Parse(match.Groups["column1"].Value), int.Parse(match.Groups["row1"].Value)),
            Square.FromColumnAndRow(int.Parse(match.Groups["column2"].Value), int.Parse(match.Groups["row2"].Value))
        );
}
