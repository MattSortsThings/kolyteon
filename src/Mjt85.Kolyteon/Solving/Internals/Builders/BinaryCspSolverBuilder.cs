using Mjt85.Kolyteon.Solving.Common;
using Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;
using Mjt85.Kolyteon.Solving.Internals.SearchStrategies;
using Mjt85.Kolyteon.Solving.Silent;
using Mjt85.Kolyteon.Solving.Verbose;

namespace Mjt85.Kolyteon.Solving.Internals.Builders;

internal sealed class BinaryCspSolverBuilder : IBinaryCspSolverBuilder.ISearchStrategySetter,
    IBinaryCspSolverBuilder.IOrderingStrategySetter,
    IBinaryCspSolverBuilder.IModeSetter,
    IBinaryCspSolverBuilder.ISilentTerminal,
    IBinaryCspSolverBuilder.IVerboseStepDelaySetter,
    IBinaryCspSolverBuilder.IVerboseTerminal
{
    private readonly int _capacity;
    private Ordering _orderingStrategy;
    private Search _searchStrategy;
    private TimeSpan _stepDelay;

    internal BinaryCspSolverBuilder(int capacity)
    {
        _capacity = capacity;
    }

    public IBinaryCspSolverBuilder.ISilentTerminal Silent() => this;

    public IBinaryCspSolverBuilder.IVerboseStepDelaySetter Verbose() => this;

    public IBinaryCspSolverBuilder.IModeSetter AndInitialOrderingStrategy(Ordering strategy)
    {
        _orderingStrategy = strategy;

        return this;
    }

    public IBinaryCspSolverBuilder.IOrderingStrategySetter AndInitialSearchStrategy(Search strategy)
    {
        _searchStrategy = strategy;

        return this;
    }

    SilentBinaryCspSolver<V, D> IBinaryCspSolverBuilder.ISilentTerminal.Build<V, D>()
    {
        SearchStrategyFactory<V, D> searchStrategyFactory = new();
        OrderingStrategyFactory orderingStrategyFactory = new();
        ISearchStrategy<V, D> searchStrategy = searchStrategyFactory.CreateInstance(_searchStrategy, _capacity);
        IOrderingStrategy orderingStrategy = orderingStrategyFactory.CreateInstance(_orderingStrategy);

        return new SilentBinaryCspSolver<V, D>(searchStrategyFactory,
            orderingStrategyFactory,
            searchStrategy,
            orderingStrategy);
    }

    public IBinaryCspSolverBuilder.IVerboseTerminal WithInitialStepDelay(TimeSpan delay)
    {
        _stepDelay = delay;

        return this;
    }

    VerboseBinaryCspSolver<V, D> IBinaryCspSolverBuilder.IVerboseTerminal.Build<V, D>()
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
