using Mjt85.Kolyteon.Solving.OrderingStrategies;
using Mjt85.Kolyteon.Solving.SearchStrategies;

namespace Mjt85.Kolyteon.Solving.Builders;

internal sealed class BinaryCspSolverBuilder<V, D> : IBinaryCspSolverBuilder<V, D>,
    IBinaryCspSolverBuilder<V, D>.ISearchStrategySetter,
    IBinaryCspSolverBuilder<V, D>.IOrderingStrategySetter,
    IBinaryCspSolverBuilder<V, D>.ITerminal
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    private int _capacity;
    private Ordering _ordering;
    private Search _search;

    public IBinaryCspSolverBuilder<V, D>.ISearchStrategySetter WithInitialCapacity(int capacity)
    {
        _capacity = capacity >= 0
            ? capacity
            : throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Value must not be negative.");

        return this;
    }

    public IBinaryCspSolverBuilder<V, D>.ITerminal AndInitialOrderingStrategy(Ordering strategy)
    {
        _ordering = strategy;

        return this;
    }

    public IBinaryCspSolverBuilder<V, D>.IOrderingStrategySetter AndInitialSearchStrategy(Search strategy)
    {
        _search = strategy;

        return this;
    }

    public BinaryCspSolver<V, D> Build()
    {
        SearchStrategyFactory<V, D> searchStrategyFactory = new();
        OrderingStrategyFactory orderingStrategyFactory = new();
        ISearchStrategy<V, D> searchStrategy = searchStrategyFactory.CreateInstance(_search, _capacity);
        IOrderingStrategy orderingStrategy = orderingStrategyFactory.CreateInstance(_ordering);

        return new BinaryCspSolver<V, D>(searchStrategyFactory, orderingStrategyFactory, searchStrategy, orderingStrategy);
    }
}
