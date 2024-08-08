namespace Kolyteon.Shikaku;

/// <summary>
///     Can generate a random, solvableShikaku problem from parameters specified by the client.
/// </summary>
public interface IShikakuGenerator
{
    /// <summary>
    ///     Generates and returns a random Shikaku problem that has the specified grid side length and number of hints, and is
    ///     guaranteed to have at least one solution.
    /// </summary>
    /// <param name="gridSideLength">
    ///     An integer greater than or equal to 5 and less than or equal to 20. The problem grid side length in squares.
    /// </param>
    /// <param name="hints">
    ///     An integer greater than zero and less than or equal to double the grid side length. The number of hints in the
    ///     problem.
    /// </param>
    /// <returns>A new <see cref="ShikakuProblem" /> instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="gridSideLength" /> is less than 5, or
    ///     <paramref name="gridSideLength" /> is greater than 20, or <paramref name="hints" /> is less than 1, or
    ///     <paramref name="hints" /> is greater than double the value of <paramref name="gridSideLength" />.
    /// </exception>
    public ShikakuProblem Generate(int gridSideLength, int hints);

    /// <summary>
    ///     Sets the seed value for the random number sequence used by this instance to generate the problem.
    /// </summary>
    /// <param name="seed">
    ///     A number used to calculate a starting value for the pseudo-random number sequence used by the generator algorithm.
    ///     If a negative number is specified, the absolute value of the number is used.
    /// </param>
    public void UseSeed(int seed);
}
