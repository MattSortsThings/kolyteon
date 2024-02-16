using System.Text.Json.Serialization;

namespace Mjt85.Kolyteon.MapColouring;

/// <summary>
///     Represents an ordered pair of neighbouring regions in a Map Colouring puzzle.
/// </summary>
/// <remarks>
///     A <see cref="NeighbourPair" /> instance is represented by its <see cref="First" /> and <see cref="Second" />
///     <see cref="Region" /> values. <see cref="First" /> always precedes <see cref="Second" /> using the
///     <see cref="Region" /> struct type's <see cref="IComparable{T}" /> implementation.
/// </remarks>
[Serializable]
public sealed record NeighbourPair : IComparable<NeighbourPair>
{
    /// <summary>
    ///     Initializes a new <see cref="NeighbourPair" /> instance with the specified <see cref="First" /> and
    ///     <see cref="Second" /> values.
    /// </summary>
    /// <remarks>
    ///     The order in which the two arguments are passed matters. The <paramref name="first" /> parameter must precede
    ///     the <paramref name="second" /> parameter using the <see cref="Region" /> struct type's
    ///     <see cref="IComparable{T}" /> implementation.
    /// </remarks>
    /// <param name="first">The first of the two neighbouring regions.</param>
    /// <param name="second">The second of the two neighbouring regions.</param>
    /// <exception cref="ArgumentException"><paramref name="first" /> does not precede <see cref="second" />.</exception>
    /// <seealso cref="Region.CompareTo" />
    [JsonConstructor]
    public NeighbourPair(Region first, Region second)
    {
        if (first.CompareTo(second) >= 0)
        {
            throw new ArgumentException("First must precede Second.");
        }

        First = first;
        Second = second;
    }

    /// <summary>
    ///     Gets the first of the two neighbouring regions.
    /// </summary>
    /// <value>A <see cref="Region" /> instance. The first of the two neighbouring regions.</value>
    public Region First { get; }

    /// <summary>
    ///     Gets the second of the two neighbouring regions.
    /// </summary>
    /// <value>A <see cref="Region" /> instance. The second of the two neighbouring regions.</value>
    public Region Second { get; }

    /// <summary>
    ///     Compares this instance with the specified <see cref="NeighbourPair" /> instance and indicates whether this instance
    ///     precedes, follows, or appears in the same position in the sort order as the other.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="NeighbourPair" /> instances are initially compared by their <see cref="First" /> values using the
    ///     <see cref="Region" /> struct type's <see cref="IComparable{T}" /> implementation. If their <see cref="First" />
    ///     values are equal, they are then compared by their <see cref="Second" /> values.
    /// </remarks>
    /// <param name="other">The <see cref="NeighbourPair" /> instance against which this instance is to be compared.</param>
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
    public int CompareTo(NeighbourPair? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        if (ReferenceEquals(null, other))
        {
            return 1;
        }

        var firstComparison = First.CompareTo(other.First);

        return firstComparison != 0 ? firstComparison : Second.CompareTo(other.Second);
    }

    /// <summary>
    ///     Determines whether this instance and the specified <see cref="NeighbourPair" /> instance have equal value, that is,
    ///     they represent the same ordered pair of neighbouring map regions.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="NeighbourPair" /> instances have equal value if they have equal <see cref="First" /> values and
    ///     equal <see cref="Second" /> values (irrespective of order).
    /// </remarks>
    /// <param name="other">The <see cref="NeighbourPair" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <c>true</c> if this instance and the <paramref name="other" /> parameter have equal value; otherwise,
    ///     <c>false</c>. If the <paramref name="other" /> parameter is <c>null</c>, the method returns <c>false</c>.
    /// </returns>
    public bool Equals(NeighbourPair? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return First.Equals(other.First) && Second.Equals(other.Second);
    }

    /// <summary>
    ///     Deconstructs this instance.
    /// </summary>
    /// <param name="first">The first of the two neighbouring regions.</param>
    /// <param name="second">The second of the two neighbouring regions.</param>
    public void Deconstruct(out Region first, out Region second)
    {
        first = First;
        second = Second;
    }

    /// <summary>
    ///     Gets the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(First, Second);

    /// <summary>
    ///     Creates and returns a new <see cref="NeighbourPair" /> instance composed of the two specified <see cref="Region" />
    ///     instances.
    /// </summary>
    /// <remarks>
    ///     The two <see cref="Region" /> arguments may be passed in either order. Whichever of the two arguments is
    ///     sorted first using the <see cref="Region" /> struct type's <see cref="IComparable{T}" /> implementation is assigned
    ///     to the <see cref="First" /> property of the returned <see cref="NeighbourPair" /> instance; the other argument is
    ///     assigned to its <see cref="Second" /> property.
    /// </remarks>
    /// <param name="regionA">One of the two neighbouring map regions.</param>
    /// <param name="regionB">The other of the two neighbouring map regions.</param>
    /// <returns>A new <see cref="NeighbourPair" /> instance.</returns>
    /// <exception cref="ArgumentException"><paramref name="regionA" /> is equal to <see cref="regionB" />.</exception>
    public static NeighbourPair Of(Region regionA, Region regionB)
    {
        if (regionA.Equals(regionB))
        {
            throw new ArgumentException("Region cannot be neighbour of itself.");
        }

        return regionA.CompareTo(regionB) < 0 ? new NeighbourPair(regionA, regionB) : new NeighbourPair(regionB, regionA);
    }
}
