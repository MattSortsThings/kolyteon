namespace Mjt85.Kolyteon.MapColouring.Internals;

/// <summary>
///     Guard clauses.
/// </summary>
internal static class Guard
{
    /// <summary>
    ///     Checks that the specified <see cref="MapColouringPuzzle" /> represents a valid Map Colouring puzzle and throws an
    ///     exception if it is invalid.
    /// </summary>
    /// <param name="puzzle">The <see cref="MapColouringPuzzle" /> to be checked.</param>
    /// <exception cref="InvalidOperationException">
    ///     The <paramref name="puzzle" /> parameter does not represent a valid Map Colouring puzzle.
    /// </exception>
    public static void AgainstInvalidPuzzle(MapColouringPuzzle puzzle)
    {
        (IReadOnlyList<RegionDatum> regionData, IReadOnlyList<NeighbourPair> neighbourPairs) = puzzle;
        ThrowIfEmpty(regionData);
        Region[] regions = ExtractAllRegions(regionData);
        ThrowIfDuplicateRegions(regions);
        ThrowIfNeighbourWithNoRegionDatum(neighbourPairs, regions);
    }

    private static void ThrowIfEmpty(IReadOnlyCollection<RegionDatum> regionData)
    {
        if (regionData.Count == 0)
        {
            throw new InvalidOperationException("RegionData is empty.");
        }
    }

    private static Region[] ExtractAllRegions(IEnumerable<RegionDatum> regionData) =>
        regionData.OrderBy(d => d.Region)
            .Select(d => d.Region)
            .ToArray();

    private static void ThrowIfDuplicateRegions(IEnumerable<Region> regions)
    {
        IEnumerable<string> errorQuery = regions.GroupBy(d => d)
            .Where(g => g.Count() > 1)
            .Select(g => $"RegionData has multiple items with same Region \"{g.Key}\".");

        var firstError = errorQuery.FirstOrDefault();

        if (firstError is not null)
        {
            throw new InvalidOperationException(firstError);
        }
    }

    private static void ThrowIfNeighbourWithNoRegionDatum(IReadOnlyList<NeighbourPair> neighbourPairs, Region[] regions)
    {
        IEnumerable<string> errorQuery = neighbourPairs.ExceptBy(regions, d => d.First)
            .Concat(neighbourPairs.ExceptBy(regions, d => d.Second))
            .Select(n => $"NeighbourPairs has item without matching RegionData item for one or both Regions: {n}.");

        var firstError = errorQuery.FirstOrDefault();

        if (firstError is not null)
        {
            throw new InvalidOperationException(firstError);
        }
    }
}
