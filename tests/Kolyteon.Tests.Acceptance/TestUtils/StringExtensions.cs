using Kolyteon.Common;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal static class StringExtensions
{
    internal static Square ToSquare(this string value)
    {
        string[] items = value.TrimStart('(').TrimEnd(')').Split(',');

        int column = int.Parse(items[0]);
        int row = int.Parse(items[1]);

        return Square.FromColumnAndRow(column, row);
    }
}
