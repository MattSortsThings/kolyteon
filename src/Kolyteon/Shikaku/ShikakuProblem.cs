using System.Text.Json.Serialization;
using Kolyteon.Common;
using Kolyteon.Shikaku.Internals;

namespace Kolyteon.Shikaku;

/// <summary>
///     Represents a valid (but not necessarily solvable) Shikaku problem.
/// </summary>
[Serializable]
public sealed record ShikakuProblem
{
    internal const int MinGridSideLength = 5;
    internal const int MinHintNumber = 2;

    [JsonConstructor]
    internal ShikakuProblem(Block grid, IReadOnlyList<NumberedSquare> hints)
    {
        Grid = grid;
        Hints = hints;
    }

    /// <summary>
    ///     Gets a <see cref="Block" /> value representing the problem grid.
    /// </summary>
    public Block Grid { get; }

    /// <summary>
    ///     Gets an immutable list of <see cref="NumberedSquare" /> values representing the problem hints.
    /// </summary>
    public IReadOnlyList<NumberedSquare> Hints { get; }

    /// <summary>
    ///     Indicates whether this <see cref="ShikakuProblem" /> instance has equal value to another instance of the same type,
    ///     that is, they represent logically identical problems.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="ShikakuProblem" /> instances have equal value if their <see cref="Hints" /> lists contain the same
    ///     values and their <see cref="Grid" /> values are equal.
    /// </remarks>
    /// <param name="other">The <see cref="ShikakuProblem" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />. If the <paramref name="other" /> parameter is <see langword="null" />, the
    ///     method returns <see langword="false" />.
    /// </returns>
    public bool Equals(ShikakuProblem? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Hints.Count == other.Hints.Count
               && Grid.Equals(other.Grid)
               && Hints.OrderBy(hint => hint).SequenceEqual(other.Hints.OrderBy(hint => hint));
    }

    /// <summary>
    ///     Deconstructs this <see cref="ShikakuProblem" /> instance.
    /// </summary>
    /// <param name="grid">The problem grid.</param>
    /// <param name="hints">The problem hints.</param>
    public void Deconstruct(out Block grid, out IReadOnlyList<NumberedSquare> hints)
    {
        grid = Grid;
        hints = Hints;
    }

    /// <summary>
    ///     Returns the hash code for this <see cref="ShikakuProblem" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Grid, Hints);

    /// <summary>
    ///     Creates and returns a new <see cref="ShikakuProblem" /> instance from the specified grid.
    /// </summary>
    /// <remarks>
    ///     In order to create a valid (but not necessarily solvable) Shikaku problem, the <paramref name="grid" /> parameter
    ///     must satisfy all the following conditions:
    ///     <list type="number">
    ///         <item>
    ///             The rank-0 and rank-1 lengths of the 2-dimensional array must be equal to each other and greater than or
    ///             equal to 5.
    ///         </item>
    ///         <item>Every non-<see langword="null" /> value in the array must be greater than or equal to 2.</item>
    ///         <item>
    ///             The sum of the non-<see langword="null" /> values in the array must be equal to the product of its rank-0
    ///             and rank-1 lengths.
    ///         </item>
    ///     </list>
    /// </remarks>
    /// <param name="grid">
    ///     A 2-dimensional array of nullable integer values representing the problem grid, in which every non-
    ///     <see langword="null" /> value represents a problem hint.
    /// </param>
    /// <returns>A new <see cref="ShikakuProblem" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="grid" /> is <see langword="null" />.</exception>
    /// <exception cref="InvalidProblemException">
    ///     The <paramref name="grid" /> parameter does not represent a valid Shikaku problem.
    /// </exception>
    public static ShikakuProblem FromGrid(int?[,] grid)
    {
        ArgumentNullException.ThrowIfNull(grid, nameof(grid));

        ThrowIfEitherLengthIsZero(grid, nameof(grid));

        ShikakuProblem problem = grid.ToShikakuProblem();

        ThrowIfInvalidProblem(problem);

        return problem;
    }

    private static void ThrowIfEitherLengthIsZero(int?[,] grid, string paramName)
    {
        if (grid.GetLength(0) == 0)
        {
            throw new ArgumentException("Rank-0 length is zero.", paramName);
        }

        if (grid.GetLength(1) == 0)
        {
            throw new ArgumentException("Rank-1 length is zero.", paramName);
        }
    }

    private static void ThrowIfInvalidProblem(ShikakuProblem problem)
    {
        CheckingResult validationResult = ProblemValidation.ValidGridDimensions
            .Then(ProblemValidation.AtLeastOneHint)
            .Then(ProblemValidation.NoHintNumberLessThanTwo)
            .Then(ProblemValidation.HintNumbersSumToGridArea)
            .Validate(problem);

        if (validationResult is { IsSuccessful: false, FirstError: not null })
        {
            throw new InvalidProblemException(validationResult.FirstError);
        }
    }
}
