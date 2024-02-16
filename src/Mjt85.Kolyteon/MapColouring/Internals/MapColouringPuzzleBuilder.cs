namespace Mjt85.Kolyteon.MapColouring.Internals;

/// <summary>
///     Fluent builder for the <see cref="MapColouringPuzzle" /> record type.
/// </summary>
/// <remarks>
///     Any <see cref="MapColouringPuzzle" /> instance built using this fluent builder API is guaranteed to represent
///     a valid (but not necessarily solvable) Map Colouring puzzle.
/// </remarks>
internal sealed class MapColouringPuzzleBuilder : IMapColouringPuzzleBuilder,
    IMapColouringPuzzleBuilder.IRegionAdder,
    IMapColouringPuzzleBuilder.IRegionAndColoursAdder
{
    private readonly HashSet<Colour> _globalColours = [];
    private readonly List<NeighbourPair> _neighbourPairs = [];
    private readonly List<RegionDatum> _regionData = [];

    /// <inheritdoc />
    public IMapColouringPuzzleBuilder.IRegionAdder WithGlobalColours(params Colour[] colours)
    {
        PopulateGlobalColours(colours);

        return this;
    }

    /// <inheritdoc />
    public IMapColouringPuzzleBuilder.ITerminal WithPresetMapAndGlobalColours(PresetMap presetMap, params Colour[] colours)
    {
        _ = presetMap ?? throw new ArgumentNullException(nameof(presetMap));
        PopulateGlobalColours(colours);
        AddRegionDataWithGlobalColours(presetMap.Regions);
        AddNeighbourPairs(presetMap.NeighbourPairs);

        return this;
    }

    /// <inheritdoc />
    public IMapColouringPuzzleBuilder.IRegionAndColoursAdder WithRegionSpecificColours() => this;

    /// <inheritdoc />
    public IMapColouringPuzzleBuilder.IRegionAdder AddRegion(Region region)
    {
        AddRegionDatumWithGlobalColours(in region);

        return this;
    }

    /// <inheritdoc />
    public IMapColouringPuzzleBuilder.IRegionAdder AddRegion(string regionId)
    {
        _ = regionId ?? throw new ArgumentNullException(nameof(regionId));

        AddRegionDatumWithGlobalColours(Region.FromId(regionId));

        return this;
    }

    /// <inheritdoc />
    public IMapColouringPuzzleBuilder.IRegionAdder AddRegions(IEnumerable<Region> regions)
    {
        _ = regions ?? throw new ArgumentNullException(nameof(regions));

        AddRegionDataWithGlobalColours(regions);

        return this;
    }

    /// <inheritdoc />
    public MapColouringPuzzle Build()
    {
        _regionData.Sort();
        _neighbourPairs.Sort();
        MapColouringPuzzle puzzle = new(_regionData, _neighbourPairs);
        Guard.AgainstInvalidPuzzle(puzzle);

        return puzzle;
    }

    /// <inheritdoc />
    public IMapColouringPuzzleBuilder.INeighboursSetter SetAsNeighbours(Region regionA, Region regionB)
    {
        AddNeighbourPair(NeighbourPair.Of(regionA, regionB));

        return this;
    }

    /// <inheritdoc />
    public IMapColouringPuzzleBuilder.INeighboursSetter SetAsNeighbours(string regionAId, string regionBId)
    {
        _ = regionAId ?? throw new ArgumentNullException(nameof(regionAId));
        _ = regionBId ?? throw new ArgumentNullException(nameof(regionBId));

        AddNeighbourPair(NeighbourPair.Of(Region.FromId(regionAId), Region.FromId(regionBId)));

        return this;
    }

    /// <inheritdoc />
    public IMapColouringPuzzleBuilder.INeighboursSetter SetAsNeighbours(IEnumerable<NeighbourPair> neighbourPairs)
    {
        _ = neighbourPairs ?? throw new ArgumentNullException(nameof(neighbourPairs));

        AddNeighbourPairs(neighbourPairs);

        return this;
    }

    /// <inheritdoc />
    public IMapColouringPuzzleBuilder.IRegionAndColoursAdder AddRegionWithColours(Region region, params Colour[] colours)
    {
        AddRegionDatumWithRegionSpecificColours(in region, [..colours]);

        return this;
    }

    /// <inheritdoc />
    public IMapColouringPuzzleBuilder.IRegionAndColoursAdder AddRegionWithColours(string regionId, params Colour[] colours)
    {
        _ = regionId ?? throw new ArgumentNullException(nameof(regionId));

        AddRegionDatumWithRegionSpecificColours(Region.FromId(regionId), [..colours]);

        return this;
    }

    private void PopulateGlobalColours(IEnumerable<Colour> colours)
    {
        foreach (Colour colour in colours)
        {
            _globalColours.Add(colour);
        }
    }

    private void AddRegionDatumWithGlobalColours(in Region region)
    {
        _regionData.Add(new RegionDatum(region, _globalColours));
    }

    private void AddRegionDatumWithRegionSpecificColours(in Region region, HashSet<Colour> colours)
    {
        _regionData.Add(new RegionDatum(region, colours));
    }

    private void AddRegionDataWithGlobalColours(IEnumerable<Region> regions)
    {
        foreach (Region region in regions)
        {
            _regionData.Add(new RegionDatum(region, _globalColours));
        }
    }

    private void AddNeighbourPair(NeighbourPair neighbourPair)
    {
        _neighbourPairs.Add(neighbourPair);
    }

    private void AddNeighbourPairs(IEnumerable<NeighbourPair> neighbourPairs)
    {
        _neighbourPairs.AddRange(neighbourPairs);
    }
}
