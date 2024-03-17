namespace Mjt85.Kolyteon.Modelling;

/// <summary>
///     Defines methods for a reusable generic binary CSP that can model any instance of its defined problem type.
/// </summary>
/// <typeparam name="P">The modelled problem type.</typeparam>
/// <typeparam name="V">The binary CSP variable type.</typeparam>
/// <typeparam name="D">The binary CSP domain value type.</typeparam>
public interface IModellingBinaryCsp<in P, V, D> : ISolvableBinaryCsp<V, D>
    where P : class
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    /// <summary>
    ///     Models the specified problem as a binary CSP.
    /// </summary>
    /// <remarks>
    ///     If this instance is already modelling a problem, it must be cleared using the <see cref="Clear" /> method
    ///     before another problem may be modelled.
    /// </remarks>
    /// <param name="problem">The problem to be modelled.</param>
    /// <exception cref="ArgumentNullException"><paramref name="problem" /> is <c>null</c>.</exception>
    /// <exception cref="InvalidOperationException">
    ///     This instance is already modelling a problem, or, this instance has zero
    ///     binary CSP variables when modelling the <paramref name="problem" /> parameter.
    /// </exception>
    public void Model(P problem);

    /// <summary>
    ///     Clears this instance so that it can model another problem.
    /// </summary>
    public void Clear();
}
