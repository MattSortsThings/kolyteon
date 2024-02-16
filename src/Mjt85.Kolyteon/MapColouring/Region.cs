using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Mjt85.Kolyteon.MapColouring;

/// <summary>
///     Represents a specific region in a map.
/// </summary>
/// <remarks>
///     A <see cref="Region" /> instance is identified by its <see cref="Id" /> value, which is a non-empty string of
///     letters and/or digits. <see cref="Region" /> instances are compared by <see cref="Id" /> value using case-sensitive
///     ordinal string comparison rules. A <see cref="Region" /> instance is represented by, serialized to, and
///     deserialized from its <see cref="Id" /> string value.
/// </remarks>
[Serializable]
[JsonConverter(typeof(RegionJsonConverter))]
public readonly record struct Region : IComparable<Region>
{
    private static readonly Regex ValidIdRegex = new("^[A-Za-z0-9]+$", RegexOptions.Compiled, TimeSpan.FromMilliseconds(500));

    /// <summary>
    ///     Initializes a new <see cref="Region" /> instance with the default <see cref="Id" /> value of <c>"Default"</c>.
    /// </summary>
    public Region() : this("Default")
    {
    }

    private Region(string id)
    {
        Id = id;
    }

    /// <summary>
    ///     Gets the region's unique identifier.
    /// </summary>
    /// <value>A non-empty string of letters and/or digits that uniquely identifies the region in its map.</value>
    public string Id { get; }

    /// <summary>
    ///     Compares this instance with the specified <see cref="Region" /> instance and indicates whether this instance
    ///     precedes, follows, or appears in the same position in the sort order as the other.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="Region" /> instances are compared by their <see cref="Id" /> values using case-sensitive ordinal
    ///     string comparison rules.
    /// </remarks>
    /// <param name="other">The <see cref="Region" /> instance against which this instance is to be compared.</param>
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
    public int CompareTo(Region other) => string.Compare(Id, other.Id, StringComparison.Ordinal);

    /// <summary>
    ///     Determines whether this instance and the specified <see cref="Region" /> instance have equal value, that is, they
    ///     represent the same region in the map.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="Region" /> instances have equal value if they have equal <see cref="Id" /> values using
    ///     case-sensitive ordinal string comparison rules.
    /// </remarks>
    /// <param name="other">The <see cref="Region" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <c>true</c> if this instance and the <paramref name="other" /> parameter have equal value; otherwise, <c>false</c>.
    /// </returns>
    public bool Equals(Region other) => string.Equals(Id, other.Id, StringComparison.Ordinal);

    /// <summary>
    ///     Gets the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => Id.GetHashCode(StringComparison.Ordinal);

    /// <summary>
    ///     Gets the string representation of this instance, which is its <see cref="Id" /> value.
    /// </summary>
    /// <returns>A non-empty string of letters and/or digits that uniquely identifies the region in its map.</returns>
    public override string ToString() => Id;

    /// <summary>
    ///     Creates and returns a new <see cref="Region" /> instance with the specified <see cref="Id" /> value.
    /// </summary>
    /// <param name="id">A non-empty string of letters and/or digits that uniquely identifies the region in its map.</param>
    /// <returns>A new <see cref="Region" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="id" /> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    ///     <paramref name="id" /> is an empty string or contains a non-alphanumeric character.
    /// </exception>
    public static Region FromId(string id)
    {
        _ = id ?? throw new ArgumentNullException(nameof(id));

        if (ValidIdRegex.IsMatch(id))
        {
            return new Region(id);
        }

        throw new ArgumentException("Value must be a non-empty string of letters and/or digits only.", nameof(id));
    }

    /// <summary>
    ///     Custom JSON converter for the <see cref="Region" /> struct type.
    /// </summary>
    private sealed class RegionJsonConverter : JsonConverter<Region>
    {
        /// <inheritdoc />
        public override Region Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                return FromId(reader.GetString()!);
            }
            catch (ArgumentException ex)
            {
                throw new JsonException("Could not deserialize to Region.", ex);
            }
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, Region value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Id);
        }
    }
}
