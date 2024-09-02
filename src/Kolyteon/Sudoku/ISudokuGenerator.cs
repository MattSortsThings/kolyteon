namespace Kolyteon.Sudoku;

/// <summary>
///     Can generate a random, solvable Sudoku problem from parameters specified by the client.
/// </summary>
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

    /// <summary>
    ///     Sets the seed value for the random number sequence used by this instance to generate the problem.
    /// </summary>
    /// <param name="seed">
    ///     A number used to calculate a starting value for the pseudo-random number sequence used by the generator algorithm.
    ///     If a negative number is specified, the absolute value of the number is used.
    /// </param>
    public void UseSeed(int seed);
}
