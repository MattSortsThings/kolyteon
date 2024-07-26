using System.Text.Json.Serialization;
using Kolyteon.Common;
using Kolyteon.Sudoku.Internals;

namespace Kolyteon.Sudoku;

/// <summary>
///     Represents a valid (but not necessarily solvable) Sudoku problem.
/// </summary>
[Serializable]
public sealed record SudokuProblem : ISolutionVerifier<IReadOnlyList<NumberedSquare>>
{
    internal const int MinNumber = 1;
    internal const int MaxNumber = 9;

    [JsonConstructor]
    internal SudokuProblem(Block grid,
        IReadOnlyList<Block> sectors,
        IReadOnlyList<NumberedSquare> filledSquares)
    {
        Grid = grid;
        Sectors = sectors;
        FilledSquares = filledSquares;
    }

    /// <summary>
    ///     Gets a <see cref="Block" /> value representing the 9x9 problem grid.
    /// </summary>
    public Block Grid { get; }

    /// <summary>
    ///     Gets an immutable list of 9 <see cref="Block" /> values representing the 9 3x3 sectors of the problem grid.
    /// </summary>
    public IReadOnlyList<Block> Sectors { get; }

    /// <summary>
    ///     Gets an immutable list of <see cref="NumberedSquare" /> values representing the filled squares in the problem grid.
    /// </summary>
    public IReadOnlyList<NumberedSquare> FilledSquares { get; }

    /// <summary>
    ///     Indicates whether this <see cref="SudokuProblem" /> instance has equal value to another instance of the same type,
    ///     that is, they represent logically identical problems.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="SudokuProblem" /> instances have equal value if their <see cref="FilledSquares" /> lists contain the
    ///     same values.
    /// </remarks>
    /// <param name="other">The <see cref="SudokuProblem" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />. If the <paramref name="other" /> parameter is <see langword="null" />, the
    ///     method returns <see langword="false" />.
    /// </returns>
    public bool Equals(SudokuProblem? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return FilledSquares.Count == other.FilledSquares.Count
               && FilledSquares.OrderBy(square => square).SequenceEqual(other.FilledSquares.OrderBy(square => square));
    }

    /// <inheritdoc />
    /// <remarks>
    ///     <para>
    ///         The solution to a <see cref="SudokuProblem" /> instance is a list of <see cref="NumberedSquare" /> values,
    ///         representing the numbers that are to be added into the empty squares in the problem grid.
    ///     </para>
    ///     <para>
    ///         This method applies the following checks in order to the <paramref name="solution" /> parameter:
    ///         <list type="number">
    ///             <item>
    ///                 The number of filled squares in the solution must be equal to the number of empty squares in the
    ///                 problem.
    ///             </item>
    ///             <item>Every filled square in the solution must be located inside the grid.</item>
    ///             <item>Every filled square in the solution must be filled with a number in the range [1,9].</item>
    ///             <item>
    ///                 When the filled squares in the problem and solution are combined, no square may be filled more than
    ///                 once.
    ///             </item>
    ///             <item>
    ///                 When the filled squares in the problem and solution are combined, no number may occur more than once
    ///                 in any column, row, or sector.
    ///             </item>
    ///         </list>
    ///     </para>
    /// </remarks>
    public CheckingResult VerifyCorrect(IReadOnlyList<NumberedSquare> solution)
    {
        ArgumentNullException.ThrowIfNull(solution);

        return SolutionVerification.OneFilledSquarePerEmptySquare
            .Then(SolutionVerification.AllFilledSquaresInGrid)
            .Then(SolutionVerification.AllFilledSquareNumbersInRange)
            .Then(SolutionVerification.NoSquareFilledMoreThanOnce)
            .Then(SolutionVerification.NoDuplicateNumbersInSameColumn)
            .Then(SolutionVerification.NoDuplicateNumbersInSameRow)
            .Then(SolutionVerification.NoDuplicateNumbersInSameSector)
            .VerifyCorrect(solution, this);
    }

    /// <summary>
    ///     Deconstructs this <see cref="SudokuProblem" /> instance.
    /// </summary>
    /// <param name="grid">The problem grid.</param>
    /// <param name="sectors">The sectors of the problem grid.</param>
    /// <param name="filledSquares">The filled squares in the problem grid.</param>
    public void Deconstruct(out Block grid, out IReadOnlyList<Block> sectors, out IReadOnlyList<NumberedSquare> filledSquares)
    {
        grid = Grid;
        sectors = Sectors;
        filledSquares = FilledSquares;
    }

    /// <summary>
    ///     Returns the hash code for this <see cref="SudokuProblem" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => FilledSquares.GetHashCode();

    /// <summary>
    ///     Creates and returns a new <see cref="SudokuProblem" /> instance from the specified grid.
    /// </summary>
    /// <remarks>
    ///     In order to create a valid (but not necessarily solvable) Sudoku problem, the <paramref name="grid" /> parameter
    ///     must satisfy all the following conditions:
    ///     <list type="number">
    ///         <item>The rank-0 and rank-1 lengths of the 2-dimensional array must both be equal to 9.</item>
    ///         <item>
    ///             Every non-<see langword="null" /> value in the array must be greater than or equal to 1 and less than or
    ///             equal to 9.
    ///         </item>
    ///         <item>No non-<see langword="null" /> value may occur more than once in the same column, row, or 3x3 sector.</item>
    ///     </list>
    /// </remarks>
    /// <param name="grid">
    ///     A 2-dimensional array of nullable integer values representing the problem grid, in which every non-
    ///     <see langword="null" /> value represents a filled square.
    /// </param>
    /// <returns>A new <see cref="SudokuProblem" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="grid" /> is <see langword="null" />.</exception>
    /// <exception cref="ArgumentException">
    ///     The rank-0 length or the rank-1 length of the <paramref name="grid" /> parameter is not equal to 9.
    /// </exception>
    /// <exception cref="InvalidProblemException">
    ///     The <paramref name="grid" /> parameter does not represent a valid Sudoku problem.
    /// </exception>
    public static SudokuProblem FromGrid(int?[,] grid)
    {
        ArgumentNullException.ThrowIfNull(grid);
        ThrowIfEitherLengthIsNotNine(grid);

        SudokuProblem problem = grid.ToSudokuProblem();

        ThrowIfInvalidProblem(problem);

        return problem;
    }

    private static void ThrowIfEitherLengthIsNotNine(int?[,] grid)
    {
        if (grid.GetLength(0) is var rank0Length && rank0Length != MaxNumber)
        {
            throw new ArgumentException($"Rank-0 length is {rank0Length}, must be 9.");
        }

        if (grid.GetLength(1) is var rank1Length && rank1Length != MaxNumber)
        {
            throw new ArgumentException($"Rank-1 length is {rank1Length}, must be 9.");
        }
    }

    private static void ThrowIfInvalidProblem(SudokuProblem problem)
    {
        CheckingResult validationResult = ProblemValidation.AtLeastOneEmptySquare
            .Then(ProblemValidation.AllFilledSquareNumbersInRange)
            .Then(ProblemValidation.NoDuplicateNumbersInSameColumn)
            .Then(ProblemValidation.NoDuplicateNumbersInSameRow)
            .Then(ProblemValidation.NoDuplicateNumbersInSameSector).Validate(problem);

        if (validationResult is { IsSuccessful: false, FirstError: not null })
        {
            throw new InvalidProblemException(validationResult.FirstError);
        }
    }
}
