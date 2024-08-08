using Kolyteon.Common;

namespace Kolyteon.MapColouring;

/// <summary>
///     Can generate a random, solvable Map Colouring problem from parameters specified by the client.
/// </summary>
public interface IMapColouringGenerator
{
    /// <summary>
    ///     Generates and returns a random Map Colouring problem that has the specified number of blocks and global permitted
    ///     colours set, and is guaranteed to have at least one solution.
    /// </summary>
    /// <param name="blocks">An integer greater than zero and less than or equal to 50. The number of blocks in the map.</param>
    /// <param name="permittedColours">A set of 4 or more colours. The global permitted colours set for the problem.</param>
    /// <returns>A new <see cref="MapColouringProblem" /> instance.</returns>
    /// <exception cref="ArgumentException"><paramref name="permittedColours" /> contains fewer than 4 values.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="permittedColours" /> is <see langword="null" />.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="blocks" /> is less than or equal to zero, or
    ///     <paramref name="blocks" /> is greater than 50.
    /// </exception>
    public MapColouringProblem Generate(int blocks, HashSet<Colour> permittedColours);

    /// <summary>
    ///     Sets the seed value for the random number sequence used by this instance to generate the problem.
    /// </summary>
    /// <param name="seed">
    ///     A number used to calculate a starting value for the pseudo-random number sequence used by the generator algorithm.
    ///     If a negative number is specified, the absolute value of the number is used.
    /// </param>
    public void UseSeed(int seed);
}
