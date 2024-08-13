using Kolyteon.Common;
using Kolyteon.Modelling;

namespace Kolyteon.NQueens;

/// <summary>
///     Extends a sequence of generic assignments with a method that converts it into an <i>N</i>-Queens problem solution.
/// </summary>
public static class AssignmentEnumerableExtensions
{
    /// <summary>
    ///     Converts the sequence of assignments into an <i>N</i>-Queens problem solution.
    /// </summary>
    /// <param name="assignments">The sequence of assignments to be converted.</param>
    /// <returns>An array of <see cref="Square" /> instances constituting a solution to an <i>N</i>-Queens problem.</returns>
    public static Square[] ToNQueensSolution(this IEnumerable<Assignment<int, Square>> assignments) =>
        assignments.Select(assignment => assignment.DomainValue).ToArray();
}
