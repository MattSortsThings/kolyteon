using System.Collections.Concurrent;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Common;

namespace Mjt85.Kolyteon.Solving.Verbose;

/// <summary>
///     Abstract base class for a verbose generic binary CSP solving progress reporter.
/// </summary>
/// <typeparam name="V">The binary CSP variable type.</typeparam>
/// <typeparam name="D">The binary CSP domain value type.</typeparam>
public abstract class SolvingProgress<V, D> : IProgress<StepNotification<V, D>>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    private const int RootLevel = -1;

    /// <summary>
    ///     Gets a collection of all the current assignments of domain values to variables.
    /// </summary>
    public ConcurrentStack<Assignment<V, D>> CurrentAssignments { get; } = new();

    /// <summary>
    ///     Gets the current level of the search in the search tree.
    /// </summary>
    public int CurrentSearchLevel { get; private set; } = RootLevel;

    /// <summary>
    ///     Gets the search tree root level (-1).
    /// </summary>
    public int SearchTreeRootLevel => RootLevel;

    /// <summary>
    ///     Gets the search tree leaf level.
    /// </summary>
    public int SearchTreeLeafLevel { get; private set; }

    /// <summary>
    ///     Gets the current state of the search.
    /// </summary>
    public SearchState CurrentSearchState { get; private set; } = SearchState.Initial;

    /// <summary>
    ///     Gets the total steps executed by the solving algorithm.
    /// </summary>
    public long TotalSteps { get; private set; }

    /// <summary>
    ///     Gets the setup steps executed by the solving algorithm.
    /// </summary>
    public long SetupSteps { get; private set; }

    /// <summary>
    ///     Gets the visiting steps executed by the solving algorithm.
    /// </summary>
    public long VisitingSteps { get; private set; }

    /// <summary>
    ///     Gets the backtracking steps executed by the solving algorithm.
    /// </summary>
    public long BacktrackingSteps { get; private set; }

    /// <summary>
    ///     Gets the type of the latest step executed by the solving algorithm.
    /// </summary>
    public StepType? LatestStepType { get; private set; }

    /// <summary>
    ///     Reports on the latest step.
    /// </summary>
    /// <param name="value">Contains data about the latest step.</param>
    public void Report(StepNotification<V, D> value)
    {
        (StepType stepType,
            SearchState currentSearchState,
            var currentSearchLevel,
            var searchTreeLeafLevel,
            Assignment<V, D>? latestAssignment) = value;

        switch (stepType)
        {
            case StepType.Visiting:
                HandleVisitingStep(in currentSearchState, in currentSearchLevel, in latestAssignment);

                break;
            case StepType.Backtracking:
                HandleBacktrackingStep(in currentSearchState, in currentSearchLevel);

                break;
            case StepType.Setup:
            default:
                HandleSetupStep(in currentSearchState, in currentSearchLevel, in searchTreeLeafLevel);

                break;
        }

        StateHasChanged();
    }

    /// <summary>
    ///     Resets the public properties of this instance to their initial values, then
    /// </summary>
    /// <remarks>The state change is a template method to be implemented by any derivative of this abstract class.</remarks>
    /// <seealso cref="StateHasChanged" />
    public virtual void Reset()
    {
        CurrentAssignments.Clear();
        CurrentSearchState = SearchState.Initial;
        CurrentSearchLevel = RootLevel;
        SearchTreeLeafLevel = 0;
        TotalSteps = 0;
        SetupSteps = 0;
        VisitingSteps = 0;
        BacktrackingSteps = 0;
        LatestStepType = null;
        StateHasChanged();
    }

    /// <summary>
    ///     Invoked after the update of one or more of the public properties of this instance.
    /// </summary>
    protected abstract void StateHasChanged();

    private void HandleSetupStep(in SearchState currentSearchState, in int currentSearchLevel, in int searchTreeLeafLevel)
    {
        Reset();
        CurrentSearchState = currentSearchState;
        CurrentSearchLevel = currentSearchLevel;
        SearchTreeLeafLevel = searchTreeLeafLevel;
        LatestStepType = StepType.Setup;
        SetupSteps++;
        TotalSteps++;
    }

    private void HandleVisitingStep(in SearchState currentSearchState,
        in int currentSearchLevel,
        in Assignment<V, D>? latestAssignment)
    {
        CurrentSearchState = currentSearchState;
        CurrentSearchLevel = currentSearchLevel;
        if (latestAssignment.HasValue)
        {
            CurrentAssignments.Push(latestAssignment.Value);
        }

        LatestStepType = StepType.Visiting;
        VisitingSteps++;
        TotalSteps++;
    }

    private void HandleBacktrackingStep(in SearchState currentSearchState, in int currentSearchLevel)
    {
        CurrentSearchState = currentSearchState;
        CurrentSearchLevel = currentSearchLevel;

        var target = Math.Max(currentSearchLevel, 0);

        while (CurrentAssignments.Count > target)
        {
            CurrentAssignments.TryPop(out Assignment<V, D> _);
        }

        LatestStepType = StepType.Backtracking;
        BacktrackingSteps++;
        TotalSteps++;
    }
}
