using System.Diagnostics.CodeAnalysis;
using Kolyteon.Common;
using Kolyteon.Modelling;

namespace Kolyteon.MapColouring;

/// <summary>
///     Models a <see cref="MapColouringProblem" /> instance as a generic binary CSP.
/// </summary>
public sealed class MapColouringConstraintGraph : ConstraintGraph<Block, Colour, MapColouringProblem>
{
    private readonly Dictionary<Block, IReadOnlyCollection<Colour>> _blocksAndPermittedColours;

    /// <summary>
    ///     Initializes a new <see cref="MapColouringConstraintGraph" /> instance with a default initial
    ///     <see cref="ConstraintGraph{TVariable,TDomainValue,TProblem}.Capacity" /> of 0.
    /// </summary>
    public MapColouringConstraintGraph()
    {
        _blocksAndPermittedColours = [];
    }

    /// <summary>
    ///     Initializes a new <see cref="MapColouringConstraintGraph" /> instance with the specified initial
    ///     <see cref="ConstraintGraph{TVariable,TDomainValue,TProblem}.Capacity" />.
    /// </summary>
    /// <param name="capacity">
    ///     A non-negative integer. The initial capacity of the <see cref="MapColouringConstraintGraph" /> instance.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity" /> is negative.</exception>
    public MapColouringConstraintGraph(int capacity) : base(capacity)
    {
        _blocksAndPermittedColours = new Dictionary<Block, IReadOnlyCollection<Colour>>(capacity);
    }

    /// <summary>
    ///     Gets or sets the total number of binary CSP variables the internal data structures of this instance can hold
    ///     without resizing.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <see cref="Capacity" /> is set to a value that is less than the value of
    ///     <see cref="ConstraintGraph{TVariable,TDomainValue,TProblem}.Variables" />.
    /// </exception>
    public override int Capacity
    {
        get => base.Capacity;
        set
        {
            base.Capacity = value;
            _blocksAndPermittedColours.TrimExcess(value);
        }
    }

    /// <summary>
    ///     Creates and returns a new <see cref="MapColouringConstraintGraph" /> instance that is modelling the specified
    ///     Map Colouring problem as a binary CSP.
    /// </summary>
    /// <param name="problem">The problem to be modelled.</param>
    /// <returns>A new <see cref="MapColouringConstraintGraph" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="problem" /> is <see langword="null" />.</exception>
    public static MapColouringConstraintGraph ModellingProblem(MapColouringProblem problem)
    {
        ArgumentNullException.ThrowIfNull(problem);

        MapColouringConstraintGraph constraintGraph = new(problem.BlockData.Count);
        constraintGraph.Model(problem);

        return constraintGraph;
    }

    protected override void PopulateProblemData(MapColouringProblem problem)
    {
        _blocksAndPermittedColours.EnsureCapacity(problem.BlockData.Count);

        foreach ((Block block, IReadOnlyCollection<Colour> permittedColours) in problem.BlockData)
        {
            _blocksAndPermittedColours.Add(block, permittedColours);
        }
    }

    protected override IEnumerable<Block> GetVariables() => _blocksAndPermittedColours.Keys;

    protected override IEnumerable<Colour> GetDomainValues(Block presentVariable) =>
        _blocksAndPermittedColours[presentVariable];

    protected override bool TryGetBinaryPredicate(Block firstVariable,
        Block secondVariable,
        [NotNullWhen(true)] out Func<Colour, Colour, bool>? binaryPredicate)
    {
        binaryPredicate = firstVariable.AdjacentTo(in secondVariable) ? DifferentColours : null;

        return binaryPredicate is not null;
    }

    protected override void ClearProblemData() => _blocksAndPermittedColours.Clear();

    private static bool DifferentColours(Colour firstColour, Colour secondColour) => firstColour != secondColour;
}
