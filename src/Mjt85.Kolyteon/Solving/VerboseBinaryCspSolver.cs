using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Builders;
using Mjt85.Kolyteon.Solving.Guards;
using Mjt85.Kolyteon.Solving.OrderingStrategies;
using Mjt85.Kolyteon.Solving.SearchStrategies;

namespace Mjt85.Kolyteon.Solving;

public sealed class VerboseBinaryCspSolver<V, D> : CoreBinaryCspSolver<V, D>, IVerboseBinaryCspSolver<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    private TimeSpan _stepDelay;

    internal VerboseBinaryCspSolver(ISearchStrategyFactory<V, D> searchStrategyFactory,
        IOrderingStrategyFactory orderingStrategyFactory,
        ISearchStrategy<V, D> searchStrategy,
        IOrderingStrategy orderingStrategy,
        TimeSpan stepDelay)
        : base(searchStrategyFactory, orderingStrategyFactory, searchStrategy, orderingStrategy)
    {
        _stepDelay = stepDelay;
    }

    public TimeSpan StepDelay
    {
        get => _stepDelay;
        set
        {
            ThrowIfLocked();
            _stepDelay = value;
        }
    }

    public async Task<Result<V, D>> SolveAsync(ISolvableBinaryCsp<V, D> binaryCsp,
        IProgress<StepNotification<V, D>> progress,
        CancellationToken cancellationToken = default)
    {
        _ = binaryCsp ?? throw new ArgumentNullException(nameof(binaryCsp));
        ThrowIfLocked();
        Guard.AgainstBinaryCspNotModellingProblem(binaryCsp);
        Lock();

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
            Unlock();
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
            await Task.Delay(_stepDelay, cancellationToken);
            switch (CurrentSearchState)
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

    private void NotifyOfSetupStep(IProgress<StepNotification<V, D>> progress)
    {
        progress.Report(new StepNotification<V, D>
        {
            StepType = StepType.Setup,
            CurrentSearchState = CurrentSearchState,
            CurrentSearchLevel = GetSearchLevel(),
            SearchTreeLeafLevel = GetSearchTreeLeafLevel()
        });
    }

    private void NotifyOfVisitingStep(IProgress<StepNotification<V, D>> progress)
    {
        progress.Report(new StepNotification<V, D>
        {
            StepType = StepType.Visiting,
            CurrentSearchState = CurrentSearchState,
            CurrentSearchLevel = GetSearchLevel(),
            SearchTreeLeafLevel = GetSearchTreeLeafLevel(),
            LatestAssignment = CurrentSearchState != SearchState.Unsafe ? GetLatestAssignment() : null
        });
    }

    private void NotifyOfBacktrackingStep(IProgress<StepNotification<V, D>> progress)
    {
        progress.Report(new StepNotification<V, D>
        {
            StepType = StepType.Backtracking,
            CurrentSearchState = CurrentSearchState,
            CurrentSearchLevel = GetSearchLevel(),
            SearchTreeLeafLevel = GetSearchTreeLeafLevel()
        });
    }
}
