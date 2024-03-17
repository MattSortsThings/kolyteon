using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Common;
using Mjt85.Kolyteon.Solving.Internals.Guards;
using Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;
using Mjt85.Kolyteon.Solving.Internals.SearchStrategies;

namespace Mjt85.Kolyteon.Solving.Silent;

/// <summary>
///     A generic binary CSP solver that silently and synchronously solves a binary CSP, and can be configured with a
///     solving algorithm composed of a search strategy and an ordering strategy,
/// </summary>
/// <remarks>
///     Use the fluent builder API, accessed via the <see cref="CreateBinaryCspSolver.WithInitialCapacity" /> static
///     method of the <see cref="CreateBinaryCspSolver" /> static class to build a
///     <see cref="SilentBinaryCspSolver{V,D}" /> instance.
/// </remarks>
/// <typeparam name="V">The binary CSP variable type.</typeparam>
/// <typeparam name="D">The binary CSP domain value type.</typeparam>
public sealed class SilentBinaryCspSolver<V, D> : BinaryCspSolver<V, D>, ISilentBinaryCspSolver<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    internal SilentBinaryCspSolver(ISearchStrategyFactory<V, D> searchStrategyFactory,
        IOrderingStrategyFactory orderingStrategyFactory,
        ISearchStrategy<V, D> searchStrategy,
        IOrderingStrategy orderingStrategy) :
        base(searchStrategyFactory, orderingStrategyFactory, searchStrategy, orderingStrategy)
    {
    }

    /// <inheritdoc />
    /// <remarks>
    ///     <para>
    ///         When this method is invoked, the solver instance runs its configured binary CSP solving algorithm on the
    ///         <paramref name="binaryCsp" /> parameter, builds a <see cref="Result{V,D}" /> object, clears and resets its
    ///         internal data structures, then returns the result.
    ///     </para>
    ///     <para>
    ///         If the solving operation is cancelled via the <paramref name="cancellationToken" /> parameter, the solver
    ///         instance halts the solving algorithm, clears and resets its internal data structures, then throws an
    ///         <see cref="OperationCanceledException" />.
    ///     </para>
    ///     <para>
    ///         This instance cannot solve more than one binary CSP at a time, as the solving algorithm requires the
    ///         population and manipulation of several data structures at each step.
    ///     </para>
    /// </remarks>
    public Result<V, D> Solve(ISolvableBinaryCsp<V, D> binaryCsp, CancellationToken cancellationToken = default)
    {
        _ = binaryCsp ?? throw new ArgumentNullException(nameof(binaryCsp));
        ThrowIfLocked();
        Guard.AgainstBinaryCspNotModellingProblem(binaryCsp);
        Lock();

        try
        {
            return TrySolve(binaryCsp, cancellationToken);
        }
        finally
        {
            Reset();
            Unlock();
        }
    }

    private Result<V, D> TrySolve(ISolvableBinaryCsp<V, D> binaryCsp, CancellationToken cancellationToken)
    {
        UpdateSearchState();
        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            switch (CurrentSearchState)
            {
                case SearchState.Safe:
                    VisitNode();

                    break;
                case SearchState.Unsafe:
                    Backtrack();

                    break;
                case SearchState.Initial:
                    Setup(binaryCsp);

                    break;
                case SearchState.Final:
                default:
                    return GetResult();
            }
        }
    }
}
