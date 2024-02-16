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
    /// <remarks>The contents of this list may be in any order.</remarks>
    /// <value>A read-only list of <see cref="Region" /> instances. The regions in the map.</value>
    public IReadOnlyList<Region> Regions { get; init; } = [];

    /// <summary>
    ///     Gets the list of all the pairs of neighbouring regions in the map.
    /// </summary>
    /// <remarks>The contents of this list may be in any order.</remarks>
    /// <value>A read-only list of <see cref="NeighbourPair" /> instances. The pairs of neighbouring regions in the map.</value>
    public IReadOnlyList<NeighbourPair> NeighbourPairs { get; init; } = [];
}
