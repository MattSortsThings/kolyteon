using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Common;
using Mjt85.Kolyteon.Solving.Internals.Guards;
using Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;
using Mjt85.Kolyteon.Solving.Internals.SearchStrategies;

namespace Mjt85.Kolyteon.Solving.Verbose;

/// <summary>
///     A generic binary CSP solver that asynchronously solves a binary CSP with progress reporting, and can be configured
///     with a step delay and a solving algorithm composed of a search strategy and an ordering strategy,
/// </summary>
/// <remarks>
///     Use the fluent builder API, accessed via the <see cref="CreateBinaryCspSolver.WithInitialCapacity" /> static
///     method of the <see cref="CreateBinaryCspSolver" /> static class to build a
///     <see cref="VerboseBinaryCspSolver{V,D}" /> instance.
/// </remarks>
/// <typeparam name="V">The binary CSP variable type.</typeparam>
/// <typeparam name="D">The binary CSP domain value type.</typeparam>
public sealed class VerboseBinaryCspSolver<V, D> : BinaryCspSolver<V, D>, IVerboseBinaryCspSolver<V, D>
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


    /// <inheritdoc />
    public TimeSpan StepDelay
    {
        get => _stepDelay;
        set
        {
            ThrowIfLocked();
            _stepDelay = value;
        }
    }

    /// <inheritdoc />
    /// <remarks>
    ///     <para>
    ///         When this method is invoked, the solver instance runs its configured binary CSP solving algorithm on the
    ///         <paramref name="binaryCsp" /> parameter, builds a <see cref="Result{V,D}" /> object, clears and resets its
    ///         internal data structures, then returns the result. After each step, the solver sends a new
    ///         <see cref="StepNotification{V,D}" /> instance to the <paramref name="progress" /> parameter and pauses for the
    ///         <see cref="StepDelay" /> duration.
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

    private async Task<Result<V, D>> TrySolveAsync(ISolvableBinaryCsp<V, D> binaryCsp,
        IProgress<StepNotification<V, D>> progress,
        CancellationToken cancellationToken)
    {
        UpdateSearchState();
        while (true)
        {
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

            await Task.Delay(_stepDelay, cancellationToken);
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
