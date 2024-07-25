using System.Text.Json.Serialization;

namespace Kolyteon.GraphColouring;

/// <summary>
///     Represents an undirected edge between two different nodes in a graph.
/// </summary>
[Serializable]
public sealed record Edge : IComparable<Edge>
{
    [JsonConstructor]
    internal Edge(Node firstNode, Node secondNode)
    {
        FirstNode = firstNode;
        SecondNode = secondNode;
    }

    /// <summary>
    ///     Gets the first node of the adjacent pair, when the nodes are ordered using <see cref="Node" /> type comparison
    ///     rules.
    /// </summary>
    public Node FirstNode { get; }

    /// <summary>
    ///     Gets the second node of the adjacent pair, when the nodes are ordered using <see cref="Node" /> type comparison
    ///     rules.
    /// </summary>
    public Node SecondNode { get; }

    /// <summary>
    ///     Compares this <see cref="Edge" /> instance with another instance of the same type and returns an integer that
    ///     indicates whether this instance precedes, follows, or occurs in the same position in the sort order as the other
    ///     instance.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="Edge" /> instances are compared by their <see cref="FirstNode" /> values, then by their
    ///     <see cref="SecondNode" /> values.
    /// </remarks>
    /// <param name="other">The <see cref="Edge" /> instance against which this instance is to be compared.</param>
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
    public int CompareTo(Edge? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        if (other is null)
        {
            return 1;
        }

        int firstNodeComparison = FirstNode.CompareTo(other.FirstNode);

        return firstNodeComparison != 0 ? firstNodeComparison : SecondNode.CompareTo(other.SecondNode);
    }

    /// <summary>
    ///     Indicates whether this <see cref="Edge" /> instance has equal value to another instance of the same type.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="Edge" /> instances have equal value if their <see cref="FirstNode" /> values are equal and their
    ///     <see cref="SecondNode" /> values are equal.
    /// </remarks>
    /// <param name="other">The <see cref="Edge" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />. If the <paramref name="other" /> parameter is <see langword="null" />, the
    ///     method returns <see langword="false" />.
    /// </returns>
    public bool Equals(Edge? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return FirstNode.Equals(other.FirstNode) && SecondNode.Equals(other.SecondNode);
    }

    /// <summary>
    ///     Deconstructs this <see cref="Edge" /> instance.
    /// </summary>
    /// <param name="firstNode">The first of the two adjacent nodes.</param>
    /// <param name="secondNode">The second of the two adjacent nodes.</param>
    public void Deconstruct(out Node firstNode, out Node secondNode)
    {
        firstNode = FirstNode;
        secondNode = SecondNode;
    }

    /// <summary>
    ///     Returns the string representation of this <see cref="Edge" /> instance.
    /// </summary>
    /// <returns>A string representing this instance, in the format <c>"{FirstNode}-{SecondNode}"</c>.</returns>
    public override string ToString() => $"{FirstNode}-{SecondNode}";

    /// <summary>
    ///     Returns the hash code for this <see cref="Node" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(FirstNode, SecondNode);

    /// <summary>
    ///     Creates and returns a new <see cref="Edge" /> instance between the two specified nodes.
    /// </summary>
    /// <remarks>
    ///     The two adjacent nodes must be different. The arguments can be passed in either order. The method sorts the
    ///     arguments so that the new instance's <see cref="FirstNode" /> value always precedes its <see cref="SecondNode" />
    ///     value.
    /// </remarks>
    /// <param name="nodeA">One of the two adjacent nodes.</param>
    /// <param name="nodeB">The other of the two adjacent nodes.</param>
    /// <returns>A new <see cref="Edge" /> instance.</returns>
    /// <exception cref="ArgumentException"><paramref name="nodeA" /> is equal to <paramref name="nodeB" />.</exception>
    public static Edge Between(Node nodeA, Node nodeB)
    {
        int comparison = nodeA.CompareTo(nodeB);

        return comparison switch
        {
            < 0 => new Edge(nodeA, nodeB),
            > 0 => new Edge(nodeB, nodeA),
            _ => throw new ArgumentException("Nodes must be different.")
        };
    }
}
