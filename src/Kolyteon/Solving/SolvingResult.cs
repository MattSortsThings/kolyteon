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

    /// <summary>
    ///     Indicates whether this <see cref="SolvingResult{TVariable, TDomainValue}" /> instance has equal value to another
    ///     instance of the same type.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="SolvingResult{TVariable, TDomainValue}" /> instances have equal value if:
    ///     <list type="bullet">
    ///         <item>their <see cref="SolvingResult{TVariable, TDomainValue}.SearchAlgorithm" /> values are equal, and</item>
    ///         <item>their <see cref="SimplifyingSteps" /> values are equal, and</item>
    ///         <item>their <see cref="AssigningSteps" /> values are equal, and</item>
    ///         <item>their <see cref="BacktrackingSteps" /> values are equal, and</item>
    ///         <item>their <see cref="Assignments" /> collections contain the same values, irrespective of order.</item>
    ///     </list>
    /// </remarks>
    /// <param name="other">The <see cref="SearchAlgorithm" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />. If the <paramref name="other" /> parameter is <see langword="null" />, the
    ///     method returns <see langword="false" />.
    /// </returns>
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

    /// <summary>
    ///     Returns the hash code for this <see cref="SolvingResult{TVariable, TDomainValue}" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() =>
        HashCode.Combine(Assignments, SearchAlgorithm, SimplifyingSteps, AssigningSteps, BacktrackingSteps);
}
