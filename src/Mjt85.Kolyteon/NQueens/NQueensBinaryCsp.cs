using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.NQueens;

/// <summary>
///     Can represent any <i>N</i>-Queens puzzle as a binary CSP.
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
    ///     The maximum number of binary CSP variables the new <see cref="NQueensBinaryCsp" /> can initially
    ///     store without needing to resize its internal data structures.
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
    ///     In the <i>N</i>-Queens binary CSP model, the variables are the set of all column index values from 0 to
    ///     (<i>N</i>-1) inclusive.
    /// </remarks>
    protected override IEnumerable<int> GetVariables() => Enumerable.Range(0, _n);

    /// <inheritdoc />
    /// <remarks>
    ///     In the <i>N</i>-Queens binary CSP model, the domain of a column index variable is the set of all possible
    ///     queens occupying every square in the column.
    /// </remarks>
    protected override IEnumerable<Queen> GetDomainOf(int variable)
    {
        var column = variable;

        return from row in Enumerable.Range(0, _n) select new Queen(column, row);
    }

    /// <inheritdoc />
    /// <remarks>
    ///     In the <i>N</i>-Queens binary CSP model, there is a notional and genuine binary constraint for every pair of
    ///     column index variables. The constraint has a binary predicate that asserts that the two column indexes must be
    ///     assigned non-capturing queens.
    /// </remarks>
    protected override IBinaryPredicate<Queen> GetBinaryPredicateFor(int variable1, int variable2) => CannotCapture;

    private sealed class CannotCapturePredicate : IBinaryPredicate<Queen>
    {
        public bool CanAssign(in Queen domainValue1, in Queen domainValue2) => !domainValue1.CanCapture(domainValue2);
    }
}
