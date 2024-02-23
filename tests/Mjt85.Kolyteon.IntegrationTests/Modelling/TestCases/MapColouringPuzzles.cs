using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class MapColouringPuzzles
{
    private static readonly Region R0 = Region.FromId("R0");
    private static readonly Region R1 = Region.FromId("R1");
    private static readonly Region R2 = Region.FromId("R2");
    private static readonly Region R3 = Region.FromId("R3");
    private static readonly Region R4 = Region.FromId("R4");
    private static readonly Region R5 = Region.FromId("R5");
    private static readonly Region R6 = Region.FromId("R6");

    private static MapColouringPuzzle FixedPuzzle1() => MapColouringPuzzle.Create()
        .WithGlobalColours(Colour.Black)
        .AddRegion(R0)
        .Build();

    private static MapColouringPuzzle FixedPuzzle2() => MapColouringPuzzle.Create()
        .WithGlobalColours(Colour.Black)
        .AddRegion(R0)
        .AddRegion(R1)
        .AddRegion(R2)
        .Build();

    private static MapColouringPuzzle FixedPuzzle3() => MapColouringPuzzle.Create()
        .WithRegionSpecificColours()
        .AddRegionWithColours(R0, Colour.Black, Colour.White)
        .AddRegionWithColours(R1, Colour.Black)
        .AddRegionWithColours(R2, Colour.DarkMagenta)
        .AddRegionWithColours(R3, Colour.Black, Colour.DarkMagenta)
        .SetAsNeighbours(R0, R1)
        .SetAsNeighbours(R1, R2)
        .SetAsNeighbours(R2, R3)
        .SetAsNeighbours(R3, R0)
        .Build();

    private static MapColouringPuzzle FixedPuzzle4() => MapColouringPuzzle.Create()
        .WithRegionSpecificColours()
        .AddRegionWithColours(R0, Colour.Red, Colour.Blue, Colour.Green)
        .AddRegionWithColours(R1, Colour.Blue, Colour.Green)
        .AddRegionWithColours(R2, Colour.Red, Colour.Blue)
        .AddRegionWithColours(R3, Colour.Red, Colour.Blue)
        .AddRegionWithColours(R4, Colour.Blue, Colour.Green)
        .AddRegionWithColours(R5, Colour.Red, Colour.Green, Colour.Yellow)
        .AddRegionWithColours(R6, Colour.Red, Colour.Blue)
        .SetAsNeighbours(R0, R1)
        .SetAsNeighbours(R0, R2)
        .SetAsNeighbours(R0, R3)
        .SetAsNeighbours(R0, R6)
        .SetAsNeighbours(R1, R5)
        .SetAsNeighbours(R2, R6)
        .SetAsNeighbours(R3, R4)
        .SetAsNeighbours(R3, R6)
        .SetAsNeighbours(R4, R5)
        .SetAsNeighbours(R4, R6)
        .Build();

    public sealed class ExpectedVariables : TheoryData<MapColouringPuzzle, IEnumerable<Region>>
    {
        public ExpectedVariables()
        {
            Add(FixedPuzzle1(), [R0]);
            Add(FixedPuzzle2(), [R0, R1, R2]);
            Add(FixedPuzzle3(), [R0, R1, R2, R3]);
            Add(FixedPuzzle4(), [R0, R1, R2, R3, R4, R5, R6]);
        }
    }

    public sealed class ExpectedDomains : TheoryData<MapColouringPuzzle, IEnumerable<IReadOnlyList<Colour>>>
    {
        public ExpectedDomains()
        {
            Add(FixedPuzzle1(), [
                [Colour.Black]
            ]);
            Add(FixedPuzzle2(), [
                [Colour.Black],
                [Colour.Black],
                [Colour.Black]
            ]);
            Add(FixedPuzzle3(), [
                [Colour.Black, Colour.White],
                [Colour.Black],
                [Colour.DarkMagenta],
                [Colour.Black, Colour.DarkMagenta]
            ]);
            Add(FixedPuzzle4(), [
                [Colour.Blue, Colour.Green, Colour.Red],
                [Colour.Blue, Colour.Green],
                [Colour.Blue, Colour.Red],
                [Colour.Blue, Colour.Red],
                [Colour.Blue, Colour.Green],
                [Colour.Green, Colour.Red, Colour.Yellow],
                [Colour.Blue, Colour.Red]
            ]);
        }
    }

    public sealed class ExpectedAdjacentVariables : TheoryData<MapColouringPuzzle, IEnumerable<Pair<Region>>>
    {
        public ExpectedAdjacentVariables()
        {
            Add(FixedPuzzle1(), []);
            Add(FixedPuzzle2(), []);
            Add(FixedPuzzle3(), [
                new Pair<Region>(R0, R1),
                new Pair<Region>(R0, R3),
                new Pair<Region>(R2, R3)
            ]);
            Add(FixedPuzzle4(), [
                new Pair<Region>(R0, R1),
                new Pair<Region>(R0, R2),
                new Pair<Region>(R0, R3),
                new Pair<Region>(R0, R6),
                new Pair<Region>(R1, R5),
                new Pair<Region>(R2, R6),
                new Pair<Region>(R3, R4),
                new Pair<Region>(R3, R6),
                new Pair<Region>(R4, R5),
                new Pair<Region>(R4, R6)
            ]);
        }
    }

    public sealed class ExpectedProblemMetrics : TheoryData<MapColouringPuzzle, ProblemMetrics>
    {
        public ExpectedProblemMetrics()
        {
            Add(FixedPuzzle1(), new ProblemMetrics
            {
                Variables = 1, Constraints = 0, ConstraintDensity = 0.0, ConstraintTightness = 0.0
            });
            Add(FixedPuzzle2(), new ProblemMetrics
            {
                Variables = 3, Constraints = 0, ConstraintDensity = 0.0, ConstraintTightness = 0.0
            });
            Add(FixedPuzzle3(), new ProblemMetrics
            {
                Variables = 4, Constraints = 3, ConstraintDensity = 0.5, ConstraintTightness = 0.375
            });
            Add(FixedPuzzle4(), new ProblemMetrics
            {
                Variables = 7, Constraints = 10, ConstraintDensity = 0.476190, ConstraintTightness = 0.307692
            });
        }
    }

    public sealed class ExpectedDomainSizeStatistics : TheoryData<MapColouringPuzzle, DomainSizeStatistics>
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
                MinimumValue = 1, MeanValue = 1.5, MaximumValue = 2, DistinctValues = 2
            });
            Add(FixedPuzzle4(), new DomainSizeStatistics
            {
                MinimumValue = 2, MeanValue = 2.285714, MaximumValue = 3, DistinctValues = 2
            });
        }
    }

    public sealed class ExpectedDegreeStatistics : TheoryData<MapColouringPuzzle, DegreeStatistics>
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
                MinimumValue = 1, MeanValue = 1.5, MaximumValue = 2, DistinctValues = 2
            });
            Add(FixedPuzzle4(), new DegreeStatistics
            {
                MinimumValue = 2, MeanValue = 2.857143, MaximumValue = 4, DistinctValues = 3
            });
        }
    }

    public sealed class ExpectedSumTightnessStatistics : TheoryData<MapColouringPuzzle, SumTightnessStatistics>
    {
        public ExpectedSumTightnessStatistics()
        {
            Add(FixedPuzzle1(), new SumTightnessStatistics
            {
                MinimumValue = 0, MeanValue = 0.0, MaximumValue = 0, DistinctValues = 1
            });
            Add(FixedPuzzle2(), new SumTightnessStatistics
            {
                MinimumValue = 0, MeanValue = 0.0, MaximumValue = 0, DistinctValues = 1
            });
            Add(FixedPuzzle3(), new SumTightnessStatistics
            {
                MinimumValue = 0.5, MeanValue = 0.625, MaximumValue = 0.75, DistinctValues = 2
            });
            Add(FixedPuzzle4(), new SumTightnessStatistics
            {
                MinimumValue = 0.333333, MeanValue = 0.904762, MaximumValue = 1.583333, DistinctValues = 7
            });
        }
    }
}
