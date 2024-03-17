using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Common;

namespace Mjt85.Kolyteon.Solving.Silent;

/// <summary>
///     Represents a generic binary CSP solver that silently solves a binary CSP, and can be configured with a solving
///     algorithm composed of a search strategy and an ordering strategy,
/// </summary>
/// <typeparam name="V">The binary CSP variable type.</typeparam>
/// <typeparam name="D">The binary CSP domain value type.</typeparam>
public interface ISilentBinaryCspSolver<V, D> : IBinaryCspSolver<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    /// <summary>
    ///     Synchronously solves the binary CSP using the current solving algorithm and returns the result.
    /// </summary>
    /// <param name="binaryCsp">The binary CSP to be solved.</param>
    /// <param name="cancellationToken">Cancels the solving operation.</param>
    /// <returns>
    ///     A new <see cref="Result{V,D}" /> instance containing the assignments that were found by the solving algorithm,
    ///     with performance metrics.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="binaryCsp" /> is <c>null</c>.</exception>
    /// <exception cref="InvalidOperationException">This instance is currently solving another binary CSP.</exception>
    /// <exception cref="ArgumentException">
    ///     The <paramref name="binaryCsp" /> parameter is not currently modelling a problem (i.e. its
    ///     <see cref="IMeasurableBinaryCsp.Variables" /> value is 0).
    /// </exception>
    /// <exception cref="OperationCanceledException">
    ///     The solving operation is cancelled using the <paramref name="cancellationToken" /> parameter.
    /// </exception>
    public Result<V, D> Solve(ISolvableBinaryCsp<V, D> binaryCsp, CancellationToken cancellationToken = default);
}
