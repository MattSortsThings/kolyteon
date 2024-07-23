using System.Reflection;
using System.Text.Json.Serialization;
using Kolyteon.Common.Internals;

namespace Kolyteon.Common;

/// <summary>
///     Defines a "smart enum" of 16 unique colour values.
/// </summary>
/// <remarks>
///     <para>A <see cref="Colour" /> instance is serialized to and from its <see cref="Name" /> string value.</para>
///     <para>
///         Colour names are taken from Spectre.Console's
///         <a href="https://github.com/spectreconsole/spectre.console/blob/main/src/Spectre.Console/Color.Generated.cs">Color</a>
///         struct.
///     </para>
///     <para>
///         The use of lazy static backing fields is taken from Steve Smith's
///         <a href="https://github.com/ardalis/SmartEnum/blob/main/src/SmartEnum/SmartEnum.cs">SmartEnum</a> class.
///     </para>
/// </remarks>
[Serializable]
[JsonConverter(typeof(ColourJsonConverter))]
public readonly record struct Colour : IComparable<Colour>
{
    private static readonly Lazy<Colour[]> AllColours = new(GetAllValues, LazyThreadSafetyMode.ExecutionAndPublication);

    private static readonly Lazy<Dictionary<string, Colour>> NameLookup = new(() =>
        AllColours.Value.ToDictionary(item => item.Name), LazyThreadSafetyMode.ExecutionAndPublication);

    private static readonly Lazy<Dictionary<int, Colour>> NumberLookup = new(() =>
        AllColours.Value.ToDictionary(item => item.Number), LazyThreadSafetyMode.ExecutionAndPublication);

    /// <summary>
    ///     Initializes a new <see cref="Colour" /> instance equal to the <see cref="Black" /> static value.
    /// </summary>
    public Colour() : this(0, "Black") { }

    private Colour(int number, string name)
    {
        Number = number;
        Name = name;
    }

    /// <summary>
    ///     Gets the colour's unique number.
    /// </summary>
    public int Number { get; }

    /// <summary>
    ///     Gets the colour's unique name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Gets the colour "Black".
    /// </summary>
    public static Colour Black { get; } = new(0, "Black");

    /// <summary>
    ///     Gets the colour "Maroon".
    /// </summary>
    public static Colour Maroon { get; } = new(1, "Maroon");

    /// <summary>
    ///     Gets the colour "Green".
    /// </summary>
    public static Colour Green { get; } = new(2, "Green");

    /// <summary>
    ///     Gets the colour "Olive".
    /// </summary>
    public static Colour Olive { get; } = new(3, "Olive");

    /// <summary>
    ///     Gets the colour "Navy".
    /// </summary>
    public static Colour Navy { get; } = new(4, "Navy");

    /// <summary>
    ///     Gets the colour "Purple".
    /// </summary>
    public static Colour Purple { get; } = new(5, "Purple");

    /// <summary>
    ///     Gets the colour "Teal".
    /// </summary>
    public static Colour Teal { get; } = new(6, "Teal");

    /// <summary>
    ///     Gets the colour "Silver".
    /// </summary>
    public static Colour Silver { get; } = new(7, "Silver");

    /// <summary>
    ///     Gets the colour "Grey".
    /// </summary>
    public static Colour Grey { get; } = new(8, "Grey");

    /// <summary>
    ///     Gets the colour "Red".
    /// </summary>
    public static Colour Red { get; } = new(9, "Red");

    /// <summary>
    ///     Gets the colour "Lime".
    /// </summary>
    public static Colour Lime { get; } = new(10, "Lime");

    /// <summary>
    ///     Gets the colour "Yellow".
    /// </summary>
    public static Colour Yellow { get; } = new(11, "Yellow");

    /// <summary>
    ///     Gets the colour "Blue".
    /// </summary>
    public static Colour Blue { get; } = new(12, "Blue");

    /// <summary>
    ///     Gets the colour "Fuchsia".
    /// </summary>
    public static Colour Fuchsia { get; } = new(13, "Fuchsia");

    /// <summary>
    ///     Gets the colour "Aqua".
    /// </summary>
    public static Colour Aqua { get; } = new(14, "Aqua");

    /// <summary>
    ///     Gets the colour "White".
    /// </summary>
    public static Colour White { get; } = new(15, "White");

    /// <summary>
    ///     Compares this <see cref="Colour" /> instance with another instance of the same type and returns an integer that
    ///     indicates whether this instance precedes, follows, or occurs in the same position in the sort order as the other
    ///     instance.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="Colour" /> instances are compared by their <see cref="Number" /> values.
    /// </remarks>
    /// <param name="other">The <see cref="Colour" /> instance against which this instance is to be compared.</param>
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
    public int CompareTo(Colour other) => Number.CompareTo(other.Number);

    /// <summary>
    ///     Indicates whether this <see cref="Colour" /> instance has equal value to another instance of the same type.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="Colour" /> instances have equal value if their <see cref="Number" /> values are equal.
    /// </remarks>
    /// <param name="other">The <see cref="Colour" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(Colour other) => Number == other.Number;

    /// <summary>
    ///     Returns the hash code for this <see cref="Colour" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => Number;

    /// <summary>
    ///     Returns the string representation of this <see cref="Colour" /> instance, which is its <see cref="Name" /> value.
    /// </summary>
    /// <returns>A string containing the colour's name.</returns>
    public override string ToString() => Name;

    /// <summary>
    ///     Returns an ordered list of all the possible <see cref="Colour" /> values.
    /// </summary>
    /// <returns>An immutable list of <see cref="Colour" /> values.</returns>
    public static IReadOnlyList<Colour> GetValues() => AllColours.Value;

    /// <summary>
    ///     Returns the <see cref="Colour" /> instance with the specified <see cref="Name" /> value (using case-sensitive
    ///     matching).
    /// </summary>
    /// <param name="name">The colour's name.</param>
    /// <returns>A <see cref="Colour" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="name" /> is <see langword="null" />.</exception>
    /// <exception cref="ArgumentException">
    ///     No <see cref="Colour" /> exists with a <see cref="Name" /> value matching the
    ///     <paramref name="name" /> parameter.
    /// </exception>
    public static Colour FromName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        return NameLookup.Value.TryGetValue(name, out Colour colour)
            ? colour
            : throw new ArgumentException($"No Colour exists with Name value '{name}'.");
    }

    /// <summary>
    ///     Returns the <see cref="Colour" /> instance with the specified <see cref="Number" /> value.
    /// </summary>
    /// <param name="number">An integer in the range [0,16]. The colour's number.</param>
    /// <returns>A <see cref="Colour" /> instance.</returns>
    /// <exception cref="ArgumentException">
    ///     No <see cref="Colour" /> exists with a <see cref="Number" /> value matching the
    ///     <paramref name="number" /> parameter.
    /// </exception>
    public static Colour FromNumber(int number) => NumberLookup.Value.TryGetValue(number, out Colour colour)
        ? colour
        : throw new ArgumentException($"No Colour exists with Number value '{number}'.");

    private static Colour[] GetAllValues() => typeof(Colour).GetProperties(BindingFlags.Public | BindingFlags.Static)
        .Select(property => (Colour)property.GetValue(null)!)
        .OrderBy(colour => colour)
        .ToArray();
}
