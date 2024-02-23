using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Sudoku;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class SudokuPuzzles
{
    private static SudokuPuzzle FixedPuzzle1() => SudokuPuzzle.FromGrid(new int?[,]
    {
        { null, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
        { 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
        { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
        { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
        { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
        { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
        { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
        { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
        { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 }
    });

    private static SudokuPuzzle FixedPuzzle2() => SudokuPuzzle.FromGrid(new int?[,]
    {
        { 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
        { 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
        { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
        { 0008, 0009, 0001, null, null, null, 0005, 0006, 0007 },
        { 0002, 0003, 0004, null, null, null, 0008, 0009, 0001 },
        { 0005, 0006, 0007, null, null, null, 0002, 0003, 0004 },
        { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
        { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
        { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 }
    });

    private static SudokuPuzzle FixedPuzzle3() => SudokuPuzzle.FromGrid(new int?[,]
    {
        { null, 0002, null, null, 0005, 0006, 0007, 0008, 0009 },
        { 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
        { null, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
        { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
        { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
        { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
        { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
        { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
        { null, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 }
    });

    private static SudokuPuzzle FixedPuzzle4() => SudokuPuzzle.FromGrid(new int?[,]
    {
        { null, 0002, null, null, 0005, 0006, 0007, 0008, 0009 },
        { null, 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
        { null, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
        { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
        { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
        { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
        { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
        { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
        { null, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 }
    });

    public sealed class ExpectedVariables : TheoryData<SudokuPuzzle, IEnumerable<EmptyCell>>
    {
        public ExpectedVariables()
        {
            Add(FixedPuzzle1(), [
                new EmptyCell(0, 0)
            ]);
            Add(FixedPuzzle2(), [
                new EmptyCell(3, 3),
                new EmptyCell(3, 4),
                new EmptyCell(3, 5),
                new EmptyCell(4, 3),
                new EmptyCell(4, 4),
                new EmptyCell(4, 5),
                new EmptyCell(5, 3),
                new EmptyCell(5, 4),
                new EmptyCell(5, 5)
            ]);
            Add(FixedPuzzle3(), [
                new EmptyCell(0, 0),
                new EmptyCell(0, 2),
                new EmptyCell(0, 8),
                new EmptyCell(2, 0),
                new EmptyCell(3, 0)
            ]);
            Add(FixedPuzzle4(), [
                new EmptyCell(0, 0),
                new EmptyCell(0, 1),
                new EmptyCell(0, 2),
                new EmptyCell(0, 8),
                new EmptyCell(2, 0),
                new EmptyCell(3, 0)
            ]);
        }
    }

    public sealed class ExpectedDomains : TheoryData<SudokuPuzzle, IEnumerable<IReadOnlyList<int>>>
    {
        public ExpectedDomains()
        {
            Add(FixedPuzzle1(), [
                [1]
            ]);
            Add(FixedPuzzle2(), [
                [2],
                [5],
                [8],
                [3],
                [6],
                [9],
                [4],
                [7],
                [1]
            ]);
            Add(FixedPuzzle3(), [
                [1, 3],
                [7],
                [3],
                [3],
                [4]
            ]);
            Add(FixedPuzzle4(), [
                [1, 3, 4],
                [4],
                [7],
                [3],
                [3],
                [4]
            ]);
        }
    }

    public sealed class ExpectedAdjacentVariables : TheoryData<SudokuPuzzle, IEnumerable<Pair<EmptyCell>>>
    {
        public ExpectedAdjacentVariables()
        {
            Add(FixedPuzzle1(), Array.Empty<Pair<EmptyCell>>());
            Add(FixedPuzzle2(), Array.Empty<Pair<EmptyCell>>());
            Add(FixedPuzzle3(), [
                new Pair<EmptyCell>(new EmptyCell(0, 0), new EmptyCell(0, 8)),
                new Pair<EmptyCell>(new EmptyCell(0, 0), new EmptyCell(2, 0))
            ]);
            Add(FixedPuzzle4(), [
                new Pair<EmptyCell>(new EmptyCell(0, 0), new EmptyCell(0, 1)),
                new Pair<EmptyCell>(new EmptyCell(0, 0), new EmptyCell(0, 8)),
                new Pair<EmptyCell>(new EmptyCell(0, 0), new EmptyCell(2, 0)),
                new Pair<EmptyCell>(new EmptyCell(0, 0), new EmptyCell(3, 0))
            ]);
        }
    }

    public sealed class ExpectedProblemMetrics : TheoryData<SudokuPuzzle, ProblemMetrics>
    {
        public ExpectedProblemMetrics()
        {
            Add(FixedPuzzle1(), new ProblemMetrics
            {
                Variables = 1, Constraints = 0, ConstraintDensity = 0.0, ConstraintTightness = 0.0
            });
            Add(FixedPuzzle2(), new ProblemMetrics
            {
                Variables = 9, Constraints = 0, ConstraintDensity = 0.0, ConstraintTightness = 0.0
            });
            Add(FixedPuzzle3(), new ProblemMetrics
            {
                Variables = 5, Constraints = 2, ConstraintDensity = 0.2, ConstraintTightness = 0.5
            });
            Add(FixedPuzzle4(), new ProblemMetrics
            {
                Variables = 6, Constraints = 4, ConstraintDensity = 0.266667, ConstraintTightness = 0.333333
            });
        }
    }

    public sealed class ExpectedDomainSizeStatistics : TheoryData<SudokuPuzzle, DomainSizeStatistics>
    {
        public ExpectedDomainSizeStatistics()
        {
            Add(FixedPuzzle1(), new DomainSizeStatistics
            {
                MinimumValue = 1, MeanValue = 1.0, MaximumValue = 1, DistinctValues = 1
            });
            Add(FixedPuzzle2(), new DomainSizeStatistics
            {
                MinimumValue = 1, MeanValue = 1.0, MaximumValue = 1, DistinctValues = 1
            });
            Add(FixedPuzzle3(), new DomainSizeStatistics
            {
                MinimumValue = 1, MeanValue = 1.2, MaximumValue = 2, DistinctValues = 2
            });
            Add(FixedPuzzle4(), new DomainSizeStatistics
            {
                MinimumValue = 1, MeanValue = 1.333333, MaximumValue = 3, DistinctValues = 2
            });
        }
    }

    public sealed class ExpectedDegreeStatistics : TheoryData<SudokuPuzzle, DegreeStatistics>
    {
        public ExpectedDegreeStatistics()
        {
            Add(FixedPuzzle1(), new DegreeStatistics
            {
                MinimumValue = 0, MeanValue = 0.0, MaximumValue = 0, DistinctValues = 1
            });
            Add(FixedPuzzle2(), new DegreeStatistics
            {
                MinimumValue = 0, MeanValue = 0.0, MaximumValue = 0, DistinctValues = 1
            });
            Add(FixedPuzzle3(), new DegreeStatistics
            {
                MinimumValue = 0, MeanValue = 0.8, MaximumValue = 2, DistinctValues = 3
            });
            Add(FixedPuzzle4(), new DegreeStatistics
            {
                MinimumValue = 0, MeanValue = 1.333333, MaximumValue = 4, DistinctValues = 3
            });
        }
    }

    public sealed class ExpectedSumTightnessStatistics : TheoryData<SudokuPuzzle, SumTightnessStatistics>
    {
        public ExpectedSumTightnessStatistics()
        {
            Add(FixedPuzzle1(), new SumTightnessStatistics
            {
                MinimumValue = 0.0, MeanValue = 0.0, MaximumValue = 0.0, DistinctValues = 1
            });
            Add(FixedPuzzle2(), new SumTightnessStatistics
            {
                MinimumValue = 0.0, MeanValue = 0.0, MaximumValue = 0.0, DistinctValues = 1
            });
            Add(FixedPuzzle3(), new SumTightnessStatistics
            {
                MinimumValue = 0.0, MeanValue = 0.4, MaximumValue = 1.0, DistinctValues = 3
            });
            Add(FixedPuzzle4(), new SumTightnessStatistics
            {
                MinimumValue = 0.0, MeanValue = 0.444444, MaximumValue = 1.333333, DistinctValues = 3
            });
        }
    }
}
