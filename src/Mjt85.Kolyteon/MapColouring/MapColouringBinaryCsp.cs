using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.MapColouring;

/// <summary>
///     Can model any Map Colouring puzzle as a binary CSP.
/// </summary>
public sealed class MapColouringBinaryCsp : BinaryCsp<MapColouringPuzzle, Region, Colour>
{
    private static readonly DifferentColoursPredicate DifferentColours = new();

    private readonly Dictionary<Region, IReadOnlyCollection<Colour>> _coloursByRegion;
    private readonly Dictionary<Region, HashSet<Region>> _neighboursByRegion;

    /// <summary>
    ///     Initializes a new <see cref="MapColouringBinaryCsp" /> instance that is not modelling a problem and has the
    ///     specified initial capacity.
    /// </summary>
    /// <param name="capacity">
    ///     The maximum number of binary CSP variables the new <see cref="MapColouringBinaryCsp" /> can initially store without
    ///     needing to resize its internal data structures.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity" /> is negative.</exception>
    public MapColouringBinaryCsp(int capacity) : base(capacity)
    {
        _coloursByRegion = new Dictionary<Region, IReadOnlyCollection<Colour>>(capacity);
        _neighboursByRegion = new Dictionary<Region, HashSet<Region>>(capacity);
    }

    /// <summary>
    ///     Ensures that the capacity of this instance is at least the specified capacity.
    /// </summary>
    /// <returns>A non-negative 32-bit signed integer. The new capacity of this instance.</returns>
    protected override int EnsureCapacity(int capacity)
    {
        var newCapacity = base.EnsureCapacity(capacity);
        _ = _coloursByRegion.EnsureCapacity(capacity);
        _ = _neighboursByRegion.EnsureCapacity(capacity);

        return newCapacity;
    }

    /// <summary>
    ///     Sets the capacity of this instance to the actual number of binary CSP variables, if that number is less than a
    ///     threshold value.
    /// </summary>
    /// <remarks>
    ///     This method can be used to reduce overhead if this instance is modelling a problem and it is known that it will not
    ///     need to model a larger problem.
    /// </remarks>
    protected override void TrimExcess()
    {
        base.TrimExcess();
        _coloursByRegion.TrimExcess();
        _neighboursByRegion.TrimExcess();
    }

    /// <inheritdoc />
    private protected override void PopulateProblemData(MapColouringPuzzle problem)
    {
        foreach ((Region region, IReadOnlyCollection<Colour> colours) in problem.RegionData)
        {
            _coloursByRegion.Add(region, colours);
        }

        foreach (IGrouping<Region, NeighbourPair> grouping in problem.NeighbourPairs.GroupBy(pair => pair.First))
        {
            _neighboursByRegion.Add(grouping.Key, grouping.Select(pair => pair.Second).ToHashSet());
        }
    }

    /// <inheritdoc />
    private protected override void ClearProblemData()
    {
        _coloursByRegion.Clear();
        _neighboursByRegion.Clear();
    }

    /// <inheritdoc />
    /// <remarks>In the Map Colouring binary CSP model, the variables are the set of all the regions in the map.</remarks>
    private protected override IEnumerable<Region> GetVariables() => _coloursByRegion.Keys;

    /// <inheritdoc />
    /// <remarks>In the Map Colouring binary CSP model, the domain of a region variable is its set of permitted colours.</remarks>
    private protected override IEnumerable<Colour> GetDomainOf(Region variable) => _coloursByRegion[variable];

    /// <inheritdoc />
    /// <remarks>
    ///     In the Map Colouring binary CSP model, there is a notional binary constraint for every pair of region
    ///     variables that are neighbours of each other. The constraint has a binary predicate that asserts that the two
    ///     regions must be assigned different colours. The constraint is only added to the binary CSP if it is genuine, that
    ///     is, if there exists at least one pair of equal colours from the Cartesian product of the variables' domains.
    /// </remarks>
    private protected override IBinaryPredicate<Colour> GetBinaryPredicateFor(Region variable1, Region variable2)
    {
        if (_neighboursByRegion.TryGetValue(variable1, out HashSet<Region>? neighbours) && neighbours.Contains(variable2))
        {
            return DifferentColours;
        }

        return NotAdjacent;
    }

    private sealed class DifferentColoursPredicate : IBinaryPredicate<Colour>
    {
        public bool CanAssign(in Colour domainValue1, in Colour domainValue2) => domainValue1 != domainValue2;
    }
}
