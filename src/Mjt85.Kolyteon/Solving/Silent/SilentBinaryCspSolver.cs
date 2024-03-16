using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Common;
using Mjt85.Kolyteon.Solving.Internals.Guards;
using Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;
using Mjt85.Kolyteon.Solving.Internals.SearchStrategies;

namespace Mjt85.Kolyteon.Solving.Silent;

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
