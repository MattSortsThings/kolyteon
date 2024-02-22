using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Shikaku;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class ShikakuProblemMetrics : TheoryData<ShikakuPuzzle, ProblemMetrics>
{
    public ShikakuProblemMetrics()
    {
        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0025, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null }
        }), new ProblemMetrics
        {
            Variables = 1, Constraints = 0, ConstraintDensity = 0.0, ConstraintTightness = 0.0
        });

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0005, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0020 }
        }), new ProblemMetrics
        {
            Variables = 2, Constraints = 1, ConstraintDensity = 1.0, ConstraintTightness = 0.5
        });

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0004, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0021 }
        }), new ProblemMetrics
        {
            Variables = 2, Constraints = 0, ConstraintDensity = 0.0, ConstraintTightness = 0.0
        });

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { 0005, null, null, null, null },
            { 0020, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null }
        }), new ProblemMetrics
        {
            Variables = 2, Constraints = 0, ConstraintDensity = 0.0, ConstraintTightness = 0.0
        });

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { null, 0004, 0005, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0006 },
            { null, null, null, null, 0010 },
            { null, null, null, null, null }
        }), new ProblemMetrics
        {
            Variables = 4, Constraints = 3, ConstraintDensity = 0.5, ConstraintTightness = 0.6
        });

        Add(ShikakuPuzzle.FromGrid(new int?[,]
        {
            { null, null, null, null, 0010 },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0010 },
            { 0005, null, null, null, null }
        }), new ProblemMetrics
        {
            Variables = 3, Constraints = 2, ConstraintDensity = 0.666667, ConstraintTightness = 0.5
        });
    }
}
