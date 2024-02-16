using System.Text.Json.Serialization;

namespace Mjt85.Kolyteon.NQueens;

/// <summary>
///     Represents an <i>N</i>-Queens puzzle.
/// </summary>
/// <remarks>
///     <para>
///         An <i>N</i>-Queens puzzle consists of an <i>N</i>x<i>N</i> square chess board and <i>N</i> queens, for some
///         value <i>N</i> &#8805; 1. To solve the puzzle, one must place every queen on a different square on the
///         chess board so that no two queens can capture each other. In chess, a queen can capture any piece on the same
///         row or column or diagonal.
///     </para>
///     <para>
///         An instance of the <see cref="NQueensPuzzle" /> record type is an immutable data structure that represents an
///         <i>N</i>-Queens puzzle. The record type exposes a single read-only <see cref="N" /> property. It can only be
///         instantiated outside its assembly by one of these means:
///         <list type="bullet">
///             <item>
///                 Using the <see cref="FromN" /> static factory method to create an <see cref="NQueensPuzzle" />
///                 instance for any valid <i>N</i> value.
///             </item>
///             <item>Deserialization from JSON (which does not validate the puzzle).</item>
///         </list>
///     </para>
///     <para>
///         Throughout this library, the columns of an <i>N</i>x<i>N</i> square chess board are identified by their
///         zero-based index from 0 (left) to <i>N</i>-1 (right). The rows are identified by their zero-based index from 0
///         (top) to <i>N</i>-1 (bottom).
///     </para>
/// </remarks>
[Serializable]
public sealed record NQueensPuzzle
{
    /// <summary>
    ///     Initializes a new <see cref="NQueensPuzzle" /> instance with the specified <see cref="N" /> value.
    /// </summary>
    /// <remarks>Use the <see cref="FromN" /> static factory method to instantiate this type outside its assembly.</remarks>
    /// <param name="n">The value of <i>N</i> for the puzzle</param>
    [JsonConstructor]
    internal NQueensPuzzle(int n)
    {
        N = n;
    }

    /// <summary>
    ///     Gets the value of <i>N</i> for the puzzle.
    /// </summary>
    /// <value>A 32-bit signed integer greater than or equal to 1. The value of <i>N</i> for the puzzle.</value>
    public int N { get; }

    /// <summary>
    ///     Determines whether this instance and the specified <see cref="NQueensPuzzle" /> instance have equal value, that is,
    ///     they represent logically identical <i>N</i>-Queens puzzles.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="NQueensPuzzle" /> instances have equal value if their <see cref="N" /> values are equal.
    /// </remarks>
    /// <param name="other">The <see cref="NQueensPuzzle" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <c>true</c> if this instance and the <paramref name="other" /> parameter have equal value; otherwise,
    ///     <c>false</c>. If the <paramref name="other" /> parameter is <c>null</c>, the method returns <c>false</c>.
    /// </returns>
    public bool Equals(NQueensPuzzle? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        return ReferenceEquals(this, other) || N == other.N;
    }

    /// <summary>
    ///     Gets the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => N;

    /// <summary>
    ///     Creates and returns a new <see cref="NQueensPuzzle" /> instance with the specified <see cref="N" /> value.
    /// </summary>
    /// <remarks>
    ///     Static factory method. Any <see cref="NQueensPuzzle" /> instance returned by this method is guaranteed to
    ///     represent a valid (but not necessarily solvable) <i>N</i>-Queens puzzle.
    /// </remarks>
    /// <param name="n">The value of <i>N</i> for the puzzle (greater than or equal to 1).</param>
    /// <returns>A new <see cref="NQueensPuzzle" /> instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="n" /> is less than 1.</exception>
    public static NQueensPuzzle FromN(int n) => n >= 1
        ? new NQueensPuzzle(n)
        : throw new ArgumentOutOfRangeException(nameof(n), n, "Value of N must be greater than or equal to 1.");
}
