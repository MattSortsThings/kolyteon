using System.Text.Json.Serialization;

namespace Mjt85.Kolyteon.NQueens;

/// <summary>
///     Represents a queen occupying a specific square on a chess board.
/// </summary>
/// <remarks>
///     <para>
///         A <see cref="Queen" /> instance is identified by its <see cref="Column" /> and <see cref="Row" /> values,
///         which are the zero-based indexes of the queen's column and row on the chess board. Chess board columns are
///         indexed from left to right; rows are indexed from top to bottom.
///     </para>
///     <para>
///         Two <see cref="Queen" /> instances with equal value represent two queens occupying the same chess board
///         square.
///     </para>
/// </remarks>
[Serializable]
public readonly record struct Queen : IComparable<Queen>
{
    /// <summary>
    ///     Initializes a new <see cref="Queen" /> instance with default <see cref="Column" /> and <see cref="Row" />
    ///     values of 0.
    /// </summary>
    public Queen() : this(0, 0)
    {
    }

    /// <summary>
    ///     Initializes a new <see cref="Queen" /> instance with the specified <see cref="Column" /> and <see cref="Row" />
    ///     values.
    /// </summary>
    /// <param name="column">The zero-based index of the queen's column on the chess board (indexed from left to right).</param>
    /// <param name="row">The zero-based index of the queen's row on the chess board (indexed from top to bottom).</param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="column" /> or <paramref name="row" /> is negative.
    /// </exception>
    [JsonConstructor]
    public Queen(int column, int row)
    {
        Column = column >= 0
            ? column
            : throw new ArgumentOutOfRangeException(nameof(column), column, "Value must not be negative.");

        Row = row >= 0
            ? row
            : throw new ArgumentOutOfRangeException(nameof(row), row, "Value must not be negative.");
    }

    /// <summary>
    ///     Gets the zero-based index of the queen's column on the chess board (indexed from left to right).
    /// </summary>
    /// <value>
    ///     A non-negative 32-bit signed integer. The zero-based index of the queen's column on the chess board (indexed
    ///     from left to right).
    /// </value>
    public int Column { get; }

    /// <summary>
    ///     Gets the zero-based index of the queen's row on the chess board (indexed from top to bottom).
    /// </summary>
    /// <value>
    ///     A non-negative 32-bit signed integer. The zero-based index of the queen's row on the chess board (indexed from
    ///     top to bottom).
    /// </value>
    public int Row { get; }

    /// <summary>
    ///     Compares this instance against the specified <see cref="Queen" /> instance and returns a value indicating whether
    ///     this instance precedes, follows, or appears in the same position in the sort order as the other.
    /// </summary>
    /// <remarks>
    ///     <see cref="Queen" /> instances are sorted in ascending order by <see cref="Column" /> value then by
    ///     <see cref="Row" /> value.
    /// </remarks>
    /// <param name="other">The <see cref="Queen" /> instance against which this instance is to be compared.</param>
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
    public int CompareTo(Queen other)
    {
        var columnComparison = Column.CompareTo(other.Column);

        return columnComparison != 0 ? columnComparison : Row.CompareTo(other.Row);
    }

    /// <summary>
    ///     Determines whether this instance and the specified <see cref="Queen" /> instance have equal value, that is,
    ///     they represent queens occupying the same chess board square.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="Queen" /> instances have equal value if their <see cref="Column" /> values are equal and their
    ///     <see cref="Row" /> values are equal.
    /// </remarks>
    /// <param name="other">The <see cref="Queen" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <c>true</c> if this instance and the <paramref name="other" /> parameter have equal value; otherwise, <c>false</c>.
    /// </returns>
    public bool Equals(Queen other) => Column == other.Column && Row == other.Row;

    /// <summary>
    ///     Gets the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Column, Row);

    /// <summary>
    ///     Determines whether the queens represented by this instance and the specified <see cref="Queen" /> instance can
    ///     capture each other.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         In chess, a queen can capture any piece on the same column, row, or diagonal. Two <see cref="Queen" />
    ///         instances can capture each other if any of the following conditions is satisfied:
    ///         <list type="bullet">
    ///             <item>Their <see cref="Column" /> values are equal.</item>
    ///             <item>Their <see cref="Row" /> values are equal.</item>
    ///             <item>
    ///                 The absolute difference between their <see cref="Column" /> values is equal to the absolute
    ///                 difference between their <see cref="Row" /> values.
    ///             </item>
    ///         </list>
    ///     </para>
    ///     <para>
    ///         If the <paramref name="other" /> parameter is a <see cref="Queen" /> of equal value to the instance on which
    ///         the method is invoked (i.e. representing two queens occupying the same square), the method returns <c>true</c>.
    ///     </para>
    /// </remarks>
    /// <param name="other">The <see cref="Queen" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <c>true</c> if this instance and the <paramref name="other" /> parameter can capture each other; otherwise,
    ///     <c>false</c>.
    /// </returns>
    public bool CanCapture(in Queen other)
    {
        var (otherColumn, otherRow) = other;

        return Column == otherColumn || Row == otherRow || Math.Abs(Column - otherColumn) == Math.Abs(Row - otherRow);
    }

    /// <summary>
    ///     Deconstructs this instance.
    /// </summary>
    /// <param name="column">The zero-based index of the queen's column on the chess board (indexed from left to right).</param>
    /// <param name="row">The zero-based index of the queen's row on the chess board (indexed from top to bottom).</param>
    public void Deconstruct(out int column, out int row)
    {
        column = Column;
        row = Row;
    }

    /// <summary>
    ///     Creates and returns the string representation of this instance.
    /// </summary>
    /// <remarks>A <see cref="Queen" /> instance is represented by a string in the format <c>"({Row},{Column})"</c>.</remarks>
    /// <example>
    ///     <code>
    /// class Example
    /// {
    ///   public static void Main()
    ///   {
    ///     Queen q0 = new(0,0);
    ///     Queen q1 = (10,3);
    /// 
    ///     Console.WriteLine(q0);
    ///     Console.WriteLine(q1);
    ///   }
    /// }
    /// // This example produces the following console output:
    /// // (0,0)
    /// // (10,3)
    /// </code>
    /// </example>
    /// <returns>The string representation of this instance.</returns>
    public override string ToString() => $"({Column},{Row})";
}
