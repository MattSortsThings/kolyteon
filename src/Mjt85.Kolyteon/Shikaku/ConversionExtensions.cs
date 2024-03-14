using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.Shikaku;

/// <summary>
///     Contains a single conversion extension method.
/// </summary>
public static class ConversionExtensions
{
    /// <summary>
    ///     Converts this enumerable into a Shikaku puzzle solution list.
    /// </summary>
    /// <remarks>
    ///     The returned list can be used as the argument to a <see cref="ShikakuPuzzle.ValidSolution" /> method invocation on
    ///     a <see cref="ShikakuPuzzle" /> instance.
    /// </remarks>
    /// <param name="assignments">The assignments to be converted.</param>
    /// <returns>
    ///     A new read-only list of <see cref="Rectangle" /> values, containing one value for every assignment in this
    ///     enumerable.
    /// </returns>
    public static IReadOnlyList<Rectangle> ToPuzzleSolution(this IEnumerable<Assignment<Hint, Rectangle>> assignments)
    {
        return assignments.Select(a => a.DomainValue).ToList();
    }
}
