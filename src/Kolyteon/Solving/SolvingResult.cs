using System.Text.Json.Serialization;
using Kolyteon.Modelling;

namespace Kolyteon.Solving;

/// <summary>
///     Contains the result of a completed binary CSP solving operation on a binary CSP modelling a problem.
/// </summary>
/// <typeparam name="TVariable">The binary CSP variable type.</typeparam>
/// <typeparam name="TDomainValue">The binary CSP domain value type.</typeparam>
[Serializable]
public sealed record SolvingResult<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    /// <summary>
    ///     Gets or initializes a list of the variables and their assigned domain values constituting the solution that was
    ///     found by the binary CSP solving operation.
    /// </summary>
    /// <remarks>
    ///     When the binary CSP was found to have no solution, this list will be empty. Otherwise, it will contain an
    ///     <see cref="Assignment{TVariable,TDomainValue}" /> instance for every variable in the binary CSP.
    /// </remarks>
    public required IReadOnlyList<Assignment<TVariable, TDomainValue>> Assignments { get; init; }

    /// <summary>
    ///     Gets or initializes the backtracking search algorithm used by the binary CSP solver.
    /// </summary>
    public required SearchAlgorithm SearchAlgorithm { get; init; }

    /// <summary>
    ///     Gets or initializes the number of simplifying steps executed during the backtracking search algorithm.
    /// </summary>
    public required int SimplifyingSteps { get; init; }

    /// <summary>
    ///     Gets or initializes the number of assigning steps executed during the backtracking search algorithm.
    /// </summary>
    public required int AssigningSteps { get; init; }

    /// <summary>
    ///     Gets or initializes the number of backtracking steps executed during the backtracking search algorithm.
    /// </summary>
    public required int BacktrackingSteps { get; init; }

    /// <summary>
    ///     Calculates and returns the total number of steps executed during the backtracking search algorithm.
    /// </summary>
    /// <remarks>
    ///     The value returned by this computed property is equal to the sum of the <see cref="SimplifyingSteps" />,
    ///     <see cref="AssigningSteps" /> and <see cref="BacktrackingSteps" /> values for this instance.
    /// </remarks>
    [JsonIgnore]
    public int TotalSteps => SimplifyingSteps + AssigningSteps + BacktrackingSteps;

    public bool Equals(SolvingResult<TVariable, TDomainValue>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return SearchAlgorithm.Equals(other.SearchAlgorithm)
               && SimplifyingSteps == other.SimplifyingSteps
               && AssigningSteps == other.AssigningSteps
               && BacktrackingSteps == other.BacktrackingSteps
               && Assignments.Count == other.Assignments.Count
               && Assignments.OrderBy(assignment => assignment)
                   .SequenceEqual(other.Assignments.OrderBy(assignment => assignment));
    }

    public override int GetHashCode() =>
        HashCode.Combine(Assignments, SearchAlgorithm, SimplifyingSteps, AssigningSteps, BacktrackingSteps);
}
