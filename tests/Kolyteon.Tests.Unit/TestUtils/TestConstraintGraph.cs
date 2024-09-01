using System.Diagnostics.CodeAnalysis;
using Kolyteon.Modelling;

namespace Kolyteon.Tests.Unit.TestUtils;

/// <summary>
///     Can model any <see cref="TestProblem" /> instance as a generic binary CSP for use in testing.
/// </summary>
internal sealed class TestConstraintGraph : ConstraintGraph<char, int, TestProblem>
{
    private readonly Dictionary<char, int[]> _variablesAndDomains;

    public TestConstraintGraph(int capacity = 0) : base(capacity)
    {
        _variablesAndDomains = new Dictionary<char, int[]>(capacity);
    }

    public override int Capacity
    {
        get => base.Capacity;
        set
        {
            base.Capacity = value;
            _variablesAndDomains.TrimExcess();
            _variablesAndDomains.EnsureCapacity(value);
        }
    }

    public static TestConstraintGraph ModellingProblem(TestProblem problem)
    {
        TestConstraintGraph constraintGraph = new(problem.Count);
        constraintGraph.Model(problem);

        return constraintGraph;
    }

    protected override void PopulateProblemData(TestProblem problem)
    {
        foreach ((char variable, int[] domain) in problem)
        {
            _variablesAndDomains.Add(variable, domain);
        }
    }

    protected override IEnumerable<char> GetVariables() => _variablesAndDomains.Keys;

    protected override IEnumerable<int> GetDomainValues(char presentVariable) => _variablesAndDomains[presentVariable];

    protected override bool TryGetBinaryPredicate(char firstVariable,
        char secondVariable,
        [NotNullWhen(true)] out Func<int, int, bool>? binaryPredicate)
    {
        binaryPredicate = DifferentValues;

        return true;
    }

    protected override void ClearProblemData() => _variablesAndDomains.Clear();

    private static bool DifferentValues(int x, int y) => x != y;
}
