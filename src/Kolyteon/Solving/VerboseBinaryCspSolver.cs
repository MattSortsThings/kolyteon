using System.Diagnostics;
using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.Builders;
using Kolyteon.Solving.Internals.Strategies.Checking.Common;
using Kolyteon.Solving.Internals.Strategies.Ordering;

namespace Kolyteon.Solving;

public sealed class VerboseBinaryCspSolver<TVariable, TDomainValue> : BinaryCspSolver<TVariable, TDomainValue>,
    IVerboseBinaryCspSolver<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    internal VerboseBinaryCspSolver(ICheckingStrategyFactory<TVariable, TDomainValue> checkingStrategyFactory,
        IOrderingStrategyFactory orderingStrategyFactory,
        ICheckingStrategy<TVariable, TDomainValue> checkingStrategy,
        IOrderingStrategy orderingStrategy,
        TimeSpan stepDelay) : base(checkingStrategyFactory, orderingStrategyFactory, checkingStrategy, orderingStrategy)
    {
        StepDelay = stepDelay;
    }

    public TimeSpan StepDelay { get; set; }

    public async Task<SolvingResult<TVariable, TDomainValue>> SolveAsync(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp,
        SolvingProgressReporter<TVariable, TDomainValue> progressReporter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(binaryCsp);
        ArgumentNullException.ThrowIfNull(progressReporter);
        ThrowIfNotModellingAProblem(binaryCsp);

        SolvingResult<TVariable, TDomainValue> result;

        await SetupAsync(binaryCsp, progressReporter);
        try
        {
            result = await SearchAsync(progressReporter, cancellationToken);
        }
        catch (TaskCanceledException ex)
        {
            throw new OperationCanceledException("The binary CSP solving operation was cancelled.", ex);
        }
        finally
        {
            Teardown();
        }

        return result;
    }

    private async Task SetupAsync(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp,
        SolvingProgressReporter<TVariable, TDomainValue> progressReporter)
    {
        Setup(binaryCsp);
        progressReporter.Setup(this);
        await Task.Delay(StepDelay);
    }

    private async Task<SolvingResult<TVariable, TDomainValue>> SearchAsync(
        SolvingProgressReporter<TVariable, TDomainValue> progressReporter, CancellationToken cancellationToken)
    {
        while (true)
        {
            switch (SolvingState)
            {
                case SolvingState.Assigning:
                    ExecuteAssigningStep();
                    NotifyOfAssigningStep(progressReporter);


                    break;
                case SolvingState.Backtracking:
                    ExecuteBacktrackingStep();
                    NotifyOfBacktrackingStep(progressReporter);

                    break;

                case SolvingState.Simplifying:
                    ExecuteSimplifyingStep();
                    NotifyOfSimplifyingStep(progressReporter);

                    break;

                case SolvingState.Ready:
                    throw new UnreachableException();

                case SolvingState.Finished:
                default:
                    return CreateSolvingResult();
            }

            await Task.Delay(StepDelay, cancellationToken);
        }
    }

    public static IVerboseBinaryCspSolverBuilder<TVariable, TDomainValue> Create() =>
        new VerboseBinaryCspSolverBuilder<TVariable, TDomainValue>();

    private void NotifyOfAssigningStep(SolvingProgressReporter<TVariable, TDomainValue> progressReporter) =>
        progressReporter.Report(new SolvingStepDatum<TVariable, TDomainValue>(SolvingStepType.Assigning,
            SearchLevel,
            SolvingState,
            SolvingState is SolvingState.Assigning or SolvingState.Finished ? GetMostRecentAssignment() : null));

    private void NotifyOfBacktrackingStep(SolvingProgressReporter<TVariable, TDomainValue> progressReporter) =>
        progressReporter.Report(new SolvingStepDatum<TVariable, TDomainValue>(SolvingStepType.Backtracking,
            SearchLevel,
            SolvingState));

    private void NotifyOfSimplifyingStep(SolvingProgressReporter<TVariable, TDomainValue> progressReporter) =>
        progressReporter.Report(new SolvingStepDatum<TVariable, TDomainValue>(SolvingStepType.Simplifying,
            SearchLevel,
            SolvingState));
}
