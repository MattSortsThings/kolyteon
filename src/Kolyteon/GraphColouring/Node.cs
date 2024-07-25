using System.Text.Json.Serialization;

namespace Kolyteon.GraphColouring;

/// <summary>
///     Represents a named node in a graph.
/// </summary>
[Serializable]
public readonly record struct Node : IComparable<Node>
{
    /// <summary>
    ///     Initializes a new <see cref="Node" /> instance with the <see cref="Name" /> value <c>"undefined"</c>.
    /// </summary>
    public Node() : this("undefined") { }

    [JsonConstructor]
    internal Node(string name)
    {
        Name = name;
    }

    /// <summary>
    ///     Gets the name of the node.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Compares this <see cref="Node" /> instance with another instance of the same type and returns an integer that
    ///     indicates whether this instance precedes, follows, or occurs in the same position in the sort order as the other
    ///     instance.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="Node" /> instances are compared by their <see cref="Name" /> values, using ordinal (case-sensitive)
    ///     string comparison rules.
    /// </remarks>
    /// <param name="other">The <see cref="Node" /> instance against which this instance is to be compared.</param>
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
    public int CompareTo(Node other) => StringComparer.Ordinal.Compare(Name, other.Name);

    /// <summary>
    ///     Indicates whether this <see cref="Node" /> instance has equal value to another instance of the same type.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="Node" /> instances have equal value if their <see cref="Name" /> values are equal, using ordinal
    ///     (case-sensitive) string comparison rules.
    /// </remarks>
    /// <param name="other">The <see cref="Node" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(Node other) => Name.Equals(other.Name, StringComparison.Ordinal);

    /// <summary>
    ///     Returns the hash code for this <see cref="Node" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => Name.GetHashCode(StringComparison.Ordinal);

    /// <summary>
    ///     Returns the string representation of this <see cref="Node" /> instance.
    /// </summary>
    /// <returns>A string representing this instance, in the format <c>"({Name})"</c>.</returns>
    public override string ToString() => $"({Name})";

    /// <summary>
    ///     Creates and returns a new <see cref="Node" /> instance with the specified <see cref="Name" /> value.
    /// </summary>
    /// <param name="name">The name of the node.</param>
    /// <returns>A new <see cref="Node" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="name" /> is <see langword="null" />.</exception>
    /// <exception cref="ArgumentException"><paramref name="name" /> is an empty string or composed entirely of white space.</exception>
    public static Node FromName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return new Node(name);
    }
}
