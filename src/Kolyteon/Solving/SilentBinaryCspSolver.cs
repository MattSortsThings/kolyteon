using System.Diagnostics;
using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.Builders;
using Kolyteon.Solving.Internals.Strategies.Checking.Common;
using Kolyteon.Solving.Internals.Strategies.Ordering;

namespace Kolyteon.Solving;

/// <summary>
///     A silent, synchronous, configurable generic binary CSP solver.
/// </summary>
/// <typeparam name="TVariable">The binary CSP variable type.</typeparam>
/// <typeparam name="TDomainValue">The binary CSP domain value type.</typeparam>
public sealed class SilentBinaryCspSolver<TVariable, TDomainValue> :
    BinaryCspSolver<TVariable, TDomainValue>,
    ISilentBinaryCspSolver<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    internal SilentBinaryCspSolver(ICheckingStrategyFactory<TVariable, TDomainValue> checkingStrategyFactory,
        IOrderingStrategyFactory orderingStrategyFactory,
        ICheckingStrategy<TVariable, TDomainValue> checkingStrategy,
        IOrderingStrategy orderingStrategy) : base(checkingStrategyFactory,
        orderingStrategyFactory,
        checkingStrategy,
        orderingStrategy)
    {
    }

    /// <inheritdoc />
    public SolvingResult<TVariable, TDomainValue> Solve(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(binaryCsp);
        ThrowIfNotModellingAProblem(binaryCsp);

        SolvingResult<TVariable, TDomainValue> result;

        Setup(binaryCsp);
        try
        {
            result = Search(cancellationToken);
        }
        finally
        {
            Teardown();
        }

        return result;
    }

    /// <summary>
    ///     Starts the process of building a new <see cref="SilentBinaryCspSolver{TVariable,TDomainValue}" /> using the fluent
    ///     builder API.
    /// </summary>
    /// <returns>A new fluent builder instance, to which method invocations can be chained.</returns>
    public static ISilentBinaryCspSolverBuilder<TVariable, TDomainValue> Create() =>
        new SilentBinaryCspSolverBuilder<TVariable, TDomainValue>();

    private SolvingResult<TVariable, TDomainValue> Search(CancellationToken cancellationToken)
    {
        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            switch (State)
            {
                case SolvingState.Assigning:
                    ExecuteAssigningStep();

                    break;
                case SolvingState.Backtracking:
                    ExecuteBacktrackingStep();

                    break;

                case SolvingState.Simplifying:
                    ExecuteSimplifyingStep();

                    break;

                case SolvingState.Ready:
                    throw new UnreachableException();

                case SolvingState.Finished:
                default:
                    return CreateSolvingResult();
            }
        }
    }
}
