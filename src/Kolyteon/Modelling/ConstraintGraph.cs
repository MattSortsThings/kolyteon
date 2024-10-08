using System.Diagnostics.CodeAnalysis;
using Kolyteon.Modelling.Testing;

namespace Kolyteon.Modelling;

/// <summary>
///     Generic abstract base class for a constraint graph data structure that can model any valid instance of a problem
///     type as a binary CSP.
/// </summary>
/// <typeparam name="TVariable">The binary CSP variable type.</typeparam>
/// <typeparam name="TDomainValue">The binary CSP domain value type.</typeparam>
/// <typeparam name="TProblem">The problem type.</typeparam>
public abstract partial class ConstraintGraph<TVariable, TDomainValue, TProblem> : IBinaryCsp<TVariable, TDomainValue, TProblem>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
    where TProblem : class
{
    private readonly List<List<IAdjacency>> _adjacencyMatrix;
    private readonly List<Edge> _edges;
    private readonly List<Node> _nodes;
    private readonly IAdjacency _nonAdjacentNodesFallback;
    private readonly IAdjacency _sameNodeFallback;

    /// <summary>
    ///     Initializes a new constraint graph instance that is not modelling a problem and has a default initial
    ///     <see cref="Capacity" /> of 0.
    /// </summary>
    /// <remarks>
    ///     When a parameterless constructor is declared in a problem-specific concrete derivative of the
    ///     <see cref="ConstraintGraph{TVariable,TDomainValue,TProblem}" /> class, the constructor in the derivative must call
    ///     the base class parameterless constructor.
    /// </remarks>
    protected ConstraintGraph()
    {
        _adjacencyMatrix = [];
        _edges = [];
        _nodes = [];
        _nonAdjacentNodesFallback = new NonAdjacentNodesFallback(this);
        _sameNodeFallback = new SameNodeFallback(this);
    }

    /// <summary>
    ///     Initializes a new constraint graph instance that is not modelling a problem and has the specified initial
    ///     <see cref="Capacity" />.
    /// </summary>
    /// <remarks>
    ///     When a one-integer-argument constructor is declared in a problem-specific concrete derivative of the
    ///     <see cref="ConstraintGraph{TVariable,TDomainValue,TProblem}" /> class, the constructor in the derivative must call
    ///     the base class one-integer-argument constructor.
    /// </remarks>
    /// <param name="capacity">A non-negative integer. Specifies the initial capacity of the constraint graph instance.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity" /> is negative.</exception>
    protected ConstraintGraph(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity);

        _adjacencyMatrix = new List<List<IAdjacency>>(capacity);
        _edges = new List<Edge>(capacity);
        _nodes = new List<Node>(capacity);
        _sameNodeFallback = new SameNodeFallback(this);
        _nonAdjacentNodesFallback = new NonAdjacentNodesFallback(this);
    }

    private int MaxPossibleConstraints => (_nodes.Count - 1) * _nodes.Count / 2;

    private double SumOfReciprocalsOfEdgeTightnessValues => _edges.Sum(edge => Math.Pow(edge.Tightness, -1));

    /// <summary>
    ///     Gets or sets the total number of binary CSP variables the internal data structures of this instance can hold
    ///     without resizing.
    /// </summary>
    /// <remarks>
    ///     When this virtual property is overridden in a problem-specific concrete derivative of the
    ///     <see cref="ConstraintGraph{TVariable,TDomainValue,TProblem}" /> class, the getter and setter in the derivative must
    ///     invoke the getter and setter in the base class.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <see cref="Capacity" /> is set to a value that is less than the value of
    ///     <see cref="ConstraintGraph{TVariable,TDomainValue,TProblem}.Variables" />.
    /// </exception>
    public virtual int Capacity
    {
        get => _nodes.Capacity;
        set
        {
            _adjacencyMatrix.Capacity = value;
            _nodes.Capacity = value;
            _edges.Capacity = value;
        }
    }

    /// <inheritdoc />
    public int Variables => _nodes.Count;

    /// <inheritdoc />
    public int Constraints => _edges.Count;

    /// <inheritdoc />
    public double ConstraintDensity => Variables <= 1 || Constraints == 0 ? 0 : (double)Constraints / MaxPossibleConstraints;

    /// <inheritdoc />
    public double MeanTightness => Constraints == 0 ? 0 : Constraints / SumOfReciprocalsOfEdgeTightnessValues;

    /// <inheritdoc />
    public bool Adjacent(int indexA, int indexB)
    {
        try
        {
            return TryCheckAdjacent(indexA, indexB);
        }
        catch (ArgumentOutOfRangeException)
        {
            throw new VariableIndexOutOfRangeException();
        }
    }

    /// <inheritdoc />
    public bool Consistent(IAssignment assignmentA, IAssignment assignmentB)
    {
        ArgumentNullException.ThrowIfNull(assignmentA);
        ArgumentNullException.ThrowIfNull(assignmentB);

        try
        {
            return TryCheckConsistent(assignmentA, assignmentB);
        }
        catch (ArgumentOutOfRangeException)
        {
            throw new VariableIndexOutOfRangeException();
        }
    }

    /// <inheritdoc />
    public TVariable GetVariableAt(int index)
    {
        try
        {
            return TryGetVariableAt(index);
        }
        catch (ArgumentOutOfRangeException)
        {
            throw new VariableIndexOutOfRangeException();
        }
    }

    /// <inheritdoc />
    public IReadOnlyList<TDomainValue> GetDomainAt(int index)
    {
        try
        {
            return TryGetDomainAt(index);
        }
        catch (ArgumentOutOfRangeException)
        {
            throw new VariableIndexOutOfRangeException();
        }
    }

    /// <inheritdoc />
    public int GetDomainSizeAt(int index)
    {
        try
        {
            return TryGetDomainSizeAt(index);
        }
        catch (ArgumentOutOfRangeException)
        {
            throw new VariableIndexOutOfRangeException();
        }
    }

    /// <inheritdoc />
    public int GetDegreeAt(int index)
    {
        try
        {
            return TryGetDegreeAt(index);
        }
        catch (ArgumentOutOfRangeException)
        {
            throw new VariableIndexOutOfRangeException();
        }
    }

    /// <inheritdoc />
    public double GetSumTightnessAt(int index)
    {
        try
        {
            return TryGetSumTightnessAt(index);
        }
        catch (ArgumentOutOfRangeException)
        {
            throw new VariableIndexOutOfRangeException();
        }
    }

    /// <inheritdoc />
    public Assignment<TVariable, TDomainValue> Map(IAssignment assignment)
    {
        ArgumentNullException.ThrowIfNull(assignment);

        try
        {
            return TryMap(assignment);
        }
        catch (ArgumentOutOfRangeException)
        {
            throw new VariableIndexOutOfRangeException();
        }
    }

    /// <inheritdoc />
    public void Model(TProblem problem)
    {
        ArgumentNullException.ThrowIfNull(problem);
        ThrowIfModellingProblem();
        PopulateProblemData(problem);
        PopulateNodes();
        InitializeAdjacencyMatrix();
        PopulateEdgesAndAdjacencyMatrix();
    }

    /// <inheritdoc />
    public void Clear()
    {
        _adjacencyMatrix.Clear();
        _nodes.Clear();
        _edges.Clear();
        ClearProblemData();
    }

    /// <summary>
    ///     Generates a sequence of data structures describing each constraint graph node, in variable index order.
    /// </summary>
    /// <remarks>
    ///     This method can be used for unit testing problem-specific derivatives of the
    ///     <see cref="ConstraintGraph{TVariable,TDomainValue,TProblem}" /> class, to verify that the <see cref="Model" />
    ///     method populates the constraint graph with the correct nodes for a given problem.
    /// </remarks>
    /// <returns>A sequence of <see cref="ConstraintGraphNodeDatum{TVariable,TDomainValue}" /> objects.</returns>
    protected internal IEnumerable<ConstraintGraphNodeDatum<TVariable, TDomainValue>> GetNodeData() =>
        _nodes.Select(node => node.ToConstraintGraphNodeDatum());

    /// <summary>
    ///     Generates a sequence of data structures describing each undirected edge in the constraint graph, in variable index
    ///     order.
    /// </summary>
    /// <remarks>
    ///     This method can be used for unit testing problem-specific derivatives of the
    ///     <see cref="ConstraintGraph{TVariable,TDomainValue,TProblem}" /> class, to verify that the <see cref="Model" />
    ///     method populates the constraint graph with the correct edges for a given problem.
    /// </remarks>
    /// <returns>A sequence of <see cref="ConstraintGraphEdgeDatum{TVariable,TDomainValue}" /> objects.</returns>
    protected internal IEnumerable<ConstraintGraphEdgeDatum<TVariable, TDomainValue>> GetEdgeData() =>
        _edges.Select(edge => edge.ToConstraintGraphEdgeDatum());

    /// <summary>
    ///     Populates this instance's internal data structures with all necessary data from the specified problem to model it
    ///     as a binary CSP.
    /// </summary>
    /// <remarks>
    ///     This method must be overridden in any problem-specific derivative of the
    ///     <see cref="ConstraintGraph{TVariable, TDomainValue, TProblem}" /> abstract base class.
    /// </remarks>
    /// <param name="problem">The problem to be modelled.</param>
    protected abstract void PopulateProblemData(TProblem problem);

    /// <summary>
    ///     Enumerates the binary CSP variables for the problem being modelled.
    /// </summary>
    /// <remarks>
    ///     This method must be overridden in any problem-specific derivative of the
    ///     <see cref="ConstraintGraph{TVariable, TDomainValue, TProblem}" /> abstract base class. The variables can be
    ///     returned in any order.
    /// </remarks>
    /// <returns>A finite sequence of unique values of the <typeparamref name="TVariable" /> type.</returns>
    protected abstract IEnumerable<TVariable> GetVariables();

    /// <summary>
    ///     Enumerates the domain values for the present binary CSP variable.
    /// </summary>
    /// <remarks>
    ///     This method must be overridden in any problem-specific derivative of the
    ///     <see cref="ConstraintGraph{TVariable, TDomainValue, TProblem}" /> abstract base class. The domain values can be
    ///     returned in any order.
    /// </remarks>
    /// <param name="presentVariable">The present binary CSP variable in the problem being modelled.</param>
    /// <returns>A finite sequence of unique values of the <typeparamref name="TDomainValue" /> type.</returns>
    protected abstract IEnumerable<TDomainValue> GetDomainValues(TVariable presentVariable);

    /// <summary>
    ///     Gets the binary constraint predicate for the two ordered binary CSP variables, if they are theoretically adjacent.
    /// </summary>
    /// <remarks>
    ///     This method must be overridden in any problem-specific derivative of the
    ///     <see cref="ConstraintGraph{TVariable, TDomainValue, TProblem}" /> abstract base class.
    /// </remarks>
    /// <param name="firstVariable">The first binary CSP variable in the problem being modelled.</param>
    /// <param name="secondVariable">The second binary CSP variable in the problem being modelled.</param>
    /// <param name="binaryPredicate">
    ///     When this method returns, contains the binary constraint predicate for the theoretical
    ///     binary constraint in which the two variables participate; or <see langword="null" /> if they are not theoretically
    ///     adjacent. This parameter is passed uninitialized.
    /// </param>
    /// <returns>
    ///     <see langword="true" /> if the <paramref name="firstVariable" /> and <paramref name="secondVariable" />
    ///     parameters are theoretically adjacent given the rules of the modelled problem type; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    protected abstract bool TryGetBinaryPredicate(TVariable firstVariable,
        TVariable secondVariable,
        [NotNullWhen(true)] out Func<TDomainValue, TDomainValue, bool>? binaryPredicate);

    /// <summary>
    ///     Clears this instance's internal data structures of all data that was added during the
    ///     <see cref="PopulateProblemData(TProblem)" /> method, so that this instance is ready to model another problem.
    /// </summary>
    /// <remarks>
    ///     This method must be overridden in any problem-specific derivative of the
    ///     <see cref="ConstraintGraph{TVariable, TDomainValue, TProblem}" /> abstract base class.
    /// </remarks>
    protected abstract void ClearProblemData();

    private int TryGetDegreeAt(int index) => _nodes[index].Degree;

    private double TryGetSumTightnessAt(int index) => _nodes[index].SumTightness;

    private bool TryCheckAdjacent(int indexA, int indexB) => _adjacencyMatrix[indexA][indexB].Adjacent;

    private bool TryCheckConsistent(IAssignment assignmentA, IAssignment assignmentB) =>
        _adjacencyMatrix[assignmentA.VariableIndex][assignmentB.VariableIndex].Consistent(assignmentA, assignmentB);

    private bool TryEdge(int firstIndex, int secondIndex, [NotNullWhen(true)] out Edge? edge)
    {
        edge = null;

        Node firstNode = _nodes[firstIndex];
        Node secondNode = _nodes[secondIndex];

        if (!TryGetBinaryPredicate(firstNode.Variable, secondNode.Variable,
                out Func<TDomainValue, TDomainValue, bool>? predicate))
        {
            return false;
        }

        edge = Edge.CreateIfProvenAdjacent(firstNode, secondNode, predicate);

        return edge is not null;
    }

    private void PopulateNodes() => _nodes.AddRange(CreateNodes());

    private IEnumerable<Node> CreateNodes() => from presentVariable in GetVariables().OrderBy(variable => variable)
        let domain = GetDomainValues(presentVariable).Distinct().OrderBy(domainValue => domainValue).ToArray()
        select new Node { Variable = presentVariable, Domain = domain };

    private void PopulateEdgesAndAdjacencyMatrix()
    {
        for (int i = 0; i < _nodes.Count; i++)
        {
            for (int j = i + 1; j < _nodes.Count; j++)
            {
                if (!TryEdge(i, j, out Edge? edge))
                {
                    continue;
                }

                _edges.Add(edge);
                (ForwardEdge forward, ReversedEdge reversed) = edge.ToDirectedEdgePair();
                _adjacencyMatrix[i][j] = forward;
                _adjacencyMatrix[j][i] = reversed;
            }
        }
    }

    private void InitializeAdjacencyMatrix()
    {
        for (int i = 0; i < _nodes.Count; i++)
        {
            List<IAdjacency> row = Enumerable.Repeat(_nonAdjacentNodesFallback, _nodes.Count).ToList();
            row[i] = _sameNodeFallback;
            _adjacencyMatrix.Add(row);
        }
    }

    private TVariable TryGetVariableAt(int index) => _nodes[index].Variable;

    private TDomainValue[] TryGetDomainAt(int index) => _nodes[index].Domain;

    private int TryGetDomainSizeAt(int index) => _nodes[index].DomainSize;

    private Assignment<TVariable, TDomainValue> TryMap(IAssignment assignment) =>
        _nodes[assignment.VariableIndex].Map(assignment);

    private void ThrowIfModellingProblem()
    {
        if (_nodes.Count > 0)
        {
            throw new InvalidOperationException("Already modelling a problem.");
        }
    }
}
