using Kolyteon.Common;

namespace Kolyteon.Shikaku.Internals;

internal static class NullableInt32ArrayExtensions
{
    internal static ShikakuProblem ToShikakuProblem(this int?[,] grid)
    {
        int?[,] targetGrid = grid;

        int rows = targetGrid.GetLength(0);
        int columns = targetGrid.GetLength(1);

        IEnumerable<NumberedSquare> hintsQuery = from column in Enumerable.Range(0, columns)
            from row in Enumerable.Range(0, rows)
            where targetGrid[row, column].HasValue
            select Square.FromColumnAndRow(column, row).ToNumberedSquare(targetGrid[row, column].GetValueOrDefault());

        return new ShikakuProblem(Dimensions.FromWidthAndHeight(columns, rows).ToBlock(),
            hintsQuery.ToArray());
    }
}
