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

    protected override void PopulateProblemData(NQueensPuzzle problem) => _n = problem.N;

    protected override void ClearProblemData() => _n = default;

    protected override IEnumerable<int> GetVariables() => Enumerable.Range(0, _n);

    protected override IEnumerable<Queen> GetDomainOf(int variable)
    {
        var column = variable;

        return from row in Enumerable.Range(0, _n) select new Queen(column, row);
    }

    protected override IBinaryPredicate<Queen> GetBinaryPredicateFor(int variable1, int variable2) => CannotCapture;

    private sealed class CannotCapturePredicate : IBinaryPredicate<Queen>
    {
        public bool CanAssign(in Queen domainValue1, in Queen domainValue2) => !domainValue1.CanCapture(domainValue2);
    }
}
