using System.Diagnostics;
using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.Builders;
using Kolyteon.Solving.Internals.Strategies.Checking.Common;
using Kolyteon.Solving.Internals.Strategies.Ordering;

namespace Kolyteon.Solving;

/// <summary>
///     A verbose, asynchronous, configurable generic binary CSP solver.
/// </summary>
/// <typeparam name="TVariable">The binary CSP variable type.</typeparam>
/// <typeparam name="TDomainValue">The binary CSP domain value type.</typeparam>
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

    /// <summary>
    ///     Gets or sets the delay between each step of the backtracking search algorithm.
    /// </summary>
    public TimeSpan StepDelay { get; set; }

    /// <inheritdoc />
    public async Task<SolvingResult<TVariable, TDomainValue>> SolveAsync(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp,
        ISolvingProgress<TVariable, TDomainValue> progress,
        SearchAlgorithm? searchAlgorithm = default,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(binaryCsp);
        ArgumentNullException.ThrowIfNull(progress);
        ThrowIfNotModellingAProblem(binaryCsp);

        if (searchAlgorithm is not null)
        {
            Reconfigure(searchAlgorithm);
        }

        return await VerboseBacktrackingSearch(binaryCsp, progress, cancellationToken);
    }

    /// <summary>
    ///     Starts the process of building a new <see cref="VerboseBinaryCspSolver{TVariable,TDomainValue}" /> using the fluent
    ///     builder API.
    /// </summary>
    /// <returns>A new fluent builder instance, to which method invocations can be chained.</returns>
    public static IVerboseBinaryCspSolverBuilder<TVariable, TDomainValue> Create() =>
        new VerboseBinaryCspSolverBuilder<TVariable, TDomainValue>();

    private async Task<SolvingResult<TVariable, TDomainValue>> VerboseBacktrackingSearch(
        IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp,
        ISolvingProgress<TVariable, TDomainValue> progress,
        CancellationToken cancellationToken)
    {
        SolvingResult<TVariable, TDomainValue> result;

        await SetupAsync(binaryCsp, progress);
        try
        {
            result = await SearchAsync(progress, cancellationToken);
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
        ISolvingProgress<TVariable, TDomainValue> progress)
    {
        Setup(binaryCsp);
        progress.Reset(LeafLevel);
        await Task.Delay(StepDelay);
    }

    private async Task<SolvingResult<TVariable, TDomainValue>> SearchAsync(ISolvingProgress<TVariable, TDomainValue> progress,
        CancellationToken cancellationToken)
    {
        while (true)
        {
            switch (SolvingState)
            {
                case SolvingState.Assigning:
                    ExecuteAssigningStep();
                    NotifyOfAssigningStep(progress);


                    break;
                case SolvingState.Backtracking:
                    ExecuteBacktrackingStep();
                    NotifyOfBacktrackingStep(progress);

                    break;

                case SolvingState.Simplifying:
                    ExecuteSimplifyingStep();
                    NotifyOfSimplifyingStep(progress);

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

    private void NotifyOfAssigningStep(ISolvingProgress<TVariable, TDomainValue> progress) =>
        progress.Report(new SolvingStepDatum<TVariable, TDomainValue>(SolvingStepType.Assigning,
            SearchLevel,
            SolvingState,
            SolvingState is SolvingState.Assigning or SolvingState.Finished ? GetMostRecentAssignment() : null));

    private void NotifyOfBacktrackingStep(ISolvingProgress<TVariable, TDomainValue> progress) =>
        progress.Report(new SolvingStepDatum<TVariable, TDomainValue>(SolvingStepType.Backtracking,
            SearchLevel,
            SolvingState));

    private void NotifyOfSimplifyingStep(ISolvingProgress<TVariable, TDomainValue> progress) =>
        progress.Report(new SolvingStepDatum<TVariable, TDomainValue>(SolvingStepType.Simplifying,
            SearchLevel,
            SolvingState));
}
