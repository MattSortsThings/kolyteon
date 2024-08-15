using System.Collections.Concurrent;
using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.SearchTrees;

namespace Kolyteon.Solving;

public abstract class SolvingProgressReporter<TVariable, TDomainValue> : IProgress<SolvingStepDatum<TVariable, TDomainValue>>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    public SolvingState SolvingState { get; private set; } = SolvingState.Ready;

    public int SimplifyingSteps { get; private set; }

    public int AssigningSteps { get; private set; }

    public int BacktrackingSteps { get; private set; }

    public int TotalSteps { get; private set; }

    public int RootLevel { get; private set; } = Constants.Levels.Root;

    public int LeafLevel { get; private set; }

    public int SearchLevel { get; private set; } = Constants.Levels.Root;

    public ConcurrentStack<Assignment<TVariable, TDomainValue>> Assignments { get; } = new();

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

    public void Setup(BinaryCspSolver<TVariable, TDomainValue> solver)
    {
        SolvingState = solver.SolvingState;
        SimplifyingSteps = solver.SimplifyingSteps;
        AssigningSteps = solver.AssigningSteps;
        BacktrackingSteps = solver.BacktrackingSteps;
        TotalSteps = SimplifyingSteps + AssigningSteps + BacktrackingSteps;
        RootLevel = solver.RootLevel;
        LeafLevel = solver.LeafLevel;
        SearchLevel = solver.SearchLevel;
        Assignments.Clear();

        OnSetup();
    }

    private void UpdateForAssigningStep(Assignment<TVariable, TDomainValue>? assignment)
    {
        AssigningSteps++;
        TotalSteps++;
        if (assignment.HasValue)
        {
            Assignments.Push(assignment.Value);
        }
    }

    private void UpdateForBacktrackingStep()
    {
        BacktrackingSteps++;
        TotalSteps++;

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
    }

    protected internal abstract void OnSetup();

    protected internal abstract void OnReport();
}
