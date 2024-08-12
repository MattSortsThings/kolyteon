using System.Reflection;
using System.Text.Json.Serialization;
using Kolyteon.Solving.Internals.Serialization;

namespace Kolyteon.Solving;

/// <summary>
///     Specifies the ordering strategy component of a backtracking search algorithm.
/// </summary>
/// <remarks>
///     The use of lazy static backing fields is taken from Steve Smith's
///     <a href="https://github.com/ardalis/SmartEnum/blob/main/src/SmartEnum/SmartEnum.cs">SmartEnum</a> class.
/// </remarks>
[Serializable]
[JsonConverter(typeof(OrderingStrategyJsonConverter))]
public sealed record OrderingStrategy : IComparable<OrderingStrategy>
{
    private static readonly Lazy<OrderingStrategy[]> AllOrderingStrategies =
        new(GetAllValues, LazyThreadSafetyMode.ExecutionAndPublication);

    private static readonly Lazy<Dictionary<string, OrderingStrategy>> CodeLookup = new(() =>
            AllOrderingStrategies.Value.ToDictionary(orderingStrategy => orderingStrategy.Code),
        LazyThreadSafetyMode.ExecutionAndPublication);

    private static readonly Lazy<Dictionary<int, OrderingStrategy>> NumberLookup = new(() =>
            AllOrderingStrategies.Value.ToDictionary(orderingStrategy => orderingStrategy.Number),
        LazyThreadSafetyMode.ExecutionAndPublication);

    private OrderingStrategy(int number, string code, string name)
    {
        Number = number;
        Code = code;
        Name = name;
    }

    /// <summary>
    ///     Gets the ordering strategy's unique number.
    /// </summary>
    public int Number { get; }

    /// <summary>
    ///     Gets the ordering strategy's unique alphabetical code.
    /// </summary>
    public string Code { get; }

    /// <summary>
    ///     Gets the ordering strategy's full name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Gets the NO (Natural Ordering) ordering strategy.
    /// </summary>
    public static OrderingStrategy NaturalOrdering { get; } = new(0, "NO", "Natural Ordering");

    /// <summary>
    ///     Gets the BZ (Brelaz Heuristic) ordering strategy.
    /// </summary>
    public static OrderingStrategy BrelazHeuristic { get; } = new(1, "BZ", "Brelaz Heuristic");

    /// <summary>
    ///     Gets the MC (Max Cardinality) ordering strategy.
    /// </summary>
    public static OrderingStrategy MaxCardinality { get; } = new(2, "MC", "Max Cardinality");

    /// <summary>
    ///     Gets the MT (Max Tightness) ordering strategy.
    /// </summary>
    public static OrderingStrategy MaxTightness { get; } = new(3, "MT", "Max Tightness");

    /// <summary>
    ///     Compares this <see cref="OrderingStrategy" /> instance with another instance of the same type and returns an
    ///     integer that indicates whether this instance precedes, follows, or occurs in the same position in the sort order as
    ///     the other instance.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="OrderingStrategy" /> instances are compared by their <see cref="Number" /> values.
    /// </remarks>
    /// <param name="other">The <see cref="OrderingStrategy" /> instance against which this instance is to be compared.</param>
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
    public int CompareTo(OrderingStrategy? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        return other is null ? 1 : Number.CompareTo(other.Number);
    }

    /// <summary>
    ///     Indicates whether this <see cref="OrderingStrategy" /> instance has equal value to another instance of the same
    ///     type.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="OrderingStrategy" /> instances have equal value if their <see cref="Number" /> values are equal.
    /// </remarks>
    /// <param name="other">The <see cref="OrderingStrategy" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />. If the <paramref name="other" /> parameter is <see langword="null" />, the
    ///     method returns <see langword="false" />.
    /// </returns>
    public bool Equals(OrderingStrategy? other)
    {
        if (other is null)
        {
            return false;
        }

        return ReferenceEquals(this, other) || Number == other.Number;
    }

    /// <summary>
    ///     Returns the hash code for this <see cref="OrderingStrategy" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => Number;

    /// <summary>
    ///     Returns the string representation of this <see cref="OrderingStrategy" /> instance, which is its
    ///     <see cref="Code" /> value.
    /// </summary>
    /// <returns>A string containing the ordering strategy's code.</returns>
    public override string ToString() => Code;

    /// <summary>
    ///     Creates and returns a descriptive string representation of this <see cref="OrderingStrategy" /> instance.
    /// </summary>
    /// <returns>A string representing this instance, in the format <c>"{Code} ({Name})"</c>.</returns>
    public string ToLongString() => $"{Code} ({Name})";

    /// <summary>
    ///     Returns an ordered list of all the possible <see cref="OrderingStrategy" /> values.
    /// </summary>
    /// <returns>An immutable list of <see cref="OrderingStrategy" /> values.</returns>
    public static IReadOnlyList<OrderingStrategy> GetValues() => AllOrderingStrategies.Value;

    /// <summary>
    ///     Returns the <see cref="OrderingStrategy" /> instance with the specified <see cref="Code" /> value (using
    ///     case-sensitive matching).
    /// </summary>
    /// <param name="code">The ordering strategy's alphabetical code.</param>
    /// <returns>A <see cref="OrderingStrategy" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="code" /> is <see langword="null" />.</exception>
    /// <exception cref="ArgumentException">
    ///     No <see cref="OrderingStrategy" /> exists with a <see cref="Code" /> value matching the <paramref name="code" />
    ///     parameter.
    /// </exception>
    public static OrderingStrategy FromCode(string code)
    {
        ArgumentNullException.ThrowIfNull(code);

        return CodeLookup.Value.TryGetValue(code, out OrderingStrategy? orderingStrategy)
            ? orderingStrategy
            : throw new ArgumentException($"No Ordering Strategy exists with Code value '{code}'.");
    }

    /// <summary>
    ///     Returns the <see cref="OrderingStrategy" /> instance with the specified <see cref="Number" /> value (using
    ///     case-sensitive matching).
    /// </summary>
    /// <param name="number">The ordering strategy's number.</param>
    /// <returns>A <see cref="OrderingStrategy" /> instance.</returns>
    /// <exception cref="ArgumentException">
    ///     No <see cref="OrderingStrategy" /> exists with a <see cref="Number" /> value matching the
    ///     <paramref name="number" /> parameter.
    /// </exception>
    public static OrderingStrategy FromNumber(int number) =>
        NumberLookup.Value.TryGetValue(number, out OrderingStrategy? orderingStrategy)
            ? orderingStrategy
            : throw new ArgumentException($"No Ordering Strategy exists with Number value '{number}'.");

    private static OrderingStrategy[] GetAllValues() => typeof(OrderingStrategy)
        .GetProperties(BindingFlags.Public | BindingFlags.Static)
        .Select(property => (OrderingStrategy)property.GetValue(null)!)
        .OrderBy(orderingStrategy => orderingStrategy)
        .ToArray();
}
