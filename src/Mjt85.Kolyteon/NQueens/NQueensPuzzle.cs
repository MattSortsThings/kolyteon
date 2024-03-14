using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mjt85.Kolyteon.NQueens;

/// <summary>
///     Represents an <i>N</i>-Queens puzzle.
/// </summary>
/// <remarks>
///     <para>
///         An <i>N</i>-Queens puzzle comprises an <i>N</i>x<i>N</i> square chess board and <i>N</i> queens, for some
///         fixed value <i>N</i> &#8805; 1.
///     </para>
///     <para>
///         To solve the puzzle, one must place every queen onto a different square on the chess board so that no two
///         queens can capture each other. The <i>N</i>-Queens puzzle is solvable for every value of <i>N</i> except 2
///         and 3.
///     </para>
///     <para>
///         An <see cref="NQueensPuzzle" /> instance is an immutable data structure representing an <i>N</i>-Queens puzzle.
///         This type can only be instantiated outside its assembly by:
///         <list type="bullet">
///             <item>
///                 using the <see cref="FromN" /> static factory method, which throws an exception if the specified value
///                 of <i>N</i> is less than 1, <i>or</i>
///             </item>
///             <item>deserialization.</item>
///         </list>
///     </para>
/// </remarks>
[Serializable]
public sealed record NQueensPuzzle
{
    /// <summary>
    ///     Initializes a new <see cref="NQueensPuzzle" /> instance with the specified <see cref="N" /> value.
    /// </summary>
    /// <remarks>This internal constructor is for deserialization and testing only.</remarks>
    /// <param name="n">The value of <i>N</i> for the puzzle.</param>
    [JsonConstructor]
    internal NQueensPuzzle(int n)
    {
        N = n;
    }

    /// <summary>
    ///     Gets the value of <i>N</i> for the puzzle, denoting the chess board side length and the number of queens.
    /// </summary>
    /// <value>
    ///     A 32-bit signed integer greater than or equal to 1. The value of <i>N</i> for the puzzle, denoting the chess
    ///     board side length and the number of queens.
    /// </value>
    public int N { get; }

    /// <summary>
    ///     Determines whether this instance and the specified <see cref="NQueensPuzzle" /> instance have equal value, that is,
    ///     they represent logically identical <i>N</i>-Queens puzzles.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="NQueensPuzzle" /> instances are equal if their <see cref="N" /> values are equal.
    /// </remarks>
    /// <param name="other">The <see cref="NQueensPuzzle" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <c>true</c> if this instance and the <paramref name="other" /> parameter have equal value; otherwise, <c>false</c>.
    ///     If the <paramref name="other" /> parameter is <c>null</c>, the method returns <c>false</c>.
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
    ///     Determines whether the proposed solution is valid for the <i>N</i>-Queens puzzle represented by this instance.
    /// </summary>
    /// <remarks>
    ///     This method applies the following validation checks to the <paramref name="solution" /> parameter sequentially, and
    ///     returns the first validation error encountered (if any):
    ///     <list type="number">
    ///         <item>
    ///             The number of queens in the <paramref name="solution" /> list is equal to this instance's <see cref="N" />
    ///             value.
    ///         </item>
    ///         <item>No queen occupies a square outside the dimensions of an <i>N</i>x<i>N</i> chess board.</item>
    ///         <item>No pair of queens can capture each other.</item>
    ///     </list>
    /// </remarks>
    /// <param name="solution">A list of <see cref="Queen" /> values. The proposed solution to the puzzle.</param>
    /// <returns>
    ///     <see cref="ValidationResult.Success" /> (i.e. <c>null</c>) if the <paramref name="solution" /> parameter is a
    ///     valid solution; otherwise, a <see cref="ValidationResult" /> instance with an error message reporting the first
    ///     validation error encountered.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="solution" /> is <c>null</c>.</exception>
    public ValidationResult? ValidSolution(IReadOnlyList<Queen> solution)
    {
        _ = solution ?? throw new ArgumentNullException(nameof(solution));

        return ApplyChainedValidators(solution);
    }

    /// <summary>
    ///     Creates and returns a new <see cref="NQueensPuzzle" /> instance from the specified <i>N</i> value.
    /// </summary>
    /// <example>
    ///     <code>
    /// class Example
    /// {
    ///   public static void Main()
    ///   {
    ///     NQueensPuzzle puzzle = NQueensPuzzle.FromN(8);
    ///   }
    /// }
    /// </code>
    /// </example>
    /// <param name="n">
    ///     An integer greater than or equal to 1. The value of <i>N</i> for the puzzle, denoting the chess board
    ///     side length and the number of queens.
    /// </param>
    /// <returns>A new <see cref="NQueensPuzzle" /> instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="n" /> is less than 1.</exception>
    public static NQueensPuzzle FromN(int n) => n >= 1
        ? new NQueensPuzzle(n)
        : throw new ArgumentOutOfRangeException(nameof(n), n, "Value of N must be greater than or equal to 1.");

    private ValidationResult? ApplyChainedValidators(IReadOnlyList<Queen> solution) => SolutionHasCorrectSize(solution);

    private ValidationResult? SolutionHasCorrectSize(IReadOnlyList<Queen> solution) => solution.Count != N
        ? new ValidationResult($"Solution size is {solution.Count}, should be {N}.")
        : NoQueenOutsideChessBoard(solution);

    private ValidationResult? NoQueenOutsideChessBoard(IReadOnlyList<Queen> solution)
    {
        IEnumerable<ValidationResult> errorQuery = from queen in solution
            where queen.Column >= N || queen.Row >= N
            select new ValidationResult($"Queen {queen} outside chess board.");

        ValidationResult? firstError = errorQuery.FirstOrDefault();

        return firstError ?? NoPairOfQueensCanCaptureEachOther(solution);
    }

    private static ValidationResult? NoPairOfQueensCanCaptureEachOther(IReadOnlyList<Queen> solution)
    {
        IEnumerable<CaptureQueryItem> pairQuery = solution.SelectMany((_, i) =>
            solution.Take(i), (queenAtI, queenAtH) =>
            new CaptureQueryItem(queenAtH, queenAtI));

        IEnumerable<ValidationResult> errorQuery = from item in pairQuery
            where item.FirstQueen.CanCapture(item.SecondQueen)
            select new ValidationResult($"Queens {item.FirstQueen} and {item.SecondQueen} can capture each other.");

        return errorQuery.FirstOrDefault(ValidationResult.Success);
    }

    private readonly record struct CaptureQueryItem(Queen FirstQueen, Queen SecondQueen);
}
