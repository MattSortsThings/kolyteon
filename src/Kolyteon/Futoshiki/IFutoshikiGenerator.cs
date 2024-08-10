namespace Kolyteon.Futoshiki;

/// <summary>
///     Can generate a random, solvable Futoshiki problem from parameters specified by the client.
/// </summary>
public interface IFutoshikiGenerator
{
    /// <summary>
    ///     Generates and returns a random Futoshiki problem that has the specified grid side length and number of hints, and
    ///     is guaranteed to have at least one solution.
    /// </summary>
    /// <param name="gridSideLength">
    ///     An integer greater than or equal to 4 and less than or equal to 9. The problem grid side length in squares.
    /// </param>
    /// <param name="emptySquares">
    ///     An integer greater than zero and less than the problem grid area in squares. The number of empty squares in the
    ///     problem grid.
    /// </param>
    /// <returns>A new <see cref="FutoshikiProblem" /> instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="gridSideLength" /> is less than 4, or
    ///     <paramref name="gridSideLength" /> is greater than 9, or <paramref name="emptySquares" /> is less than 1, or
    ///     <paramref name="emptySquares" /> is greater than the square of <paramref name="gridSideLength" />.
    /// </exception>
    public FutoshikiProblem Generate(int gridSideLength, int emptySquares);

    /// <summary>
    ///     Sets the seed value for the random number sequence used by this instance to generate the problem.
    /// </summary>
    /// <param name="seed">
    ///     A number used to calculate a starting value for the pseudo-random number sequence used by the generator algorithm.
    ///     If a negative number is specified, the absolute value of the number is used.
    /// </param>
    public void UseSeed(int seed);
}
