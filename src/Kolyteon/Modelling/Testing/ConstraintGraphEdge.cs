namespace Kolyteon.Modelling.Testing;

/// <summary>
///     Describes an edge between two ordered nodes in a constraint graph modelling a generic binary CSP.
/// </summary>
/// <remarks>An edge between two nodes represents a binary constraint for two variables.</remarks>
/// <typeparam name="TVariable">The binary CSP variable type.</typeparam>
/// <typeparam name="TDomainValue">The binary CSP domain value type.</typeparam>
public sealed record ConstraintGraphEdge<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    private const double Tolerance = 0.000000001;

    /// <summary>
    ///     Gets or initializes the first node's variable.
    /// </summary>
    public required TVariable FirstVariable { get; init; }

    /// <summary>
    ///     Gets or initializes the second node's variable.
    /// </summary>
    public required TVariable SecondVariable { get; init; }

    /// <summary>
    ///     Gets or initializes the tightness of the binary constraint.
    /// </summary>
    public required double Tightness { get; init; }

    /// <summary>
    ///     Gets or initializes an immutable, ordered list of all possible assignment pairs from the domains of the two
    ///     adjacent variables.
    /// </summary>
    public required IReadOnlyList<AssignmentPair<TDomainValue>> AssignmentPairs { get; init; }

    /// <summary>
    ///     Indicates whether this <see cref="ConstraintGraphEdge{TVariable,TDomainValue}" /> instance has equal value to
    ///     another instance of the same type.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="ConstraintGraphEdge{TVariable,TDomainValue}" /> instances have equal value if their
    ///     <see cref="FirstVariable" /> values are equal, their <see cref="SecondVariable" /> values are equal, their
    ///     <see cref="Tightness" /> values are equal (to 9 decimal places), and their <see cref="AssignmentPairs" /> lists
    ///     contain the same values in the same order.
    /// </remarks>
    /// <param name="other">
    ///     The <see cref="ConstraintGraphEdge{TVariable,TDomainValue}" /> instance against which this instance
    ///     is to be compared.
    /// </param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />. If the <paramref name="other" /> parameter is <see langword="null" />, the
    ///     method returns <see langword="false" />.
    /// </returns>
    public bool Equals(ConstraintGraphEdge<TVariable, TDomainValue>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return FirstVariable.Equals(other.FirstVariable)
               && SecondVariable.Equals(other.SecondVariable)
               && AssignmentPairs.Count == other.AssignmentPairs.Count
               && Math.Abs(Tightness - other.Tightness) <= Tolerance
               && AssignmentPairs.SequenceEqual(other.AssignmentPairs);
    }

    /// <summary>
    ///     Returns the hash code for this <see cref="ConstraintGraphEdge{TVariable,TDomainValue}" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(FirstVariable, SecondVariable, Tightness, AssignmentPairs);
}
