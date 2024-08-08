namespace Kolyteon.Sudoku;

public interface ISudokuGenerator
{
    /// <summary>
    ///     Generates and returns a random Sudoku problem that has the specified number of empty squares and is guaranteed to
    ///     have at least one solution.
    /// </summary>
    /// <param name="emptySquares">
    ///     An integer greater than 0 and less than 81. The number of empty squares in the problem grid.
    /// </param>
    /// <returns>A new <see cref="SudokuProblem" /> instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="emptySquares" /> is equal to or less than 0, or
    ///     <paramref name="emptySquares" /> is equal to or greater than 81.
    /// </exception>
    public SudokuProblem Generate(int emptySquares);

    public void UseSeed(int seed);
}
