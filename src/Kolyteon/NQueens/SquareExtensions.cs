using Kolyteon.Common;

namespace Kolyteon.NQueens;

/// <summary>
///     Extends the <see cref="Square" /> struct type with an additional method for the <i>N</i>-Queens problem type.
/// </summary>
public static class SquareExtensions
{
    /// <summary>
    ///     Compares this <see cref="Square" /> instance against another instance of the same type and returns a value
    ///     indicating whether a pair of queens positioned on the squares capture each other.
    /// </summary>
    /// <remarks>In chess, a queen can capture any piece on the same column, row, or diagonal.</remarks>
    /// <param name="square">The <see cref="Square" /> on which the method is invoked.</param>
    /// <param name="other">The <see cref="Square" /> against which this instance is to be compared.</param>
    /// <returns><see langword="true" /> if the squares capture each other; otherwise, <see langword="false" />.</returns>
    public static bool Captures(this Square square, in Square other) =>
        square.Column == other.Column
        || square.Row == other.Row
        || Math.Abs(square.Column - other.Column) == Math.Abs(square.Row - other.Row);
}
