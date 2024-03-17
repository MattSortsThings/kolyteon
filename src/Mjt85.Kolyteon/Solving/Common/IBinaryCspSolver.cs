namespace Mjt85.Kolyteon.Solving.Common;

/// <summary>
///     Represents a generic binary CSP solver that can be configured with a solving algorithm composed of a search
///     strategy and an ordering strategy.
/// </summary>
/// <typeparam name="V">The binary CSP variable type.</typeparam>
/// <typeparam name="D">The binary CSP domain value type.</typeparam>
public interface IBinaryCspSolver<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    /// <summary>
    ///     Gets or sets the search strategy component of the solving algorithm used by this binary CSP solver instance.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     The value of <see cref="SearchStrategy" /> is set while this binary CSP solver instance is currently solving a
    ///     binary CSP.
    /// </exception>
    public Search SearchStrategy { get; set; }

    /// <summary>
    ///     Gets or sets the ordering strategy component of the solving algorithm used by this binary CSP solver instance.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     The value of <see cref="OrderingStrategy" /> is set while this binary CSP solver instance is currently solving a
    ///     binary CSP.
    /// </exception>
    public Ordering OrderingStrategy { get; set; }
}
