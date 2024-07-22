using System.Text.Json.Serialization;

namespace Kolyteon.Common;

/// <summary>
///     Represents a specific square located on a 2-dimensional axis of squares.
/// </summary>
[Serializable]
public readonly record struct Square : IComparable<Square>
{
    private const int MinColumnAndRow = 0;

    /// <summary>
    ///     Initializes a new <see cref="Square" /> instance with the <see cref="Column" /> value of 0 and the
    ///     <see cref="Row" /> value of 0.
    /// </summary>
    public Square() : this(MinColumnAndRow, MinColumnAndRow) { }

    [JsonConstructor]
    internal Square(int column, int row)
    {
        Column = column;
        Row = row;
    }

    /// <summary>
    ///     Gets the zero-based column index of the square.
    /// </summary>
    /// <remarks>Columns are zero-indexed from the left.</remarks>
    public int Column { get; }

    /// <summary>
    ///     Gets the zero-based row index of the square.
    /// </summary>
    /// <remarks>Rows are zero-indexed from the top.</remarks>
    public int Row { get; }

    /// <summary>
    ///     Compares this <see cref="Square" /> instance with another instance of the same type and returns an integer that
    ///     indicates whether this instance precedes, follows, or occurs in the same position in the sort order as the other
    ///     instance.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="Square" /> instances are compared by their <see cref="Column" /> values, then by their
    ///     <see cref="Row" /> values.
    /// </remarks>
    /// <param name="other">The <see cref="Square" /> instance against which this instance is to be compared.</param>
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
    public int CompareTo(Square other)
    {
        int columnComparison = Column.CompareTo(other.Column);

        return columnComparison != 0 ? columnComparison : Row.CompareTo(other.Row);
    }

    /// <summary>
    ///     Indicates whether this <see cref="Square" /> instance has equal value to another instance of the same type.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="Square" /> instances have equal value if their <see cref="Column" /> values are equal and their
    ///     <see cref="Row" /> values are equal.
    /// </remarks>
    /// <param name="other">The <see cref="Square" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(Square other) => Column == other.Column && Row == other.Row;

    /// <summary>
    ///     Deconstructs this <see cref="Square" /> instance.
    /// </summary>
    /// <param name="column">The zero-based column index of the square.</param>
    /// <param name="row">The zero-based row index of the square.</param>
    public void Deconstruct(out int column, out int row)
    {
        column = Column;
        row = Row;
    }

    /// <summary>
    ///     Returns the hash code for this <see cref="Square" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Column, Row);

    /// <summary>
    ///     Returns the string representation of this <see cref="Square" /> instance.
    /// </summary>
    /// <returns>A string representing this instance, in the format <c>"({Column},{Row})"</c>.</returns>
    public override string ToString() => $"({Column},{Row})";

    /// <summary>
    ///     Creates and returns a new <see cref="Block" /> instance with the specified <see cref="Dimensions" /> as its
    ///     <see cref="Block.Dimensions" /> and this <see cref="Square" /> instance as its <see cref="Block.OriginSquare" />.
    /// </summary>
    /// <param name="dimensions">The dimensions of the block.</param>
    /// <returns>A new <see cref="Block" /> instance.</returns>
    public Block ToBlock(Dimensions dimensions) => new(this, dimensions);

    /// <summary>
    ///     Creates and returns a new <see cref="NumberedSquare" /> instance with this <see cref="Square" /> instance as its
    ///     <see cref="NumberedSquare.Square" /> and the specified <see cref="NumberedSquare.Number" /> value.
    /// </summary>
    /// <param name="number">The integer number that fills the square.</param>
    /// <returns>A new <see cref="NumberedSquare" /> instance.</returns>
    public NumberedSquare ToNumberedSquare(int number) => new(this, number);

    /// <summary>
    ///     Creates and returns a new <see cref="Square" /> instance with the specified <see cref="Column" /> and
    ///     <see cref="Row" />.
    /// </summary>
    /// <param name="column">An integer greater than or equal to 0. The zero-based column index of the square.</param>
    /// <param name="row">An integer greater than or equal to 0. The zero-based row index of the square.</param>
    /// <returns>A new <see cref="Square" /> instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="column" /> is less than 0, or <paramref name="row" /> is less than 0.
    /// </exception>
    public static Square FromColumnAndRow(int column, int row)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(column, MinColumnAndRow);
        ArgumentOutOfRangeException.ThrowIfLessThan(row, MinColumnAndRow);

        return new Square(column, row);
    }
}
