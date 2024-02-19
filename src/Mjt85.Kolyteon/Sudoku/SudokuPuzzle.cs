using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Mjt85.Kolyteon.Sudoku.Internals;

namespace Mjt85.Kolyteon.Sudoku;

/// <summary>
///     Represents a Sudoku puzzle.
/// </summary>
/// <remarks>
///     <para>
///         A Sudoku puzzle comprises a 9x9 square grid of cells, sub-divided into 9 3x3 sectors. Some of the cells contain
///         numbers from the range [1,9].
///     </para>
///     <para>
///         To solve the puzzle, one must fill all the empty cells with numbers from the range [1,9] so that every column,
///         row, and sector contains all the numbers from 1 to 9 exactly once.
///     </para>
///     <para>
///         A <see cref="SudokuPuzzle" /> instance is an immutable data structure exposing <see cref="FilledCells" />
///         property and a static <see cref="GridSideLength" /> field with the value of 9. It represents a valid (but not
///         necessarily solvable) puzzle if the size of the <see cref="FilledCells" /> list is less than 81, and no pair of
///         items in the <see cref="FilledCells" /> list having equal <see cref="FilledCell.Number" /> values also have
///         equal <see cref="FilledCell.Column" />, <see cref="FilledCell.Row" />, or <see cref="FilledCell.Sector" />
///         values.
///     </para>
///     <para>
///         This type can only be instantiated outside its assembly by one of the following means:
///         <list type="bullet">
///             <item>
///                 Using the <see cref="FromGrid" /> static factory method, which throws an exception if the instantiated
///                 puzzle is invalid thereby guaranteeing a valid puzzle.
///             </item>
///             <item>Deserialization, which does not validate the instantiated puzzle.</item>
///         </list>
///     </para>
/// </remarks>
[Serializable]
public sealed record SudokuPuzzle
{
    /// <summary>
    ///     The puzzle grid's height in cells and width in cells (9).
    /// </summary>
    /// <value>A read-only 32-bit signed integer. The puzzle grid's height in cells and width in cells (9).</value>
    public const int GridSideLength = 9;

    /// <summary>
    ///     Initializes a new <see cref="SudokuPuzzle" /> instance with the specified <see cref="FilledCells" /> list.
    /// </summary>
    /// <remarks>
    ///     This internal constructor is for deserialization and testing only. Use the <see cref="FromGrid" /> static factory
    ///     method to instantiate this type outside its assembly.
    /// </remarks>
    /// <param name="filledCells">The puzzle's filled cells.</param>
    /// <exception cref="ArgumentNullException"><paramref name="filledCells" /> is <c>null</c>.</exception>
    [JsonConstructor]
    internal SudokuPuzzle(IReadOnlyList<FilledCell> filledCells)
    {
        FilledCells = filledCells ?? throw new ArgumentNullException(nameof(filledCells));
    }

    /// <summary>
    ///     Gets the puzzle's filled cells.
    /// </summary>
    /// <remarks>The contents of this may be in any order.</remarks>
    /// <value>A read-only list of <see cref="FilledCell" /> instances. The puzzle's filled cells.</value>
    public IReadOnlyList<FilledCell> FilledCells { get; } = Array.Empty<FilledCell>();

    /// <summary>
    ///     Determines whether this instance and the specified <see cref="SudokuPuzzle" /> instance have equal value,
    ///     that is, they represent logically identical Sudoku puzzles.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="SudokuPuzzle" /> instances are equal if their respective <see cref="FilledCells" /> lists contain
    ///     logically equivalent items (irrespective of order).
    /// </remarks>
    /// <param name="other">The <see cref="SudokuPuzzle" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <c>true</c> if this instance and the <paramref name="other" /> parameter have equal value; otherwise,
    ///     <c>false</c>. If the <paramref name="other" /> parameter is <c>null</c>, the method returns <c>false</c>.
    /// </returns>
    public bool Equals(SudokuPuzzle? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return FilledCells.Count == other.FilledCells.Count
               && FilledCells.OrderBy(c => c).SequenceEqual(other.FilledCells.OrderBy(c => c));
    }

    /// <summary>
    ///     Gets the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => FilledCells.GetHashCode();

    /// <summary>
    ///     Determines whether the proposed solution is valid for the Sudoku puzzle represented by this instance.
    /// </summary>
    /// <remarks>
    ///     This method applies the following validation checks to the <paramref name="solution" /> parameter sequentially and
    ///     returns on the first validation error encountered (if any):
    ///     <list type="number">
    ///         <item>
    ///             The number of <see cref="FilledCell" /> instances in the <paramref name="solution" /> is equal to the
    ///             number of <see cref="FilledCell" /> instances in <see cref="FilledCells" /> subtracted from 81 (i.e. the
    ///             number of empty cells in the puzzle).
    ///         </item>
    ///         <item>No pair of filled cells in the solution is in the same cell.</item>
    ///         <item>No pair of filled cells in the solution obstruct each other.</item>
    ///         <item>No filled cell in the solution is in the same cell as any filled cell in the puzzle.</item>
    ///         <item>No filled cell in the solution obstructs any filled cell in the puzzle.</item>
    ///     </list>
    /// </remarks>
    /// <param name="solution">A list of <see cref="FilledCell" /> instances. The proposed solution to the puzzle.</param>
    /// <returns>
    ///     <see cref="ValidationResult.Success" /> (i.e. <c>null</c>) if the <paramref name="solution" /> parameter is a
    ///     valid solution; otherwise, a <see cref="ValidationResult" /> instance with an error message reporting the first
    ///     validation error encountered.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="solution" /> is <c>null</c>.</exception>
    public ValidationResult? ValidSolution(IReadOnlyList<FilledCell> solution)
    {
        _ = solution ?? throw new ArgumentNullException(nameof(solution));

        return ApplyChainedValidators(solution);
    }

    /// <summary>
    ///     Creates and returns a new <see cref="SudokuPuzzle" /> from the specified grid.
    /// </summary>
    /// <remarks>
    ///     Static factory method. Any <see cref="SudokuPuzzle" /> instance returned by this method is guaranteed to represent
    ///     a valid (but not necessarily solvable) Sudoku puzzle.
    /// </remarks>
    /// <param name="grid">
    ///     A 2-dimensional square grid of size 9x9, in which any non-null value is a fixed number in the range [1,9]. The grid
    ///     to be converted.
    /// </param>
    /// <returns>A new <see cref="SudokuPuzzle" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><see cref="grid" /> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    ///     <see cref="grid" /> has a rank 0 length not equal to 9, or has a rank 1 length not equal to 9, or contains a
    ///     non-null value less than 1, or contains a non-null value greater than 9.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    ///     The instantiated <see cref="SudokuPuzzle" /> does not represent a valid Sudoku puzzle.
    /// </exception>
    public static SudokuPuzzle FromGrid(int?[,] grid)
    {
        _ = grid ?? throw new ArgumentNullException(nameof(grid));

        Guard.AgainstInvalidGridDimensions(grid);
        SudokuPuzzle puzzle = new(EnumerateFilledCells(grid).ToArray());
        Guard.AgainstInvalidSudokuPuzzle(puzzle);

        return puzzle;
    }

    private ValidationResult? ApplyChainedValidators(IReadOnlyList<FilledCell> solution) => SolutionHasCorrectSize(solution);

    private ValidationResult? SolutionHasCorrectSize(IReadOnlyList<FilledCell> solution)
    {
        var totalEmptyCells = GridSideLength * GridSideLength - FilledCells.Count;

        return solution.Count != totalEmptyCells
            ? new ValidationResult($"Solution size is {solution.Count}, should be {totalEmptyCells}.")
            : SolutionHasNoPairOfFilledCellsInSameCell(solution);
    }

    private ValidationResult? SolutionHasNoPairOfFilledCellsInSameCell(IReadOnlyList<FilledCell> solution)
    {
        IEnumerable<PairQueryItem> pairQuery = solution.SelectMany((_, i) =>
            solution.Take(i), (filledCellAtI, filledCellAtH) =>
            new PairQueryItem(filledCellAtH, filledCellAtI));

        IEnumerable<ValidationResult> errorQuery = from item in pairQuery
            let f1 = item.FirstFilledCell
            let f2 = item.SecondFilledCell
            where f1.Column == f2.Column && f1.Row == f2.Row
            select new ValidationResult($"Solution filled cells {f1} and {f2} are in same cell.");

        ValidationResult? firstError = errorQuery.FirstOrDefault();

        return firstError ?? SolutionHasNoPairOfObstructingFilledCells(solution);
    }

    private ValidationResult? SolutionHasNoPairOfObstructingFilledCells(IReadOnlyList<FilledCell> solution)
    {
        IEnumerable<PairQueryItem> pairQuery = solution.SelectMany((_, i) =>
            solution.Take(i), (filledCellAtI, filledCellAtH) =>
            new PairQueryItem(filledCellAtH, filledCellAtI));

        IEnumerable<ValidationResult> errorQuery = from item in pairQuery
            let f1 = item.FirstFilledCell
            let f2 = item.SecondFilledCell
            where f1.Obstructs(f2)
            select new ValidationResult($"Solution filled cells {f1} and {f2} obstruct each other.");

        ValidationResult? firstError = errorQuery.FirstOrDefault();

        return firstError ?? SolutionHasNoFilledCellInSameCellAsPuzzleFilledCell(solution);
    }

    private ValidationResult? SolutionHasNoFilledCellInSameCellAsPuzzleFilledCell(IReadOnlyList<FilledCell> solution)
    {
        IEnumerable<PuzzleQueryItem> puzzleQuery = from solutionFilledCell in solution
            from puzzleFilledCell in FilledCells
            select new PuzzleQueryItem(solutionFilledCell, puzzleFilledCell);

        IEnumerable<ValidationResult> errorQuery = from item in puzzleQuery
            let solutionFilledCell = item.SolutionFilledCell
            let puzzleFilledCell = item.PuzzleFilledCell
            where solutionFilledCell.Column == puzzleFilledCell.Column && solutionFilledCell.Row == puzzleFilledCell.Row
            select new ValidationResult($"Solution filled cell {solutionFilledCell} " +
                                        $"and puzzle filled cell {puzzleFilledCell} are in same cell.");

        ValidationResult? firstError = errorQuery.FirstOrDefault();

        return firstError ?? SolutionHasNoFilledCellObstructingPuzzleFilledCell(solution);
    }

    private ValidationResult? SolutionHasNoFilledCellObstructingPuzzleFilledCell(IReadOnlyList<FilledCell> solution)
    {
        IEnumerable<PuzzleQueryItem> puzzleQuery = from solutionFilledCell in solution
            from puzzleFilledCell in FilledCells
            select new PuzzleQueryItem(solutionFilledCell, puzzleFilledCell);

        IEnumerable<ValidationResult> errorQuery = from item in puzzleQuery
            let solutionFilledCell = item.SolutionFilledCell
            let puzzleFilledCell = item.PuzzleFilledCell
            where solutionFilledCell.Obstructs(puzzleFilledCell)
            select new ValidationResult($"Solution filled cell {solutionFilledCell} " +
                                        $"obstructs puzzle filled cell {puzzleFilledCell}.");

        ValidationResult? firstError = errorQuery.FirstOrDefault();

        return firstError ?? ValidationResult.Success;
    }

    private static IEnumerable<FilledCell> EnumerateFilledCells(int?[,] squareGrid)
    {
        for (var column = 0; column < GridSideLength; column++)
        {
            for (var row = 0; row < GridSideLength; row++)
            {
                var cellContent = squareGrid[row, column];
                switch (cellContent)
                {
                    case null:
                        continue;
                    case < 1:
                        throw new ArgumentException($"Grid has illegal number at index [{row},{column}].");
                    case > 9:
                        throw new ArgumentException($"Grid has illegal number at index [{row},{column}].");
                    default:
                        yield return new FilledCell(column, row, cellContent.Value);

                        break;
                }
            }
        }
    }

    private readonly record struct PairQueryItem(FilledCell FirstFilledCell, FilledCell SecondFilledCell);

    private readonly record struct PuzzleQueryItem(FilledCell SolutionFilledCell, FilledCell PuzzleFilledCell);
}
