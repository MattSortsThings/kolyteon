using System.Text.Json.Serialization;

namespace Mjt85.Kolyteon.MapColouring;

/// <summary>
///     Immutable data structure containing information about a single region in a Map Colouring puzzle.
/// </summary>
[Serializable]
public sealed record RegionDatum : IComparable<RegionDatum>
{
    /// <summary>
    ///     Initializes a new <see cref="RegionDatum" /> instance with the specified <see cref="Region" /> and
    ///     <see cref="Colours" />.
    /// </summary>
    /// <param name="region">The region.</param>
    /// <param name="colours">The permitted colours for the region.</param>
    /// <exception cref="ArgumentNullException"><paramref name="colours" /> is <c>null</c>.</exception>
    [JsonConstructor]
    public RegionDatum(Region region, IReadOnlyCollection<Colour> colours)
    {
        Region = region;
        Colours = colours ?? throw new ArgumentNullException(nameof(colours));
    }

    /// <summary>
    ///     Gets the region.
    /// </summary>
    /// <value>A <see cref="Region" /> instance. The region.</value>
    public Region Region { get; }

    /// <summary>
    ///     Gets the permitted colours for the region.
    /// </summary>
    /// <remarks>No assumptions should be made about the ordering of the values in this collection.</remarks>
    /// <value>A read-only collection of <see cref="Colour" /> values. The permitted colours for the region.</value>
    public IReadOnlyCollection<Colour> Colours { get; }

    /// <summary>
    ///     Compares this instance with the specified <see cref="RegionDatum" /> instance and indicates whether this instance
    ///     precedes, follows, or appears in the same position in the sort order as the other.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="RegionDatum" /> instances are compared by their <see cref="Region" /> values using the
    ///     <see cref="Region" /> struct type's <see cref="IComparable{T}" /> implementation.
    /// </remarks>
    /// <param name="other">The <see cref="RegionDatum" /> instance against which this instance is to be compared.</param>
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
    ///             <description>
    ///                 This instance has the same position in the sort order as the <paramref name="other" />.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>Greater than zero</term>
    ///             <description>
    ///                 This instance follows the <paramref name="other" />, or <paramref name="other" /> is <c>null</c>.
    ///             </description>
    ///         </item>
    ///     </list>
    /// </returns>
    public int CompareTo(RegionDatum? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        return ReferenceEquals(null, other) ? 1 : Region.CompareTo(other.Region);
    }

    /// <summary>
    ///     Determines whether this instance and the specified <see cref="RegionDatum" /> instance have equal value.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="RegionDatum" /> instances have equal value if they have equal <see cref="Region" /> values and
    ///     their respective <see cref="Colours" /> collections contain the exact same values (irrespective of order).
    /// </remarks>
    /// <param name="other">The <see cref="RegionDatum" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <c>true</c> if this instance and the <paramref name="other" /> parameter have equal value; otherwise,
    ///     <c>false</c>. If the <paramref name="other" /> parameter is <c>null</c>, the method returns <c>false</c>.
    /// </returns>
    public bool Equals(RegionDatum? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Region.Equals(other.Region) && Colours.Count.Equals(other.Colours.Count) && !Colours.Except(other.Colours).Any();
    }

    /// <summary>
    ///     Gets the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Region, Colours);

    /// <summary>
    ///     Deconstructs this instance.
    /// </summary>
    /// <param name="region">The region.</param>
    /// <param name="colours">The permitted colours for the region.</param>
    public void Deconstruct(out Region region, out IReadOnlyCollection<Colour> colours)
    {
        region = Region;
        colours = Colours;
    }
}
