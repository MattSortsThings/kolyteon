using Kolyteon.Common;

namespace Kolyteon.Sudoku.Internals;

internal static class NullableInt32ArrayExtensions
{
    internal static SudokuProblem ToSudokuProblem(this int?[,] grid)
    {
        int?[,] targetGrid = grid;

        int rows = targetGrid.GetLength(0);
        int columns = targetGrid.GetLength(1);

        IEnumerable<NumberedSquare> hintsQuery = from column in Enumerable.Range(0, columns)
            from row in Enumerable.Range(0, rows)
            where targetGrid[row, column].HasValue
            select Square.FromColumnAndRow(column, row).ToNumberedSquare(targetGrid[row, column].GetValueOrDefault());

        return new SudokuProblem(Dimensions.FromWidthAndHeight(columns, rows).ToBlock(),
            CreateSudokuGridSectors(),
            hintsQuery.ToArray());
    }


    private static Block[] CreateSudokuGridSectors() =>
    [
        Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
        Square.FromColumnAndRow(0, 3).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
        Square.FromColumnAndRow(0, 6).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
        Square.FromColumnAndRow(3, 0).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
        Square.FromColumnAndRow(3, 3).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
        Square.FromColumnAndRow(3, 6).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
        Square.FromColumnAndRow(6, 0).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
        Square.FromColumnAndRow(6, 3).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
        Square.FromColumnAndRow(6, 6).ToBlock(Dimensions.FromWidthAndHeight(3, 3))
    ];
}
