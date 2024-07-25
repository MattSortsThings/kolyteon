using System.Text.Json.Serialization;
using Kolyteon.Common;

namespace Kolyteon.GraphColouring;

/// <summary>
///     Immutable data structure containing information for a node in a Graph Colouring problem.
/// </summary>
[Serializable]
public sealed record NodeDatum : IComparable<NodeDatum>
{
    /// <summary>
    ///     Initializes a new <see cref="NodeDatum" /> instance with the specified <see cref="NodeDatum.Node" /> and
    ///     <see cref="PermittedColours" /> values.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="permittedColours">The set of permitted colours that may be assigned to the node.</param>
    /// <exception cref="ArgumentNullException"><paramref name="permittedColours" /> is <see langword="null" />.</exception>
    [JsonConstructor]
    public NodeDatum(Node node, IReadOnlyCollection<Colour> permittedColours)
    {
        Node = node;
        PermittedColours = permittedColours ?? throw new ArgumentNullException(nameof(permittedColours));
    }

    /// <summary>
    ///     Gets the node.
    /// </summary>
    public Node Node { get; }

    /// <summary>
    ///     Gets the set of permitted colours that may be assigned to the node.
    /// </summary>
    public IReadOnlyCollection<Colour> PermittedColours { get; }

    /// <summary>
    ///     Compares this <see cref="NodeDatum" /> instance with another instance of the same type and returns an integer that
    ///     indicates whether this instance precedes, follows, or occurs in the same position in the sort order as the other
    ///     instance.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="NodeDatum" /> instances are compared by their <see cref="NodeDatum.Node" /> values.
    /// </remarks>
    /// <param name="other">The <see cref="NodeDatum" /> instance against which this instance is to be compared.</param>
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
    public int CompareTo(NodeDatum? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        return other is null ? 1 : Node.CompareTo(other.Node);
    }

    /// <summary>
    ///     Indicates whether this <see cref="NodeDatum" /> instance has equal value to another instance of the same type.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="NodeDatum" /> instances have equal value if their <see cref="NodeDatum.Node" /> values are equal and
    ///     their <see cref="PermittedColours" /> collections contain the same values.
    /// </remarks>
    /// <param name="other">The <see cref="NodeDatum" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />. If the <paramref name="other" /> parameter is <see langword="null" />, the
    ///     method returns <see langword="false" />.
    /// </returns>
    public bool Equals(NodeDatum? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Node.Equals(other.Node)
               && PermittedColours.Count == other.PermittedColours.Count
               && PermittedColours.OrderBy(colour => colour).SequenceEqual(other.PermittedColours.OrderBy(colour => colour));
    }

    /// <summary>
    ///     Deconstructs this <see cref="NodeDatum" /> instance.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="permittedColours">The set of permitted colours that may be assigned to the node.</param>
    public void Deconstruct(out Node node, out IReadOnlyCollection<Colour> permittedColours)
    {
        node = Node;
        permittedColours = PermittedColours;
    }

    /// <summary>
    ///     Returns the hash code for this <see cref="NodeDatum" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Node, PermittedColours);
}
