using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.NQueens;

/// <summary>
///     Contains a single conversion extension method.
/// </summary>
public static class ConversionExtensions
{
    /// <summary>
    ///     Converts this enumerable into an <i>N</i>-Queens puzzle solution list.
    /// </summary>
    /// <remarks>
    ///     The returned list can be used as the argument to an <see cref="NQueensPuzzle.ValidSolution" /> method invocation on
    ///     a <see cref="NQueensPuzzle" /> instance.
    /// </remarks>
    /// <param name="assignments">The assignments to be converted.</param>
    /// <returns>
    ///     A new read-only list of <see cref="Queen" /> values, containing one value for every assignment in this enumerable.
    /// </returns>
    public static IReadOnlyList<Queen> ToPuzzleSolution(this IEnumerable<Assignment<int, Queen>> assignments)
    {
        return assignments.Select(a => a.DomainValue).ToArray();
    }
}
