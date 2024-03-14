namespace Mjt85.Kolyteon.MapColouring;

/// <summary>
///     Contains data for a preset map for use in a Map Colouring puzzle.
/// </summary>
[Serializable]
public sealed record PresetMap
{
    /// <summary>
    ///     Gets the list of all the regions in the map.
    /// </summary>
    /// <remarks>No assumptions should be made about the ordering of the values in this list.</remarks>
    /// <value>A read-only list of <see cref="Region" /> instances. The regions in the map.</value>
    public IReadOnlyList<Region> Regions { get; init; } = [];

    /// <summary>
    ///     Gets the list of all the pairs of neighbouring regions in the map.
    /// </summary>
    /// <remarks>No assumptions should be made about the ordering of the values in this list.</remarks>
    /// <value>A read-only list of <see cref="NeighbourPair" /> instances. The pairs of neighbouring regions in the map.</value>
    public IReadOnlyList<NeighbourPair> NeighbourPairs { get; init; } = [];
}
