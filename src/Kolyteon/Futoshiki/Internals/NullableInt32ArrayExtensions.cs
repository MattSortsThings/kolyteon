using Kolyteon.Common;

namespace Kolyteon.Futoshiki.Internals;

internal static class NullableInt32ArrayExtensions
{
    internal static Block ToProblemGrid(this int?[,] grid) =>
        Dimensions.FromWidthAndHeight(grid.GetLength(1), grid.GetLength(0))
            .ToBlock();

    internal static NumberedSquare[] ToFilledSquares(this int?[,] grid)
    {
        int?[,] targetGrid = grid;

        int columns = grid.GetLength(1);
        int rows = grid.GetLength(0);

        return Enumerable.Range(0, columns)
            .SelectMany(col => Enumerable.Range(0, rows)
                .Where(row => targetGrid[row, col].HasValue)
                .Select(row =>
                    Square.FromColumnAndRow(col, row).ToNumberedSquare(targetGrid[row, col].GetValueOrDefault()))
            )
            .ToArray();
    }
}
