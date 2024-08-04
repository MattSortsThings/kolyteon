namespace Kolyteon.Modelling.Testing;

/// <summary>
///     Describes a node in a constraint graph modelling a generic binary CSP.
/// </summary>
/// <typeparam name="TVariable">The binary CSP variable type.</typeparam>
/// <typeparam name="TDomainValue">The binary CSP domain value type.</typeparam>
public sealed record ConstraintGraphNode<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    private const double Tolerance = 0.000000001;

    /// <summary>
    ///     Gets or initializes the binary CSP variable.
    /// </summary>
    public required TVariable Variable { get; init; }

    /// <summary>
    ///     Gets or initializes an immutable, ordered list of the variable's domain values.
    /// </summary>
    public required IReadOnlyList<TDomainValue> Domain { get; init; }

    /// <summary>
    ///     Gets the variable's degree.
    /// </summary>
    public required int Degree { get; init; }

    /// <summary>
    ///     Gets the variable's sum constraint tightness.
    /// </summary>
    public required double SumTightness { get; init; }

    /// <summary>
    ///     Indicates whether this <see cref="ConstraintGraphNode{TVariable,TDomainValue}" /> instance has equal value to
    ///     another instance of the same type.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="ConstraintGraphNode{TVariable,TDomainValue}" /> instances have equal value if their
    ///     <see cref="Variable" /> values are equal, their <see cref="Degree" /> values are equal, their
    ///     <see cref="SumTightness" /> values are equal (to 9 decimal places), and their <see cref="Domain" /> lists contain
    ///     the same values in the same order.
    /// </remarks>
    /// <param name="other">
    ///     The <see cref="ConstraintGraphNode{TVariable,TDomainValue}" /> instance against which this instance
    ///     is to be compared.
    /// </param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />. If the <paramref name="other" /> parameter is <see langword="null" />, the
    ///     method returns <see langword="false" />.
    /// </returns>
    public bool Equals(ConstraintGraphNode<TVariable, TDomainValue>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Variable.Equals(other.Variable)
               && Degree == other.Degree
               && Domain.Count == other.Domain.Count
               && Math.Abs(SumTightness - other.SumTightness) <= Tolerance
               && Domain.SequenceEqual(other.Domain);
    }

    /// <summary>
    ///     Returns the hash code for this <see cref="ConstraintGraphNode{TVariable,TDomainValue}" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Variable, Degree, SumTightness, Domain);
}
