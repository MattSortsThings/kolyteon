using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.UnitTests.Helpers;

/// <summary>
///     Derivative of the <see cref="BinaryCsp{P,V,D}" /> abstract base class, allowing the latter to be unit-tested.
/// </summary>
public sealed class TestBinaryCsp : BinaryCsp<TestProblem, Letter, Digit>
{
    private static readonly DifferentValuesPredicate DifferentValues = new();

    private readonly Dictionary<Letter, Digit[]> _problemData;

    public TestBinaryCsp(int capacity) : base(capacity)
    {
        _problemData = new Dictionary<Letter, Digit[]>(capacity);
    }

    public override void TrimExcess()
    {
        base.TrimExcess();
        _problemData.TrimExcess();
    }

    public override int EnsureCapacity(int capacity)
    {
        var newCapacity = base.EnsureCapacity(capacity);
        _ = _problemData.EnsureCapacity(capacity);

        return newCapacity;
    }

    protected override void PopulateProblemData(TestProblem problem)
    {
        foreach ((Letter key, Digit[] value) in problem)
        {
            _problemData.Add(key, value);
        }
    }

    protected override void ClearProblemData() => _problemData.Clear();

    protected override IEnumerable<Letter> GetVariables() => _problemData.Keys;

    protected override IEnumerable<Digit> GetDomainOf(Letter variable) => _problemData[variable];

    protected override IBinaryPredicate<Digit> GetBinaryPredicateFor(Letter variable1, Letter variable2) => DifferentValues;

    public static TestBinaryCsp WithCapacity(int capacity) => new(capacity);

    public static TestBinaryCsp ModellingProblem(TestProblem problem)
    {
        TestBinaryCsp binaryCsp = new(problem.Count);
        binaryCsp.Model(problem);

        return binaryCsp;
    }

    private sealed class DifferentValuesPredicate : IBinaryPredicate<Digit>
    {
        public bool CanAssign(in Digit domainValue1, in Digit domainValue2) => domainValue1 != domainValue2;
    }
}
