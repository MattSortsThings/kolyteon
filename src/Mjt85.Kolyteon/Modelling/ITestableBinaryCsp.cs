namespace Mjt85.Kolyteon.Modelling;

/// <summary>
///     Defines methods to verify that a binary CSP has modelled a given problem correctly.
/// </summary>
/// <typeparam name="P">The modelled problem type.</typeparam>
/// <typeparam name="V">The binary CSP variable type.</typeparam>
/// <typeparam name="D">The binary CSP domain value type.</typeparam>
public interface ITestableBinaryCsp<in P, V, D> : IModellingBinaryCsp<P, V, D>
    where P : class
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    /// <summary>
    ///     Iterates through all the binary CSP variables in variable index order.
    /// </summary>
    /// <returns>
    ///     A sequence of instances of type <typeparamref name="V" />. The binary CSP variables. Returns an empty sequence
    ///     if this instance is not modelling a problem.
    /// </returns>
    public IEnumerable<V> GetAllVariables();

    /// <summary>
    ///     Iterates through all the binary CSP domains in variable index order.
    /// </summary>
    /// <returns>
    ///     A sequence of read-only lists of generic type <typeparamref name="D" />. The binary CSP domains. Returns an empty
    ///     sequence if this instance is not modelling a problem.
    /// </returns>
    public IEnumerable<IReadOnlyList<D>> GetAllDomains();

    /// <summary>
    ///     Iterates through all the pairs of adjacent binary CSP variables in variable index order.
    /// </summary>
    /// <remarks>
    ///     The returned sequence contains one <see cref="Pair{T}" /> instance for every binary constraint. Specifically, the
    ///     sequence contains a <see cref="Pair{T}" /> for every pair of adjacent variables at index <i>i</i> and at index
    ///     <i>j</i>, for all <i>i</i> &#8712; [0,<i>n</i>), for all <i>j</i> &#8712; (<i>i</i>,<i>n</i>), where <i>i</i> is
    ///     the number of binary CSP variables.
    /// </remarks>
    /// <returns>
    ///     A sequence of <see cref="Pair{T}" /> instances of generic type <typeparamref name="V" />. The pairs of
    ///     adjacent binary CSP variables. Returns an empty sequence if the binary CSP has no constraints, or if this instance
    ///     is not modelling a problem.
    /// </returns>
    public IEnumerable<Pair<V>> GetAllAdjacentVariables();
}
