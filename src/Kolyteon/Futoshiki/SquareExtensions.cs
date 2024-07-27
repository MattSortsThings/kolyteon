using Kolyteon.Common;

namespace Kolyteon.Futoshiki;

/// <summary>
///     Extends the <see cref="Square" /> struct type with an additional method for the Futoshiki problem type.
/// </summary>
public static class SquareExtensions
{
    /// <summary>
    ///     Compares the squares represented by this <see cref="Square" /> instance and another instance of the same type and
    ///     returns a value indicating whether the two squares are adjacent to each other.
    /// </summary>
    /// <param name="square">The <see cref="Square" /> instance on which the method is invoked.</param>
    /// <param name="other">The <see cref="Square" /> instance against which this instance is to be compared.</param>
    /// <returns><see langword="true" /> if the squares are adjacent; otherwise, <see langword="false" />.</returns>
    public static bool AdjacentTo(this Square square, in Square other)
    {
        ((int xCol, int xRow), (int yCol, int yRow)) = (square, other);

        return (xCol == yCol && Math.Abs(xRow - yRow) == 1) || (xRow == yRow && Math.Abs(xCol - yCol) == 1);
    }
}
