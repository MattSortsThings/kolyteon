using Kolyteon.Common;

namespace Kolyteon.Sudoku;

/// <summary>
///     Extends the <see cref="Square" /> struct type with an additional method for the Sudoku problem type.
/// </summary>
public static class SquareExtensions
{
    /// <summary>
    ///     Returns an integer in the range [0,8] denoting the sector of the square represented by this <see cref="Square" />
    ///     instance in a 9x9 grid of squares.
    /// </summary>
    /// <param name="square">The <see cref="Square" /> instance on which the method is invoked.</param>
    /// <returns>An integer in the range [0,8]. The square's sector in the grid.</returns>
    public static int GetSector(this Square square)
    {
        (int column, int row) = square;

        return (row / 3) + (3 * (column / 3));
    }
}
