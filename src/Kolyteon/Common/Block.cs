using System.Text.Json.Serialization;

namespace Kolyteon.Common;

/// <summary>
///     Represents a rectangular block of squares located on a 2-dimensional axis of squares.
/// </summary>
[Serializable]
public readonly record struct Block : IComparable<Block>
{
    /// <summary>
    ///     Initializes a new <see cref="Block" /> instance with an <see cref="OriginSquare" /> value having
    ///     <see cref="Square.Column" /> and <see cref="Square.Row" /> values of 0 and a <see cref="Dimensions" /> value having
    ///     minimum <see cref="Dimensions.WidthInSquares" /> and <see cref="Dimensions.HeightInSquares" /> values of 1.
    /// </summary>
    public Block() : this(new Square(), new Dimensions()) { }

    [JsonConstructor]
    internal Block(Square originSquare, Dimensions dimensions)
    {
        OriginSquare = originSquare;
        Dimensions = dimensions;
    }

    /// <summary>
    ///     Gets the top-left square in the block.
    /// </summary>
    public Square OriginSquare { get; }

    /// <summary>
    ///     Gets the block's dimensions in squares.
    /// </summary>
    public Dimensions Dimensions { get; }

    /// <summary>
    ///     Gets the block's area in squares.
    /// </summary>
    /// <remarks>The value of this property is computed every time it is accessed.</remarks>
    [JsonIgnore]
    public int AreaInSquares => Dimensions.WidthInSquares * Dimensions.HeightInSquares;

    /// <summary>
    ///     Gets the bottom-right square in the block.
    /// </summary>
    /// <remarks>The value of this property is computed every time it is accessed.</remarks>
    [JsonIgnore]
    public Square TerminusSquare => Square.FromColumnAndRow(OriginSquare.Column + Dimensions.WidthInSquares - 1,
        OriginSquare.Row + Dimensions.HeightInSquares - 1);

    /// <summary>
    ///     Compares this <see cref="Block" /> instance with another instance of the same type and returns an integer that
    ///     indicates whether this instance precedes, follows, or occurs in the same position in the sort order as the other
    ///     instance.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="Block" /> instances are compared by their <see cref="OriginSquare" /> values, then by their
    ///     <see cref="Dimensions" /> values.
    /// </remarks>
    /// <param name="other">The <see cref="Block" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     A value that indicates the relative order of the objects being compared. The return value has these meanings:
    ///     <list type="table">
    ///         <listheader>
    ///             <term>Value</term><description>Meaning</description>
    ///         </listheader>
    ///         <item>
    ///             <term>Less than zero</term>
    ///             <description>This instance precedes <paramref name="other" /> in the sort order.</description>
    ///         </item>
    ///         <item>
    ///             <term>Zero</term>
    ///             <description>
    ///                 This instance occurs in the same position in the sort order as <paramref name="other" />.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>Greater than zero</term>
    ///             <description>This instance follows <paramref name="other" /> in the sort order.</description>
    ///         </item>
    ///     </list>
    /// </returns>
    public int CompareTo(Block other)
    {
        int originSquareComparison = OriginSquare.CompareTo(other.OriginSquare);

        return originSquareComparison != 0 ? originSquareComparison : Dimensions.CompareTo(other.Dimensions);
    }

    /// <summary>
    ///     Indicates whether this <see cref="Block" /> instance has equal value to another instance of the same type.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="Block" /> instances have equal value if their <see cref="OriginSquare" /> values are equal and their
    ///     <see cref="Dimensions" /> values are equal.
    /// </remarks>
    /// <param name="other">The <see cref="Block" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(Block other) => OriginSquare.Equals(other.OriginSquare) && Dimensions.Equals(other.Dimensions);

    /// <summary>
    ///     Indicates whether the block represented by this <see cref="Block" /> instance contains the square represented by
    ///     the specified <see cref="Square" /> instance.
    /// </summary>
    /// <param name="square">The <see cref="Square" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance contains the <see cref="square" /> parameter; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    public bool Contains(in Square square)
    {
        (int hintColumn, int hintRow) = square;

        return hintColumn >= OriginSquare.Column
               && hintRow >= OriginSquare.Row
               && hintColumn < OriginSquare.Column + Dimensions.WidthInSquares
               && hintRow < OriginSquare.Row + Dimensions.HeightInSquares;
    }

    /// <summary>
    ///     Indicates whether the block represented by this <see cref="Block" /> instance contains the numbered square
    ///     represented by the specified <see cref="NumberedSquare" /> instance.
    /// </summary>
    /// <param name="numberedSquare">The <see cref="NumberedSquare" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance contains the <see cref="numberedSquare" /> parameter; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    public bool Contains(in NumberedSquare numberedSquare) => Contains(numberedSquare.Square);

    /// <summary>
    ///     Indicates whether the block represented by this <see cref="Block" /> instance entirely contains the block
    ///     represented by the specified <see cref="Block" /> instance.
    /// </summary>
    /// <remarks>
    ///     A <see cref="Block" /> instance contains another <see cref="Block" /> if it contains the latter's
    ///     <see cref="OriginSquare" /> and <see cref="TerminusSquare" />.
    /// </remarks>
    /// <param name="block">The <see cref="Block" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance contains the <see cref="block" /> parameter; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    public bool Contains(in Block block) => Contains(block.OriginSquare) && Contains(block.TerminusSquare);

    /// <summary>
    ///     Indicates whether the blocks represented by this <see cref="Block" /> instance and the specified
    ///     <see cref="Block" /> instance overlap at any point.
    /// </summary>
    /// <param name="other">The <see cref="Block" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <see cref="other" /> parameter overlap at any point; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    public bool Overlaps(in Block other)
    {
        (Square thisOrigin, Square otherTerminus) = (OriginSquare, other.TerminusSquare);

        if (thisOrigin.RightOf(in otherTerminus) || thisOrigin.Below(in otherTerminus))
        {
            return false;
        }

        (Square otherOrigin, Square thisTerminus) = (other.OriginSquare, TerminusSquare);

        return !otherOrigin.RightOf(in thisTerminus) && !otherOrigin.Below(in thisTerminus);
    }

    /// <summary>
    ///     Deconstructs this <see cref="Block" /> instance.
    /// </summary>
    /// <param name="originSquare">The top-left square in the block.</param>
    /// <param name="dimensions">The block's dimensions in squares.</param>
    public void Deconstruct(out Square originSquare, out Dimensions dimensions)
    {
        originSquare = OriginSquare;
        dimensions = Dimensions;
    }

    /// <summary>
    ///     Returns the hash code for this <see cref="Block" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(OriginSquare, Dimensions);

    /// <summary>
    ///     Returns the string representation of this <see cref="Block" /> instance.
    /// </summary>
    /// <returns>A string representing this instance, in the format <c>"{OriginSquare} [{Dimensions}]"</c>.</returns>
    public override string ToString() => $"{OriginSquare} [{Dimensions}]";
}
