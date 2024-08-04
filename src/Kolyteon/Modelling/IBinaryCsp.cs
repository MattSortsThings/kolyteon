namespace Kolyteon.Modelling;

/// <summary>
///     A mutable data structure that can model any instance of a problem type as a generic binary CSP.
/// </summary>
/// <typeparam name="TVariable">The binary CSP variable type.</typeparam>
/// <typeparam name="TDomainValue">The binary CSP domain value type.</typeparam>
/// <typeparam name="TProblem">The problem type.</typeparam>
public interface IBinaryCsp<TVariable, TDomainValue, in TProblem> : IReadOnlyBinaryCsp<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
    where TProblem : class
{
    /// <summary>
    ///     Populates this instance's internal data structures so that it is modelling a binary CSP that represents the
    ///     specified problem.
    /// </summary>
    /// <remarks>
    ///     If this instance is already modelling a problem, it must be cleared by invoking the <see cref="Clear" />
    ///     method before the <see cref="Model" /> method can be invoked with a new problem.
    /// </remarks>
    /// <param name="problem">The problem to be modelled.</param>
    /// <exception cref="ArgumentNullException"><paramref name="problem" /> is <see langword="null" />.</exception>
    /// <exception cref="InvalidOperationException">This instance is already modelling a problem.</exception>
    public void Model(TProblem problem);

    /// <summary>
    ///     Clears this instance's internal data structures so that it is ready to model another problem as a binary CSP.
    /// </summary>
    public void Clear();
}
