using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.Strategies.Checking.Common;
using Kolyteon.Solving.Internals.Strategies.Ordering;

namespace Kolyteon.Solving;

/// <summary>
///     Abstract base class for a configurable generic binary CSP solver.
/// </summary>
/// <typeparam name="TVariable">The binary CSP variable type.</typeparam>
/// <typeparam name="TDomainValue">The binary CSP domain value type.</typeparam>
public abstract class BinaryCspSolver<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    private readonly ICheckingStrategyFactory<TVariable, TDomainValue> _checkingStrategyFactory;
    private readonly IOrderingStrategyFactory _orderingStrategyFactory;
    private ICheckingStrategy<TVariable, TDomainValue> _checkingStrategy;
    private IOrderingStrategy _orderingStrategy;

    internal BinaryCspSolver(ICheckingStrategyFactory<TVariable, TDomainValue> checkingStrategyFactory,
        IOrderingStrategyFactory orderingStrategyFactory,
        ICheckingStrategy<TVariable, TDomainValue> checkingStrategy,
        IOrderingStrategy orderingStrategy)
    {
        _checkingStrategyFactory = checkingStrategyFactory ?? throw new ArgumentNullException(nameof(checkingStrategyFactory));
        _orderingStrategyFactory = orderingStrategyFactory ?? throw new ArgumentNullException(nameof(orderingStrategyFactory));
        _checkingStrategy = checkingStrategy ?? throw new ArgumentNullException(nameof(checkingStrategy));
        _orderingStrategy = orderingStrategy ?? throw new ArgumentNullException(nameof(orderingStrategy));
        SearchAlgorithm = new SearchAlgorithm(checkingStrategy.Identifier, orderingStrategy.Identifier);
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

    public SearchAlgorithm SearchAlgorithm { get; private set; }

    internal SolvingState SolvingState { get; private set; }

    internal int SimplifyingSteps { get; private set; }

    internal int AssigningSteps { get; private set; }

    internal int BacktrackingSteps { get; private set; }

    internal int RootLevel => _checkingStrategy.RootLevel;

    internal int LeafLevel => _checkingStrategy.LeafLevel;

    internal int SearchLevel => _checkingStrategy.SearchLevel;

    private protected void Reconfigure(SearchAlgorithm searchAlgorithm)
    {
        (CheckingStrategy checkingStrategy, OrderingStrategy orderingStrategy) = searchAlgorithm;

        if (_checkingStrategy.Identifier != checkingStrategy)
        {
            _checkingStrategy = _checkingStrategyFactory.Create(checkingStrategy, _checkingStrategy.Capacity);
        }

        if (_orderingStrategy.Identifier != orderingStrategy)
        {
            _orderingStrategy = _orderingStrategyFactory.Create(orderingStrategy);
        }

        SearchAlgorithm = searchAlgorithm;
    }

    private protected void Setup(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp)
    {
        _checkingStrategy.Populate(binaryCsp);
        SolvingState = SolvingState.Simplifying;
    }

    private protected void ExecuteAssigningStep()
    {
        _checkingStrategy.TryAssign();
        if (_checkingStrategy.Safe)
        {
            _checkingStrategy.Advance();
            if (_checkingStrategy.SearchLevel == _checkingStrategy.LeafLevel)
            {
                SolvingState = SolvingState.Finished;
            }
            else
            {
                _checkingStrategy.SelectNext(_orderingStrategy);
                SolvingState = SolvingState.Assigning;
            }
        }
        else
        {
            SolvingState = SolvingState.Backtracking;
        }

        AssigningSteps++;
    }

    private protected void ExecuteBacktrackingStep()
    {
        _checkingStrategy.Backtrack();
        if (_checkingStrategy.Safe)
        {
            SolvingState = SolvingState.Assigning;
        }
        else
        {
            SolvingState = _checkingStrategy.SearchLevel == _checkingStrategy.RootLevel
                ? SolvingState.Finished
                : SolvingState.Backtracking;
        }

        BacktrackingSteps++;
    }

    private protected void ExecuteSimplifyingStep()
    {
        _checkingStrategy.Simplify();
        if (_checkingStrategy.Safe)
        {
            _checkingStrategy.Advance();
            _checkingStrategy.SelectNext(_orderingStrategy);
            SolvingState = SolvingState.Assigning;
        }
        else
        {
            SolvingState = SolvingState.Finished;
        }

        SimplifyingSteps++;
    }

    private protected Assignment<TVariable, TDomainValue> GetMostRecentAssignment() =>
        _checkingStrategy.GetMostRecentAssignment();

    private protected SolvingResult<TVariable, TDomainValue> CreateSolvingResult() => new()
    {
        Assignments = _checkingStrategy.GetAllAssignments(),
        SearchAlgorithm = SearchAlgorithm,
        SimplifyingSteps = SimplifyingSteps,
        AssigningSteps = AssigningSteps,
        BacktrackingSteps = BacktrackingSteps
    };

    private protected void Teardown()
    {
        _checkingStrategy.Reset();
        SolvingState = SolvingState.Ready;
        AssigningSteps = 0;
        SimplifyingSteps = 0;
        BacktrackingSteps = 0;
    }

    private protected static void ThrowIfNotModellingAProblem(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp)
    {
        if (binaryCsp.Variables == 0)
        {
            throw new ArgumentException("Binary CSP is not modelling a problem.");
        }
    }
}
