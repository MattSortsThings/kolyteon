using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.NQueens;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class NQueensPuzzles
{
    private static NQueensPuzzle FixedPuzzle1() => NQueensPuzzle.FromN(1);

    private static NQueensPuzzle FixedPuzzle2() => NQueensPuzzle.FromN(2);

    private static NQueensPuzzle FixedPuzzle3() => NQueensPuzzle.FromN(5);

    public sealed class ExpectedVariables : TheoryData<NQueensPuzzle, IEnumerable<int>>
    {
        public ExpectedVariables()
        {
            Add(FixedPuzzle1(), [0]);
            Add(FixedPuzzle2(), [0, 1]);
            Add(FixedPuzzle3(), [0, 1, 2, 3, 4]);
        }
    }

    public sealed class ExpectedDomains : TheoryData<NQueensPuzzle, IEnumerable<IReadOnlyList<Queen>>>
    {
        public ExpectedDomains()
        {
            Add(FixedPuzzle1(), [
                [new Queen(0, 0)]
            ]);
            Add(FixedPuzzle2(), [
                [new Queen(0, 0), new Queen(0, 1)],
                [new Queen(1, 0), new Queen(1, 1)]
            ]);
            Add(FixedPuzzle3(), [
                [new Queen(0, 0), new Queen(0, 1), new Queen(0, 2), new Queen(0, 3), new Queen(0, 4)],
                [new Queen(1, 0), new Queen(1, 1), new Queen(1, 2), new Queen(1, 3), new Queen(1, 4)],
                [new Queen(2, 0), new Queen(2, 1), new Queen(2, 2), new Queen(2, 3), new Queen(2, 4)],
                [new Queen(3, 0), new Queen(3, 1), new Queen(3, 2), new Queen(3, 3), new Queen(3, 4)],
                [new Queen(4, 0), new Queen(4, 1), new Queen(4, 2), new Queen(4, 3), new Queen(4, 4)]
            ]);
        }
    }

    public sealed class ExpectedAdjacentVariables : TheoryData<NQueensPuzzle, IEnumerable<Pair<int>>>
    {
        public ExpectedAdjacentVariables()
        {
            Add(FixedPuzzle1(), []);
            Add(FixedPuzzle2(), [
                new Pair<int>(0, 1)
            ]);
            Add(FixedPuzzle3(), [
                new Pair<int>(0, 1),
                new Pair<int>(0, 2),
                new Pair<int>(0, 3),
                new Pair<int>(0, 4),
                new Pair<int>(1, 2),
                new Pair<int>(1, 3),
                new Pair<int>(1, 4),
                new Pair<int>(2, 3),
                new Pair<int>(2, 4),
                new Pair<int>(3, 4)
            ]);
        }
    }

    public sealed class ExpectedProblemMetrics : TheoryData<NQueensPuzzle, ProblemMetrics>
    {
        public ExpectedProblemMetrics()
        {
            Add(FixedPuzzle1(), new ProblemMetrics
            {
                Variables = 1, Constraints = 0, ConstraintDensity = 0, ConstraintTightness = 0
            });
            Add(FixedPuzzle2(), new ProblemMetrics
            {
                Variables = 2, Constraints = 1, ConstraintDensity = 1.0, ConstraintTightness = 1.0
            });
            Add(FixedPuzzle3(), new ProblemMetrics
            {
                Variables = 5, Constraints = 10, ConstraintDensity = 1.0, ConstraintTightness = 0.44
            });
        }
    }

    public sealed class ExpectedDomainSizeStatistics : TheoryData<NQueensPuzzle, DomainSizeStatistics>
    {
        public ExpectedDomainSizeStatistics()
        {
            Add(FixedPuzzle1(), new DomainSizeStatistics
            {
                MinimumValue = 1, MeanValue = 1.0, MaximumValue = 1, DistinctValues = 1
            });
            Add(FixedPuzzle2(), new DomainSizeStatistics
            {
                MinimumValue = 2, MeanValue = 2.0, MaximumValue = 2, DistinctValues = 1
            });
            Add(FixedPuzzle3(), new DomainSizeStatistics
            {
                MinimumValue = 5, MeanValue = 5.0, MaximumValue = 5, DistinctValues = 1
            });
        }
    }

    public sealed class ExpectedDegreeStatistics : TheoryData<NQueensPuzzle, DegreeStatistics>
    {
        public ExpectedDegreeStatistics()
        {
            Add(FixedPuzzle1(), new DegreeStatistics
            {
                MinimumValue = 0, MeanValue = 0.0, MaximumValue = 0, DistinctValues = 1
            });
            Add(FixedPuzzle2(), new DegreeStatistics
            {
                MinimumValue = 1, MeanValue = 1.0, MaximumValue = 1, DistinctValues = 1
            });
            Add(FixedPuzzle3(), new DegreeStatistics
            {
                MinimumValue = 4, MeanValue = 4.0, MaximumValue = 4, DistinctValues = 1
            });
        }
    }

    public sealed class ExpectedSumTightnessStatistics : TheoryData<NQueensPuzzle, SumTightnessStatistics>
    {
        public ExpectedSumTightnessStatistics()
        {
            Add(FixedPuzzle1(), new SumTightnessStatistics
            {
                MinimumValue = 0.0, MeanValue = 0.0, MaximumValue = 0.0, DistinctValues = 1
            });
            Add(FixedPuzzle2(), new SumTightnessStatistics
            {
                MinimumValue = 1.0, MeanValue = 1.0, MaximumValue = 1.0, DistinctValues = 1
            });
            Add(FixedPuzzle3(), new SumTightnessStatistics
            {
                MinimumValue = 1.6, MeanValue = 1.76, MaximumValue = 1.92, DistinctValues = 3
            });
        }
    }
}
