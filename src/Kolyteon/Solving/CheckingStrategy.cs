using System.Reflection;
using System.Text.Json.Serialization;
using Kolyteon.Solving.Internals.Serialization;

namespace Kolyteon.Solving;

/// <summary>
///     Specifies the checking strategy component of a backtracking search algorithm.
/// </summary>
/// <remarks>
///     The use of lazy static backing fields is taken from Steve Smith's
///     <a href="https://github.com/ardalis/SmartEnum/blob/main/src/SmartEnum/SmartEnum.cs">SmartEnum</a> class.
/// </remarks>
[Serializable]
[JsonConverter(typeof(CheckingStrategyJsonConverter))]
public sealed record CheckingStrategy : IComparable<CheckingStrategy>
{
    private static readonly Lazy<CheckingStrategy[]> AllCheckingStrategies =
        new(GetAllValues, LazyThreadSafetyMode.ExecutionAndPublication);

    private static readonly Lazy<Dictionary<string, CheckingStrategy>> CodeLookup = new(() =>
            AllCheckingStrategies.Value.ToDictionary(checkingStrategy => checkingStrategy.Code),
        LazyThreadSafetyMode.ExecutionAndPublication);

    private static readonly Lazy<Dictionary<int, CheckingStrategy>> NumberLookup = new(() =>
            AllCheckingStrategies.Value.ToDictionary(checkingStrategy => checkingStrategy.Number),
        LazyThreadSafetyMode.ExecutionAndPublication);

    private CheckingStrategy(int number, string code, string name, CheckingStrategyType type)
    {
        Number = number;
        Code = code;
        Name = name;
        Type = type;
    }

    /// <summary>
    ///     Gets the checking strategy's unique number.
    /// </summary>
    public int Number { get; }

    /// <summary>
    ///     Gets the checking strategy's unique alphabetical code.
    /// </summary>
    public string Code { get; }

    /// <summary>
    ///     Gets the checking strategy's full name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Gets the checking strategy's type.
    /// </summary>
    public CheckingStrategyType Type { get; }

    /// <summary>
    ///     Gets the BT (Naive Backtracking, Retrospective) checking strategy.
    /// </summary>
    public static CheckingStrategy NaiveBacktracking { get; } =
        new(0,
            "BT",
            "Naive Backtracking",
            CheckingStrategyType.Retrospective);

    /// <summary>
    ///     Gets the BJ (Backjumping, Retrospective) checking strategy.
    /// </summary>
    public static CheckingStrategy Backjumping { get; } =
        new(1,
            "BJ",
            "Backjumping", CheckingStrategyType.Retrospective);

    /// <summary>
    ///     Gets the GBJ (Graph-Based Backjumping, Retrospective) checking strategy.
    /// </summary>
    public static CheckingStrategy GraphBasedBackjumping { get; } =
        new(2,
            "GBJ",
            "Graph-Based Backjumping",
            CheckingStrategyType.Retrospective);

    /// <summary>
    ///     Gets the CBJ (Conflict-Directed Backjumping, Retrospective) checking strategy.
    /// </summary>
    public static CheckingStrategy ConflictDirectedBackjumping { get; } =
        new(3,
            "CBJ",
            "Conflict-Directed Backjumping",
            CheckingStrategyType.Retrospective);

    /// <summary>
    ///     Gets the FC (Forward Checking, Prospective) checking strategy.
    /// </summary>
    public static CheckingStrategy ForwardChecking { get; } =
        new(4,
            "FC",
            "Forward Checking",
            CheckingStrategyType.Prospective);

    /// <summary>
    ///     Gets the PLA (Partial Looking Ahead, Prospective) checking strategy.
    /// </summary>
    public static CheckingStrategy PartialLookingAhead { get; } =
        new(5,
            "PLA",
            "Partial Looking Ahead",
            CheckingStrategyType.Prospective);

    /// <summary>
    ///     Gets the FLA (Full Looking Ahead, Prospective) checking strategy.
    /// </summary>
    public static CheckingStrategy FullLookingAhead { get; } =
        new(6,
            "FLA",
            "Full Looking Ahead",
            CheckingStrategyType.Prospective);

    /// <summary>
    ///     Gets the MAC (Maintaining Arc Consistency, Prospective) checking strategy.
    /// </summary>
    public static CheckingStrategy MaintainingArcConsistency { get; } =
        new(7,
            "MAC",
            "Maintaining Arc Consistency",
            CheckingStrategyType.Prospective);

    /// <summary>
    ///     Compares this <see cref="CheckingStrategy" /> instance with another instance of the same type and returns an
    ///     integer that indicates whether this instance precedes, follows, or occurs in the same position in the sort order as
    ///     the other instance.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="CheckingStrategy" /> instances are compared by their <see cref="Number" /> values.
    /// </remarks>
    /// <param name="other">The <see cref="CheckingStrategy" /> instance against which this instance is to be compared.</param>
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
    public int CompareTo(CheckingStrategy? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        return other is null ? 1 : Number.CompareTo(other.Number);
    }

    /// <summary>
    ///     Indicates whether this <see cref="CheckingStrategy" /> instance has equal value to another instance of the same
    ///     type.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="CheckingStrategy" /> instances have equal value if their <see cref="Number" /> values are equal.
    /// </remarks>
    /// <param name="other">The <see cref="CheckingStrategy" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />. If the <paramref name="other" /> parameter is <see langword="null" />, the
    ///     method returns <see langword="false" />.
    /// </returns>
    public bool Equals(CheckingStrategy? other)
    {
        if (other is null)
        {
            return false;
        }

        return ReferenceEquals(this, other) || Number == other.Number;
    }

    /// <summary>
    ///     Returns the hash code for this <see cref="CheckingStrategy" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => Number;

    /// <summary>
    ///     Returns the string representation of this <see cref="CheckingStrategy" /> instance, which is its
    ///     <see cref="Code" /> value.
    /// </summary>
    /// <returns>A string containing the checking strategy's code.</returns>
    public override string ToString() => Code;

    /// <summary>
    ///     Creates and returns a descriptive string representation of this <see cref="CheckingStrategy" /> instance.
    /// </summary>
    /// <returns>A string representing this instance, in the format <c>"{Code} ({Name}, {Type})"</c>.</returns>
    public string ToLongString() => $"{Code} ({Name}, {Type})";

    /// <summary>
    ///     Returns an ordered list of all the possible <see cref="CheckingStrategy" /> values.
    /// </summary>
    /// <returns>An immutable list of <see cref="CheckingStrategy" /> values.</returns>
    public static IReadOnlyList<CheckingStrategy> GetValues() => AllCheckingStrategies.Value;

    /// <summary>
    ///     Returns the <see cref="CheckingStrategy" /> instance with the specified <see cref="Code" /> value (using
    ///     case-sensitive matching).
    /// </summary>
    /// <param name="code">The checking strategy's alphabetical code.</param>
    /// <returns>A <see cref="CheckingStrategy" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="code" /> is <see langword="null" />.</exception>
    /// <exception cref="ArgumentException">
    ///     No <see cref="CheckingStrategy" /> exists with a <see cref="Code" /> value matching the <paramref name="code" />
    ///     parameter.
    /// </exception>
    public static CheckingStrategy FromCode(string code)
    {
        ArgumentNullException.ThrowIfNull(code);

        return CodeLookup.Value.TryGetValue(code, out CheckingStrategy? checkingStrategy)
            ? checkingStrategy
            : throw new ArgumentException($"No Checking Strategy exists with Code value '{code}'.");
    }

    /// <summary>
    ///     Returns the <see cref="CheckingStrategy" /> instance with the specified <see cref="Number" /> value (using
    ///     case-sensitive matching).
    /// </summary>
    /// <param name="number">The checking strategy's number.</param>
    /// <returns>A <see cref="CheckingStrategy" /> instance.</returns>
    /// <exception cref="ArgumentException">
    ///     No <see cref="CheckingStrategy" /> exists with a <see cref="Number" /> value matching the
    ///     <paramref name="number" /> parameter.
    /// </exception>
    public static CheckingStrategy FromNumber(int number) =>
        NumberLookup.Value.TryGetValue(number, out CheckingStrategy? checkingStrategy)
            ? checkingStrategy
            : throw new ArgumentException($"No Checking Strategy exists with Number value '{number}'.");

    private static CheckingStrategy[] GetAllValues() => typeof(CheckingStrategy)
        .GetProperties(BindingFlags.Public | BindingFlags.Static)
        .Select(property => (CheckingStrategy)property.GetValue(null)!)
        .OrderBy(checkingStrategy => checkingStrategy)
        .ToArray();
}
