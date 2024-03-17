using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Mjt85.Kolyteon.Modelling.Internals;

namespace Mjt85.Kolyteon.Modelling;

/// <summary>
///     Abstract base class representing a testable, solvable, measurable, generic binary CSP, that is queryable by index
///     and can model any instance of a problem type.
/// </summary>
/// <remarks>
///     To create a problem-specific derivative of this abstract base class:
///     <list type="number">
///         <item>Identify a class to represent the problem. This will be generic type <typeparamref name="P" />.</item>
///         <item>Identify a struct type for the binary CSP variables. This will be generic type <typeparamref name="V" />.</item>
///         <item>
///             Identify a struct type for the binary CSP domain values. This will be generic type
///             <typeparamref name="D" />.
///         </item>
///         <item>
///             Define a problem-specific derivative of <see cref="BinaryCsp{P,V,D}" />, parametrized over the three
///             generic types.
///         </item>
///         <item>Implement the inherited constructor. The implementation must call the base class constructor.</item>
///         <item>
///             Implement all the private protected abstract methods to define the problem data and how it is to be
///             modelled as a binary CSP.
///         </item>
///     </list>
/// </remarks>
/// <typeparam name="P">The modelled problem type.</typeparam>
/// <typeparam name="V">The binary CSP variable.</typeparam>
/// <typeparam name="D">The binary CSP domain value type.</typeparam>
public abstract class BinaryCsp<P, V, D> : ITestableBinaryCsp<P, V, D>
    where P : class
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    private const string VariableIndexOutOfRange =
        "Variable index was out of range. Must be non-negative and less than the number of binary CSP variables.";

    private const string DomainValueIndexOutOfRange =
        "Domain value index was out of range. " +
        "Must be non-negative and less than the number of values in the binary CSP variable's domain.";

    private static readonly AlwaysConsistentPredicate AlwaysConsistent = new();

    private readonly List<List<Edge?>> _edgeMatrix;
    private readonly List<Node> _nodes;

    private int _totalAdjacentAssignmentPairs;
    private int _totalIllegalAssignmentPairs;

    /// <summary>
    ///     Initializes a new <see cref="BinaryCsp{P,V,D}" /> instance that is not modelling a problem and has the specified
    ///     initial capacity.
    /// </summary>
    /// <remarks>
    ///     A problem-specific derivative must implement this constructor. The implementation must call the base class
    ///     constructor.
    /// </remarks>
    /// <param name="capacity">
    ///     The number of binary CSP variables the new <see cref="BinaryCsp{P,V,D}" /> instance can initially store.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity" /> is negative.</exception>
    protected BinaryCsp(int capacity)
    {
        if (capacity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Value must not be negative.");
        }

        _nodes = new List<Node>(capacity);
        _edgeMatrix = new List<List<Edge?>>(capacity);
    }

    /// <summary>
    ///     Gets the total number of binary CSP variables this instance can hold before resizing is required.
    /// </summary>
    /// <value>
    ///     A non-negative 32-bit signed integer. The total number of binary CSP variables this instance can hold before
    ///     resizing is required.
    /// </value>
    public int Capacity => _nodes.Capacity;

    /// <summary>
    ///     Gets the binary predicate that is to be used for a pair of binary CSP variables that are non-adjacent (i.e. are
    ///     known not to participate in a binary constraint).
    /// </summary>
    protected IBinaryPredicate<D> NotAdjacent => AlwaysConsistent;

    /// <inheritdoc />
    public int Variables => _nodes.Count;

    /// <inheritdoc />
    public int Constraints { get; private set; }

    /// <inheritdoc />
    public double ConstraintDensity => _nodes.Count > 1
        ? Constraints / (_nodes.Count * (_nodes.Count - 1) * 0.5)
        : 0;

    /// <inheritdoc />
    public double ConstraintTightness => _totalAdjacentAssignmentPairs > 0
        ? (double)_totalIllegalAssignmentPairs / _totalAdjacentAssignmentPairs
        : 0;

    /// <inheritdoc />
    public ProblemMetrics GetProblemMetrics() => new()
    {
        Variables = Variables,
        Constraints = Constraints,
        ConstraintDensity = ConstraintDensity,
        ConstraintTightness = ConstraintTightness
    };

    /// <inheritdoc />
    public DomainSizeStatistics GetDomainSizeStatistics() => _nodes.Count > 0
        ? QueryDomainSizeStatistics()
        : new DomainSizeStatistics();

    /// <inheritdoc />
    public DegreeStatistics GetDegreeStatistics() => _nodes.Count > 0
        ? QueryDegreeStatistics()
        : new DegreeStatistics();

    /// <inheritdoc />
    public SumTightnessStatistics GetSumTightnessStatistics() => _nodes.Count > 0
        ? QuerySumTightnessStatistics()
        : new SumTightnessStatistics();

    /// <inheritdoc />
    public bool Adjacent(int indexA, int indexB)
    {
        try
        {
            return TryCheckIfAdjacent(indexA, indexB);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw new ArgumentOutOfRangeException(VariableIndexOutOfRange, ex);
        }
    }

    /// <inheritdoc />
    public bool Consistent(IAssignment assignmentA, IAssignment assignmentB)
    {
        _ = assignmentA ?? throw new ArgumentNullException(nameof(assignmentA));
        _ = assignmentB ?? throw new ArgumentNullException(nameof(assignmentB));

        try
        {
            return TryCheckIfAssignmentsAreConsistent(assignmentA, assignmentB);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw new ArgumentOutOfRangeException(VariableIndexOutOfRange, ex);
        }
        catch (IndexOutOfRangeException ex)
        {
            throw new ArgumentOutOfRangeException(DomainValueIndexOutOfRange, ex);
        }
    }

    /// <inheritdoc />
    public Assignment<V, D> Map(IAssignment assignment)
    {
        _ = assignment ?? throw new ArgumentNullException(nameof(assignment));

        try
        {
            return TryGetMappedAssignment(assignment);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw new ArgumentOutOfRangeException(VariableIndexOutOfRange, ex);
        }
        catch (IndexOutOfRangeException ex)
        {
            throw new ArgumentOutOfRangeException(DomainValueIndexOutOfRange, ex);
        }
    }

    /// <inheritdoc />
    public V GetVariableAt(int index)
    {
        try
        {
            return TryGetVariableAt(index);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw new ArgumentOutOfRangeException(VariableIndexOutOfRange, ex);
        }
    }

    /// <inheritdoc />
    public IReadOnlyList<D> GetDomainAt(int index)
    {
        try
        {
            return TryGetDomainAt(index);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw new ArgumentOutOfRangeException(VariableIndexOutOfRange, ex);
        }
    }

    /// <inheritdoc />
    public int GetDomainSizeAt(int index)
    {
        try
        {
            return TryGetDomainSizeAt(index);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw new ArgumentOutOfRangeException(VariableIndexOutOfRange, ex);
        }
    }

    /// <inheritdoc />
    public int GetDegreeAt(int index)
    {
        try
        {
            return TryGetDegreeAt(index);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw new ArgumentOutOfRangeException(VariableIndexOutOfRange, ex);
        }
    }

    /// <inheritdoc />
    public double GetSumTightnessAt(int index)
    {
        try
        {
            return TryGetSumTightnessAt(index);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw new ArgumentOutOfRangeException(VariableIndexOutOfRange, ex);
        }
    }

    /// <inheritdoc />
    public void Model(P problem)
    {
        _ = problem ?? throw new ArgumentNullException(nameof(problem));
        Guard.AgainstOverwritingNonEmptyBinaryCsp(this);
        try
        {
            TryModel(problem);
        }
        catch (InvalidOperationException)
        {
            Clear();

            throw;
        }
    }

    /// <inheritdoc />
    public void Clear()
    {
        _nodes.Clear();
        _edgeMatrix.Clear();
        ClearProblemData();
        Constraints = 0;
        _totalAdjacentAssignmentPairs = 0;
        _totalIllegalAssignmentPairs = 0;
    }

    /// <inheritdoc />
    public IEnumerable<V> GetAllVariables() => from node in _nodes select node.Variable;

    /// <inheritdoc />
    public IEnumerable<IReadOnlyList<D>> GetAllDomains() => from node in _nodes select node.Domain;

    /// <inheritdoc />
    public IEnumerable<Pair<V>> GetAllAdjacentVariables()
    {
        var nodes = _nodes.Count;

        return from i in Enumerable.Range(0, nodes)
            from j in Enumerable.Range(0, nodes).Skip(i + 1)
            where TryCheckIfAdjacent(i, j)
            select new Pair<V>(TryGetVariableAt(i), TryGetVariableAt(j));
    }

    /// <summary>
    ///     Ensures that the capacity of this instance is at least the specified capacity.
    /// </summary>
    /// <remarks>
    ///     If a problem-specific derivative overrides this virtual method, the overriding implementation should in turn
    ///     invoke this method on the base class.
    /// </remarks>
    /// <returns>A non-negative 32-bit signed integer. The new capacity of this instance.</returns>
    protected virtual int EnsureCapacity(int capacity)
    {
        var newCapacity = _nodes.EnsureCapacity(capacity);
        _ = _edgeMatrix.EnsureCapacity(capacity);

        return newCapacity;
    }

    /// <summary>
    ///     Sets the capacity of this instance to the actual number of binary CSP variables, if that number is less than a
    ///     threshold value.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This method can be used to reduce overhead if this instance is modelling a problem and it is known that it
    ///         will not need to model a larger problem.
    ///     </para>
    ///     <para>
    ///         If a problem-specific derivative overrides this virtual method, the overriding implementation should in turn
    ///         invoke this method on the base class.
    ///     </para>
    /// </remarks>
    protected virtual void TrimExcess()
    {
        _nodes.TrimExcess();
        _edgeMatrix.TrimExcess();
    }

    /// <summary>
    ///     Populates the problem data from the given problem, which will be used to model the binary CSP.
    /// </summary>
    /// <param name="problem">The problem to be modelled.</param>
    private protected abstract void PopulateProblemData(P problem);

    /// <summary>
    ///     Clears the problem data so that this instance can model another problem.
    /// </summary>
    private protected abstract void ClearProblemData();

    /// <summary>
    ///     Uses the problem data to generate the binary CSP variables.
    /// </summary>
    /// <remarks>
    ///     <para>During invocation of the <see cref="Model" /> method, this method is invoked exactly once.</para>
    ///     <para>
    ///         The binary CSP variables returned by this method will be sorted using the <see cref="IComparable{T}" />
    ///         implementation of the <typeparamref name="V" /> type before being added to the binary CSP.
    ///     </para>
    /// </remarks>
    /// <returns>An enumerable containing all the binary CSP variables.</returns>
    private protected abstract IEnumerable<V> GetVariables();

    /// <summary>
    ///     Uses the problem data to generate the domain of the specified binary CSP variable.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         During invocation of the <see cref="Model" /> method, this method is invoked once for the variable at index
    ///         <i>i</i>, for all <i>i</i> &#8712; [0,<i>n</i>), where <i>n</i> is the number of binary CSP variables.
    ///     </para>
    ///     <para>
    ///         The binary CSP domain values returned by this method will be sorted using the <see cref="IComparable{T}" />
    ///         implementation of the <typeparamref name="D" /> type before being added to the binary CSP.
    ///     </para>
    /// </remarks>
    /// <param name="variable">The binary CSP variable.</param>
    /// <returns>An enumerable containing all the domain values for the <paramref name="variable" /> parameter.</returns>
    private protected abstract IEnumerable<D> GetDomainOf(V variable);

    /// <summary>
    ///     Uses the problem data to find the binary predicate for the two specified binary CSP variables, which are ordered by
    ///     index.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         During invocation of the <see cref="Model" /> method, this method is invoked once for the pair of variables at
    ///         index <i>i</i> and index <i>j</i>, for all <i>i</i> &#8712; [0,<i>n</i>), for all <i>j</i> &#8712; (<i>i</i>,
    ///         <i>n</i>), where <i>n</i> is the number of binary CSP variables.
    ///     </para>
    ///     <para>
    ///         If the two variables are notionally adjacent given the problem data, the method should return the binary
    ///         predicate for the two variables. This will be tested against the Cartesian product of the variables' domains. A
    ///         constraint is only added to the binary CSP if there is at least one inconsistent domain value pair for the
    ///         predicate.
    ///     </para>
    ///     <para>
    ///         If the two variables are not notionally adjacent given the problem data, the method should return the
    ///         <see cref="NotAdjacent" /> property. No constraint will be added to the binary CSP.
    ///     </para>
    /// </remarks>
    /// <param name="variable1">The first of the two ordered binary CSP variables.</param>
    /// <param name="variable2">The second of the two ordered binary CSP variables.</param>
    /// <returns>The binary predicate for the two variables.</returns>
    private protected abstract IBinaryPredicate<D> GetBinaryPredicateFor(V variable1, V variable2);

    private DomainSizeStatistics QueryDomainSizeStatistics()
    {
        IEnumerable<int> query = from node in _nodes select node.DomainSize;

        var aggregator = query.Aggregate(new
        {
            MinValue = int.MaxValue,
            SumValues = 0.0,
            Count = 0,
            MaxValue = int.MinValue,
            DistinctValues = new HashSet<int>()
        }, (accumulator, val) =>
        {
            accumulator.DistinctValues.Add(val);

            return new
            {
                MinValue = Math.Min(accumulator.MinValue, val),
                SumValues = accumulator.SumValues + val,
                Count = accumulator.Count + 1,
                MaxValue = Math.Max(accumulator.MaxValue, val),
                accumulator.DistinctValues
            };
        });

        return new DomainSizeStatistics
        {
            MinimumValue = aggregator.MinValue,
            MeanValue = aggregator.SumValues / aggregator.Count,
            MaximumValue = aggregator.MaxValue,
            DistinctValues = aggregator.DistinctValues.Count
        };
    }

    private DegreeStatistics QueryDegreeStatistics()
    {
        IEnumerable<int> query = from node in _nodes select node.Degree;
        var aggregator = query.Aggregate(new
        {
            MinValue = int.MaxValue,
            SumValues = 0.0,
            Count = 0,
            MaxValue = int.MinValue,
            DistinctValues = new HashSet<int>()
        }, (accumulator, val) =>
        {
            accumulator.DistinctValues.Add(val);

            return new
            {
                MinValue = Math.Min(accumulator.MinValue, val),
                SumValues = accumulator.SumValues + val,
                Count = accumulator.Count + 1,
                MaxValue = Math.Max(accumulator.MaxValue, val),
                accumulator.DistinctValues
            };
        });

        return new DegreeStatistics
        {
            MinimumValue = aggregator.MinValue,
            MeanValue = aggregator.SumValues / aggregator.Count,
            MaximumValue = aggregator.MaxValue,
            DistinctValues = aggregator.DistinctValues.Count
        };
    }

    private SumTightnessStatistics QuerySumTightnessStatistics()
    {
        IEnumerable<double> query = from node in _nodes select node.SumTightness;
        var aggregator = query.Aggregate(new
        {
            MinValue = double.MaxValue,
            SumValues = 0.0,
            Count = 0,
            MaxValue = double.MinValue,
            DistinctValues = new HashSet<decimal>()
        }, (accumulator, val) =>
        {
            accumulator.DistinctValues.Add(new decimal(Math.Round(val, 6)));

            return new
            {
                MinValue = Math.Min(accumulator.MinValue, val),
                SumValues = accumulator.SumValues + val,
                Count = accumulator.Count + 1,
                MaxValue = Math.Max(accumulator.MaxValue, val),
                accumulator.DistinctValues
            };
        });

        return new SumTightnessStatistics
        {
            MinimumValue = aggregator.MinValue,
            MeanValue = aggregator.SumValues / aggregator.Count,
            MaximumValue = aggregator.MaxValue,
            DistinctValues = aggregator.DistinctValues.Count
        };
    }

    private bool TryCheckIfAdjacent(int variableIndexA, int variableIndexB) =>
        _edgeMatrix[variableIndexA][variableIndexB] is not null || variableIndexA == variableIndexB;

    private bool TryCheckIfAssignmentsAreConsistent(IAssignment assignmentA, IAssignment assignmentB)
    {
        Edge? edge = _edgeMatrix[assignmentA.VariableIndex][assignmentB.VariableIndex];

        return edge?.Consistent(assignmentA.DomainValueIndex, assignmentB.DomainValueIndex) ??
               ApplyFallbackConsistencyCheck(assignmentA, assignmentB);
    }

    private bool ApplyFallbackConsistencyCheck(IAssignment assignmentA, IAssignment assignmentB)
    {
        (V variableA, D domainValueA) = TryGetMappedAssignment(assignmentA);
        (V variableB, D domainValueB) = TryGetMappedAssignment(assignmentB);

        return !variableA.Equals(variableB) || domainValueA.Equals(domainValueB);
    }

    private Assignment<V, D> TryGetMappedAssignment(IAssignment assignment)
    {
        Node node = _nodes[assignment.VariableIndex];

        return new Assignment<V, D>(node.Variable, node.Domain[assignment.DomainValueIndex]);
    }

    private V TryGetVariableAt(int index) => _nodes[index].Variable;

    private D[] TryGetDomainAt(int index) => _nodes[index].Domain;

    private int TryGetDomainSizeAt(int index) => _nodes[index].DomainSize;

    private int TryGetDegreeAt(int index) => _nodes[index].Degree;

    private double TryGetSumTightnessAt(int index) => _nodes[index].SumTightness;

    private void TryModel(P problem)
    {
        PopulateProblemData(problem);
        PopulateNodes();
        SetEdgeMatrixDimensions();
        PopulateEdgeMatrix();
        Guard.AgainstBinaryCspWithZeroVariables(this);
    }

    private void PopulateNodes()
    {
        IEnumerable<Node> nodesQuery = from v in GetVariables().Distinct()
            orderby v
            select new Node
            {
                Variable = v,
                Domain = (from d in GetDomainOf(v).Distinct() orderby d select d).ToArray()
            };

        _nodes.AddRange(nodesQuery);
    }

    private void SetEdgeMatrixDimensions()
    {
        var nodes = _nodes.Count;
        IEnumerable<List<Edge?>> edgeMatrixQuery = Enumerable.Range(0, nodes)
            .Select(_ => Enumerable.Range(0, nodes).Select<int, Edge?>(_ => null).ToList());

        _edgeMatrix.AddRange(edgeMatrixQuery);
    }

    private void PopulateEdgeMatrix()
    {
        var nodesCount = _nodes.Count;
        for (var fromNodeIndex = 0; fromNodeIndex < nodesCount; fromNodeIndex++)
        {
            for (var toNodeIndex = fromNodeIndex + 1; toNodeIndex < nodesCount; toNodeIndex++)
            {
                AddEdgesIfAnyForNodesAtIndexes(fromNodeIndex, toNodeIndex);
            }
        }
    }

    private void AddEdgesIfAnyForNodesAtIndexes(int fromNodeIndex, int toNodeIndex)
    {
        Node fromNode = _nodes[fromNodeIndex];
        Node toNode = _nodes[toNodeIndex];

        IBinaryPredicate<D> predicate = GetBinaryPredicateFor(fromNode.Variable, toNode.Variable);

        if (ReferenceEquals(NotAdjacent, predicate) ||
            !TryCreateOutgoingEdge(fromNode.Domain, toNode.Domain, predicate, out ForwardEdge? outgoingEdge))
        {
            return;
        }

        _edgeMatrix[fromNodeIndex][toNodeIndex] = outgoingEdge;
        _edgeMatrix[toNodeIndex][fromNodeIndex] = outgoingEdge.GetReversed();
        fromNode.UpdateAdjacencyMeasures(outgoingEdge);
        toNode.UpdateAdjacencyMeasures(outgoingEdge);
        _totalIllegalAssignmentPairs += outgoingEdge.IllegalAssignmentPairs;
        _totalAdjacentAssignmentPairs += outgoingEdge.CartesianProductSize;
        Constraints++;
    }

    private static bool TryCreateOutgoingEdge(IReadOnlyList<D> fromNodeDomain,
        IReadOnlyList<D> toNodeDomain,
        IBinaryPredicate<D> predicate,
        [NotNullWhen(true)] out ForwardEdge? edge)
    {
        var fromDomainSize = fromNodeDomain.Count;
        var toDomainSize = toNodeDomain.Count;
        var inconsistent = 0;
        var i = 0;
        var cartesianProductSize = fromDomainSize * toDomainSize;

        for (; inconsistent == 0 && i < cartesianProductSize; i++)
        {
            D f = fromNodeDomain[i / toDomainSize];
            D t = toNodeDomain[i % toDomainSize];
            if (!predicate.CanAssign(in f, in t))
            {
                inconsistent++;
            }
        }

        if (inconsistent == 0)
        {
            edge = null;

            return false;
        }

        BitArray array = new(cartesianProductSize, true) { [i - 1] = false };

        for (; i < cartesianProductSize; i++)
        {
            D f = fromNodeDomain[i / toDomainSize];
            D t = toNodeDomain[i % toDomainSize];
            if (predicate.CanAssign(in f, in t))
            {
                continue;
            }

            inconsistent++;
            array[i] = false;
        }

        edge = new ForwardEdge
        {
            FromNodeDomainSize = fromDomainSize,
            ToNodeDomainSize = toDomainSize,
            ConsistencyMatrix = array,
            IllegalAssignmentPairs = inconsistent
        };

        return true;
    }

    private record Node
    {
        public V Variable { get; init; }

        public D[] Domain { get; init; } = Array.Empty<D>();

        public int Degree { get; private set; }

        public int DomainSize => Domain.Length;

        public double SumTightness { get; private set; }

        public void UpdateAdjacencyMeasures(Edge edge)
        {
            Degree++;
            SumTightness += (double)edge.IllegalAssignmentPairs / edge.CartesianProductSize;
        }
    }

    private abstract record Edge
    {
        public BitArray ConsistencyMatrix { get; init; } = null!;

        public int FromNodeDomainSize { get; init; }

        public int ToNodeDomainSize { get; init; }

        public int IllegalAssignmentPairs { get; init; }

        public int CartesianProductSize => ConsistencyMatrix.Length;

        public abstract bool Consistent(int fromNodeDomainValueIndex, int toNodeDomainValueIndex);
    }

    private sealed record ForwardEdge : Edge
    {
        public override bool Consistent(int fromNodeDomainValueIndex, int toNodeDomainValueIndex)
        {
            var initialOffset = fromNodeDomainValueIndex >= 0 && fromNodeDomainValueIndex < FromNodeDomainSize
                ? ToNodeDomainSize * fromNodeDomainValueIndex
                : throw new IndexOutOfRangeException(nameof(fromNodeDomainValueIndex));

            var finalOffset = toNodeDomainValueIndex >= 0 && toNodeDomainValueIndex < ToNodeDomainSize
                ? initialOffset + toNodeDomainValueIndex
                : throw new IndexOutOfRangeException(nameof(toNodeDomainValueIndex));

            return ConsistencyMatrix[finalOffset];
        }

        public ReversedEdge GetReversed() => new()
        {
            FromNodeDomainSize = ToNodeDomainSize,
            ToNodeDomainSize = FromNodeDomainSize,
            IllegalAssignmentPairs = IllegalAssignmentPairs,
            ConsistencyMatrix = ConsistencyMatrix
        };
    }

    private sealed record ReversedEdge : Edge
    {
        public override bool Consistent(int fromNodeDomainValueIndex, int toNodeDomainValueIndex)
        {
            var initialOffset = toNodeDomainValueIndex >= 0 && toNodeDomainValueIndex < ToNodeDomainSize
                ? FromNodeDomainSize * toNodeDomainValueIndex
                : throw new IndexOutOfRangeException(nameof(toNodeDomainValueIndex));

            var finalOffset = fromNodeDomainValueIndex >= 0 && fromNodeDomainValueIndex < FromNodeDomainSize
                ? initialOffset + fromNodeDomainValueIndex
                : throw new IndexOutOfRangeException(nameof(fromNodeDomainValueIndex));

            return ConsistencyMatrix[finalOffset];
        }
    }

    private sealed class AlwaysConsistentPredicate : IBinaryPredicate<D>
    {
        public bool CanAssign(in D domainValue1, in D domainValue2) => true;
    }
}
