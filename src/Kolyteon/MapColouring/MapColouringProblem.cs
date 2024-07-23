using System.Text.Json.Serialization;
using Kolyteon.Common;
using Kolyteon.MapColouring.Internals;

namespace Kolyteon.MapColouring;

/// <summary>
///     Represents a valid (but not necessarily solvable) Map Colouring problem.
/// </summary>
[Serializable]
public sealed record MapColouringProblem
{
    [JsonConstructor]
    internal MapColouringProblem(Block canvas, IReadOnlyList<BlockDatum> blockData)
    {
        Canvas = canvas;
        BlockData = blockData;
    }

    /// <summary>
    ///     Gets a <see cref="Block" /> representing the canvas dimensions for the problem.
    /// </summary>
    public Block Canvas { get; }

    /// <summary>
    ///     Gets an immutable list of all block data for the problem.
    /// </summary>
    public IReadOnlyList<BlockDatum> BlockData { get; }

    /// <summary>
    ///     Indicates whether this <see cref="MapColouringProblem" /> instance has equal value to another instance of the same
    ///     type, that is, they represent logically identical Map Colouring problems.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="MapColouringProblem" /> instances have equal value if their <see cref="Canvas" /> values are equal
    ///     and their <see cref="BlockData" /> collections contain equal values.
    /// </remarks>
    /// <param name="other">The <see cref="MapColouringProblem" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />. If the <paramref name="other" /> parameter is <see langword="null" />, the
    ///     method returns <see langword="false" />.
    /// </returns>
    public bool Equals(MapColouringProblem? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Canvas.Equals(other.Canvas)
               && BlockData.Count == other.BlockData.Count
               && BlockData.OrderBy(datum => datum).SequenceEqual(other.BlockData.OrderBy(datum => datum));
    }

    /// <summary>
    ///     Deconstructs this <see cref="MapColouringProblem" /> instance.
    /// </summary>
    /// <param name="canvas">Represents the canvas dimensions for the problem.</param>
    /// <param name="blockData">Contains block data for the problem.</param>
    public void Deconstruct(out Block canvas, out IReadOnlyList<BlockDatum> blockData)
    {
        canvas = Canvas;
        blockData = BlockData;
    }

    /// <summary>
    ///     Returns the hash code for this <see cref="MapColouringProblem" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Canvas, BlockData);

    /// <summary>
    ///     Starts the process of building a new <see cref="MapColouringProblem" /> using the fluent builder API.
    /// </summary>
    /// <returns>A new fluent builder instance, to which method invocations can be chained.</returns>
    public static IMapColouringProblemBuilder Create() => new MapColouringProblemBuilder();
}
