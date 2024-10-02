using Kolyteon.Modelling;

namespace Kolyteon.Solving;

/// <summary>
///     Contains the result of a completed binary CSP solving operation.
/// </summary>
/// <param name="Solution">
///     The solution to the binary CSP that was found. This is an empty list if the binary CSP was found
///     to have no solution.
/// </param>
/// <param name="SearchMetrics">Metrics for the backtracking search that was executed to reach the solution.</param>
/// <typeparam name="TVariable">The binary CSP variable type.</typeparam>
/// <typeparam name="TDomainValue">The binary CSP domain value type.</typeparam>
[Serializable]
public sealed record SolvingResult<TVariable, TDomainValue>(
    IReadOnlyList<Assignment<TVariable, TDomainValue>> Solution,
    SearchMetrics SearchMetrics)
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>;
