using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Common;

namespace Mjt85.Kolyteon.Solving.Verbose;

/// <summary>
///     Represents a generic binary CSP solver that solves a binary CSP with progress reporting, and can be configured with
///     a step delay and a solving algorithm composed of a search strategy and an ordering strategy,
/// </summary>
/// <typeparam name="V">The binary CSP variable type.</typeparam>
/// <typeparam name="D">The binary CSP domain value type.</typeparam>
public interface IVerboseBinaryCspSolver<V, D> : IBinaryCspSolver<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    /// <summary>
    ///     Gets or sets the time delay inserted between binary CSP solving algorithm steps.
    /// </summary>
    /// <remarks>Use this value to slow down the verbose binary CSP solver artificially.</remarks>
    /// <value>A <see cref="TimeSpan" /> value. The time delay inserted between binary CSP solving algorithm steps.</value>
    /// <exception cref="InvalidOperationException">
    ///     The value of <see cref="StepDelay" /> is set while this binary CSP solver instance is currently solving a binary
    ///     CSP.
    /// </exception>
    public TimeSpan StepDelay { get; set; }

    /// <summary>
    ///     Asynchronously solves the binary CSP using the current solving algorithm, notifying the specified progress object
    ///     and imposing a step delay after every step, and returns the result.
    /// </summary>
    /// <param name="binaryCsp">The binary CSP to be solved.</param>
    /// <param name="progress">Reports the progress of the solving operation after every step.</param>
    /// <param name="cancellationToken">Cancels the solving operation.</param>
    /// <returns>
    ///     A new <see cref="Result{V,D}" /> instance containing the assignments that were found by the solving algorithm,
    ///     with performance metrics.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="binaryCsp" /> is <c>null</c>; or, <paramref name="progress" />
    ///     is <c>null</c>.
    /// </exception>
    /// <exception cref="InvalidOperationException">This instance is currently solving another binary CSP.</exception>
    /// <exception cref="ArgumentException">
    ///     The <paramref name="binaryCsp" /> parameter is not currently modelling a problem (i.e. its
    ///     <see cref="IMeasurableBinaryCsp.Variables" /> value is 0).
    /// </exception>
    /// <exception cref="OperationCanceledException">
    ///     The solving operation is cancelled using the <paramref name="cancellationToken" /> parameter.
    /// </exception>
    public Task<Result<V, D>> SolveAsync(ISolvableBinaryCsp<V, D> binaryCsp,
        IProgress<StepNotification<V, D>> progress,
        CancellationToken cancellationToken = default);
}
