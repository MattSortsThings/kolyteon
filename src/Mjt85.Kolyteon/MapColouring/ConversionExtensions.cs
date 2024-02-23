using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.MapColouring;

/// <summary>
///     Contains a single conversion extension method.
/// </summary>
public static class ConversionExtensions
{
    /// <summary>
    ///     Converts this enumerable into a Map Colouring puzzle solution dictionary.
    /// </summary>
    /// <remarks>
    ///     The returned dictionary can be used as the argument to a <see cref="MapColouringPuzzle.ValidSolution" />
    ///     method invocation on a <see cref="MapColouringPuzzle" /> instance.
    /// </remarks>
    /// <param name="assignments">The assignments to be converted.</param>
    /// <returns>
    ///     A new <see cref="Dictionary{TKey,TValue}" /> instance containing a key-value pair for every item in this
    ///     enumerable.
    /// </returns>
    public static IReadOnlyDictionary<Region, Colour> ToPuzzleSolution(this IEnumerable<Assignment<Region, Colour>> assignments)
    {
        return assignments.ToDictionary(a => a.Variable, a => a.DomainValue);
    }
}
