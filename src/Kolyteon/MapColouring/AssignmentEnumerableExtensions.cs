using Kolyteon.Common;
using Kolyteon.Modelling;

namespace Kolyteon.MapColouring;

/// <summary>
///     Extends a sequence of generic assignments with a method that converts it into a Map Colouring problem solution.
/// </summary>
public static class AssignmentEnumerableExtensions
{
    /// <summary>
    ///     Converts the sequence of assignments into a Map Colouring problem solution.
    /// </summary>
    /// <param name="assignments">The sequence of assignments to be converted.</param>
    /// <returns>
    ///     A dictionary of <see cref="Block" /> keys and <see cref="Colour" /> values, constituting a solution to a Map
    ///     Colouring problem.
    /// </returns>
    public static Dictionary<Block, Colour> ToMapColouringSolution(this IEnumerable<Assignment<Block, Colour>> assignments) =>
        assignments.ToDictionary(assignment => assignment.Variable, assignment => assignment.DomainValue);
}
