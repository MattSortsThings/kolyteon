namespace Mjt85.Kolyteon.Shikaku.Internals;

/// <summary>
///     Guard clauses.
/// </summary>
internal static class Guard
{
    /// <summary>
    ///     Checks that the specified grid has valid dimensions for a Shikaku puzzle and throws and exception if it is invalid.
    /// </summary>
    /// <param name="grid">The grid to be checked.</param>
    /// <exception cref="ArgumentException">
    ///     The <paramref name="grid" /> parameter has a rank 0 length less than 5 or a rank 1
    ///     length not equal to its rank 0 length.
    /// </exception>
    public static void AgainstInvalidGridDimensions(int?[,] grid)
    {
        var rank0Length = grid.GetLength(0);
        var rank1Length = grid.GetLength(1);
        if (rank0Length < 5 || rank1Length < 5 || rank0Length != rank1Length)
        {
            throw new ArgumentException("Grid must be a square no smaller than 5x5 in size.");
        }
    }

    /// <summary>
    ///     Checks that the specified <see cref="ShikakuPuzzle" /> represents a valid Shikaku puzzle and throws an exception if
    ///     it is invalid.
    /// </summary>
    /// <param name="puzzle">The <see cref="ShikakuPuzzle" /> to be checked.</param>
    /// <exception cref="InvalidOperationException">
    ///     The <see cref="puzzle" /> parameter does not represent a valid Shikaku
    ///     puzzle.
    /// </exception>
    public static void AgainstInvalidPuzzle(ShikakuPuzzle puzzle)
    {
        var gridArea = puzzle.GridSideLength * puzzle.GridSideLength;
        var sumHintNumbers = puzzle.Hints.Sum(h => h.Number);

        if (sumHintNumbers != gridArea)
        {
            throw new InvalidOperationException($"Hint numbers sum to {sumHintNumbers}, grid area is {gridArea}.");
        }
    }
}
