using Kolyteon.Common;
using Kolyteon.Modelling.Testing;
using Kolyteon.Shikaku;

namespace Kolyteon.Tests.Unit.Shikaku;

public static partial class ShikakuConstraintGraphTests
{
    private static readonly TestCase TestCaseOne = new()
    {
        Problem = ShikakuProblem.FromGrid(new int?[,]
        {
            { null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null },
            { null, null, null, 0049, null, null, null },
            { null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null },
            { null, null, null, null, null, null, null }
        }),
        ExpectedNodes =
        [
            new ConstraintGraphNode<NumberedSquare, Block>
            {
                Variable = NumberedSquare.Parse("(3,3) [49]"),
                Degree = 0,
                SumTightness = 0.0,
                Domain =
                [
                    Block.Parse("(0,0) [7x7]")
                ]
            }
        ],
        ExpectedEdges = []
    };

    private static readonly TestCase TestCaseTwo = new()
    {
        Problem = ShikakuProblem.FromGrid(new int?[,]
        {
            { 0005, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0020 }
        }),
        ExpectedNodes =
        [
            new ConstraintGraphNode<NumberedSquare, Block>
            {
                Variable = NumberedSquare.Parse("(0,0) [5]"),
                Degree = 1,
                SumTightness = 0.5,
                Domain =
                [
                    Block.Parse("(0,0) [1x5]"),
                    Block.Parse("(0,0) [5x1]")
                ]
            },
            new ConstraintGraphNode<NumberedSquare, Block>
            {
                Variable = NumberedSquare.Parse("(4,4) [20]"),
                Degree = 1,
                SumTightness = 0.5,
                Domain =
                [
                    Block.Parse("(0,1) [5x4]"),
                    Block.Parse("(1,0) [4x5]")
                ]
            }
        ],
        ExpectedEdges =
        [
            new ConstraintGraphEdge<NumberedSquare, Block>
            {
                FirstVariable = NumberedSquare.Parse("(0,0) [5]"),
                SecondVariable = NumberedSquare.Parse("(4,4) [20]"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<Block>(Block.Parse("(0,0) [1x5]"), Block.Parse("(0,1) [5x4]"), false),
                    new AssignmentPair<Block>(Block.Parse("(0,0) [1x5]"), Block.Parse("(1,0) [4x5]"), true),
                    new AssignmentPair<Block>(Block.Parse("(0,0) [5x1]"), Block.Parse("(0,1) [5x4]"), true),
                    new AssignmentPair<Block>(Block.Parse("(0,0) [5x1]"), Block.Parse("(1,0) [4x5]"), false)
                ]
            }
        ]
    };

    private static readonly TestCase TestCaseThree = new()
    {
        Problem = ShikakuProblem.FromGrid(new int?[,]
        {
            { 0004, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0021 }
        }),
        ExpectedNodes =
        [
            new ConstraintGraphNode<NumberedSquare, Block>
            {
                Variable = NumberedSquare.Parse("(0,0) [4]"),
                Degree = 0,
                SumTightness = 0.0,
                Domain =
                [
                    Block.Parse("(0,0) [1x4]"),
                    Block.Parse("(0,0) [2x2]"),
                    Block.Parse("(0,0) [4x1]")
                ]
            },
            new ConstraintGraphNode<NumberedSquare, Block>
            {
                Variable = NumberedSquare.Parse("(4,4) [21]"),
                Degree = 0,
                SumTightness = 0.0,
                Domain =
                [
                ]
            }
        ],
        ExpectedEdges = []
    };

    private static readonly TestCase TestCaseFour = new()
    {
        Problem = ShikakuProblem.FromGrid(new int?[,]
        {
            { 0005, null, null, null, null },
            { null, null, null, null, null },
            { 0020, null, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, null }
        }),
        ExpectedNodes =
        [
            new ConstraintGraphNode<NumberedSquare, Block>
            {
                Variable = NumberedSquare.Parse("(0,0) [5]"),
                Degree = 0,
                SumTightness = 0.0,
                Domain =
                [
                    Block.Parse("(0,0) [5x1]")
                ]
            },
            new ConstraintGraphNode<NumberedSquare, Block>
            {
                Variable = NumberedSquare.Parse("(0,2) [20]"),
                Degree = 0,
                SumTightness = 0.0,
                Domain =
                [
                    Block.Parse("(0,1) [5x4]")
                ]
            }
        ],
        ExpectedEdges = []
    };

    private static readonly TestCase TestCaseFive = new()
    {
        Problem = ShikakuProblem.FromGrid(new int?[,]
        {
            { null, null, 0003, null, null },
            { null, 0010, null, null, null },
            { null, null, null, null, null },
            { null, null, null, null, 0012 },
            { null, null, null, null, null }
        }),
        ExpectedNodes =
        [
            new ConstraintGraphNode<NumberedSquare, Block>
            {
                Variable = NumberedSquare.Parse("(1,1) [10]"),
                Degree = 2,
                SumTightness = 1.125,
                Domain =
                [
                    Block.Parse("(0,0) [2x5]"),
                    Block.Parse("(0,1) [5x2]")
                ]
            },
            new ConstraintGraphNode<NumberedSquare, Block>
            {
                Variable = NumberedSquare.Parse("(2,0) [3]"),
                Degree = 2,
                SumTightness = 0.625,
                Domain =
                [
                    Block.Parse("(0,0) [3x1]"),
                    Block.Parse("(1,0) [3x1]"),
                    Block.Parse("(2,0) [1x3]"),
                    Block.Parse("(2,0) [3x1]")
                ]
            },
            new ConstraintGraphNode<NumberedSquare, Block>
            {
                Variable = NumberedSquare.Parse("(4,3) [12]"),
                Degree = 2,
                SumTightness = 1.0,
                Domain =
                [
                    Block.Parse("(1,2) [4x3]"),
                    Block.Parse("(2,1) [3x4]")
                ]
            }
        ],
        ExpectedEdges =
        [
            new ConstraintGraphEdge<NumberedSquare, Block>
            {
                FirstVariable = NumberedSquare.Parse("(1,1) [10]"),
                SecondVariable = NumberedSquare.Parse("(2,0) [3]"),
                Tightness = 0.375,
                AssignmentPairs =
                [
                    new AssignmentPair<Block>(Block.Parse("(0,0) [2x5]"), Block.Parse("(0,0) [3x1]"), false),
                    new AssignmentPair<Block>(Block.Parse("(0,0) [2x5]"), Block.Parse("(1,0) [3x1]"), false),
                    new AssignmentPair<Block>(Block.Parse("(0,0) [2x5]"), Block.Parse("(2,0) [1x3]"), true),
                    new AssignmentPair<Block>(Block.Parse("(0,0) [2x5]"), Block.Parse("(2,0) [3x1]"), true),
                    new AssignmentPair<Block>(Block.Parse("(0,1) [5x2]"), Block.Parse("(0,0) [3x1]"), true),
                    new AssignmentPair<Block>(Block.Parse("(0,1) [5x2]"), Block.Parse("(1,0) [3x1]"), true),
                    new AssignmentPair<Block>(Block.Parse("(0,1) [5x2]"), Block.Parse("(2,0) [1x3]"), false),
                    new AssignmentPair<Block>(Block.Parse("(0,1) [5x2]"), Block.Parse("(2,0) [3x1]"), true)
                ]
            },
            new ConstraintGraphEdge<NumberedSquare, Block>
            {
                FirstVariable = NumberedSquare.Parse("(1,1) [10]"),
                SecondVariable = NumberedSquare.Parse("(4,3) [12]"),
                Tightness = 0.75,
                AssignmentPairs =
                [
                    new AssignmentPair<Block>(Block.Parse("(0,0) [2x5]"), Block.Parse("(1,2) [4x3]"), false),
                    new AssignmentPair<Block>(Block.Parse("(0,0) [2x5]"), Block.Parse("(2,1) [3x4]"), true),
                    new AssignmentPair<Block>(Block.Parse("(0,1) [5x2]"), Block.Parse("(1,2) [4x3]"), false),
                    new AssignmentPair<Block>(Block.Parse("(0,1) [5x2]"), Block.Parse("(2,1) [3x4]"), false)
                ]
            },
            new ConstraintGraphEdge<NumberedSquare, Block>
            {
                FirstVariable = NumberedSquare.Parse("(2,0) [3]"),
                SecondVariable = NumberedSquare.Parse("(4,3) [12]"),
                Tightness = 0.25,
                AssignmentPairs =
                [
                    new AssignmentPair<Block>(Block.Parse("(0,0) [3x1]"), Block.Parse("(1,2) [4x3]"), true),
                    new AssignmentPair<Block>(Block.Parse("(0,0) [3x1]"), Block.Parse("(2,1) [3x4]"), true),
                    new AssignmentPair<Block>(Block.Parse("(1,0) [3x1]"), Block.Parse("(1,2) [4x3]"), true),
                    new AssignmentPair<Block>(Block.Parse("(1,0) [3x1]"), Block.Parse("(2,1) [3x4]"), true),
                    new AssignmentPair<Block>(Block.Parse("(2,0) [1x3]"), Block.Parse("(1,2) [4x3]"), false),
                    new AssignmentPair<Block>(Block.Parse("(2,0) [1x3]"), Block.Parse("(2,1) [3x4]"), false),
                    new AssignmentPair<Block>(Block.Parse("(2,0) [3x1]"), Block.Parse("(1,2) [4x3]"), true),
                    new AssignmentPair<Block>(Block.Parse("(2,0) [3x1]"), Block.Parse("(2,1) [3x4]"), true)
                ]
            }
        ]
    };

    private static readonly TestCase TestCaseSix = new()
    {
        Problem = ShikakuProblem.FromGrid(new int?[,]
        {
            { null, null, 0009, null, null },
            { null, null, null, null, 0010 },
            { null, null, null, null, null },
            { null, 0004, 0002, null, null },
            { null, null, null, null, null }
        }),
        ExpectedNodes =
        [
            new ConstraintGraphNode<NumberedSquare, Block>
            {
                Variable = NumberedSquare.Parse("(1,3) [4]"),
                Degree = 2,
                SumTightness = 1.125,
                Domain =
                [
                    Block.Parse("(0,2) [2x2]"),
                    Block.Parse("(0,3) [2x2]"),
                    Block.Parse("(1,0) [1x4]"),
                    Block.Parse("(1,1) [1x4]")
                ]
            },
            new ConstraintGraphNode<NumberedSquare, Block>
            {
                Variable = NumberedSquare.Parse("(2,0) [9]"),
                Degree = 3,
                SumTightness = 1.833333333,
                Domain =
                [
                    Block.Parse("(0,0) [3x3]"),
                    Block.Parse("(1,0) [3x3]")
                ]
            },
            new ConstraintGraphNode<NumberedSquare, Block>
            {
                Variable = NumberedSquare.Parse("(2,3) [2]"),
                Degree = 2,
                SumTightness = 0.666666667,
                Domain =
                [
                    Block.Parse("(2,2) [1x2]"),
                    Block.Parse("(2,3) [1x2]"),
                    Block.Parse("(2,3) [2x1]")
                ]
            },
            new ConstraintGraphNode<NumberedSquare, Block>
            {
                Variable = NumberedSquare.Parse("(4,1) [10]"),
                Degree = 3,
                SumTightness = 1.458333333,
                Domain =
                [
                    Block.Parse("(0,1) [5x2]"),
                    Block.Parse("(3,0) [2x5]")
                ]
            }
        ],
        ExpectedEdges =
        [
            new ConstraintGraphEdge<NumberedSquare, Block>
            {
                FirstVariable = NumberedSquare.Parse("(1,3) [4]"),
                SecondVariable = NumberedSquare.Parse("(2,0) [9]"),
                Tightness = 0.75,
                AssignmentPairs =
                [
                    new AssignmentPair<Block>(Block.Parse("(0,2) [2x2]"), Block.Parse("(0,0) [3x3]"), false),
                    new AssignmentPair<Block>(Block.Parse("(0,2) [2x2]"), Block.Parse("(1,0) [3x3]"), false),
                    new AssignmentPair<Block>(Block.Parse("(0,3) [2x2]"), Block.Parse("(0,0) [3x3]"), true),
                    new AssignmentPair<Block>(Block.Parse("(0,3) [2x2]"), Block.Parse("(1,0) [3x3]"), true),
                    new AssignmentPair<Block>(Block.Parse("(1,0) [1x4]"), Block.Parse("(0,0) [3x3]"), false),
                    new AssignmentPair<Block>(Block.Parse("(1,0) [1x4]"), Block.Parse("(1,0) [3x3]"), false),
                    new AssignmentPair<Block>(Block.Parse("(1,1) [1x4]"), Block.Parse("(0,0) [3x3]"), false),
                    new AssignmentPair<Block>(Block.Parse("(1,1) [1x4]"), Block.Parse("(1,0) [3x3]"), false)
                ]
            },
            new ConstraintGraphEdge<NumberedSquare, Block>
            {
                FirstVariable = NumberedSquare.Parse("(1,3) [4]"),
                SecondVariable = NumberedSquare.Parse("(4,1) [10]"),
                Tightness = 0.375,
                AssignmentPairs =
                [
                    new AssignmentPair<Block>(Block.Parse("(0,2) [2x2]"), Block.Parse("(0,1) [5x2]"), false),
                    new AssignmentPair<Block>(Block.Parse("(0,2) [2x2]"), Block.Parse("(3,0) [2x5]"), true),
                    new AssignmentPair<Block>(Block.Parse("(0,3) [2x2]"), Block.Parse("(0,1) [5x2]"), true),
                    new AssignmentPair<Block>(Block.Parse("(0,3) [2x2]"), Block.Parse("(3,0) [2x5]"), true),
                    new AssignmentPair<Block>(Block.Parse("(1,0) [1x4]"), Block.Parse("(0,1) [5x2]"), false),
                    new AssignmentPair<Block>(Block.Parse("(1,0) [1x4]"), Block.Parse("(3,0) [2x5]"), true),
                    new AssignmentPair<Block>(Block.Parse("(1,1) [1x4]"), Block.Parse("(0,1) [5x2]"), false),
                    new AssignmentPair<Block>(Block.Parse("(1,1) [1x4]"), Block.Parse("(3,0) [2x5]"), true)
                ]
            },
            new ConstraintGraphEdge<NumberedSquare, Block>
            {
                FirstVariable = NumberedSquare.Parse("(2,0) [9]"),
                SecondVariable = NumberedSquare.Parse("(2,3) [2]"),
                Tightness = 0.333333333,
                AssignmentPairs =
                [
                    new AssignmentPair<Block>(Block.Parse("(0,0) [3x3]"), Block.Parse("(2,2) [1x2]"), false),
                    new AssignmentPair<Block>(Block.Parse("(0,0) [3x3]"), Block.Parse("(2,3) [1x2]"), true),
                    new AssignmentPair<Block>(Block.Parse("(0,0) [3x3]"), Block.Parse("(2,3) [2x1]"), true),
                    new AssignmentPair<Block>(Block.Parse("(1,0) [3x3]"), Block.Parse("(2,2) [1x2]"), false),
                    new AssignmentPair<Block>(Block.Parse("(1,0) [3x3]"), Block.Parse("(2,3) [1x2]"), true),
                    new AssignmentPair<Block>(Block.Parse("(1,0) [3x3]"), Block.Parse("(2,3) [2x1]"), true)
                ]
            },
            new ConstraintGraphEdge<NumberedSquare, Block>
            {
                FirstVariable = NumberedSquare.Parse("(2,0) [9]"),
                SecondVariable = NumberedSquare.Parse("(4,1) [10]"),
                Tightness = 0.75,
                AssignmentPairs =
                [
                    new AssignmentPair<Block>(Block.Parse("(0,0) [3x3]"), Block.Parse("(0,1) [5x2]"), false),
                    new AssignmentPair<Block>(Block.Parse("(0,0) [3x3]"), Block.Parse("(3,0) [2x5]"), true),
                    new AssignmentPair<Block>(Block.Parse("(1,0) [3x3]"), Block.Parse("(0,1) [5x2]"), false),
                    new AssignmentPair<Block>(Block.Parse("(1,0) [3x3]"), Block.Parse("(3,0) [2x5]"), false)
                ]
            },
            new ConstraintGraphEdge<NumberedSquare, Block>
            {
                FirstVariable = NumberedSquare.Parse("(2,3) [2]"),
                SecondVariable = NumberedSquare.Parse("(4,1) [10]"),
                Tightness = 0.333333333,
                AssignmentPairs =
                [
                    new AssignmentPair<Block>(Block.Parse("(2,2) [1x2]"), Block.Parse("(0,1) [5x2]"), false),
                    new AssignmentPair<Block>(Block.Parse("(2,2) [1x2]"), Block.Parse("(3,0) [2x5]"), true),
                    new AssignmentPair<Block>(Block.Parse("(2,3) [1x2]"), Block.Parse("(0,1) [5x2]"), true),
                    new AssignmentPair<Block>(Block.Parse("(2,3) [1x2]"), Block.Parse("(3,0) [2x5]"), true),
                    new AssignmentPair<Block>(Block.Parse("(2,3) [2x1]"), Block.Parse("(0,1) [5x2]"), true),
                    new AssignmentPair<Block>(Block.Parse("(2,3) [2x1]"), Block.Parse("(3,0) [2x5]"), false)
                ]
            }
        ]
    };

    private sealed record TestCase
    {
        public required ShikakuProblem Problem { get; init; }

        public required IList<ConstraintGraphNode<NumberedSquare, Block>> ExpectedNodes { get; init; }

        public required IList<ConstraintGraphEdge<NumberedSquare, Block>> ExpectedEdges { get; init; }

        public void Deconstruct(out ShikakuProblem problem,
            out IList<ConstraintGraphNode<NumberedSquare, Block>> expectedNodes,
            out IList<ConstraintGraphEdge<NumberedSquare, Block>> expectedEdges)
        {
            problem = Problem;
            expectedNodes = ExpectedNodes;
            expectedEdges = ExpectedEdges;
        }
    }
}
