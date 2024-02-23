using System.Text.Json.Serialization;

namespace Mjt85.Kolyteon.Shikaku;

/// <summary>
///     Represents a specific hint occupying a specific cell in a Shikaku puzzle grid.
/// </summary>
/// <remarks>
///     <para>
///         A <see cref="Hint" /> instance is identified by its <see cref="Column" /> and <see cref="Row" /> values, which
///         are the zero-based indexes of the queen's column and row on the chess board, together with its
///         <see cref="Number" /> value, a number greater than or equal to 2 that denotes the size of the rectangle that
///         must enclose the hint. Puzzle grid columns are indexed from left to right; rows are indexed from top to bottom.
///     </para>
///     <para>
///         Two <see cref="Hint" /> instances with equal value represent the same hint in the same cell.
///     </para>
/// </remarks>
[Serializable]
public readonly record struct Hint : IComparable<Hint>
{
    /// <summary>
    ///     Initializes a new <see cref="Hint" /> instance with default <see cref="Column" /> and <see cref="Row" /> values of
    ///     0 and the minimum <see cref="Number" /> value of 2.
    /// </summary>
    public Hint() : this(0, 0, 2)
    {
    }

    /// <summary>
    ///     Initializes a new <see cref="Hint" /> instance with the specified <see cref="Column" />, <see cref="Row" />, and
    ///     <see cref="Number" /> values.
    /// </summary>
    /// <param name="column">The zero-based index of the hint's column in the puzzle grid (indexed from left to right).</param>
    /// <param name="row">The zero-based index of the hint's row in the puzzle grid (indexed from top to bottom).</param>
    /// <param name="number">
    ///     The hint's number, denoting the size of the rectangle that must enclose the hint (minimum valid hint number is 2).
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="column" /> is negative, or <paramref name="row" /> is negative, or <paramref name="number" /> is
    ///     less than 2.
    /// </exception>
    [JsonConstructor]
    public Hint(int column, int row, int number)
    {
        Column = column >= 0
            ? column
            : throw new ArgumentOutOfRangeException(nameof(column), column, "Value must not be negative.");

        Row = row >= 0
            ? row
            : throw new ArgumentOutOfRangeException(nameof(row), row, "Value must not be negative.");

        Number = number >= 2
            ? number
            : throw new ArgumentOutOfRangeException(nameof(number), number, "Value must not be less than 2.");
    }

    /// <summary>
    ///     Gets the zero-based index of the hint's column in the puzzle grid (indexed from left to right).
    /// </summary>
    /// <value>
    ///     A non-negative 32-bit signed integer. The zero-based index of the hint's column in the puzzle grid (indexed from
    ///     left to right).
    /// </value>
    public int Column { get; }

    /// <summary>
    ///     Gets the zero-based index of the hint's row in the puzzle grid (indexed from top to bottom).
    /// </summary>
    /// <value>
    ///     A non-negative 32-bit signed integer. The zero-based index of the hint's row in the puzzle grid (indexed from top
    ///     to bottom).
    /// </value>
    public int Row { get; }

    /// <summary>
    ///     Gets the hint's number, denoting the size of the rectangle that must enclose the hint.
    /// </summary>
    /// <value>
    ///     A 32-bit signed integer greater than or equal to 2. The hint's number, denoting the size of the rectangle that
    ///     must enclose the hint.
    /// </value>
    public int Number { get; }

    /// <summary>
    ///     Compares this instance against the specified <see cref="Hint" /> instance and returns a value indicating whether
    ///     this instance precedes, follows, or appears in the same position in the sort order as the other.
    /// </summary>
    /// <remarks>
    ///     <see cref="Hint" /> instances are sorted in ascending order by <see cref="Column" /> value, then by
    ///     <see cref="Row" /> value, then by <see cref="Number" /> value.
    /// </remarks>
    /// <param name="other">The <see cref="Hint" /> instance against which this instance is to be compared.</param>
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
    public int CompareTo(Hint other)
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
    ///     Determines whether this instance and the specified <see cref="Hint" /> instance have equal value, that is,
    ///     they represent the same hint in the same cell.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="Hint" /> instances have equal value if their <see cref="Column" /> values are equal, their
    ///     <see cref="Row" /> values are equal, and their <see cref="Number" /> values are equal.
    /// </remarks>
    /// <param name="other">The <see cref="Hint" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <c>true</c> if this instance and the <paramref name="other" /> parameter have equal value; otherwise, <c>false</c>.
    /// </returns>
    public bool Equals(Hint other) => Column == other.Column && Row == other.Row && Number == other.Number;

    /// <summary>
    ///     Deconstructs this instance.
    /// </summary>
    /// <param name="column">The zero-based index of the hint's column in the puzzle grid (indexed from left to right).</param>
    /// <param name="row">The zero-based index of the hint's row in the puzzle grid (indexed from top to bottom).</param>
    /// <param name="number">The hint's number, denoting the size of the rectangle that must enclose the hint.</param>
    public void Deconstruct(out int column, out int row, out int number)
    {
        column = Column;
        row = Row;
        number = Number;
    }

    /// <summary>
    ///     Gets the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Column, Row, Number);

    /// <summary>
    ///     Creates and returns the string representation of this instance.
    /// </summary>
    /// <remarks>
    ///     A <see cref="Hint" /> instance is represented by a string in the format <c>"({Row},{Column}) [{Number}]".</c>
    /// </remarks>
    /// <example>
    ///     <code>
    /// class Example
    /// {
    ///   public static void Main()
    ///   {
    ///     Hint h0 = new(0,0,2);
    ///     Hint h1 = new(10,3,15);
    /// 
    ///     Console.WriteLine(h0);
    ///     Console.WriteLine(h1);
    ///   }
    /// }
    /// // This example produces the following console output:
    /// // (0,0) [2]
    /// // (10,3) [15]
    /// </code>
    /// </example>
    /// <returns>The string representation of this instance.</returns>
    public override string ToString() => $"({Column},{Row}) [{Number}]";
}
