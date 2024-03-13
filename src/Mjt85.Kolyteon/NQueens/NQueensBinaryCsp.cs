using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.NQueens;

/// <summary>
///     Can model any <i>N</i>-Queens puzzle as a binary CSP.
/// </summary>
public sealed class NQueensBinaryCsp : BinaryCsp<NQueensPuzzle, int, Queen>
{
    private static readonly CannotCapturePredicate CannotCapture = new();

    private int _n;

    /// <summary>
    ///     Initializes a new <see cref="NQueensBinaryCsp" /> instance that is not modelling a problem and has the specified
    ///     initial capacity.
    /// </summary>
    /// <param name="capacity">
    ///     The number of binary CSP variables the new <see cref="NQueensBinaryCsp" /> instance can initially store.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity" /> is negative.</exception>
    public NQueensBinaryCsp(int capacity) : base(capacity)
    {
    }

    /// <inheritdoc />
    protected override void PopulateProblemData(NQueensPuzzle problem) => _n = problem.N;

    /// <inheritdoc />
    protected override void ClearProblemData() => _n = default;

    /// <inheritdoc />
    /// <remarks>
    ///     In a binary CSP modelling an <i>N</i>-Queens puzzle, the variables are the <i>N</i> consecutive column indexes
    ///     from 0 to (<i>N</i>-1).
    /// </remarks>
    protected override IEnumerable<int> GetVariables() => Enumerable.Range(0, _n);

    /// <inheritdoc />
    /// <remarks>
    ///     In a binary CSP modelling an <i>N</i>-Queens puzzle, for a column index variable <i>V</i>, its domain is the
    ///     set of possible queens for all the squares in the column.
    /// </remarks>
    protected override IEnumerable<Queen> GetDomainOf(int variable)
    {
        var column = variable;

        return from row in Enumerable.Range(0, _n) select new Queen(column, row);
    }

    /// <inheritdoc />
    /// <remarks>
    ///     In a binary CSP modelling an <i>N</i>-Queens puzzle, every pair of column index variables participates in a
    ///     binary constraint. The predicate states that the two column indexes must be assigned non-capturing queens.
    /// </remarks>
    protected override IBinaryPredicate<Queen> GetBinaryPredicateFor(int variable1, int variable2) => CannotCapture;

    /// <summary>
    ///     Defines the binary constraint predicate that two queens cannot capture each other.
    /// </summary>
    private sealed class CannotCapturePredicate : IBinaryPredicate<Queen>
    {
        public bool CanAssign(in Queen domainValue1, in Queen domainValue2) => !domainValue1.CanCapture(domainValue2);
    }
}
