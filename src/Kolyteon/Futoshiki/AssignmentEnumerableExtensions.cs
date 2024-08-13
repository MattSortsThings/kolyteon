using Kolyteon.Common;
using Kolyteon.Modelling;

namespace Kolyteon.Futoshiki;

/// <summary>
///     Extends a sequence of generic assignments with a method that converts it into a Futoshiki problem solution.
/// </summary>
public static class AssignmentEnumerableExtensions
{
    /// <summary>
    ///     Converts the sequence of assignments into a Futoshiki problem solution.
    /// </summary>
    /// <param name="assignments">The sequence of assignments to be converted.</param>
    /// <returns>An array of <see cref="NumberedSquare" /> instances constituting a solution to a Futoshiki problem.</returns>
    public static NumberedSquare[] ToFutoshikiSolution(this IEnumerable<Assignment<Square, int>> assignments) =>
        assignments.Select(assignment =>
        {
            (Square square, int number) = assignment;

            return square.ToNumberedSquare(number);
        }).ToArray();
}
