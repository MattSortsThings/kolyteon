namespace Mjt85.Kolyteon.MapColouring;

/// <summary>
///     Fluent builder for the <see cref="MapColouringPuzzle" /> record type.
/// </summary>
/// <remarks>
///     Any <see cref="MapColouringPuzzle" /> instance built using this fluent builder API is guaranteed to represent
///     a valid (but not necessarily solvable) Map Colouring puzzle.
/// </remarks>
public interface IMapColouringPuzzleBuilder
{
    /// <summary>
    ///     Specifies the global permitted colours for the puzzle.
    /// </summary>
    /// <param name="colours">
    ///     The global permitted colours for the puzzle. Any duplicate <see cref="Colour" /> values will be
    ///     disregarded.
    /// </param>
    /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
    public IRegionAdder WithGlobalColours(params Colour[] colours);

    /// <summary>
    ///     Specifies the preset map and the global permitted colours for the puzzle.
    /// </summary>
    /// <remarks>
    ///     This method has the effect of populating the returned <see cref="MapColouringPuzzle" /> instance's
    ///     <see cref="MapColouringPuzzle.RegionData" /> and <see cref="MapColouringPuzzle.NeighbourPairs" /> lists with
    ///     items matching the method arguments. No further additions can be made.
    /// </remarks>
    /// <param name="presetMap">Contains the regions and neighbours for the puzzle.</param>
    /// <param name="colours">The global permitted colours for the puzzle. Duplicate values are disregarded.</param>
    /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="presetMap" /> is <c>null</c>.</exception>
    public ITerminal WithPresetMapAndGlobalColours(PresetMap presetMap, params Colour[] colours);

    /// <summary>
    ///     Specifies that every region in the puzzle will have its own specific permitted colours.
    /// </summary>
    /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
    public IRegionAndColoursAdder WithRegionSpecificColours();

    /// <summary>
    ///     Fluent builder for the <see cref="MapColouringPuzzle" /> record type.
    /// </summary>
    /// <remarks>
    ///     Any <see cref="MapColouringPuzzle" /> instance built using this fluent builder API is guaranteed to represent
    ///     a valid (but not necessarily solvable) Map Colouring puzzle.
    /// </remarks>
    public interface IRegionAndColoursAdder : INeighboursSetter
    {
        /// <summary>
        ///     Adds the specified region to the map with the specified permitted colours.
        /// </summary>
        /// <remarks>
        ///     This method has the effect of adding an item to the returned <see cref="MapColouringPuzzle" />'s
        ///     <see cref="MapColouringPuzzle.RegionData" /> list.
        /// </remarks>
        /// <param name="region">The region to be added.</param>
        /// <param name="colours">The permitted colours for the region. Duplicate values are disregarded.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        public IRegionAndColoursAdder AddRegionWithColours(Region region, params Colour[] colours);

        /// <summary>
        ///     Adds a region with the specified <see cref="Region.Id" /> value to the map, with the specified permitted colours.
        /// </summary>
        /// <remarks>
        ///     This method has the effect of adding an item to the returned <see cref="MapColouringPuzzle" />'s
        ///     <see cref="MapColouringPuzzle.RegionData" /> list.
        /// </remarks>
        /// <param name="regionId">The identifier of the region to be added.</param>
        /// <param name="colours">The permitted colours for the region. Duplicate values are disregarded.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="regionId" /> is <c>null</c>.</exception>
        public IRegionAndColoursAdder AddRegionWithColours(string regionId, params Colour[] colours);
    }

    /// <summary>
    ///     Fluent builder for the <see cref="MapColouringPuzzle" /> record type.
    /// </summary>
    /// <remarks>
    ///     Any <see cref="MapColouringPuzzle" /> instance built using this fluent builder API is guaranteed to represent
    ///     a valid (but not necessarily solvable) Map Colouring puzzle.
    /// </remarks>
    public interface IRegionAdder : INeighboursSetter
    {
        /// <summary>
        ///     Adds the specified region to the map, having the global permitted colours set.
        /// </summary>
        /// <remarks>
        ///     This method has the effect of adding an item to the returned <see cref="MapColouringPuzzle" />'s
        ///     <see cref="MapColouringPuzzle.RegionData" /> list.
        /// </remarks>
        /// <param name="region">The region to be added.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        public IRegionAdder AddRegion(Region region);

        /// <summary>
        ///     Adds a region with the specified <see cref="Region.Id" /> value to the map, having the global permitted
        ///     colours set.
        /// </summary>
        /// <remarks>
        ///     This method has the effect of adding an item to the returned <see cref="MapColouringPuzzle" />'s
        ///     <see cref="MapColouringPuzzle.RegionData" /> list.
        /// </remarks>
        /// <param name="regionId">The identifier of the region to be added.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="regionId" /> is <c>null</c>.</exception>
        public IRegionAdder AddRegion(string regionId);

        /// <summary>
        ///     Adds all the specified regions to the map, each of which has the global permitted colours set.
        /// </summary>
        /// <remarks>
        ///     This method has the effect of adding an item to the returned <see cref="MapColouringPuzzle" />'s
        ///     <see cref="MapColouringPuzzle.RegionData" /> list for each item in the specified enumerable.
        /// </remarks>
        /// <param name="regions">The regions to be added.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="regions" /> is <c>null</c>.</exception>
        public IRegionAdder AddRegions(IEnumerable<Region> regions);
    }

    /// <summary>
    ///     Fluent builder for the <see cref="MapColouringPuzzle" /> record type.
    /// </summary>
    /// <remarks>
    ///     Any <see cref="MapColouringPuzzle" /> instance built using this fluent builder API is guaranteed to represent
    ///     a valid (but not necessarily solvable) Map Colouring puzzle.
    /// </remarks>
    public interface INeighboursSetter : ITerminal
    {
        /// <summary>
        ///     Sets as neighbours the two specified map regions, which have previously been added to the map.
        /// </summary>
        /// <remarks>
        ///     This method has the effect of adding an item to the returned <see cref="MapColouringPuzzle" />'s
        ///     <see cref="MapColouringPuzzle.NeighbourPairs" /> list. The two <see cref="Region" /> arguments can be passed
        ///     in either order.
        /// </remarks>
        /// <param name="regionA">One of the two neighbouring map regions.</param>
        /// <param name="regionB">The other of the two neighbouring map regions.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        public INeighboursSetter SetAsNeighbours(Region regionA, Region regionB);

        /// <summary>
        ///     Sets as neighbours the two regions with the specified <see cref="Region.Id" /> values, which have previously been
        ///     added to the map.
        /// </summary>
        /// <remarks>
        ///     This method has the effect of adding an item to the returned <see cref="MapColouringPuzzle" />'s
        ///     <see cref="MapColouringPuzzle.NeighbourPairs" /> list. The two string arguments can be passed in either
        ///     order.
        /// </remarks>
        /// <param name="regionAId">The identifier of one of the two neighbouring map regions.</param>
        /// <param name="regionBId">The identifier of the other of the two neighbouring map regions.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="regionAId" /> or <paramref name="regionBId" /> is <c>null</c>.
        /// </exception>
        public INeighboursSetter SetAsNeighbours(string regionAId, string regionBId);

        /// <summary>
        ///     Sets as neighbours each of the specified pairs of regions, which have previously been added to the map.
        /// </summary>
        /// <remarks>
        ///     This method has the effect of adding the contents of the specified enumerable to the returned
        ///     <see cref="MapColouringPuzzle" />'s <see cref="MapColouringPuzzle.NeighbourPairs" /> list.
        /// </remarks>
        /// <param name="neighbourPairs">The pairs of neighbours to be set.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="neighbourPairs" /> is <c>null</c>.</exception>
        public INeighboursSetter SetAsNeighbours(IEnumerable<NeighbourPair> neighbourPairs);
    }

    /// <summary>
    ///     Fluent builder for the <see cref="MapColouringPuzzle" /> record type.
    /// </summary>
    /// <remarks>
    ///     Any <see cref="MapColouringPuzzle" /> instance built using this fluent builder API is guaranteed to represent
    ///     a valid (but not necessarily solvable) Map Colouring puzzle.
    /// </remarks>
    public interface ITerminal
    {
        /// <summary>
        ///     Creates and returns a <see cref="MapColouringPuzzle" /> matching all the previous fluent builder method
        ///     invocations.
        /// </summary>
        /// <remarks>
        ///     Any <see cref="MapColouringPuzzle" /> instance created using the fluent builder API is guaranteed to represent
        ///     a valid (but not necessarily solvable) Map Colouring puzzle.
        /// </remarks>
        /// <returns>A new <see cref="MapColouringPuzzle" /> instance.</returns>
        /// <exception cref="InvalidOperationException">
        ///     The instantiated <see cref="MapColouringPuzzle" />, as defined by the previous method invocations, does not
        ///     represent a valid Map Colouring puzzle.
        /// </exception>
        public MapColouringPuzzle Build();
    }
}
