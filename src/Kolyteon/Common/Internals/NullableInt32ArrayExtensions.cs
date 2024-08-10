namespace Kolyteon.Common.Internals;

internal static class NullableInt32ArrayExtensions
{
    internal static void SwapNumbers(this int?[,] grid, int x, int y)
    {
        for (int column = grid.GetLength(1) - 1; column >= 0; column--)
        {
            for (int row = grid.GetLength(0) - 1; row >= 0; row--)
            {
                if (grid[row, column].GetValueOrDefault() is var n && (n == x || n == y))
                {
                    grid[row, column] = n == x ? y : x;
                }
            }
        }
    }

    internal static void SwapColumns(this int?[,] grid, int columnX, int columnY)
    {
        for (int row = grid.GetLength(0) - 1; row >= 0; row--)
        {
            (grid[row, columnX], grid[row, columnY]) = (grid[row, columnY], grid[row, columnX]);
        }
    }

    internal static void SwapRows(this int?[,] grid, int rowX, int rowY)
    {
        for (int column = grid.GetLength(1) - 1; column >= 0; column--)
        {
            (grid[rowX, column], grid[rowY, column]) = (grid[rowY, column], grid[rowX, column]);
        }
    }

    internal static bool EliminateNumberInSquare(this int?[,] grid, in Square square)
    {
        (int column, int row) = square;

        if (!grid[row, column].HasValue)
        {
            return false;
        }

        grid[row, column] = null;

        return true;
    }
}
