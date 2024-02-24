using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Builders;
using Mjt85.Kolyteon.Solving.Guards;
using Mjt85.Kolyteon.Solving.OrderingStrategies;
using Mjt85.Kolyteon.Solving.SearchStrategies;
using Mjt85.Kolyteon.Solving.SearchTrees;

namespace Mjt85.Kolyteon.Solving;

public sealed class BinaryCspSolver<V, D> : IBinaryCspSolver<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    private readonly IOrderingStrategyFactory _orderingStrategyFactory;
    private readonly ISearchStrategyFactory<V, D> _searchStrategyFactory;
    private long _backtrackingSteps;
    private IOrderingStrategy _orderingStrategy;
    private ISearchStrategy<V, D> _searchStrategy;
    private long _setupSteps;
    private long _visitingSteps;

    internal BinaryCspSolver(ISearchStrategyFactory<V, D> searchStrategyFactory,
        IOrderingStrategyFactory orderingStrategyFactory,
        ISearchStrategy<V, D> searchStrategy, IOrderingStrategy orderingStrategy)
    {
        _searchStrategyFactory = searchStrategyFactory ?? throw new ArgumentNullException(nameof(searchStrategyFactory));
        _orderingStrategyFactory = orderingStrategyFactory ?? throw new ArgumentNullException(nameof(orderingStrategyFactory));
        _searchStrategy = searchStrategy ?? throw new ArgumentNullException(nameof(searchStrategy));
        _orderingStrategy = orderingStrategy ?? throw new ArgumentNullException(nameof(orderingStrategy));
    }

    public int Capacity => _searchStrategy.Capacity;

    public Search SearchStrategy
    {
        get => _searchStrategy.Identifier;
        set
        {
            if (value != _searchStrategy.Identifier)
            {
                _searchStrategy = _searchStrategyFactory.CreateInstance(value, _searchStrategy.Capacity);
            }
        }
    }

    public Ordering OrderingStrategy
    {
        get => _orderingStrategy.Identifier;
        set
        {
            if (value != _orderingStrategy.Identifier)
            {
                _orderingStrategy = _orderingStrategyFactory.CreateInstance(value);
            }
        }
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

    public int EnsureCapacity(int capacity) => _searchStrategy.EnsureCapacity(capacity);

    public void TrimExcess(int capacity)
    {
        _searchStrategy.TrimExcess(capacity);
    }

    public static IBinaryCspSolverBuilder<V, D> Create() => new BinaryCspSolverBuilder<V, D>();

    private Result<V, D> TrySolve(ISolvableBinaryCsp<V, D> binaryCsp, CancellationToken cancellationToken)
    {
        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            switch (GetSearchState())
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

    private void Setup(ISolvableBinaryCsp<V, D> binaryCsp)
    {
        _searchStrategy.Setup(binaryCsp, _orderingStrategy);
        _setupSteps++;
    }

    private void VisitNode()
    {
        _searchStrategy.Visit(_orderingStrategy);
        _visitingSteps++;
    }

    private void Backtrack()
    {
        _searchStrategy.Backtrack();
        _backtrackingSteps++;
    }

    private Result<V, D> GetResult() => new()
    {
        Algorithm = new Algorithm(SearchStrategy, OrderingStrategy),
        Assignments = _searchStrategy.GetAssignments(),
        SetupSteps = _setupSteps,
        VisitingSteps = _visitingSteps,
        BacktrackingSteps = _backtrackingSteps,
        TotalSteps = _setupSteps + _visitingSteps + _backtrackingSteps
    };

    private void Reset()
    {
        _searchStrategy.Reset();
        _setupSteps = 0;
        _visitingSteps = 0;
        _backtrackingSteps = 0;
    }

    private SearchState GetSearchState() => _searchStrategy.SearchState;
}
