using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Mjt85.Kolyteon.MapColouring.Internals;

namespace Mjt85.Kolyteon.MapColouring;

/// <summary>
///     Represents a Map Colouring Puzzle.
/// </summary>
/// <remarks>
///     <para>
///         A Map Colouring puzzle comprises a 2-dimensional map of uniquely identifiable regions. Every region has a
///         finite set of permitted colours that may be assigned to it. This may be a specific set for each region, or a
///         global set for all regions.
///     </para>
///     <para>
///         To solve the puzzle, one must colour in every region with one of its permitted colours, so that no two
///         neighbouring regions are the same colour. The Map Colouring puzzle can always be solved when it has a global
///         set of at least four permitted colours.
///     </para>
///     <para>
///         A <see cref="MapColouringPuzzle" /> instance is an immutable data structure representing a Map Colouring
///         puzzle. This type can only be instantiated outside its assembly by:
///         <list type="bullet">
///             <item>
///                 using the fluent builder API, accessed via the <see cref="Create" /> static method, which throws an
///                 exception if the <see cref="MapColouringPuzzle" /> instance contains invalid data, <i>or</i>
///             </item>
///             <item>deserialization.</item>
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
    /// <remarks>No assumptions should be made about the ordering of the items in this list.</remarks>
    /// <value>A read-only list of <see cref="RegionDatum" /> instances. The region data for the puzzle.</value>
    public IReadOnlyList<RegionDatum> RegionData { get; }

    /// <summary>
    ///     Gets the list of neighbouring region pairs for the puzzle.
    /// </summary>
    /// <remarks>No assumptions should be made about the ordering of the items in this list.</remarks>
    /// <value>
    ///     A read-only list of <see cref="NeighbourPair" /> instances. The list of neighbouring region pairs for the puzzle.
    /// </value>
    public IReadOnlyList<NeighbourPair> NeighbourPairs { get; }

    /// <summary>
    ///     Determines whether this instance and the specified <see cref="MapColouringPuzzle" /> instance have equal value,
    ///     that is, they represent logically identical Map Colouring puzzles.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="MapColouringPuzzle" /> instances are equal if:
    ///     <list type="bullet">
    ///         <item>
    ///             their respective <see cref="RegionData" /> lists contain logically equal items (irrespective of order),
    ///             <i>and</i>
    ///         </item>
    ///         <item>
    ///             their respective <see cref="NeighbourPairs" /> lists contain logically equal items (irrespective of
    ///             order).
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
    ///     Determines whether the proposed solution is valid for the Map Colouring puzzle represented by this instance.
    /// </summary>
    /// <remarks>
    ///     This method applies the following validation checks to the <paramref name="solution" /> parameter sequentially, and
    ///     returns the first validation error encountered (if any):
    ///     <list type="number">
    ///         <item>
    ///             The number of key-value pairs in the <paramref name="solution" /> dictionary is equal to the number of
    ///             items in this instance's <see cref="RegionData" /> list.
    ///         </item>
    ///         <item>Every region in the map is present as a key in the solution.</item>
    ///         <item>Every region is assigned one of its permitted colours.</item>
    ///         <item>For every pair of neighbouring regions, each region in the pair is assigned a different colour.</item>
    ///     </list>
    /// </remarks>
    /// <param name="solution">
    ///     A dictionary in which each key is a <see cref="Region" /> instance and its value is its assigned
    ///     <see cref="Colour" /> value. The proposed solution to the puzzle.
    /// </param>
    /// <returns>
    ///     <see cref="ValidationResult.Success" /> (i.e. <c>null</c>) if the <paramref name="solution" /> parameter is a
    ///     valid solution; otherwise, a <see cref="ValidationResult" /> instance with an error message reporting the first
    ///     validation error encountered.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="solution" /> is <c>null</c>.</exception>
    public ValidationResult? ValidSolution(IReadOnlyDictionary<Region, Colour> solution)
    {
        _ = solution ?? throw new ArgumentNullException(nameof(solution));

        return ApplyChainedValidators(solution);
    }

    /// <summary>
    ///     Begins the process of creating a <see cref="MapColouringPuzzle" /> using the fluent builder API.
    /// </summary>
    /// <remarks>
    ///     Any <see cref="MapColouringPuzzle" /> created using the fluent builder API is guaranteed to represent a valid
    ///     (but not necessarily solvable) Map Colouring puzzle.
    /// </remarks>
    /// <returns>A new fluent builder instance, to which method invocations can be chained.</returns>
    public static IMapColouringPuzzleBuilder Create() => new MapColouringPuzzleBuilder();

    private ValidationResult? ApplyChainedValidators(IReadOnlyDictionary<Region, Colour> solution) =>
        SolutionHasCorrectSize(solution);

    private ValidationResult? SolutionHasCorrectSize(IReadOnlyDictionary<Region, Colour> solution) =>
        solution.Count != RegionData.Count
            ? new ValidationResult($"Solution size is {solution.Count}, should be {RegionData.Count}.")
            : EveryRegionPresentAsSolutionKey(solution);

    private ValidationResult? EveryRegionPresentAsSolutionKey(IReadOnlyDictionary<Region, Colour> solution)
    {
        IEnumerable<ValidationResult> errorQuery = from region in
                (from d in RegionData select d.Region).Except(solution.Keys)
            select new ValidationResult($"Region {region} not present as solution key.");

        ValidationResult? firstError = errorQuery.FirstOrDefault();

        return firstError ?? EveryRegionAssignedOneOfItsPermittedColours(solution);
    }

    private ValidationResult? EveryRegionAssignedOneOfItsPermittedColours(IReadOnlyDictionary<Region, Colour> solution)
    {
        IEnumerable<SingleQueryItem> singleQuery = from d in RegionData
            join kv in solution on d.Region equals kv.Key
            select new SingleQueryItem(d.Region, kv.Value, d.Colours);

        IEnumerable<ValidationResult> errorQuery = from item in singleQuery
            where !item.PermittedColours.Contains(item.Colour)
            select new ValidationResult($"Region {item.Region} assigned colour ({item.Colour}) outside its permitted colours.");

        ValidationResult? firstError = errorQuery.FirstOrDefault();

        return firstError ?? NoPairOfNeighbouringRegionsAssignedSameColour(solution);
    }

    private ValidationResult? NoPairOfNeighbouringRegionsAssignedSameColour(IReadOnlyDictionary<Region, Colour> solution)
    {
        IEnumerable<PairQueryItem> pairQuery = from p in NeighbourPairs
            let c1 = solution[p.First]
            let c2 = solution[p.Second]
            select new PairQueryItem(p.First, c1, p.Second, c2);

        IEnumerable<ValidationResult> errorQuery = from item in pairQuery
            where item.FirstColour == item.SecondColour
            select new ValidationResult($"Neighbouring regions {item.FirstRegion} and {item.SecondRegion} " +
                                        $"assigned same colour ({item.FirstColour}).");

        return errorQuery.FirstOrDefault(ValidationResult.Success);
    }

    private readonly record struct SingleQueryItem(Region Region, Colour Colour, IReadOnlyCollection<Colour> PermittedColours);

    private readonly record struct PairQueryItem(
        Region FirstRegion,
        Colour FirstColour,
        Region SecondRegion,
        Colour SecondColour);
}
