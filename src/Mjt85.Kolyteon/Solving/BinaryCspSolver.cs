using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Builders;
using Mjt85.Kolyteon.Solving.Guards;
using Mjt85.Kolyteon.Solving.OrderingStrategies;
using Mjt85.Kolyteon.Solving.SearchStrategies;

namespace Mjt85.Kolyteon.Solving;

public sealed class BinaryCspSolver<V, D> : CoreBinaryCspSolver<V, D>, IBinaryCspSolver<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    internal BinaryCspSolver(ISearchStrategyFactory<V, D> searchStrategyFactory,
        IOrderingStrategyFactory orderingStrategyFactory,
        ISearchStrategy<V, D> searchStrategy,
        IOrderingStrategy orderingStrategy) :
        base(searchStrategyFactory, orderingStrategyFactory, searchStrategy, orderingStrategy)
    {
    }

    public Result<V, D> Solve(ISolvableBinaryCsp<V, D> binaryCsp, CancellationToken cancellationToken = default)
    {
        _ = binaryCsp ?? throw new ArgumentNullException(nameof(binaryCsp));
        Guard.AgainstBinaryCspNotModellingProblem(binaryCsp);

        try
        {
            return TrySolve(binaryCsp, cancellationToken);
        }
        finally
        {
            Reset();
        }
    }

    public static IBinaryCspSolverBuilder<V, D> Create() => new BinaryCspSolverBuilder<V, D>();

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
