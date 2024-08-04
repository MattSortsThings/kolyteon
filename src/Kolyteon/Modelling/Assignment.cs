namespace Kolyteon.Modelling;

/// <summary>
///     Represents the assignment of a domain value to a binary CSP variable.
/// </summary>
/// <typeparam name="TVariable">The binary CSP variable type.</typeparam>
/// <typeparam name="TDomainValue">The binary CSP domain value type.</typeparam>
public readonly record struct Assignment<TVariable, TDomainValue> : IComparable<Assignment<TVariable, TDomainValue>>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    /// <summary>
    ///     Initializes a new <see cref="Assignment{TVariable,TDomainValue}" /> instance with the specified
    ///     <see cref="Variable" /> and <see cref="DomainValue" /> values.
    /// </summary>
    /// <param name="variable"></param>
    /// <param name="domainValue"></param>
    public Assignment(TVariable variable, TDomainValue domainValue)
    {
        Variable = variable;
        DomainValue = domainValue;
    }

    /// <summary>
    ///     Gets the variable in the binary CSP.
    /// </summary>
    public TVariable Variable { get; }

    /// <summary>
    ///     Gets the assigned value in the variable's domain.
    /// </summary>
    public TDomainValue DomainValue { get; }

    /// <summary>
    ///     Compares this <see cref="Assignment{TVariable,TDomainValue}" /> instance with another instance of the same type and
    ///     returns an integer that indicates whether this instance precedes, follows, or occurs in the same position in the
    ///     sort order as the other instance.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="Assignment{TVariable,TDomainValue}" /> instances are compared by their <see cref="Variable" />
    ///     values, then by their <see cref="DomainValue" /> values.
    /// </remarks>
    /// <param name="other">
    ///     The <see cref="Assignment{TVariable,TDomainValue}" /> instance against which this instance is to be
    ///     compared.
    /// </param>
    /// <returns>
    ///     A value that indicates the relative order of the objects being compared. The return value has these meanings:
    ///     <list type="table">
    ///         <listheader>
    ///             <term>Value</term><description>Meaning</description>
    ///         </listheader>
    ///         <item>
    ///             <term>Less than zero</term>
    ///             <description>This instance precedes <paramref name="other" /> in the sort order.</description>
    ///         </item>
    ///         <item>
    ///             <term>Zero</term>
    ///             <description>
    ///                 This instance occurs in the same position in the sort order as <paramref name="other" />.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>Greater than zero</term>
    ///             <description>This instance follows <paramref name="other" /> in the sort order.</description>
    ///         </item>
    ///     </list>
    /// </returns>
    public int CompareTo(Assignment<TVariable, TDomainValue> other) =>
        Variable.CompareTo(other.Variable) is var variableComparison && variableComparison != 0
            ? variableComparison
            : DomainValue.CompareTo(other.DomainValue);

    /// <summary>
    ///     Indicates whether this <see cref="Assignment{TVariable,TDomainValue}" /> instance has equal value to another
    ///     instance of the same type.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="Assignment{TVariable,TDomainValue}" /> instances have equal value if their <see cref="Variable" />
    ///     values are equal and their <see cref="DomainValue" /> values are equal.
    /// </remarks>
    /// <param name="other">
    ///     The <see cref="Assignment{TVariable,TDomainValue}" /> instance against which this instance is to be
    ///     compared.
    /// </param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(Assignment<TVariable, TDomainValue> other) =>
        Variable.Equals(other.Variable) && DomainValue.Equals(other.DomainValue);

    /// <summary>
    ///     Deconstructs this <see cref="Assignment{TVariable,TDomainValue}" /> instance.
    /// </summary>
    /// <param name="variable">The binary CSP variable.</param>
    /// <param name="domainValue">The assigned domain value.</param>
    public void Deconstruct(out TVariable variable, out TDomainValue domainValue)
    {
        variable = Variable;
        domainValue = DomainValue;
    }

    /// <summary>
    ///     Returns the hash code for this <see cref="Assignment{TVariable,TDomainValue}" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Variable, DomainValue);
}
