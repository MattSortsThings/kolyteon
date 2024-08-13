using Kolyteon.Common;
using Kolyteon.Modelling;

namespace Kolyteon.Shikaku;

/// <summary>
///     Extends a sequence of generic assignments with a method that converts it into a Shikaku problem solution.
/// </summary>
public static class AssignmentEnumerableExtensions
{
    /// <summary>
    ///     Converts the sequence of assignments into a Shikaku problem solution.
    /// </summary>
    /// <param name="assignments">The sequence of assignments to be converted.</param>
    /// <returns>An array of <see cref="Block" /> instances constituting a solution to a Shikaku problem.</returns>
    public static Block[] ToShikakuSolution(this IEnumerable<Assignment<NumberedSquare, Block>> assignments) =>
        assignments.Select(assignment => assignment.DomainValue).ToArray();
}
