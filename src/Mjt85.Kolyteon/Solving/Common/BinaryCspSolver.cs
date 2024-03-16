using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;
using Mjt85.Kolyteon.Solving.Internals.SearchStrategies;

namespace Mjt85.Kolyteon.Solving.Common;

public abstract class BinaryCspSolver<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    private readonly IOrderingStrategyFactory _orderingStrategyFactory;
    private readonly ISearchStrategyFactory<V, D> _searchStrategyFactory;
    private long _backtrackingSteps;
    private bool _locked;
    private IOrderingStrategy _orderingStrategy;
    private ISearchStrategy<V, D> _searchStrategy;
    private long _setupSteps;
    private long _visitingSteps;

    internal BinaryCspSolver(ISearchStrategyFactory<V, D> searchStrategyFactory,
        IOrderingStrategyFactory orderingStrategyFactory,
        ISearchStrategy<V, D> searchStrategy,
        IOrderingStrategy orderingStrategy)
    {
        _searchStrategyFactory = searchStrategyFactory ?? throw new ArgumentNullException(nameof(searchStrategyFactory));
        _orderingStrategyFactory = orderingStrategyFactory ?? throw new ArgumentNullException(nameof(orderingStrategyFactory));
        _searchStrategy = searchStrategy ?? throw new ArgumentNullException(nameof(searchStrategy));
        _orderingStrategy = orderingStrategy ?? throw new ArgumentNullException(nameof(orderingStrategy));
        CurrentSearchState = SearchState.Initial;
    }

    public int Capacity => _searchStrategy.Capacity;

    public Search SearchStrategy
    {
        get => _searchStrategy.Identifier;
        set
        {
            ThrowIfLocked();
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
            ThrowIfLocked();
            if (value != _orderingStrategy.Identifier)
            {
                _orderingStrategy = _orderingStrategyFactory.CreateInstance(value);
            }
        }
    }

    private protected SearchState CurrentSearchState { get; private set; }

    public int EnsureCapacity(int capacity) => _searchStrategy.EnsureCapacity(capacity);

    public void TrimExcess(int capacity)
    {
        _searchStrategy.TrimExcess(capacity);
    }

    private protected void UpdateSearchState()
    {
        CurrentSearchState = _searchStrategy.SearchState;
    }

    private protected void Setup(ISolvableBinaryCsp<V, D> binaryCsp)
    {
        _searchStrategy.Setup(binaryCsp, _orderingStrategy);
        _setupSteps++;
        UpdateSearchState();
    }

    private protected void VisitNode()
    {
        _searchStrategy.Visit(_orderingStrategy);
        _visitingSteps++;
        UpdateSearchState();
    }

    private protected void Backtrack()
    {
        _searchStrategy.Backtrack();
        _backtrackingSteps++;
        UpdateSearchState();
    }

    private protected Result<V, D> GetResult() => new()
    {
        Algorithm = new Algorithm(SearchStrategy, OrderingStrategy),
        Assignments = _searchStrategy.GetAssignments(),
        SetupSteps = _setupSteps,
        VisitingSteps = _visitingSteps,
        BacktrackingSteps = _backtrackingSteps,
        TotalSteps = _setupSteps + _visitingSteps + _backtrackingSteps
    };

    private protected void Reset()
    {
        _searchStrategy.Reset();
        _setupSteps = 0;
        _visitingSteps = 0;
        _backtrackingSteps = 0;
        CurrentSearchState = SearchState.Initial;
    }

    private protected int GetSearchLevel() => _searchStrategy.SearchLevel;

    private protected int GetSearchTreeLeafLevel() => _searchStrategy.SearchTreeLeafLevel;

    private protected Assignment<V, D> GetLatestAssignment() => _searchStrategy.GetLatestAssignment();

    private protected void ThrowIfLocked()
    {
        if (_locked)
        {
            throw new InvalidOperationException("solving operation is in progress.");
        }
    }

    private protected void Lock() => _locked = true;

    private protected void Unlock() => _locked = false;
}
