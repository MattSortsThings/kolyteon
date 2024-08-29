using Kolyteon.Modelling;

namespace Kolyteon.Solving;

/// <summary>
///     A verbose, asynchronous, configurable generic binary CSP solver.
/// </summary>
/// <typeparam name="TVariable">The binary CSP variable type.</typeparam>
/// <typeparam name="TDomainValue">The binary CSP domain value type.</typeparam>
public interface IVerboseBinaryCspSolver<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    /// <summary>
    ///     Gets the search algorithm being used by this instance.
    /// </summary>
    public SearchAlgorithm SearchAlgorithm { get; }

    /// <summary>
    ///     Gets or sets the delay between each step of the backtracking search algorithm.
    /// </summary>
    public TimeSpan StepDelay { get; set; }

    /// <summary>
    ///     Reconfigures this instance to use the specified backtracking search algorithm if supplied, then applies the
    ///     configured backtracking search algorithm to the specified binary CSP, notifying the progress object after each
    ///     step, and returns the result.
    /// </summary>
    /// <remarks>
    ///     If the optional <paramref name="searchAlgorithm" /> parameter is specified, this instance reconfigures itself
    ///     to use the specified search algorithm for the present and future solving operations. The value of the
    ///     <see cref="IVerboseBinaryCspSolver{TVariable,TDomainValue}.SearchAlgorithm" /> property is updated.
    /// </remarks>
    /// <param name="binaryCsp">The binary CSP modelling a problem, to be solved.</param>
    /// <param name="progress">Provides updates for solving step notifications.</param>
    /// <param name="searchAlgorithm">Optionally specifies the backtracking search algorithm to be used.</param>
    /// <param name="cancellationToken">Cancels the solving operation.</param>
    /// <returns>
    ///     A new <see cref="SolvingResult{TVariable,TDomainValue}" /> instance containing the solution that was reached
    ///     (if a solution exists) and backtracking search algorithm execution metrics.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="binaryCsp" /> is <see langword="null" />, or <paramref name="progress" /> is
    ///     <see langword="null" />.
    /// </exception>
    /// <exception cref="ArgumentException"><paramref name="binaryCsp" /> is not modelling a problem.</exception>
    /// <exception cref="OperationCanceledException">
    ///     The solving operation is cancelled using the <paramref name="cancellationToken" /> parameter.
    /// </exception>
    public Task<SolvingResult<TVariable, TDomainValue>> SolveAsync(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp,
        ISolvingProgress<TVariable, TDomainValue> progress,
        SearchAlgorithm? searchAlgorithm = default,
        CancellationToken cancellationToken = default);
}
