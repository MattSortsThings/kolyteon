using Kolyteon.Common;
using Kolyteon.Modelling;

namespace Kolyteon.GraphColouring;

/// <summary>
///     Extends a sequence of generic assignments with a method that converts it into a Graph Colouring problem solution.
/// </summary>
public static class AssignmentEnumerableExtensions
{
    /// <summary>
    ///     Converts the sequence of assignments into a Graph Colouring problem solution.
    /// </summary>
    /// <param name="assignments">The sequence of assignments to be converted.</param>
    /// <returns>
    ///     A dictionary of <see cref="Node" /> keys and <see cref="Colour" /> values, constituting a solution to a Graph
    ///     Colouring problem.
    /// </returns>
    public static Dictionary<Node, Colour> ToGraphColouringSolution(this IEnumerable<Assignment<Node, Colour>> assignments) =>
        assignments.ToDictionary(assignment => assignment.Variable, assignment => assignment.DomainValue);
}
