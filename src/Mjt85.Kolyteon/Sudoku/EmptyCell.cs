using System.Text.Json.Serialization;

namespace Mjt85.Kolyteon.Sudoku;

/// <summary>
///     Represents an empty cell in a Sudoku puzzle grid.
/// </summary>
/// <remarks>
///     Puzzle grid columns are zero-indexed from left to right. Rows are zero-indexed from top to bottom. Sectors are
///     zero-indexed, starting with the top-left (0), then centre-left (1), then bottom-left (2), then top-centre (3), and
///     so on to the bottom-right (8).
/// </remarks>
[Serializable]
public readonly record struct EmptyCell : IComparable<EmptyCell>
{
    /// <summary>
    ///     Initializes a new <see cref="EmptyCell" /> instance with default <see cref="Column" />, <see cref="Row" /> and
    ///     <see cref="Sector" /> values of 0.
    /// </summary>
    public EmptyCell() : this(0, 0)
    {
    }

    /// <summary>
    ///     Initializes a new <see cref="EmptyCell" /> instance with the specified <see cref="Column" /> and <see cref="Row" />
    ///     values and a calculated <see cref="Sector" /> value.
    /// </summary>
    /// <param name="column">The zero-based index of the cell's column in the puzzle grid.</param>
    /// <param name="row">The zero-based index of the cell's row in the puzzle grid.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="column" /> is negative or greater than 8; or, <paramref name="row" /> is negative or greater than
    ///     8.
    /// </exception>
    [JsonConstructor]
    public EmptyCell(int column, int row)
    {
        Column = column is >= 0 and <= 8
            ? column
            : throw new ArgumentOutOfRangeException(nameof(column), column, "Value must be non-negative and less than 9.");

        Row = row is >= 0 and <= 8
            ? row
            : throw new ArgumentOutOfRangeException(nameof(row), row, "Value must be non-negative and less than 9.");

        Sector = Row / 3 + 3 * (Column / 3);
    }

    /// <summary>
    ///     Gets the zero-based index of the cell's column in the puzzle grid.
    /// </summary>
    /// <value>A 32-bit signed integer in the range [0,8]. The zero-based index of the cell's column in the puzzle grid.</value>
    public int Column { get; }

    /// <summary>
    ///     Gets the zero-based index of the cell's row in the puzzle grid.
    /// </summary>
    /// <value>A 32-bit signed integer in the range [0,8]. The zero-based index of the cell's row in the puzzle grid.</value>
    public int Row { get; }

    /// <summary>
    ///     Gets the zero-based index of the cell's sector in the puzzle grid.
    /// </summary>
    /// <value>A 32-bit signed integer in the range [0,8]. The zero-based index of the cell's sector in the puzzle grid.</value>
    [JsonIgnore]
    public int Sector { get; }

    /// <summary>
    ///     Compares this instance against the specified <see cref="EmptyCell" /> instance and returns a value indicating
    ///     whether this instance precedes, follows, or appears in the same position in the sort order as the other.
    /// </summary>
    /// <remarks>
    ///     <see cref="EmptyCell" /> instances are sorted in ascending order by <see cref="Column" /> value, then by
    ///     <see cref="Row" /> value.
    /// </remarks>
    /// <param name="other">The <see cref="EmptyCell" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in
    ///     the sort order as the <paramref name="other" /> parameter.
    ///     <list type="table">
    ///         <listheader>
    ///             <term>Value</term><description>Condition</description>
    ///         </listheader>
    ///         <item>
    ///             <term>Less than zero</term>
    ///             <description>This instance precedes the <paramref name="other" />.</description>
    ///         </item>
    ///         <item>
    ///             <term>Zero</term>
    ///             <description>
    ///                 This instance has the same position in the sort order as the <paramref name="other" />.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>Greater than zero</term>
    ///             <description>This instance follows the <paramref name="other" />.</description>
    ///         </item>
    ///     </list>
    /// </returns>
    public int CompareTo(EmptyCell other)
    {
        var columnComparison = Column.CompareTo(other.Column);

        return columnComparison != 0 ? columnComparison : Row.CompareTo(other.Row);
    }

    /// <summary>
    ///     Determines whether this instance and the specified <see cref="EmptyCell" /> instance have equal value, that is,
    ///     they represent the same cell in the puzzle grid.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="EmptyCell" /> instances have equal value if:
    ///     <list type="bullet">
    ///         <item>their <see cref="Column" /> values are equal, <i>and</i></item>
    ///         <item>their <see cref="Row" /> values are equal.</item>
    ///     </list>
    /// </remarks>
    /// <param name="other">The <see cref="EmptyCell" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <c>true</c> if this instance and the <paramref name="other" /> parameter have equal value; otherwise, <c>false</c>.
    /// </returns>
    public bool Equals(EmptyCell other) => Column == other.Column && Row == other.Row;

    /// <summary>
    ///     Gets the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Column, Row);

    /// <summary>
    ///     Deconstructs this instance.
    /// </summary>
    /// <param name="column">The zero-based index of the cell's column in the puzzle grid.</param>
    /// <param name="row">The zero-based index of the cell's row in the puzzle grid.</param>
    /// <param name="sector">The zero-based index of the cell's sector in the puzzle grid.</param>
    public void Deconstruct(out int column, out int row, out int sector)
    {
        column = Column;
        row = Row;
        sector = Sector;
    }


    /// <summary>
    ///     Creates and returns the string representation of this instance.
    /// </summary>
    /// <remarks>
    ///     An <see cref="EmptyCell" /> instance is represented by a string in the format <c>"({Row},{Column})"</c>.
    /// </remarks>
    /// <example>
    ///     <code>
    /// class Example
    /// {
    ///   public static void Main()
    ///   {
    ///     EmptyCell e0 = new(0, 0);
    ///     EmptyCell e1 = new(8, 3);
    /// 
    ///     Console.WriteLine(e0);
    ///     Console.WriteLine(e1);
    ///   }
    /// }
    /// // This example produces the following console output:
    /// // (0,0)
    /// // (8,3)
    /// </code>
    /// </example>
    /// <returns>The string representation of this instance.</returns>
    public override string ToString() => $"({Column},{Row})";
}
