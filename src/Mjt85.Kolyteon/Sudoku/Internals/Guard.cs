namespace Mjt85.Kolyteon.Sudoku.Internals;

/// <summary>
///     Guard clauses.
/// </summary>
internal static class Guard
{
    /// <summary>
    ///     Checks that the specified grid has valid dimensions for a Sudoku puzzle and throws and exception if it is invalid.
    /// </summary>
    /// <param name="grid">The grid to be checked.</param>
    /// <exception cref="ArgumentException">
    ///     The <paramref name="grid" /> parameter has a rank 0 length not equal to 9 or a rank 1 length not equal to 9.
    /// </exception>
    public static void AgainstInvalidGridDimensions(int?[,] grid)
    {
        var rank0Length = grid.GetLength(0);
        var rank1Length = grid.GetLength(1);
        if (rank0Length != 9 || rank1Length != 9)
        {
            throw new ArgumentException("Grid must be a 9x9 square.");
        }
    }

    /// <summary>
    ///     Checks that the specified <see cref="SudokuPuzzle" /> represents a valid Sudoku puzzle and throws an exception if
    ///     it is invalid.
    /// </summary>
    /// <param name="puzzle">The <see cref="SudokuPuzzle" /> to be checked.</param>
    /// <exception cref="InvalidOperationException">
    ///     The <see cref="puzzle" /> parameter does not represent a valid Sudoku puzzle.
    /// </exception>
    public static void AgainstInvalidSudokuPuzzle(SudokuPuzzle puzzle)
    {
        ThrowIfNoEmptyCells(puzzle.FilledCells);
        ThrowIfObstructingCells(puzzle.FilledCells);
    }

    private static void ThrowIfNoEmptyCells(IReadOnlyCollection<FilledCell> filledCells)
    {
        if (filledCells.Count >= SudokuPuzzle.GridSideLength * SudokuPuzzle.GridSideLength)
        {
            throw new InvalidOperationException("No empty cells.");
        }
    }

    private static void ThrowIfObstructingCells(IReadOnlyList<FilledCell> filledCells)
    {
        for (var i = 1; i < filledCells.Count; i++)
        {
            FilledCell filledCellAtI = filledCells[i];

            for (var h = 0; h < i; h++)
            {
                FilledCell filledCellAtH = filledCells[h];

                if (filledCellAtH.Obstructs(in filledCellAtI))
                {
                    throw new InvalidOperationException(
                        $"Filled cells {filledCellAtH} and {filledCellAtI} obstruct each other.");
                }
            }
        }
    }
}
