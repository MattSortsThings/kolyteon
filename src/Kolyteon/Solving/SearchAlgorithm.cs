using System.Text.Json.Serialization;

namespace Kolyteon.Solving;

/// <summary>
///     Specifies the backtracking search algorithm used during a binary CSP solving operation.
/// </summary>
[Serializable]
public sealed record SearchAlgorithm : IComparable<SearchAlgorithm>
{
    /// <summary>
    ///     Initializes a new <see cref="SearchAlgorithm" /> instance with the specified
    ///     <see cref="SearchAlgorithm.CheckingStrategy" /> and <see cref="SearchAlgorithm.OrderingStrategy" /> values
    /// </summary>
    /// <param name="checkingStrategy">Specifies the checking strategy component of the algorithm.</param>
    /// <param name="orderingStrategy">Specifies the ordering strategy component of the algorithm.</param>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="checkingStrategy" /> is <see langword="null" />, or
    ///     <paramref name="orderingStrategy" /> is <see langword="null" />.
    /// </exception>
    [JsonConstructor]
    public SearchAlgorithm(CheckingStrategy checkingStrategy, OrderingStrategy orderingStrategy)
    {
        CheckingStrategy = checkingStrategy ?? throw new ArgumentNullException(nameof(checkingStrategy));
        OrderingStrategy = orderingStrategy ?? throw new ArgumentNullException(nameof(orderingStrategy));
    }

    /// <summary>
    ///     Gets the checking strategy component of the algorithm.
    /// </summary>
    public CheckingStrategy CheckingStrategy { get; }

    /// <summary>
    ///     Gets the ordering strategy component of the algorithm.
    /// </summary>
    public OrderingStrategy OrderingStrategy { get; }

    /// <summary>
    ///     Compares this <see cref="SearchAlgorithm" /> instance with another instance of the same type and returns an
    ///     integer that indicates whether this instance precedes, follows, or occurs in the same position in the sort order as
    ///     the other instance.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="SearchAlgorithm" /> instances are compared by their <see cref="SearchAlgorithm.CheckingStrategy" />
    ///     values, then by their <see cref="SearchAlgorithm.OrderingStrategy" /> values.
    /// </remarks>
    /// <param name="other">The <see cref="SearchAlgorithm" /> instance against which this instance is to be compared.</param>
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
    public int CompareTo(SearchAlgorithm? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        if (other is null)
        {
            return 1;
        }

        return CheckingStrategy.CompareTo(other.CheckingStrategy) is var checkingComparison && checkingComparison != 0
            ? checkingComparison
            : OrderingStrategy.CompareTo(other.OrderingStrategy);
    }

    /// <summary>
    ///     Indicates whether this <see cref="SearchAlgorithm" /> instance has equal value to another instance of the same
    ///     type.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="SearchAlgorithm" /> instances have equal value if their
    ///     <see cref="SearchAlgorithm.CheckingStrategy" /> values are equal and their
    ///     <see cref="SearchAlgorithm.OrderingStrategy" /> values are equal.
    /// </remarks>
    /// <param name="other">The <see cref="SearchAlgorithm" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />. If the <paramref name="other" /> parameter is <see langword="null" />, the
    ///     method returns <see langword="false" />.
    /// </returns>
    public bool Equals(SearchAlgorithm? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return CheckingStrategy.Equals(other.CheckingStrategy) && OrderingStrategy.Equals(other.OrderingStrategy);
    }

    /// <summary>
    ///     Deconstructs this <see cref="SearchAlgorithm" /> instance.
    /// </summary>
    /// <param name="checkingStrategy">The checking strategy component of the algorithm.</param>
    /// <param name="orderingStrategy">The ordering strategy component of the algorithm.</param>
    public void Deconstruct(out CheckingStrategy checkingStrategy, out OrderingStrategy orderingStrategy)
    {
        checkingStrategy = CheckingStrategy;
        orderingStrategy = OrderingStrategy;
    }

    /// <summary>
    ///     Creates and returns the string representation of this <see cref="SearchAlgorithm" /> instance.
    /// </summary>
    /// <returns>
    ///     A string representing this instance, in the format <c>"{CheckingStrategyCode}+{OrderingStrategyCode}"</c>.
    /// </returns>
    public override string ToString() => $"{CheckingStrategy.Code}+{OrderingStrategy.Code}";

    /// <summary>
    ///     Returns the hash code for this <see cref="SearchAlgorithm" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(CheckingStrategy, OrderingStrategy);
}
