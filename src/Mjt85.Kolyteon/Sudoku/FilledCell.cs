using System.Text.Json.Serialization;

namespace Mjt85.Kolyteon.Sudoku;

/// <summary>
///     Represents a cell in a Sudoku puzzle grid that has been filled with a number from the range [1,9].
/// </summary>
/// <remarks>
///     Puzzle grid columns are zero-indexed from left to right. Rows are zero-indexed from top to bottom. Sectors are
///     zero-indexed, starting with the top-left (0), then centre-left (1), then bottom-left (2), then top-centre (3), and
///     so on to the bottom-right (8).
/// </remarks>
[Serializable]
public readonly record struct FilledCell : IComparable<FilledCell>
{
    /// <summary>
    ///     Initializes a new <see cref="FilledCell" /> instance with default <see cref="Column" />, <see cref="Row" /> and
    ///     <see cref="Sector" /> values of 0 and the minimum <see cref="Number" /> value of 1.
    /// </summary>
    public FilledCell() : this(0, 0, 1)
    {
    }

    /// <summary>
    ///     Initializes a new <see cref="FilledCell" /> instance with the specified <see cref="Column" />, <see cref="Row" />
    ///     and <see cref="Number" /> values and a calculated <see cref="Sector" /> value.
    /// </summary>
    /// <param name="column">The zero-based index of the cell's column in the puzzle grid.</param>
    /// <param name="row">The zero-based index of the cell's row in the puzzle grid.</param>
    /// <param name="number">The number from the range [1,9] that fills the cell.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="column" /> is negative or greater than 8; or, <paramref name="row" /> is negative or greater than
    ///     8; or <paramref name="number" /> is less than 1 or greater than 9.
    /// </exception>
    [JsonConstructor]
    public FilledCell(int column, int row, int number)
    {
        Column = column is >= 0 and <= 8
            ? column
            : throw new ArgumentOutOfRangeException(nameof(column), column, "Value must be non-negative and less than 9.");

        Row = row is >= 0 and <= 8
            ? row
            : throw new ArgumentOutOfRangeException(nameof(row), row, "Value must be non-negative and less than 9.");

        Number = number is >= 1 and <= 9
            ? number
            : throw new ArgumentOutOfRangeException(nameof(number), number,
                "Value must be greater than or equal to 1 and less than or equal to 9.");

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
    ///     Gets the number from the range [1,9] that fills the cell.
    /// </summary>
    /// <value>A 32-bit signed integer in the range [1,9]. The number that fills the cell.</value>
    public int Number { get; }

    /// <summary>
    ///     Compares this instance against the specified <see cref="FilledCell" /> instance and returns a value indicating
    ///     whether this instance precedes, follows, or appears in the same position in the sort order as the other.
    /// </summary>
    /// <remarks>
    ///     <see cref="FilledCell" /> instances are sorted in ascending order by <see cref="Column" /> value, then by
    ///     <see cref="Row" /> value, then by <see cref="Number" /> value.
    /// </remarks>
    /// <param name="other">The <see cref="FilledCell" /> instance against which this instance is to be compared.</param>
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
    public int CompareTo(FilledCell other)
    {
        var columnComparison = Column.CompareTo(other.Column);

        if (columnComparison != 0)
        {
            return columnComparison;
        }

        var rowComparison = Row.CompareTo(other.Row);

        return rowComparison != 0 ? rowComparison : Number.CompareTo(other.Number);
    }

    /// <summary>
    ///     Determines whether this instance and the specified <see cref="FilledCell" /> instance have equal value, that is,
    ///     they represent the same number in the same cell in the puzzle grid.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="FilledCell" /> instances have equal value if:
    ///     <list type="bullet">
    ///         <item>their <see cref="Column" /> values are equal, <i>and</i></item>
    ///         <item>their <see cref="Row" /> values are equal, <i>and</i></item>
    ///         <item>their <see cref="Number" /> values are equal.</item>
    ///     </list>
    /// </remarks>
    /// <param name="other">The <see cref="FilledCell" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <c>true</c> if this instance and the <paramref name="other" /> parameter have equal value; otherwise, <c>false</c>.
    /// </returns>
    public bool Equals(FilledCell other) => Column == other.Column && Row == other.Row && Number == other.Number;

    /// <summary>
    ///     Determines whether the filled cells represented by this instance and the specified <see cref="FilledCell" />
    ///     instance obstruct each other.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="FilledCell" /> instances obstruct each other if their <see cref="Number" /> values are equal
    ///     <i>and</i>:
    ///     <list type="bullet">
    ///         <item>their <see cref="Column" /> values are equal, <i>and/or</i></item>
    ///         <item>their <see cref="Row" /> values are equal, <i>and/or</i></item>
    ///         <item>their <see cref="Sector" /> values are equal.</item>
    ///     </list>
    /// </remarks>
    /// <param name="other">The <see cref="FilledCell" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <c>true</c> if this instance and the <paramref name="other" /> parameter obstruct each other; otherwise,
    ///     <c>false</c>.
    /// </returns>
    public bool Obstructs(in FilledCell other) => Number == other.Number
                                                  && (Column == other.Column || Row == other.Row || Sector == other.Sector);

    /// <summary>
    ///     Gets the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Column, Row, Number);

    /// <summary>
    ///     Deconstructs this instance.
    /// </summary>
    /// <param name="column">The zero-based index of the cell's column in the puzzle grid.</param>
    /// <param name="row">The zero-based index of the cell's row in the puzzle grid.</param>
    /// <param name="sector">The zero-based index of the cell's sector in the puzzle grid.</param>
    /// <param name="number">The number from the range [1,9] that fills the cell.</param>
    public void Deconstruct(out int column, out int row, out int sector, out int number)
    {
        column = Column;
        row = Row;
        sector = Sector;
        number = Number;
    }

    /// <summary>
    ///     Creates and returns the string representation of this instance.
    /// </summary>
    /// <remarks>
    ///     A <see cref="FilledCell" /> instance is represented by a string in the format <c>"({Row},{Column}) [{Number}]"</c>.
    /// </remarks>
    /// <example>
    ///     <code>
    /// class Example
    /// {
    ///   public static void Main()
    ///   {
    ///     FilledCell f0 = new(0, 0, 2);
    ///     FilledCell f1 = new(8, 3, 5);
    /// 
    ///     Console.WriteLine(f0);
    ///     Console.WriteLine(f1);
    ///   }
    /// }
    /// // This example produces the following console output:
    /// // (0,0) [2]
    /// // (8,3) [5]
    /// </code>
    /// </example>
    /// <returns>The string representation of this instance.</returns>
    public override string ToString() => $"({Column},{Row}) [{Number}]";
}
