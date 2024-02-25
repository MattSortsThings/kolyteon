using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Builders;
using Mjt85.Kolyteon.Solving.Guards;
using Mjt85.Kolyteon.Solving.OrderingStrategies;
using Mjt85.Kolyteon.Solving.SearchStrategies;

namespace Mjt85.Kolyteon.Solving;

public sealed class VerboseBinaryCspSolver<V, D> : IVerboseBinaryCspSolver<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    private readonly IOrderingStrategyFactory _orderingStrategyFactory;
    private readonly ISearchStrategyFactory<V, D> _searchStrategyFactory;
    private long _backtrackingSteps;
    private IOrderingStrategy _orderingStrategy;
    private SearchState _searchState;
    private ISearchStrategy<V, D> _searchStrategy;
    private long _setupSteps;
    private long _visitingSteps;

    internal VerboseBinaryCspSolver(ISearchStrategyFactory<V, D> searchStrategyFactory,
        IOrderingStrategyFactory orderingStrategyFactory,
        ISearchStrategy<V, D> searchStrategy,
        IOrderingStrategy orderingStrategy,
        TimeSpan stepDelay)
    {
        _orderingStrategyFactory = orderingStrategyFactory ?? throw new ArgumentNullException(nameof(orderingStrategyFactory));
        _searchStrategyFactory = searchStrategyFactory ?? throw new ArgumentNullException(nameof(searchStrategyFactory));
        _orderingStrategy = orderingStrategy ?? throw new ArgumentNullException(nameof(orderingStrategy));
        _searchStrategy = searchStrategy ?? throw new ArgumentNullException(nameof(searchStrategy));
        StepDelay = stepDelay;
        _searchState = SearchState.Initial;
    }

    public int Capacity => _searchStrategy.Capacity;

    public TimeSpan StepDelay { get; set; }

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

    public async Task<Result<V, D>> SolveAsync(ISolvableBinaryCsp<V, D> binaryCsp,
        IProgress<StepNotification<V, D>> progress,
        CancellationToken cancellationToken = default)
    {
        _ = binaryCsp ?? throw new ArgumentNullException(nameof(binaryCsp));
        Guard.AgainstBinaryCspNotModellingProblem(binaryCsp);

        try
        {
            return await TrySolveAsync(binaryCsp, progress, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            throw new OperationCanceledException(cancellationToken);
        }
        finally
        {
            Reset();
        }
    }

    public static IVerboseBinaryCspSolverBuilder<V, D> Create() => new VerboseBinaryCspSolverBuilder<V, D>();

    private async Task<Result<V, D>> TrySolveAsync(ISolvableBinaryCsp<V, D> binaryCsp,
        IProgress<StepNotification<V, D>> progress,
        CancellationToken cancellationToken)
    {
        UpdateSearchState();
        while (true)
        {
            await Task.Delay(StepDelay, cancellationToken);
            switch (_searchState)
            {
                case SearchState.Safe:
                    VisitNode();
                    NotifyOfVisitingStep(progress);

                    break;
                case SearchState.Unsafe:
                    Backtrack();
                    NotifyOfBacktrackingStep(progress);

                    break;
                case SearchState.Initial:
                    Setup(binaryCsp);
                    NotifyOfSetupStep(progress);

                    break;
                case SearchState.Final:
                default:
                    return GetResult();
            }
        }
    }

    private void UpdateSearchState()
    {
        _searchState = _searchStrategy.SearchState;
    }

    private void Setup(ISolvableBinaryCsp<V, D> binaryCsp)
    {
        _searchStrategy.Setup(binaryCsp, _orderingStrategy);
        _setupSteps++;
        UpdateSearchState();
    }

    private void NotifyOfSetupStep(IProgress<StepNotification<V, D>> progress)
    {
        progress.Report(new StepNotification<V, D>
        {
            StepType = StepType.Setup,
            CurrentSearchState = _searchState,
            CurrentSearchLevel = GetSearchLevel(),
            SearchTreeLeafLevel = GetSearchTreeLeafLevel()
        });
    }

    private void VisitNode()
    {
        _searchStrategy.Visit(_orderingStrategy);
        _visitingSteps++;
        UpdateSearchState();
    }

    private void NotifyOfVisitingStep(IProgress<StepNotification<V, D>> progress)
    {
        progress.Report(new StepNotification<V, D>
        {
            StepType = StepType.Visiting,
            CurrentSearchState = _searchState,
            CurrentSearchLevel = GetSearchLevel(),
            SearchTreeLeafLevel = GetSearchTreeLeafLevel(),
            LatestAssignment = _searchState != SearchState.Unsafe ? GetLatestAssignment() : null
        });
    }

    private void Backtrack()
    {
        _searchStrategy.Backtrack();
        _backtrackingSteps++;
        UpdateSearchState();
    }

    private void NotifyOfBacktrackingStep(IProgress<StepNotification<V, D>> progress)
    {
        progress.Report(new StepNotification<V, D>
        {
            StepType = StepType.Backtracking,
            CurrentSearchState = _searchState,
            CurrentSearchLevel = GetSearchLevel(),
            SearchTreeLeafLevel = GetSearchTreeLeafLevel()
        });
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
        _searchState = SearchState.Initial;
    }

    private int GetSearchLevel() => _searchStrategy.SearchLevel;

    private int GetSearchTreeLeafLevel() => _searchStrategy.SearchTreeLeafLevel;

    private Assignment<V, D> GetLatestAssignment() => _searchStrategy.GetLatestAssignment();
}
