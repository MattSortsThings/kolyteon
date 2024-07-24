using Kolyteon.Common;

namespace Kolyteon.MapColouring;

/// <summary>
///     Extends the <see cref="Block" /> struct type with an additional method for the Map Colouring problem type.
/// </summary>
public static class BlockExtensions
{
    /// <summary>
    ///     Compares this <see cref="Block" /> instance against another instance of the same type and returns a value
    ///     indicating whether the two blocks touch along any of their sides without overlapping.
    /// </summary>
    /// <remarks>
    ///     Given two blocks <i>A</i> and <i>B</i>, the two blocks are adjacent if any of the following conditions is
    ///     satisfied:
    ///     <list type="bullet">
    ///         <item>The left edge of <i>A</i> touches the right edge of <i>B</i>.</item>
    ///         <item>The left edge of <i>B</i> touches the right edge of <i>A</i>.</item>
    ///         <item>The top edge of <i>A</i> touches the bottom edge of <i>B</i>.</item>
    ///         <item>The top edge of <i>B</i> touches the bottom edge of <i>A</i>.</item>
    ///     </list>
    ///     Blocks with touching corners are not adjacent. Overlapping blocks are not adjacent.
    /// </remarks>
    /// <param name="block">The <see cref="Block" /> on which the method is invoked.</param>
    /// <param name="other">The <see cref="Block" /> against which this instance is to be compared.</param>
    /// <returns><see langword="true" /> if the blocks are adjacent; otherwise, <see langword="false" />.</returns>
    public static bool AdjacentTo(this Block block, in Block other)
    {
        ((int xOriginCol, int xOriginRow), (int xTerminusCol, int xTerminusRow)) = (block.OriginSquare, block.TerminusSquare);
        ((int yOriginCol, int yOriginRow), (int yTerminusCol, int yTerminusRow)) = (other.OriginSquare, other.TerminusSquare);

        return (xOriginCol - yTerminusCol == 1 && xOriginRow <= yTerminusRow && xTerminusRow >= yOriginRow)
               || (yOriginCol - xTerminusCol == 1 && yOriginRow <= xTerminusRow && yTerminusRow >= xOriginRow)
               || (xOriginRow - yTerminusRow == 1 && xOriginCol <= yTerminusCol && xTerminusCol >= yOriginCol)
               || (yOriginRow - xTerminusRow == 1 && yOriginCol <= xTerminusCol && yTerminusCol >= xOriginCol);
    }
}
