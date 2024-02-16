using System.Text.Json.Serialization;
using Mjt85.Kolyteon.MapColouring.Internals;

namespace Mjt85.Kolyteon.MapColouring;

/// <summary>
///     Represents a Map Colouring Puzzle.
/// </summary>
/// <remarks>
///     <para>
///         A Map Colouring puzzle comprises a non-empty 2-dimensional map of uniquely identifiable regions, each of
///         which has a finite set of permitted colours that may be assigned to it. The puzzle may have a specific
///         permitted colours set for each region, or a global set for all regions.
///     </para>
///     <para>
///         To solve the puzzle, one must assign every region one of its permitted colours, so that no two neighbouring
///         regions are the same colour. The Map Colouring puzzle is always solvable for any 2-dimensional map and a global
///         set of at least 4 permitted colours.
///     </para>
///     <para>
///         A <see cref="MapColouringPuzzle" /> instance is an immutable data structure exposing two read-only
///         <see cref="RegionData" /> and <see cref="NeighbourPairs" /> properties. It represents a valid (but not
///         necessarily solvable) puzzle if its <see cref="RegionData" /> list is non-empty and contains no items with
///         duplicate <see cref="RegionDatum.Region" /> values and every item in its <see cref="NeighbourPairs" /> list has
///         two matching <see cref="RegionData" /> items.
///     </para>
///     <para>
///         This type can only be instantiated outside its assembly by one of the following means:
///         <list type="bullet">
///             <item>
///                 Using the fluent builder API, accessed via the <see cref="Create" /> static method, which throws an
///                 exception if the instantiated puzzle is invalid thereby guaranteeing a valid puzzle.
///             </item>
///             <item>Deserialization, which does not validate the instantiated puzzle.</item>
///         </list>
///     </para>
/// </remarks>
[Serializable]
public sealed record MapColouringPuzzle
{
    /// <summary>
    ///     Initializes a new <see cref="MapColouringPuzzle" /> instance with the specified <see cref="RegionData" /> and
    ///     <see cref="NeighbourPairs" /> lists.
    /// </summary>
    /// <remarks>
    ///     This internal constructor is for deserialization and testing only. Use the fluent builder API, accessed via the
    ///     <see cref="Create" /> static method, to instantiate this type outside its assembly.
    /// </remarks>
    /// <param name="regionData">The list of region data for the puzzle.</param>
    /// <param name="neighbourPairs">The list of neighbouring region pairs for the puzzle.</param>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="regionData" /> or <paramref name="neighbourPairs" /> is <c>null</c>.
    /// </exception>
    [JsonConstructor]
    internal MapColouringPuzzle(IReadOnlyList<RegionDatum> regionData, IReadOnlyList<NeighbourPair> neighbourPairs)
    {
        RegionData = regionData ?? throw new ArgumentNullException(nameof(regionData));
        NeighbourPairs = neighbourPairs ?? throw new ArgumentNullException(nameof(neighbourPairs));
    }

    /// <summary>
    ///     Gets the list of region data for the puzzle.
    /// </summary>
    /// <remarks>The contents of this list may be in any order.</remarks>
    /// <value>A read-only list of <see cref="RegionDatum" /> instances. The region data for the puzzle.</value>
    public IReadOnlyList<RegionDatum> RegionData { get; }

    /// <summary>
    ///     Gets the list of neighbouring region pairs for the puzzle.
    /// </summary>
    /// <remarks>The contents of this list may be in any order.</remarks>
    /// <value>
    ///     A read-only list of <see cref="NeighbourPair" /> instances. The list of neighbouring region pairs for the puzzle.
    /// </value>
    public IReadOnlyList<NeighbourPair> NeighbourPairs { get; }

    /// <summary>
    ///     Determines whether this instance and the specified <see cref="MapColouringPuzzle" /> instance have equal value,
    ///     that is, they represent logically identical Map Colouring puzzles.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="MapColouringPuzzle" /> instances are equal if the following conditions are satisfied:
    ///     <list type="bullet">
    ///         <item>
    ///             Their respective <see cref="MapColouringPuzzle.RegionData" /> lists contain logically equivalent items
    ///             (irrespective of order).
    ///         </item>
    ///         <item>
    ///             Their respective <see cref="MapColouringPuzzle.NeighbourPairs" /> lists contain logically equivalent
    ///             items (irrespective of order).
    ///         </item>
    ///     </list>
    /// </remarks>
    /// <param name="other">The <see cref="MapColouringPuzzle" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <c>true</c> if this instance and the <paramref name="other" /> parameter have equal value; otherwise,
    ///     <c>false</c>. If the <paramref name="other" /> parameter is <c>null</c>, the method returns <c>false</c>.
    /// </returns>
    public bool Equals(MapColouringPuzzle? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return RegionData.Count == other.RegionData.Count
               && NeighbourPairs.Count == other.NeighbourPairs.Count
               && RegionData.OrderBy(d => d).SequenceEqual(other.RegionData.OrderBy(d => d))
               && NeighbourPairs.OrderBy(d => d).SequenceEqual(other.NeighbourPairs.OrderBy(d => d));
    }

    /// <summary>
    ///     Deconstructs this instance.
    /// </summary>
    /// <param name="regionData">The region data for the puzzle.</param>
    /// <param name="neighbourPairs">The neighbouring pairs for the puzzle.</param>
    public void Deconstruct(out IReadOnlyList<RegionDatum> regionData, out IReadOnlyList<NeighbourPair> neighbourPairs)
    {
        regionData = RegionData;
        neighbourPairs = NeighbourPairs;
    }

    /// <summary>
    ///     Gets the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(RegionData, NeighbourPairs);

    /// <summary>
    ///     Begins the process of creating a <see cref="MapColouringPuzzle" /> using the fluent builder API.
    /// </summary>
    /// <remarks>
    ///     Any <see cref="MapColouringPuzzle" /> created using the fluent builder API is guaranteed to represent a valid
    ///     (but not necessarily solvable) Map Colouring puzzle.
    /// </remarks>
    /// <returns>A new fluent builder instance, to which method invocations can be chained.</returns>
    public static IMapColouringPuzzleBuilder Create() => new MapColouringPuzzleBuilder();
}
