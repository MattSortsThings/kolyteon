using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;
using Mjt85.Kolyteon.Solving.Internals.SearchStrategies;

namespace Mjt85.Kolyteon.Solving.Common;

/// <summary>
///     Abstract base class for the silent (synchronous) and verbose (asynchronous) generic binary CSP solver classes.
/// </summary>
/// <typeparam name="V">The binary CSP variable type.</typeparam>
/// <typeparam name="D">The binary CSP domain value type.</typeparam>
public abstract class BinaryCspSolver<V, D> : IBinaryCspSolver<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    private readonly IOrderingStrategyFactory _orderingStrategyFactory;
    private readonly ISearchStrategyFactory<V, D> _searchStrategyFactory;
    private long _backtrackingSteps;
    private bool _locked;
    private IOrderingStrategy _orderingStrategy;
    private ISearchStrategy<V, D> _searchStrategy;
    private long _setupSteps;
    private long _visitingSteps;

    internal BinaryCspSolver(ISearchStrategyFactory<V, D> searchStrategyFactory,
        IOrderingStrategyFactory orderingStrategyFactory,
        ISearchStrategy<V, D> searchStrategy,
        IOrderingStrategy orderingStrategy)
    {
        _searchStrategyFactory = searchStrategyFactory ?? throw new ArgumentNullException(nameof(searchStrategyFactory));
        _orderingStrategyFactory = orderingStrategyFactory ?? throw new ArgumentNullException(nameof(orderingStrategyFactory));
        _searchStrategy = searchStrategy ?? throw new ArgumentNullException(nameof(searchStrategy));
        _orderingStrategy = orderingStrategy ?? throw new ArgumentNullException(nameof(orderingStrategy));
        CurrentSearchState = SearchState.Initial;
    }

    /// <summary>
    ///     Gets the maximum binary CSP size (in variables) this instance can accommodate without resizing its internal data
    ///     structures.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The <see cref="Capacity" /> of a binary CSP solver instance is the maximum binary CSP size (in variables) it
    ///         can accommodate before resizing is required.
    ///     </para>
    ///     <para>
    ///         If this instance is tasked with solving a binary CSP with an <see cref="IMeasurableBinaryCsp.Variables" />
    ///         value greater than its capacity, it increases its capacity by automatically resizing its internal data
    ///         structures.
    ///     </para>
    ///     <para>
    ///         Use the <see cref="EnsureCapacity" /> method to ensure the capacity of this instance is at least equal to a
    ///         specific size. Use the <see cref="TrimExcess" /> method to reduce the capacity to a specific size.
    ///     </para>
    /// </remarks>
    /// <value>
    ///     A non-negative 32-bit signed integer. The maximum binary CSP size (in variables) this instance can accommodate
    ///     without resizing its internal data structures.
    /// </value>
    public int Capacity => _searchStrategy.Capacity;

    /// <summary>
    ///     Gets the current state of the search.
    /// </summary>
    /// <value>A <see cref="SearchState" /> enumeration value. The current state of the search.</value>
    private protected SearchState CurrentSearchState { get; private set; }

    /// <inheritdoc />
    public Search SearchStrategy
    {
        get => _searchStrategy.Identifier;
        set
        {
            ThrowIfLocked();
            if (value != _searchStrategy.Identifier)
            {
                _searchStrategy = _searchStrategyFactory.CreateInstance(value, _searchStrategy.Capacity);
            }
        }
    }

    /// <inheritdoc />
    public Ordering OrderingStrategy
    {
        get => _orderingStrategy.Identifier;
        set
        {
            ThrowIfLocked();
            if (value != _orderingStrategy.Identifier)
            {
                _orderingStrategy = _orderingStrategyFactory.CreateInstance(value);
            }
        }
    }

    /// <summary>
    ///     Ensures that the capacity of this instance is at least the specified value.
    /// </summary>
    /// <param name="capacity">The minimum capacity to ensure.</param>
    /// <returns>The new capacity of this instance.</returns>
    public int EnsureCapacity(int capacity) => _searchStrategy.EnsureCapacity(capacity);

    /// <summary>
    ///     Reduces the capacity of this instance to the specified value.
    /// </summary>
    /// <param name="capacity">The required capacity.</param>
    public void TrimExcess(int capacity) => _searchStrategy.TrimExcess(capacity);

    /// <summary>
    ///     Executes a setup step.
    /// </summary>
    /// <remarks>
    ///     This method is executed when <see cref="CurrentSearchState" /> is <see cref="SearchState.Initial" />. After it
    ///     terminates, <see cref="CurrentSearchState" /> is <see cref="SearchState.Safe" /> or
    ///     <see cref="SearchState.Final" />.
    /// </remarks>
    private protected void Setup(ISolvableBinaryCsp<V, D> binaryCsp)
    {
        _searchStrategy.Setup(binaryCsp, _orderingStrategy);
        _setupSteps++;
        UpdateSearchState();
    }

    /// <summary>
    ///     Executes a visiting step.
    /// </summary>
    /// <remarks>
    ///     This method is executed when <see cref="CurrentSearchState" /> is <see cref="SearchState.Safe" />. After it
    ///     terminates, <see cref="CurrentSearchState" /> is <see cref="SearchState.Safe" />, <see cref="SearchState.Unsafe" />
    ///     or <see cref="SearchState.Final" />.
    /// </remarks>
    private protected void VisitNode()
    {
        _searchStrategy.Visit(_orderingStrategy);
        _visitingSteps++;
        UpdateSearchState();
    }

    /// <summary>
    ///     Executes a backtracking step.
    /// </summary>
    /// <remarks>
    ///     This method is executed when <see cref="CurrentSearchState" /> is <see cref="SearchState.Unsafe" />. After it
    ///     terminates, <see cref="CurrentSearchState" /> is <see cref="SearchState.Safe" />, <see cref="SearchState.Unsafe" />
    ///     or <see cref="SearchState.Final" />.
    /// </remarks>
    private protected void Backtrack()
    {
        _searchStrategy.Backtrack();
        _backtrackingSteps++;
        UpdateSearchState();
    }

    /// <summary>
    ///     Creates a <see cref="Result{V,D}" /> object from the search that has terminated.
    /// </summary>
    /// <returns>A new <see cref="Result{V,D}" /> instance.</returns>
    private protected Result<V, D> GetResult() => new()
    {
        Algorithm = new Algorithm(SearchStrategy, OrderingStrategy),
        Assignments = _searchStrategy.GetAssignments(),
        SetupSteps = _setupSteps,
        VisitingSteps = _visitingSteps,
        BacktrackingSteps = _backtrackingSteps,
        TotalSteps = _setupSteps + _visitingSteps + _backtrackingSteps
    };

    /// <summary>
    ///     Resets this instance's data structures so that it can solve a new binary CSP.
    /// </summary>
    private protected void Reset()
    {
        _searchStrategy.Reset();
        _setupSteps = 0;
        _visitingSteps = 0;
        _backtrackingSteps = 0;
        CurrentSearchState = SearchState.Initial;
    }

    /// <summary>
    ///     Gets the current level of the search in the search tree.
    /// </summary>
    /// <returns>
    ///     A 32-bit signed integer in the range [-1,<i>N</i>], where <i>N</i> is the number of binary CSP variables. The
    ///     current level of the search in the search tree.
    /// </returns>
    private protected int GetSearchLevel() => _searchStrategy.SearchLevel;

    /// <summary>
    ///     Gets the leaf level of the search tree.
    /// </summary>
    /// <remarks>The leaf level of a search tree is equal to <i>N</i>, where <i>N</i> is the number of binary CSP variables.</remarks>
    /// <returns>A non-negative 32-bit signed integer. The leaf level of the search tree.</returns>
    private protected int GetSearchTreeLeafLevel() => _searchStrategy.SearchTreeLeafLevel;

    /// <summary>
    ///     Gets the assignment for the search tree node at level (<i>N</i>-1), where <i>N</i> is the current level of the
    ///     search in the search tree.
    /// </summary>
    /// <returns>A new <see cref="Assignment{V,D}" /> instance.</returns>
    private protected Assignment<V, D> GetLatestAssignment() => _searchStrategy.GetLatestAssignment();

    /// <summary>
    ///     Guards against attempting to solve a new binary CSP while this instance is locked.
    /// </summary>
    /// <exception cref="InvalidOperationException">This instance is locked.</exception>
    private protected void ThrowIfLocked()
    {
        if (_locked)
        {
            throw new InvalidOperationException("solving operation is in progress.");
        }
    }

    /// <summary>
    ///     Locks this instance.
    /// </summary>
    private protected void Lock() => _locked = true;

    /// <summary>
    ///     Unlocks this instance.
    /// </summary>
    private protected void Unlock() => _locked = false;

    /// <summary>
    ///     Updates the search state.
    /// </summary>
    private protected void UpdateSearchState() => CurrentSearchState = _searchStrategy.SearchState;
}
