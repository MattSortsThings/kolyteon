using Kolyteon.Common;
using Kolyteon.Modelling.Testing;
using Kolyteon.NQueens;

namespace Kolyteon.Tests.Unit.NQueens;

public static partial class NQueensConstraintGraphTests
{
    private static readonly TestCase TestCaseOne = new()
    {
        Problem = NQueensProblem.FromN(1),
        ExpectedNodes =
        [
            new ConstraintGraphNode<int, Square>
            {
                Variable = 0,
                Degree = 0,
                SumTightness = 0.0,
                Domain =
                [
                    Square.Parse("(0,0)")
                ]
            }
        ],
        ExpectedEdges = []
    };

    private static readonly TestCase TestCaseTwo = new()
    {
        Problem = NQueensProblem.FromN(2),
        ExpectedNodes =
        [
            new ConstraintGraphNode<int, Square>
            {
                Variable = 0,
                Degree = 1,
                SumTightness = 1.0,
                Domain =
                [
                    Square.Parse("(0,0)"), Square.Parse("(0,1)")
                ]
            },
            new ConstraintGraphNode<int, Square>
            {
                Variable = 1,
                Degree = 1,
                SumTightness = 1.0,
                Domain =
                [
                    Square.Parse("(1,0)"), Square.Parse("(1,1)")
                ]
            }
        ],
        ExpectedEdges =
        [
            new ConstraintGraphEdge<int, Square>
            {
                FirstVariable = 0,
                SecondVariable = 1,
                Tightness = 1.0,
                AssignmentPairs =
                [
                    new AssignmentPair<Square>(Square.Parse("(0,0)"), Square.Parse("(1,0)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,0)"), Square.Parse("(1,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,1)"), Square.Parse("(1,0)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,1)"), Square.Parse("(1,1)"), false)
                ]
            }
        ]
    };

    private static readonly TestCase TestCaseThree = new()
    {
        Problem = NQueensProblem.FromN(3),
        ExpectedNodes =
        [
            new ConstraintGraphNode<int, Square>
            {
                Variable = 0,
                Degree = 2,
                SumTightness = 1.333333333,
                Domain =
                [
                    Square.Parse("(0,0)"), Square.Parse("(0,1)"), Square.Parse("(0,2)")
                ]
            },
            new ConstraintGraphNode<int, Square>
            {
                Variable = 1,
                Degree = 2,
                SumTightness = 1.555555556,
                Domain =
                [
                    Square.Parse("(1,0)"), Square.Parse("(1,1)"), Square.Parse("(1,2)")
                ]
            },
            new ConstraintGraphNode<int, Square>
            {
                Variable = 2,
                Degree = 2,
                SumTightness = 1.333333333,
                Domain =
                [
                    Square.Parse("(2,0)"), Square.Parse("(2,1)"), Square.Parse("(2,2)")
                ]
            }
        ],
        ExpectedEdges =
        [
            new ConstraintGraphEdge<int, Square>
            {
                FirstVariable = 0,
                SecondVariable = 1,
                Tightness = 0.777777778,
                AssignmentPairs =
                [
                    new AssignmentPair<Square>(Square.Parse("(0,0)"), Square.Parse("(1,0)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,0)"), Square.Parse("(1,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,0)"), Square.Parse("(1,2)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,1)"), Square.Parse("(1,0)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,1)"), Square.Parse("(1,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,1)"), Square.Parse("(1,2)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,2)"), Square.Parse("(1,0)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,2)"), Square.Parse("(1,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,2)"), Square.Parse("(1,2)"), false)
                ]
            },
            new ConstraintGraphEdge<int, Square>
            {
                FirstVariable = 0,
                SecondVariable = 2,
                Tightness = 0.555555556,
                AssignmentPairs =
                [
                    new AssignmentPair<Square>(Square.Parse("(0,0)"), Square.Parse("(2,0)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,0)"), Square.Parse("(2,1)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,0)"), Square.Parse("(2,2)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,1)"), Square.Parse("(2,0)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,1)"), Square.Parse("(2,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,1)"), Square.Parse("(2,2)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,2)"), Square.Parse("(2,0)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,2)"), Square.Parse("(2,1)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,2)"), Square.Parse("(2,2)"), false)
                ]
            },
            new ConstraintGraphEdge<int, Square>
            {
                FirstVariable = 1,
                SecondVariable = 2,
                Tightness = 0.777777778,
                AssignmentPairs =
                [
                    new AssignmentPair<Square>(Square.Parse("(1,0)"), Square.Parse("(2,0)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,0)"), Square.Parse("(2,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,0)"), Square.Parse("(2,2)"), true),
                    new AssignmentPair<Square>(Square.Parse("(1,1)"), Square.Parse("(2,0)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,1)"), Square.Parse("(2,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,1)"), Square.Parse("(2,2)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,2)"), Square.Parse("(2,0)"), true),
                    new AssignmentPair<Square>(Square.Parse("(1,2)"), Square.Parse("(2,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,2)"), Square.Parse("(2,2)"), false)
                ]
            }
        ]
    };

    private static readonly TestCase TestCaseFour = new()
    {
        Problem = NQueensProblem.FromN(4),
        ExpectedNodes =
        [
            new ConstraintGraphNode<int, Square>
            {
                Variable = 0,
                Degree = 3,
                SumTightness = 1.5,
                Domain =
                [
                    Square.Parse("(0,0)"), Square.Parse("(0,1)"), Square.Parse("(0,2)"), Square.Parse("(0,3)")
                ]
            },
            new ConstraintGraphNode<int, Square>
            {
                Variable = 1,
                Degree = 3,
                SumTightness = 1.75,
                Domain =
                [
                    Square.Parse("(1,0)"), Square.Parse("(1,1)"), Square.Parse("(1,2)"), Square.Parse("(1,3)")
                ]
            },
            new ConstraintGraphNode<int, Square>
            {
                Variable = 2,
                Degree = 3,
                SumTightness = 1.75,
                Domain =
                [
                    Square.Parse("(2,0)"), Square.Parse("(2,1)"), Square.Parse("(2,2)"), Square.Parse("(2,3)")
                ]
            },
            new ConstraintGraphNode<int, Square>
            {
                Variable = 3,
                Degree = 3,
                SumTightness = 1.5,
                Domain =
                [
                    Square.Parse("(3,0)"), Square.Parse("(3,1)"), Square.Parse("(3,2)"), Square.Parse("(3,3)")
                ]
            }
        ],
        ExpectedEdges =
        [
            new ConstraintGraphEdge<int, Square>
            {
                FirstVariable = 0,
                SecondVariable = 1,
                Tightness = 0.625,
                AssignmentPairs =
                [
                    new AssignmentPair<Square>(Square.Parse("(0,0)"), Square.Parse("(1,0)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,0)"), Square.Parse("(1,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,0)"), Square.Parse("(1,2)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,0)"), Square.Parse("(1,3)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,1)"), Square.Parse("(1,0)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,1)"), Square.Parse("(1,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,1)"), Square.Parse("(1,2)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,1)"), Square.Parse("(1,3)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,2)"), Square.Parse("(1,0)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,2)"), Square.Parse("(1,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,2)"), Square.Parse("(1,2)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,2)"), Square.Parse("(1,3)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,3)"), Square.Parse("(1,0)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,3)"), Square.Parse("(1,1)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,3)"), Square.Parse("(1,2)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,3)"), Square.Parse("(1,3)"), false)
                ]
            },
            new ConstraintGraphEdge<int, Square>
            {
                FirstVariable = 0,
                SecondVariable = 2,
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<Square>(Square.Parse("(0,0)"), Square.Parse("(2,0)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,0)"), Square.Parse("(2,1)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,0)"), Square.Parse("(2,2)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,0)"), Square.Parse("(2,3)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,1)"), Square.Parse("(2,0)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,1)"), Square.Parse("(2,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,1)"), Square.Parse("(2,2)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,1)"), Square.Parse("(2,3)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,2)"), Square.Parse("(2,0)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,2)"), Square.Parse("(2,1)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,2)"), Square.Parse("(2,2)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,2)"), Square.Parse("(2,3)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,3)"), Square.Parse("(2,0)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,3)"), Square.Parse("(2,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,3)"), Square.Parse("(2,2)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,3)"), Square.Parse("(2,3)"), false)
                ]
            },
            new ConstraintGraphEdge<int, Square>
            {
                FirstVariable = 0,
                SecondVariable = 3,
                Tightness = 0.375,
                AssignmentPairs =
                [
                    new AssignmentPair<Square>(Square.Parse("(0,0)"), Square.Parse("(3,0)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,0)"), Square.Parse("(3,1)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,0)"), Square.Parse("(3,2)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,0)"), Square.Parse("(3,3)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,1)"), Square.Parse("(3,0)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,1)"), Square.Parse("(3,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,1)"), Square.Parse("(3,2)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,1)"), Square.Parse("(3,3)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,2)"), Square.Parse("(3,0)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,2)"), Square.Parse("(3,1)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,2)"), Square.Parse("(3,2)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,2)"), Square.Parse("(3,3)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,3)"), Square.Parse("(3,0)"), false),
                    new AssignmentPair<Square>(Square.Parse("(0,3)"), Square.Parse("(3,1)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,3)"), Square.Parse("(3,2)"), true),
                    new AssignmentPair<Square>(Square.Parse("(0,3)"), Square.Parse("(3,3)"), false)
                ]
            },
            new ConstraintGraphEdge<int, Square>
            {
                FirstVariable = 1,
                SecondVariable = 2,
                Tightness = 0.625,
                AssignmentPairs =
                [
                    new AssignmentPair<Square>(Square.Parse("(1,0)"), Square.Parse("(2,0)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,0)"), Square.Parse("(2,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,0)"), Square.Parse("(2,2)"), true),
                    new AssignmentPair<Square>(Square.Parse("(1,0)"), Square.Parse("(2,3)"), true),
                    new AssignmentPair<Square>(Square.Parse("(1,1)"), Square.Parse("(2,0)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,1)"), Square.Parse("(2,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,1)"), Square.Parse("(2,2)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,1)"), Square.Parse("(2,3)"), true),
                    new AssignmentPair<Square>(Square.Parse("(1,2)"), Square.Parse("(2,0)"), true),
                    new AssignmentPair<Square>(Square.Parse("(1,2)"), Square.Parse("(2,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,2)"), Square.Parse("(2,2)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,2)"), Square.Parse("(2,3)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,3)"), Square.Parse("(2,0)"), true),
                    new AssignmentPair<Square>(Square.Parse("(1,3)"), Square.Parse("(2,1)"), true),
                    new AssignmentPair<Square>(Square.Parse("(1,3)"), Square.Parse("(2,2)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,3)"), Square.Parse("(2,3)"), false)
                ]
            },
            new ConstraintGraphEdge<int, Square>
            {
                FirstVariable = 1,
                SecondVariable = 3,
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<Square>(Square.Parse("(1,0)"), Square.Parse("(3,0)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,0)"), Square.Parse("(3,1)"), true),
                    new AssignmentPair<Square>(Square.Parse("(1,0)"), Square.Parse("(3,2)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,0)"), Square.Parse("(3,3)"), true),
                    new AssignmentPair<Square>(Square.Parse("(1,1)"), Square.Parse("(3,0)"), true),
                    new AssignmentPair<Square>(Square.Parse("(1,1)"), Square.Parse("(3,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,1)"), Square.Parse("(3,2)"), true),
                    new AssignmentPair<Square>(Square.Parse("(1,1)"), Square.Parse("(3,3)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,2)"), Square.Parse("(3,0)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,2)"), Square.Parse("(3,1)"), true),
                    new AssignmentPair<Square>(Square.Parse("(1,2)"), Square.Parse("(3,2)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,2)"), Square.Parse("(3,3)"), true),
                    new AssignmentPair<Square>(Square.Parse("(1,3)"), Square.Parse("(3,0)"), true),
                    new AssignmentPair<Square>(Square.Parse("(1,3)"), Square.Parse("(3,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(1,3)"), Square.Parse("(3,2)"), true),
                    new AssignmentPair<Square>(Square.Parse("(1,3)"), Square.Parse("(3,3)"), false)
                ]
            },
            new ConstraintGraphEdge<int, Square>
            {
                FirstVariable = 2,
                SecondVariable = 3,
                Tightness = 0.625,
                AssignmentPairs =
                [
                    new AssignmentPair<Square>(Square.Parse("(2,0)"), Square.Parse("(3,0)"), false),
                    new AssignmentPair<Square>(Square.Parse("(2,0)"), Square.Parse("(3,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(2,0)"), Square.Parse("(3,2)"), true),
                    new AssignmentPair<Square>(Square.Parse("(2,0)"), Square.Parse("(3,3)"), true),
                    new AssignmentPair<Square>(Square.Parse("(2,1)"), Square.Parse("(3,0)"), false),
                    new AssignmentPair<Square>(Square.Parse("(2,1)"), Square.Parse("(3,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(2,1)"), Square.Parse("(3,2)"), false),
                    new AssignmentPair<Square>(Square.Parse("(2,1)"), Square.Parse("(3,3)"), true),
                    new AssignmentPair<Square>(Square.Parse("(2,2)"), Square.Parse("(3,0)"), true),
                    new AssignmentPair<Square>(Square.Parse("(2,2)"), Square.Parse("(3,1)"), false),
                    new AssignmentPair<Square>(Square.Parse("(2,2)"), Square.Parse("(3,2)"), false),
                    new AssignmentPair<Square>(Square.Parse("(2,2)"), Square.Parse("(3,3)"), false),
                    new AssignmentPair<Square>(Square.Parse("(2,3)"), Square.Parse("(3,0)"), true),
                    new AssignmentPair<Square>(Square.Parse("(2,3)"), Square.Parse("(3,1)"), true),
                    new AssignmentPair<Square>(Square.Parse("(2,3)"), Square.Parse("(3,2)"), false),
                    new AssignmentPair<Square>(Square.Parse("(2,3)"), Square.Parse("(3,3)"), false)
                ]
            }
        ]
    };

    private sealed record TestCase
    {
        public required NQueensProblem Problem { get; init; }

        public required IList<ConstraintGraphNode<int, Square>> ExpectedNodes { get; init; }

        public required IList<ConstraintGraphEdge<int, Square>> ExpectedEdges { get; init; }

        public void Deconstruct(out NQueensProblem problem,
            out IList<ConstraintGraphNode<int, Square>> expectedNodes,
            out IList<ConstraintGraphEdge<int, Square>> expectedEdges)
        {
            problem = Problem;
            expectedNodes = ExpectedNodes;
            expectedEdges = ExpectedEdges;
        }
    }
}
