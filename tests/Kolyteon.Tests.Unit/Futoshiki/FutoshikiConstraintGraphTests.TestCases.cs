using Kolyteon.Common;
using Kolyteon.Futoshiki;
using Kolyteon.Modelling.Testing;

namespace Kolyteon.Tests.Unit.Futoshiki;

public static partial class FutoshikiConstraintGraphTests
{
    private static readonly TestCase TestCaseOne = new()
    {
        Problem = FutoshikiProblem.Create()
            .FromGrid(new int?[,]
            {
                { null, 0002, 0003, 0004 },
                { 0002, 0001, 0004, 0003 },
                { 0003, 0004, 0001, 0002 },
                { 0004, 0003, 0002, 0001 }
            }).Build(),
        ExpectedNodes =
        [
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(0,0)"), Degree = 0, SumTightness = 0.0, Domain = [1]
            }
        ],
        ExpectedEdges = []
    };

    private static readonly TestCase TestCaseTwo = new()
    {
        Problem = FutoshikiProblem.Create()
            .FromGrid(new int?[,]
            {
                { null, 0002, 0003, 0004 },
                { 0002, 0001, 0004, 0003 },
                { 0003, 0004, 0001, 0002 },
                { 0004, 0003, 0002, null }
            }).Build(),
        ExpectedNodes =
        [
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(0,0)"), Degree = 0, SumTightness = 0.0, Domain = [1]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(3,3)"), Degree = 0, SumTightness = 0.0, Domain = [1]
            }
        ],
        ExpectedEdges = []
    };

    private static readonly TestCase TestCaseThree = new()
    {
        Problem = FutoshikiProblem.Create()
            .FromGrid(new int?[,]
            {
                { 0001, 0002, 0003, 0004 },
                { 0002, null, null, 0003 },
                { 0003, null, null, 0002 },
                { 0004, 0003, 0002, 0001 }
            }).Build(),
        ExpectedNodes =
        [
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(1,1)"), Degree = 2, SumTightness = 1.0, Domain = [1, 4]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(1,2)"), Degree = 2, SumTightness = 1.0, Domain = [1, 4]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(2,1)"), Degree = 2, SumTightness = 1.0, Domain = [1, 4]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(2,2)"), Degree = 2, SumTightness = 1.0, Domain = [1, 4]
            }
        ],
        ExpectedEdges =
        [
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(1,1)"),
                SecondVariable = Square.Parse("(1,2)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, true),
                    new AssignmentPair<int>(4, 1, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            },
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(1,1)"),
                SecondVariable = Square.Parse("(2,1)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, true),
                    new AssignmentPair<int>(4, 1, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            },
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(1,2)"),
                SecondVariable = Square.Parse("(2,2)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, true),
                    new AssignmentPair<int>(4, 1, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            },
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(2,1)"),
                SecondVariable = Square.Parse("(2,2)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, true),
                    new AssignmentPair<int>(4, 1, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            }
        ]
    };

    private static readonly TestCase TestCaseFour = new()
    {
        Problem = FutoshikiProblem.Create()
            .FromGrid(new int?[,]
            {
                { 0001, 0002, 0003, 0004 },
                { 0002, null, null, 0003 },
                { 0003, null, null, 0002 },
                { 0004, 0003, 0002, 0001 }
            })
            .AddSign(GreaterThanSign.Parse("(1,1)>(1,2)"))
            .Build(),
        ExpectedNodes =
        [
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(1,1)"), Degree = 2, SumTightness = 1.25, Domain = [1, 4]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(1,2)"), Degree = 2, SumTightness = 1.25, Domain = [1, 4]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(2,1)"), Degree = 2, SumTightness = 1.0, Domain = [1, 4]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(2,2)"), Degree = 2, SumTightness = 1.0, Domain = [1, 4]
            }
        ],
        ExpectedEdges =
        [
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(1,1)"),
                SecondVariable = Square.Parse("(1,2)"),
                Tightness = 0.75,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, false),
                    new AssignmentPair<int>(4, 1, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            },
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(1,1)"),
                SecondVariable = Square.Parse("(2,1)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, true),
                    new AssignmentPair<int>(4, 1, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            },
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(1,2)"),
                SecondVariable = Square.Parse("(2,2)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, true),
                    new AssignmentPair<int>(4, 1, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            },
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(2,1)"),
                SecondVariable = Square.Parse("(2,2)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, true),
                    new AssignmentPair<int>(4, 1, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            }
        ]
    };

    private static readonly TestCase TestCaseFive = new()
    {
        Problem = FutoshikiProblem.Create()
            .FromGrid(new int?[,]
            {
                { 0001, 0002, 0003, 0004 },
                { 0002, null, null, 0003 },
                { 0003, null, null, 0002 },
                { 0004, 0003, 0002, 0001 }
            })
            .AddSign(LessThanSign.Parse("(1,1)<(1,2)"))
            .Build(),
        ExpectedNodes =
        [
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(1,1)"), Degree = 2, SumTightness = 1.25, Domain = [1, 4]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(1,2)"), Degree = 2, SumTightness = 1.25, Domain = [1, 4]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(2,1)"), Degree = 2, SumTightness = 1.0, Domain = [1, 4]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(2,2)"), Degree = 2, SumTightness = 1.0, Domain = [1, 4]
            }
        ],
        ExpectedEdges =
        [
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(1,1)"),
                SecondVariable = Square.Parse("(1,2)"),
                Tightness = 0.75,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, true),
                    new AssignmentPair<int>(4, 1, false),
                    new AssignmentPair<int>(4, 4, false)
                ]
            },
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(1,1)"),
                SecondVariable = Square.Parse("(2,1)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, true),
                    new AssignmentPair<int>(4, 1, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            },
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(1,2)"),
                SecondVariable = Square.Parse("(2,2)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, true),
                    new AssignmentPair<int>(4, 1, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            },
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(2,1)"),
                SecondVariable = Square.Parse("(2,2)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, true),
                    new AssignmentPair<int>(4, 1, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            }
        ]
    };

    private static readonly TestCase TestCaseSix = new()
    {
        Problem = FutoshikiProblem.Create()
            .FromGrid(new int?[,]
            {
                { 0001, 0002, 0003, 0004 },
                { 0002, null, null, 0003 },
                { 0003, null, null, 0002 },
                { 0004, 0003, 0002, 0001 }
            })
            .AddSign(GreaterThanSign.Parse("(1,0)>(1,1)"))
            .Build(),
        ExpectedNodes =
        [
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(1,1)"), Degree = 2, SumTightness = 1.0, Domain = [1]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(1,2)"), Degree = 2, SumTightness = 1.0, Domain = [1, 4]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(2,1)"), Degree = 2, SumTightness = 1.0, Domain = [1, 4]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(2,2)"), Degree = 2, SumTightness = 1.0, Domain = [1, 4]
            }
        ],
        ExpectedEdges =
        [
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(1,1)"),
                SecondVariable = Square.Parse("(1,2)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, true)
                ]
            },
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(1,1)"),
                SecondVariable = Square.Parse("(2,1)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, true)
                ]
            },
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(1,2)"),
                SecondVariable = Square.Parse("(2,2)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, true),
                    new AssignmentPair<int>(4, 1, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            },
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(2,1)"),
                SecondVariable = Square.Parse("(2,2)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, true),
                    new AssignmentPair<int>(4, 1, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            }
        ]
    };

    private static readonly TestCase TestCaseSeven = new()
    {
        Problem = FutoshikiProblem.Create()
            .FromGrid(new int?[,]
            {
                { 0001, 0002, 0003, 0004 },
                { 0002, null, null, 0003 },
                { 0003, null, null, 0002 },
                { 0004, 0003, 0002, 0001 }
            })
            .AddSign(LessThanSign.Parse("(1,0)<(1,1)"))
            .Build(),
        ExpectedNodes =
        [
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(1,1)"), Degree = 2, SumTightness = 1.0, Domain = [4]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(1,2)"), Degree = 2, SumTightness = 1.0, Domain = [1, 4]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(2,1)"), Degree = 2, SumTightness = 1.0, Domain = [1, 4]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(2,2)"), Degree = 2, SumTightness = 1.0, Domain = [1, 4]
            }
        ],
        ExpectedEdges =
        [
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(1,1)"),
                SecondVariable = Square.Parse("(1,2)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(4, 1, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            },
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(1,1)"),
                SecondVariable = Square.Parse("(2,1)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(4, 1, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            },
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(1,2)"),
                SecondVariable = Square.Parse("(2,2)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, true),
                    new AssignmentPair<int>(4, 1, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            },
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(2,1)"),
                SecondVariable = Square.Parse("(2,2)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, true),
                    new AssignmentPair<int>(4, 1, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            }
        ]
    };

    private static readonly TestCase TestCaseEight = new()
    {
        Problem = FutoshikiProblem.Create()
            .FromGrid(new int?[,]
            {
                { 0001, 0002, 0003, 0004 },
                { 0002, null, null, 0003 },
                { 0003, null, null, 0002 },
                { 0004, 0003, 0002, 0001 }
            })
            .AddSign(GreaterThanSign.Parse("(2,2)>(2,3)"))
            .Build(),
        ExpectedNodes =
        [
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(1,1)"), Degree = 2, SumTightness = 1.0, Domain = [1, 4]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(1,2)"), Degree = 2, SumTightness = 1.0, Domain = [1, 4]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(2,1)"), Degree = 2, SumTightness = 1.0, Domain = [1, 4]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(2,2)"), Degree = 2, SumTightness = 1.0, Domain = [4]
            }
        ],
        ExpectedEdges =
        [
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(1,1)"),
                SecondVariable = Square.Parse("(1,2)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, true),
                    new AssignmentPair<int>(4, 1, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            },
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(1,1)"),
                SecondVariable = Square.Parse("(2,1)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, true),
                    new AssignmentPair<int>(4, 1, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            },
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(1,2)"),
                SecondVariable = Square.Parse("(2,2)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 4, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            },
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(2,1)"),
                SecondVariable = Square.Parse("(2,2)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 4, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            }
        ]
    };

    private static readonly TestCase TestCaseNine = new()
    {
        Problem = FutoshikiProblem.Create()
            .FromGrid(new int?[,]
            {
                { 0001, 0002, 0003, 0004 },
                { 0002, null, null, 0003 },
                { 0003, null, null, 0002 },
                { 0004, 0003, 0002, 0001 }
            })
            .AddSign(LessThanSign.Parse("(2,2)<(2,3)"))
            .Build(),
        ExpectedNodes =
        [
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(1,1)"), Degree = 2, SumTightness = 1.0, Domain = [1, 4]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(1,2)"), Degree = 2, SumTightness = 1.0, Domain = [1, 4]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(2,1)"), Degree = 2, SumTightness = 1.0, Domain = [1, 4]
            },
            new ConstraintGraphNodeDatum<Square, int>
            {
                Variable = Square.Parse("(2,2)"), Degree = 2, SumTightness = 1.0, Domain = [1]
            }
        ],
        ExpectedEdges =
        [
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(1,1)"),
                SecondVariable = Square.Parse("(1,2)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, true),
                    new AssignmentPair<int>(4, 1, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            },
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(1,1)"),
                SecondVariable = Square.Parse("(2,1)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(1, 4, true),
                    new AssignmentPair<int>(4, 1, true),
                    new AssignmentPair<int>(4, 4, false)
                ]
            },
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(1,2)"),
                SecondVariable = Square.Parse("(2,2)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(4, 1, true)
                ]
            },
            new ConstraintGraphEdgeDatum<Square, int>
            {
                FirstVariable = Square.Parse("(2,1)"),
                SecondVariable = Square.Parse("(2,2)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(1, 1, false),
                    new AssignmentPair<int>(4, 1, true)
                ]
            }
        ]
    };


    private sealed record TestCase
    {
        public required FutoshikiProblem Problem { get; init; }

        public required IList<ConstraintGraphNodeDatum<Square, int>> ExpectedNodes { get; init; }

        public required IList<ConstraintGraphEdgeDatum<Square, int>> ExpectedEdges { get; init; }

        public void Deconstruct(out FutoshikiProblem problem,
            out IList<ConstraintGraphNodeDatum<Square, int>> expectedNodes,
            out IList<ConstraintGraphEdgeDatum<Square, int>> expectedEdges)
        {
            problem = Problem;
            expectedNodes = ExpectedNodes;
            expectedEdges = ExpectedEdges;
        }
    }
}
