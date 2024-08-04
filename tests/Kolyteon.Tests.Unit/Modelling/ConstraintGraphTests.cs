using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.Modelling;

public static partial class ConstraintGraphTests
{
    private const char A = 'A';
    private const char B = 'B';
    private const char C = 'C';
    private const char D = 'D';
    private const char E = 'E';

    private static TestProblem GetProblemWithOneVariable() => new() { [A] = [1] };

    private static TestProblem GetProblemWithTwoVariablesAndOneConstraint() => new() { [A] = [1, 2], [B] = [1, 2] };
}
