using Kolyteon.Modelling;

namespace Kolyteon.Solving;

/// <summary>
///     A configurable, synchronous, generic binary CSP solver.
/// </summary>
/// <typeparam name="TVariable">The binary CSP variable type.</typeparam>
/// <typeparam name="TDomainValue">The binary CSP domain value type.</typeparam>
public interface IBinaryCspSolver<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    /// <summary>
    ///     Gets or sets the checking strategy component of the backtracking search algorithm used by this instance.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     The value of <see cref="CheckingStrategy" /> is set while a solving operation is in progress.
    /// </exception>
    public CheckingStrategy CheckingStrategy { get; set; }

    /// <summary>
    ///     Gets or sets the ordering strategy component of the backtracking search algorithm used by this instance.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     The value of <see cref="OrderingStrategy" /> is set while a solving operation is in progress.
    /// </exception>
    public OrderingStrategy OrderingStrategy { get; set; }

    /// <summary>
    ///     Applies the configured backtracking search algorithm to the specified binary CSP and returns the solution that was
    ///     reached (if a solution exists) with additional execution metrics.
    /// </summary>
    /// <param name="binaryCsp">The binary CSP modelling a problem, to be solved.</param>
    /// <param name="cancellationToken">Cancels the solving operation.</param>
    /// <returns>
    ///     A new <see cref="SolvingResult{TVariable,TDomainValue}" /> instance containing the solution that was reached
    ///     (if a solution exists) and backtracking search algorithm execution metrics.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="binaryCsp" /> is <see langword="null" />.</exception>
    /// <exception cref="ArgumentException"><paramref name="binaryCsp" /> is not modelling a problem.</exception>
    /// <exception cref="OperationCanceledException">
    ///     The solving operation is cancelled using the <paramref name="cancellationToken" /> parameter.
    /// </exception>
    public SolvingResult<TVariable, TDomainValue> Solve(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp,
        CancellationToken cancellationToken = default);
}
