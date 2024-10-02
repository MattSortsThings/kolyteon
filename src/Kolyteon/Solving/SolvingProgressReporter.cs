using System.Collections.Concurrent;
using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.SearchTrees;

namespace Kolyteon.Solving;

/// <summary>
///     Abstract base class for reporting on progress updates for verbose binary CSP solving operations;
/// </summary>
/// <typeparam name="TVariable">The binary CSP variable type.</typeparam>
/// <typeparam name="TDomainValue">The binary CSP domain value type.</typeparam>
public abstract class SolvingProgressReporter<TVariable, TDomainValue> : ISolvingProgress<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    /// <summary>
    ///     Gets the current state of the solver.
    /// </summary>
    public SolvingState SolvingState { get; private set; } = SolvingState.Ready;

    /// <summary>
    ///     Gets the number of simplifying steps executed by the solver.
    /// </summary>
    public int SimplifyingSteps { get; private set; }

    /// <summary>
    ///     Gets the number of assigning steps executed by the solver.
    /// </summary>
    public int AssigningSteps { get; private set; }

    /// <summary>
    ///     Gets the number of backtracking steps executed by the solver.
    /// </summary>
    public int BacktrackingSteps { get; private set; }

    /// <summary>
    ///     Gets the total number of steps executed by the solver.
    /// </summary>
    /// <remarks>
    ///     The value of this property is always equal to the sum of <see cref="SimplifyingSteps" />,
    ///     <see cref="AssigningSteps" /> and <see cref="BacktrackingSteps" />.
    /// </remarks>
    public int TotalSteps { get; private set; }

    /// <summary>
    ///     Gets the efficiency of the backtracking search, which is the non-backtracking proportion of the total steps,
    ///     expressed as a real number in the range (0.0,1.0].
    /// </summary>
    /// <value>The default value of this property is 1.0.</value>
    public double Efficiency { get; private set; }

    /// <summary>
    ///     Gets the index of the root level of the search tree.
    /// </summary>
    /// <remarks>The index of the root level is always -1.</remarks>
    public int RootLevel { get; private set; } = Constants.Levels.Root;

    /// <summary>
    ///     Gets the index of the leaf level of the search tree.
    /// </summary>
    /// <remarks>The index of the leaf level is always equal to the number of variables in the binary CSP being solved.</remarks>
    public int LeafLevel { get; private set; }

    /// <summary>
    ///     Gets the index of the current level of the solver in the search tree.
    /// </summary>
    /// <remarks>
    ///     The value of this property is always greater than or equal to <see cref="RootLevel" /> and less than or equal
    ///     to <see cref="LeafLevel" />.
    /// </remarks>
    public int SearchLevel { get; private set; } = Constants.Levels.Root;

    /// <summary>
    ///     Gets a concurrent collection of all assignments in the past of the solving operation.
    /// </summary>
    public ConcurrentStack<Assignment<TVariable, TDomainValue>> Assignments { get; } = new();

    /// <summary>Reports a progress update after the execution of a solving step.</summary>
    /// <param name="value">Contains information about the executed step.</param>
    public void Report(SolvingStepDatum<TVariable, TDomainValue> value)
    {
        (SolvingStepType stepType,
            SearchLevel,
            SolvingState,
            Assignment<TVariable, TDomainValue>? assignment) = value;

        switch (stepType)
        {
            case SolvingStepType.Assigning:
                UpdateForAssigningStep(assignment);

                break;
            case SolvingStepType.Backtracking:
                UpdateForBacktrackingStep();

                break;
            case SolvingStepType.Simplifying:
            default:
                UpdateForSimplifyingStep();

                break;
        }

        OnReport();
    }

    /// <summary>
    ///     Resets the provider to an empty state with the specified search tree leaf level, reflecting the state of the
    ///     verbose solver immediately after the solving operation has been set up but before the first step has been executed.
    /// </summary>
    /// <param name="searchTreeLeafLevel">The search tree leaf level.</param>
    public void Reset(int searchTreeLeafLevel)
    {
        ResetAllPropertiesToDefaults();
        LeafLevel = searchTreeLeafLevel;
        OnReset();
    }

    /// <summary>
    ///     Resets the provider to an empty state.
    /// </summary>
    public void Reset()
    {
        ResetAllPropertiesToDefaults();
        OnReset();
    }

    /// <summary>
    ///     Performs additional work immediately after this instance has updated its public properties during a
    ///     <see cref="Reset(int)" /> or <see cref="Reset()" /> method invocation.
    /// </summary>
    /// <remarks>
    ///     This method must be overridden in any <see cref="SolvingProgressReporter{TVariable,TDomainValue}" />
    ///     derivative. It can be used to trigger an event defined in the derivative, e.g. to re-render the instance's state in
    ///     the user interface, or for any other purpose. It can also be left empty to do nothing.
    /// </remarks>
    protected abstract void OnReset();

    /// <summary>
    ///     Performs additional work immediately after this instance has updated its public properties during a
    ///     <see cref="Report" /> method invocation.
    /// </summary>
    /// <remarks>
    ///     This method must be overridden in any <see cref="SolvingProgressReporter{TVariable,TDomainValue}" />
    ///     derivative. It can be used to trigger an event, e.g. to re-render the instance's state in the user interface, or
    ///     for any other purpose. It can also be left empty to do nothing.
    /// </remarks>
    protected abstract void OnReport();

    private void UpdateForAssigningStep(Assignment<TVariable, TDomainValue>? assignment)
    {
        AssigningSteps++;
        TotalSteps++;
        UpdateEfficiency();

        if (assignment.HasValue)
        {
            Assignments.Push(assignment.Value);
        }
    }

    private void UpdateForBacktrackingStep()
    {
        BacktrackingSteps++;
        TotalSteps++;
        UpdateEfficiency();

        int target = Math.Max(SearchLevel, 0);

        while (Assignments.Count > target)
        {
            Assignments.TryPop(out _);
        }
    }

    private void UpdateForSimplifyingStep()
    {
        SimplifyingSteps++;
        TotalSteps++;
        UpdateEfficiency();
    }

    private void UpdateEfficiency() => Efficiency = (TotalSteps - BacktrackingSteps) / (double)TotalSteps;

    private void ResetAllPropertiesToDefaults()
    {
        Assignments.Clear();
        AssigningSteps = 0;
        BacktrackingSteps = 0;
        SimplifyingSteps = 0;
        Efficiency = 1.0;
        TotalSteps = 0;
        SearchLevel = RootLevel = Constants.Levels.Root;
        LeafLevel = 0;
        SolvingState = SolvingState.Ready;
    }
}
