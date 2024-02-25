using Mjt85.Kolyteon.Solving.OrderingStrategies;
using Mjt85.Kolyteon.Solving.SearchStrategies;

namespace Mjt85.Kolyteon.Solving.Builders;

internal sealed class VerboseBinaryCspSolverBuilder<V, D> : IVerboseBinaryCspSolverBuilder<V, D>,
    IVerboseBinaryCspSolverBuilder<V, D>.IStepDelaySetter,
    IVerboseBinaryCspSolverBuilder<V, D>.ISearchStrategySetter,
    IVerboseBinaryCspSolverBuilder<V, D>.IOrderingStrategySetter,
    IVerboseBinaryCspSolverBuilder<V, D>.ITerminal
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    private int _capacity;
    private Ordering _orderingStrategy;
    private Search _searchStrategy;
    private TimeSpan _stepDelay;


    public IVerboseBinaryCspSolverBuilder<V, D>.IStepDelaySetter WithInitialCapacity(int capacity)
    {
        _capacity = capacity >= 0
            ? capacity
            : throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Value must not be negative.");

        return this;
    }

    public IVerboseBinaryCspSolverBuilder<V, D>.ITerminal AndInitialOrderingStrategy(Ordering strategy)
    {
        _orderingStrategy = strategy;

        return this;
    }

    public IVerboseBinaryCspSolverBuilder<V, D>.IOrderingStrategySetter AndInitialSearchStrategy(Search strategy)
    {
        _searchStrategy = strategy;

        return this;
    }

    public IVerboseBinaryCspSolverBuilder<V, D>.ISearchStrategySetter AndInitialStepDelay(TimeSpan stepDelay)
    {
        _stepDelay = stepDelay;

        return this;
    }

    public VerboseBinaryCspSolver<V, D> Build()
    {
        SearchStrategyFactory<V, D> searchStrategyFactory = new();
        OrderingStrategyFactory orderingStrategyFactory = new();
        ISearchStrategy<V, D> searchStrategy = searchStrategyFactory.CreateInstance(_searchStrategy, _capacity);
        IOrderingStrategy orderingStrategy = orderingStrategyFactory.CreateInstance(_orderingStrategy);

        return new VerboseBinaryCspSolver<V, D>(searchStrategyFactory,
            orderingStrategyFactory,
            searchStrategy,
            orderingStrategy,
            _stepDelay);
    }
}
