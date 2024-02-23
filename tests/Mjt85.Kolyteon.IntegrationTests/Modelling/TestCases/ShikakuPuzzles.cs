using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Shikaku;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;

public sealed class ShikakuPuzzles
{
    private static ShikakuPuzzle FixedPuzzle1() => ShikakuPuzzle.FromGrid(new int?[,]
    {
        { 0025, null, null, null, null },
        { null, null, null, null, null },
        { null, null, null, null, null },
        { null, null, null, null, null },
        { null, null, null, null, null }
    });

    private static ShikakuPuzzle FixedPuzzle2() => ShikakuPuzzle.FromGrid(new int?[,]
    {
        { 0005, null, null, null, null },
        { null, null, null, null, null },
        { null, null, null, null, null },
        { null, null, null, null, null },
        { null, null, null, null, 0020 }
    });

    private static ShikakuPuzzle FixedPuzzle3() => ShikakuPuzzle.FromGrid(new int?[,]
    {
        { 0004, null, null, null, null },
        { null, null, null, null, null },
        { null, null, null, null, null },
        { null, null, null, null, null },
        { null, null, null, null, 0021 }
    });

    private static ShikakuPuzzle FixedPuzzle4() => ShikakuPuzzle.FromGrid(new int?[,]
    {
        { 0005, null, null, null, null },
        { 0020, null, null, null, null },
        { null, null, null, null, null },
        { null, null, null, null, null },
        { null, null, null, null, null }
    });

    private static ShikakuPuzzle FixedPuzzle5() => ShikakuPuzzle.FromGrid(new int?[,]
    {
        { null, 0004, 0005, null, null },
        { null, null, null, null, null },
        { null, null, null, null, 0006 },
        { null, null, null, null, 0010 },
        { null, null, null, null, null }
    });

    private static ShikakuPuzzle FixedPuzzle6() => ShikakuPuzzle.FromGrid(new int?[,]
    {
        { null, null, null, null, 0010 },
        { null, null, null, null, null },
        { null, null, null, null, null },
        { null, null, null, null, 0010 },
        { 0005, null, null, null, null }
    });

    public sealed class ExpectedVariables : TheoryData<ShikakuPuzzle, IEnumerable<Hint>>
    {
        public ExpectedVariables()
        {
            Add(FixedPuzzle1(), [
                new Hint(0, 0, 25)
            ]);
            Add(FixedPuzzle2(), [
                new Hint(0, 0, 5),
                new Hint(4, 4, 20)
            ]);
            Add(FixedPuzzle3(), [
                new Hint(0, 0, 4),
                new Hint(4, 4, 21)
            ]);
            Add(FixedPuzzle4(), [
                new Hint(0, 0, 5),
                new Hint(0, 1, 20)
            ]);
            Add(FixedPuzzle5(), [
                new Hint(1, 0, 4),
                new Hint(2, 0, 5),
                new Hint(4, 2, 6),
                new Hint(4, 3, 10)
            ]);
            Add(FixedPuzzle6(), [
                new Hint(0, 4, 5),
                new Hint(4, 0, 10),
                new Hint(4, 3, 10)
            ]);
        }
    }

    public sealed class ExpectedDomains : TheoryData<ShikakuPuzzle, IEnumerable<IReadOnlyList<Rectangle>>>
    {
        public ExpectedDomains()
        {
            Add(FixedPuzzle1(), [
                [
                    new Rectangle(0, 0, 5, 5)
                ]
            ]);
            Add(FixedPuzzle2(), [
                [
                    new Rectangle(0, 0, 1, 5),
                    new Rectangle(0, 0, 5, 1)
                ],
                [
                    new Rectangle(0, 1, 5, 4),
                    new Rectangle(1, 0, 4, 5)
                ]
            ]);
            Add(FixedPuzzle3(), [
                [
                    new Rectangle(0, 0, 1, 4),
                    new Rectangle(0, 0, 2, 2),
                    new Rectangle(0, 0, 4, 1)
                ],
                []
            ]);
            Add(FixedPuzzle4(), [
                [
                    new Rectangle(0, 0, 5, 1)
                ],
                [
                    new Rectangle(0, 1, 5, 4)
                ]
            ]);
            Add(FixedPuzzle5(), [
                [
                    new Rectangle(0, 0, 2, 2),
                    new Rectangle(1, 0, 1, 4)
                ],
                [
                    new Rectangle(2, 0, 1, 5)
                ],
                [
                    new Rectangle(2, 1, 3, 2),
                    new Rectangle(3, 0, 2, 3)
                ],
                [
                    new Rectangle(0, 3, 5, 2)
                ]
            ]);
            Add(FixedPuzzle6(), [
                [
                    new Rectangle(0, 0, 1, 5),
                    new Rectangle(0, 4, 5, 1)
                ],
                [
                    new Rectangle(0, 0, 5, 2)
                ],
                [
                    new Rectangle(0, 2, 5, 2)
                ]
            ]);
        }
    }

    public sealed class ExpectedAdjacentVariables : TheoryData<ShikakuPuzzle, IEnumerable<Pair<Hint>>>
    {
        public ExpectedAdjacentVariables()
        {
            Add(FixedPuzzle1(), Array.Empty<Pair<Hint>>());
            Add(FixedPuzzle2(), [
                new Pair<Hint>(new Hint(0, 0, 5), new Hint(4, 4, 20))
            ]);
            Add(FixedPuzzle3(), Array.Empty<Pair<Hint>>());
            Add(FixedPuzzle4(), Array.Empty<Pair<Hint>>());
            Add(FixedPuzzle5(), [
                new Pair<Hint>(new Hint(1, 0, 4), new Hint(4, 3, 10)),
                new Pair<Hint>(new Hint(2, 0, 5), new Hint(4, 2, 6)),
                new Pair<Hint>(new Hint(2, 0, 5), new Hint(4, 3, 10))
            ]);
            Add(FixedPuzzle6(), [
                new Pair<Hint>(new Hint(0, 4, 5), new Hint(4, 0, 10)),
                new Pair<Hint>(new Hint(0, 4, 5), new Hint(4, 3, 10))
            ]);
        }
    }

    public sealed class ExpectedProblemMetrics : TheoryData<ShikakuPuzzle, ProblemMetrics>
    {
        public ExpectedProblemMetrics()
        {
            Add(FixedPuzzle1(), new ProblemMetrics
            {
                Variables = 1, Constraints = 0, ConstraintDensity = 0.0, ConstraintTightness = 0.0
            });
            Add(FixedPuzzle2(), new ProblemMetrics
            {
                Variables = 2, Constraints = 1, ConstraintDensity = 1.0, ConstraintTightness = 0.5
            });
            Add(FixedPuzzle3(), new ProblemMetrics
            {
                Variables = 2, Constraints = 0, ConstraintDensity = 0.0, ConstraintTightness = 0.0
            });
            Add(FixedPuzzle4(), new ProblemMetrics
            {
                Variables = 2, Constraints = 0, ConstraintDensity = 0.0, ConstraintTightness = 0.0
            });
            Add(FixedPuzzle5(), new ProblemMetrics
            {
                Variables = 4, Constraints = 3, ConstraintDensity = 0.5, ConstraintTightness = 0.6
            });
            Add(FixedPuzzle6(), new ProblemMetrics
            {
                Variables = 3, Constraints = 2, ConstraintDensity = 0.666667, ConstraintTightness = 0.5
            });
        }
    }

    public sealed class ExpectedDomainSizeStatistics : TheoryData<ShikakuPuzzle, DomainSizeStatistics>
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
                MinimumValue = 0, MeanValue = 1.5, MaximumValue = 3, DistinctValues = 2
            });
            Add(FixedPuzzle4(), new DomainSizeStatistics
            {
                MinimumValue = 1, MeanValue = 1.0, MaximumValue = 1, DistinctValues = 1
            });
            Add(FixedPuzzle5(), new DomainSizeStatistics
            {
                MinimumValue = 1, MeanValue = 1.5, MaximumValue = 2, DistinctValues = 2
            });
            Add(FixedPuzzle6(), new DomainSizeStatistics
            {
                MinimumValue = 1, MeanValue = 1.333333, MaximumValue = 2, DistinctValues = 2
            });
        }
    }

    public sealed class ExpectedDegreeStatistics : TheoryData<ShikakuPuzzle, DegreeStatistics>
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
                MinimumValue = 0, MeanValue = 0.0, MaximumValue = 0, DistinctValues = 1
            });
            Add(FixedPuzzle4(), new DegreeStatistics
            {
                MinimumValue = 0, MeanValue = 0.0, MaximumValue = 0, DistinctValues = 1
            });
            Add(FixedPuzzle5(), new DegreeStatistics
            {
                MinimumValue = 1, MeanValue = 1.5, MaximumValue = 2, DistinctValues = 2
            });
            Add(FixedPuzzle6(), new DegreeStatistics
            {
                MinimumValue = 1, MeanValue = 1.333333, MaximumValue = 2, DistinctValues = 2
            });
        }
    }

    public sealed class ExpectedSumTightnessStatistics : TheoryData<ShikakuPuzzle, SumTightnessStatistics>
    {
        public ExpectedSumTightnessStatistics()
        {
            Add(FixedPuzzle1(), new SumTightnessStatistics
            {
                MinimumValue = 0.0, MeanValue = 0.0, MaximumValue = 0.0, DistinctValues = 1
            });
            Add(FixedPuzzle2(), new SumTightnessStatistics
            {
                MinimumValue = 0.5, MeanValue = 0.5, MaximumValue = 0.5, DistinctValues = 1
            });
            Add(FixedPuzzle3(), new SumTightnessStatistics
            {
                MinimumValue = 0.0, MeanValue = 0.0, MaximumValue = 0.0, DistinctValues = 1
            });
            Add(FixedPuzzle4(), new SumTightnessStatistics
            {
                MinimumValue = 0.0, MeanValue = 0.0, MaximumValue = 0.0, DistinctValues = 1
            });
            Add(FixedPuzzle5(), new SumTightnessStatistics
            {
                MinimumValue = 0.5, MeanValue = 1.0, MaximumValue = 1.5, DistinctValues = 2
            });
            Add(FixedPuzzle6(), new SumTightnessStatistics
            {
                MinimumValue = 0.5, MeanValue = 0.666667, MaximumValue = 1.0, DistinctValues = 2
            });
        }
    }
}
