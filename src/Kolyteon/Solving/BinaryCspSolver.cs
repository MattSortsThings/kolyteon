using System.Diagnostics;
using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.Builders;
using Kolyteon.Solving.Internals.Strategies.Checking.Common;
using Kolyteon.Solving.Internals.Strategies.Ordering;

namespace Kolyteon.Solving;

/// <summary>
///     A configurable, synchronous, generic binary CSP solver.
/// </summary>
/// <typeparam name="TVariable">The binary CSP variable type.</typeparam>
/// <typeparam name="TDomainValue">The binary CSP domain value type.</typeparam>
public sealed class BinaryCspSolver<TVariable, TDomainValue> : IBinaryCspSolver<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    private readonly ICheckingStrategyFactory<TVariable, TDomainValue> _checkingStrategyFactory;
    private readonly IOrderingStrategyFactory _orderingStrategyFactory;
    private int _assigningSteps;
    private int _backtrackingSteps;
    private ICheckingStrategy<TVariable, TDomainValue> _checkingStrategy;
    private IOrderingStrategy _orderingStrategy;
    private int _simplifyingSteps;
    private SolvingState _state;

    internal BinaryCspSolver(ICheckingStrategyFactory<TVariable, TDomainValue> checkingStrategyFactory,
        IOrderingStrategyFactory orderingStrategyFactory,
        ICheckingStrategy<TVariable, TDomainValue> checkingStrategy,
        IOrderingStrategy orderingStrategy)
    {
        _checkingStrategyFactory = checkingStrategyFactory;
        _orderingStrategyFactory = orderingStrategyFactory;
        _checkingStrategy = checkingStrategy;
        _orderingStrategy = orderingStrategy;
    }

    /// <summary>
    ///     Gets or sets the total number of binary CSP variables the internal data structures of this instance can hold
    ///     without resizing.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"><see cref="Capacity" /> is set to a negative value.</exception>
    /// <exception cref="InvalidOperationException">
    ///     The value of <see cref="Capacity" /> is set while a solving operation is in progress.
    /// </exception>
    public int Capacity
    {
        get => _checkingStrategy.Capacity;
        set => _checkingStrategy.Capacity = value;
    }

    /// <inheritdoc />
    public CheckingStrategy CheckingStrategy
    {
        get => _checkingStrategy.Identifier;
        set
        {
            if (value != _checkingStrategy.Identifier)
            {
                _checkingStrategy = _checkingStrategyFactory.Create(value, _checkingStrategy.Capacity);
            }
        }
    }

    /// <inheritdoc />
    public OrderingStrategy OrderingStrategy
    {
        get => _orderingStrategy.Identifier;
        set
        {
            if (value != _orderingStrategy.Identifier)
            {
                _orderingStrategy = _orderingStrategyFactory.Create(value);
            }
        }
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

    public static IBinaryCspSolverBuilder<TVariable, TDomainValue> Create() =>
        new BinaryCspSolverBuilder<TVariable, TDomainValue>();

    private void Setup(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp)
    {
        _checkingStrategy.Populate(binaryCsp);
        _state = SolvingState.Simplifying;
    }

    private void Teardown()
    {
        _checkingStrategy.Reset();
        _state = SolvingState.Ready;
        _assigningSteps = 0;
        _simplifyingSteps = 0;
        _backtrackingSteps = 0;
    }

    private SolvingResult<TVariable, TDomainValue> Search(CancellationToken cancellationToken)
    {
        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            switch (_state)
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

    private void ExecuteAssigningStep()
    {
        _assigningSteps++;

        _checkingStrategy.TryAssign();
        if (_checkingStrategy.Safe)
        {
            _checkingStrategy.Advance();
            if (_checkingStrategy.SearchLevel == _checkingStrategy.LeafLevel)
            {
                _state = SolvingState.Finished;
            }
            else
            {
                _checkingStrategy.SelectNext(_orderingStrategy);
                _state = SolvingState.Assigning;
            }
        }
        else
        {
            _state = SolvingState.Backtracking;
        }
    }

    private void ExecuteBacktrackingStep()
    {
        _backtrackingSteps++;

        _checkingStrategy.Backtrack();
        if (_checkingStrategy.Safe)
        {
            _state = SolvingState.Assigning;
        }
        else
        {
            _state = _checkingStrategy.SearchLevel == _checkingStrategy.RootLevel
                ? SolvingState.Finished
                : SolvingState.Backtracking;
        }
    }

    private void ExecuteSimplifyingStep()
    {
        _simplifyingSteps++;

        _checkingStrategy.Simplify();
        if (_checkingStrategy.Safe)
        {
            _checkingStrategy.Advance();
            _checkingStrategy.SelectNext(_orderingStrategy);
            _state = SolvingState.Assigning;
        }
        else
        {
            _state = SolvingState.Finished;
        }
    }

    private SolvingResult<TVariable, TDomainValue> CreateSolvingResult() => new()
    {
        Assignments = _checkingStrategy.GetAllAssignments(),
        SearchAlgorithm = new SearchAlgorithm(CheckingStrategy, OrderingStrategy),
        SimplifyingSteps = _simplifyingSteps,
        AssigningSteps = _assigningSteps,
        BacktrackingSteps = _backtrackingSteps
    };

    private static void ThrowIfNotModellingAProblem(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp)
    {
        if (binaryCsp.Variables == 0)
        {
            throw new ArgumentException("Binary CSP is not modelling a problem.");
        }
    }
}
