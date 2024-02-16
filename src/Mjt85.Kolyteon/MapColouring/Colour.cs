using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mjt85.Kolyteon.MapColouring;

/// <summary>
///     Represents a colour that may be assigned to a map region.
/// </summary>
/// <remarks>
///     <para>
///         A <see cref="Colour" /> instance is identified by its unique <see cref="Id" /> integer value and unique
///         <see cref="Name" /> string value. <see cref="Colour" /> instances are compared using their <see cref="Id" />
///         values. A <see cref="Colour" /> instance is represented by, serialized to, and deserialized from its
///         <see cref="Name" /> string value.
///     </para>
///     <para>
///         This struct type implements the "smart enum" pattern as exemplified by
///         <a href="https://www.meziantou.net/smart-enums-type-safe-enums-in-dotnet.htm">Gérald Barré (2018)</a>. There
///         are 16 possible <see cref="Colour" /> values, exposed as static fields, based on the
///         <see cref="ConsoleColor" /> enumeration:
///         <list type="table">
///             <listheader>
///                 <term>Id</term><description>Name</description>
///             </listheader>
///             <item>
///                 <term>0</term><description>Black</description>
///             </item>
///             <item>
///                 <term>1</term><description>DarkBlue</description>
///             </item>
///             <item>
///                 <term>2</term><description>DarkGreen</description>
///             </item>
///             <item>
///                 <term>3</term><description>DarkCyan</description>
///             </item>
///             <item>
///                 <term>4</term><description>DarkRed</description>
///             </item>
///             <item>
///                 <term>5</term><description>DarkMagenta</description>
///             </item>
///             <item>
///                 <term>6</term><description>DarkYellow</description>
///             </item>
///             <item>
///                 <term>7</term><description>Grey</description>
///             </item>
///             <item>
///                 <term>8</term><description>DarkGrey</description>
///             </item>
///             <item>
///                 <term>9</term><description>Blue</description>
///             </item>
///             <item>
///                 <term>10</term><description>Green</description>
///             </item>
///             <item>
///                 <term>11</term><description>Cyan</description>
///             </item>
///             <item>
///                 <term>12</term><description>Red</description>
///             </item>
///             <item>
///                 <term>13</term><description>Magenta</description>
///             </item>
///             <item>
///                 <term>14</term><description>Yellow</description>
///             </item>
///             <item>
///                 <term>15</term><description>White</description>
///             </item>
///         </list>
///     </para>
/// </remarks>
[Serializable]
[JsonConverter(typeof(ColourJsonConverter))]
public readonly record struct Colour : IComparable<Colour>
{
    /// <summary>
    ///     Returns the colour value <c>Black</c> (<see cref="Id" /> = 0, <see cref="Name" /> = <c>"Black"</c>).
    /// </summary>
    public static readonly Colour Black = new(0, "Black");

    /// <summary>
    ///     Returns the colour value <c>Blue</c> (<see cref="Id" /> = 9, <see cref="Name" /> = <c>"Blue"</c>).
    /// </summary>
    public static readonly Colour Blue = new(9, "Blue");

    /// <summary>
    ///     Returns the colour value <c>Cyan</c> (<see cref="Id" /> = 11, <see cref="Name" /> = <c>"Cyan"</c>).
    /// </summary>
    public static readonly Colour Cyan = new(11, "Cyan");

    /// <summary>
    ///     Returns the colour value <c>DarkBlue</c> (<see cref="Id" /> = 1, <see cref="Name" /> = <c>"DarkBlue"</c>).
    /// </summary>
    public static readonly Colour DarkBlue = new(1, "DarkBlue");

    /// <summary>
    ///     Returns the colour value <c>DarkCyan</c> (<see cref="Id" /> = 3, <see cref="Name" /> = <c>"DarkCyan"</c>).
    /// </summary>
    public static readonly Colour DarkCyan = new(3, "DarkCyan");

    /// <summary>
    ///     Returns the colour value <c>DarkGreen</c> (<see cref="Id" /> = 2, <see cref="Name" /> = <c>"DarkGreen"</c>).
    /// </summary>
    public static readonly Colour DarkGreen = new(2, "DarkGreen");

    /// <summary>
    ///     Returns the colour value <c>DarkGrey</c> (<see cref="Id" /> = 8, <see cref="Name" /> = <c>"DarkGrey"</c>).
    /// </summary>
    public static readonly Colour DarkGrey = new(8, "DarkGrey");

    /// <summary>
    ///     Returns the colour value <c>DarkMagenta</c> (<see cref="Id" /> = 5, <see cref="Name" /> = <c>"DarkMagenta"</c>).
    /// </summary>
    public static readonly Colour DarkMagenta = new(5, "DarkMagenta");

    /// <summary>
    ///     Returns the colour value <c>DarkRed</c> (<see cref="Id" /> = 4, <see cref="Name" /> = <c>"DarkRed"</c>).
    /// </summary>
    public static readonly Colour DarkRed = new(4, "DarkRed");

    /// <summary>
    ///     Returns the colour value <c>DarkYellow</c> (<see cref="Id" /> = 6, <see cref="Name" /> = <c>"DarkYellow"</c>).
    /// </summary>
    public static readonly Colour DarkYellow = new(6, "DarkYellow");

    /// <summary>
    ///     Returns the colour value <c>Green</c> (<see cref="Id" /> = 10, <see cref="Name" /> = <c>"Green"</c>).
    /// </summary>
    public static readonly Colour Green = new(10, "Green");

    /// <summary>
    ///     Returns the colour value <c>Grey</c> (<see cref="Id" /> = 7, <see cref="Name" /> = <c>"Grey"</c>).
    /// </summary>
    public static readonly Colour Grey = new(7, "Grey");

    /// <summary>
    ///     Returns the colour value <c>Magenta</c> (<see cref="Id" /> = 13, <see cref="Name" /> = <c>"Magenta"</c>).
    /// </summary>
    public static readonly Colour Magenta = new(13, "Magenta");

    /// <summary>
    ///     Returns the colour value <c>Red</c> (<see cref="Id" /> = 12, <see cref="Name" /> = <c>"Red"</c>).
    /// </summary>
    public static readonly Colour Red = new(12, "Red");

    /// <summary>
    ///     Returns the colour value <c>White</c> (<see cref="Id" /> = 15, <see cref="Name" /> = <c>"White"</c>).
    /// </summary>
    public static readonly Colour White = new(15, "White");

    /// <summary>
    ///     Returns the colour value <c>Yellow</c> (<see cref="Id" /> = 14, <see cref="Name" /> = <c>"Yellow"</c>).
    /// </summary>
    public static readonly Colour Yellow = new(14, "Yellow");

    private static readonly Colour[] ColourValuesIndexedById =
    [
        Black, DarkBlue, DarkGreen, DarkCyan, DarkRed, DarkMagenta, DarkYellow, Grey,
        DarkGrey, Blue, Green, Cyan, Red, Magenta, Yellow, White
    ];

    /// <summary>
    ///     Initializes a new <see cref="Colour" /> instance equal to the static <see cref="Black" /> value.
    /// </summary>
    public Colour() : this(0, "Black")
    {
    }

    private Colour(int id, string name)
    {
        Id = id;
        Name = name;
    }

    /// <summary>
    ///     Gets the colour's unique id number.
    /// </summary>
    /// <value>A 32-bit signed integer in the range [0,15] that uniquely identifies the colour.</value>
    public int Id { get; }

    /// <summary>
    ///     Gets the colour's unique name.
    /// </summary>
    /// <value>A string that uniquely names the colour.</value>
    public string Name { get; }

    /// <summary>
    ///     Compares this instance with the specified <see cref="Colour" /> instance and returns an integer that indicates
    ///     whether this instance precedes, follows, or appears in the same position in the sort order as the other.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="Colour" /> instances are compared by their <see cref="Id" /> values.
    /// </remarks>
    /// <param name="other">The <see cref="Colour" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in
    ///     the sort order as the <paramref name="other" /> parameter.
    ///     <list type="table">
    ///         <listheader>
    ///             <term>Value</term><description>Condition</description>
    ///         </listheader>
    ///         <item>
    ///             <term>Less than zero</term>
    ///             <description>This instance precedes the <paramref name="other" />.</description>
    ///         </item>
    ///         <item>
    ///             <term>Zero</term>
    ///             <description>This instance has the same position in the sort order as the <paramref name="other" />.</description>
    ///         </item>
    ///         <item>
    ///             <term>Greater than zero</term>
    ///             <description>This instance follows the <paramref name="other" />.</description>
    ///         </item>
    ///     </list>
    /// </returns>
    public int CompareTo(Colour other) => Id.CompareTo(other.Id);

    /// <summary>
    ///     Determines whether this instance and another specified <see cref="Colour" /> instance are the same
    ///     <see cref="Colour" /> value.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="Colour" /> instances have equal value if their <see cref="Id" /> values are equal.
    /// </remarks>
    /// <param name="other">The <see cref="Colour" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <c>true</c> if this instance and the <paramref name="other" /> parameter have equal value; otherwise, <c>false</c>.
    /// </returns>
    public bool Equals(Colour other) => Id == other.Id;

    /// <summary>
    ///     Gets the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => Id;

    /// <summary>
    ///     Gets the string representation of this instance, which is its <see cref="Name" /> value.
    /// </summary>
    /// <returns>A string that uniquely names the colour.</returns>
    public override string ToString() => Name;

    /// <summary>
    ///     Returns the <see cref="Colour" /> value corresponding to the specified <see cref="ConsoleColor" /> enumeration
    ///     value.
    /// </summary>
    /// <param name="value">A value of the <see cref="ConsoleColor" /> enumeration.</param>
    /// <returns>The requested <see cref="Colour" /> value.</returns>
    /// <exception cref="ArgumentException">
    ///     No <see cref="Colour" /> value exists matching the <paramref name="value" /> parameter.
    /// </exception>
    public static Colour FromConsoleColor(ConsoleColor value)
    {
        return value switch
        {
            ConsoleColor.Black => Black,
            ConsoleColor.DarkBlue => DarkBlue,
            ConsoleColor.DarkGreen => DarkGreen,
            ConsoleColor.DarkCyan => DarkCyan,
            ConsoleColor.DarkRed => DarkRed,
            ConsoleColor.DarkMagenta => DarkMagenta,
            ConsoleColor.DarkYellow => DarkYellow,
            ConsoleColor.Gray => Grey,
            ConsoleColor.DarkGray => DarkGrey,
            ConsoleColor.Blue => Blue,
            ConsoleColor.Green => Green,
            ConsoleColor.Cyan => Cyan,
            ConsoleColor.Red => Red,
            ConsoleColor.Magenta => Magenta,
            ConsoleColor.Yellow => Yellow,
            _ => White
        };
    }

    /// <summary>
    ///     Returns the <see cref="Colour" /> value with the specified <see cref="Id" /> value.
    /// </summary>
    /// <param name="id">An integer in the range [0,15]. The colour's unique identifier.</param>
    /// <returns>The requested <see cref="Colour" /> value.</returns>
    /// <exception cref="ArgumentException">
    ///     No <see cref="Colour" /> value exists with an <see cref="Id" /> value matching the <paramref name="id" />
    ///     parameter.
    /// </exception>
    public static Colour FromId(int id)
    {
        try
        {
            return ColourValuesIndexedById[id];
        }
        catch (IndexOutOfRangeException)
        {
            throw new ArgumentException($"No Colour value with Id = {id}.");
        }
    }

    /// <summary>
    ///     Returns the <see cref="Colour" /> value with the specified <see cref="Name" /> name.
    /// </summary>
    /// <param name="name">The name of the colour.</param>
    /// <returns>The requested <see cref="Colour" /> value.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="name" /> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    ///     No <see cref="Colour" /> value exists with a <see cref="Name" /> value matching the <paramref name="name" />
    ///     parameter.
    /// </exception>
    public static Colour FromName(string name)
    {
        _ = name ?? throw new ArgumentNullException(nameof(name));

        foreach (Colour c in ColourValuesIndexedById)
        {
            if (c.Name.Equals(name, StringComparison.Ordinal))
            {
                return c;
            }
        }

        throw new ArgumentException($"No Colour value with Name = \"{name}\".");
    }

    /// <summary>
    ///     Enumerates the <see cref="Name" /> values of all 16 <see cref="Colour" /> values, ordered by <see cref="Id" />
    ///     value.
    /// </summary>
    /// <returns>A sequence of 16 strings.</returns>
    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    public static IEnumerable<string> GetNames()
    {
        foreach (Colour c in ColourValuesIndexedById)
        {
            yield return c.Name;
        }
    }

    /// <summary>
    ///     Returns the list of all 16 <see cref="Colour" /> values, ordered by <see cref="Id" /> value.
    /// </summary>
    /// <returns>An immutable list of 16 <see cref="Colour" /> instances.</returns>
    public static IReadOnlyList<Colour> GetValues() => ColourValuesIndexedById;

    /// <summary>
    ///     Converts a given <see cref="Colour" /> instance to an integer that represents it.
    /// </summary>
    /// <param name="colour">The <see cref="Colour" /> to be converted.</param>
    /// <returns>A 32-bit signed integer, which is the <see cref="Id" /> of the <paramref name="colour" /> parameter.</returns>
    public static explicit operator int(Colour colour) => colour.Id;

    /// <summary>
    ///     Converts a given integer to the <see cref="Colour" /> value that matches it.
    /// </summary>
    /// <param name="id">An integer in the range [0,15]. The unique identifier of the <see cref="Colour" /> to be returned.</param>
    /// <returns>A new <see cref="Colour" /> instance, with an <see cref="Id" /> matching the <paramref name="id" /> parameter.</returns>
    /// <exception cref="ArgumentException">No <see cref="Colour" /> value matches the <paramref name="id" /> parameter.</exception>
    public static explicit operator Colour(int id) => FromId(id);

    /// <summary>
    ///     Custom JSON converter for the <see cref="Colour" /> struct type.
    /// </summary>
    private sealed class ColourJsonConverter : JsonConverter<Colour>
    {
        /// <inheritdoc />
        public override Colour Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                return FromName(reader.GetString()!);
            }
            catch (ArgumentException ex)
            {
                throw new JsonException("Could not deserialize to Colour.", ex);
            }
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, Colour value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Name);
        }
    }
}
