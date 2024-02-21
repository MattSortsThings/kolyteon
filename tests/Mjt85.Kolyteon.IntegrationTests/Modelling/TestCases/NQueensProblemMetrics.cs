using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.NQueens;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class NQueensProblemMetrics : TheoryData<NQueensPuzzle, ProblemMetrics>
{
    public NQueensProblemMetrics()
    {
        Add(NQueensPuzzle.FromN(1), new ProblemMetrics
        {
            Variables = 1, Constraints = 0, ConstraintDensity = 0, ConstraintTightness = 0
        });

        Add(NQueensPuzzle.FromN(2), new ProblemMetrics
        {
            Variables = 2, Constraints = 1, ConstraintDensity = 1.0, ConstraintTightness = 1.0
        });

        Add(NQueensPuzzle.FromN(5), new ProblemMetrics
        {
            Variables = 5, Constraints = 10, ConstraintDensity = 1.0, ConstraintTightness = 0.44
        });
    }
}
