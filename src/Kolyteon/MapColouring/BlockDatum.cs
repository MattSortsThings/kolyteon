using System.Text.Json.Serialization;
using Kolyteon.Common;

namespace Kolyteon.MapColouring;

/// <summary>
///     Immutable data structure containing information for a block in a Map Colouring problem.
/// </summary>
[Serializable]
public sealed record BlockDatum : IComparable<BlockDatum>
{
    /// <summary>
    ///     Initializes a new <see cref="BlockDatum" /> instance with the specified <see cref="BlockDatum.Block" /> and
    ///     <see cref="PermittedColours" /> values.
    /// </summary>
    /// <param name="block">The block.</param>
    /// <param name="permittedColours">The set of permitted colours that may be assigned to the block.</param>
    /// <exception cref="ArgumentNullException"><paramref name="permittedColours" /> is <see langword="null" />.</exception>
    [JsonConstructor]
    public BlockDatum(Block block, IReadOnlyCollection<Colour> permittedColours)
    {
        Block = block;
        PermittedColours = permittedColours ?? throw new ArgumentNullException(nameof(permittedColours));
    }

    /// <summary>
    ///     Gets the block.
    /// </summary>
    public Block Block { get; }

    /// <summary>
    ///     Gets the set of permitted colours that may be assigned to the block.
    /// </summary>
    public IReadOnlyCollection<Colour> PermittedColours { get; }

    /// <summary>
    ///     Compares this <see cref="BlockDatum" /> instance with another instance of the same type and returns an integer that
    ///     indicates whether this instance precedes, follows, or occurs in the same position in the sort order as the other
    ///     instance.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="BlockDatum" /> instances are compared by their <see cref="Block" /> values.
    /// </remarks>
    /// <param name="other">The <see cref="BlockDatum" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     A value that indicates the relative order of the objects being compared. The return value has these meanings:
    ///     <list type="table">
    ///         <listheader>
    ///             <term>Value</term><description>Meaning</description>
    ///         </listheader>
    ///         <item>
    ///             <term>Less than zero</term>
    ///             <description>This instance precedes <paramref name="other" /> in the sort order.</description>
    ///         </item>
    ///         <item>
    ///             <term>Zero</term>
    ///             <description>
    ///                 This instance occurs in the same position in the sort order as <paramref name="other" />.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>Greater than zero</term>
    ///             <description>This instance follows <paramref name="other" /> in the sort order.</description>
    ///         </item>
    ///     </list>
    /// </returns>
    public int CompareTo(BlockDatum? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        return other is null ? 1 : Block.CompareTo(other.Block);
    }

    /// <summary>
    ///     Indicates whether this <see cref="BlockDatum" /> instance has equal value to another instance of the same type.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="BlockDatum" /> instances have equal value if their <see cref="Block" /> values are equal and their
    ///     <see cref="PermittedColours" /> collections contain the same values.
    /// </remarks>
    /// <param name="other">The <see cref="BlockDatum" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />. If the <paramref name="other" /> parameter is <see langword="null" />, the
    ///     method returns <see langword="false" />.
    /// </returns>
    public bool Equals(BlockDatum? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Block.Equals(other.Block)
               && PermittedColours.Count == other.PermittedColours.Count
               && PermittedColours.OrderBy(colour => colour).SequenceEqual(other.PermittedColours.OrderBy(colour => colour));
    }

    /// <summary>
    ///     Deconstructs this <see cref="BlockDatum" /> instance.
    /// </summary>
    /// <param name="block">The block.</param>
    /// <param name="permittedColours">The set of permitted colours that may be assigned to the block.</param>
    public void Deconstruct(out Block block, out IReadOnlyCollection<Colour> permittedColours)
    {
        block = Block;
        permittedColours = PermittedColours;
    }

    /// <summary>
    ///     Returns the hash code for this <see cref="BlockDatum" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Block, PermittedColours);
}
