using Kolyteon.Common;
using Kolyteon.MapColouring;
using Kolyteon.Modelling.Testing;

namespace Kolyteon.Tests.Unit.MapColouring;

public static partial class MapColouringConstraintGraphTests
{
    private static readonly TestCase TestCaseOne = new()
    {
        Problem = MapColouringProblem.Create()
            .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
            .UseGlobalColours(Colour.White, Colour.Red, Colour.White, Colour.Black)
            .AddBlock(Block.Parse("(0,0) [3x3]")).Build(),
        ExpectedNodes =
        [
            new ConstraintGraphNodeDatum<Block, Colour>
            {
                Variable = Block.Parse("(0,0) [3x3]"),
                Degree = 0,
                SumTightness = 0.0,
                Domain = [Colour.Black, Colour.Red, Colour.White]
            }
        ],
        ExpectedEdges = []
    };

    private static readonly TestCase TestCaseTwo = new()
    {
        Problem = MapColouringProblem.Create()
            .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
            .UseGlobalColours(Colour.Black, Colour.White)
            .AddBlock(Block.Parse("(3,0) [3x3]"))
            .AddBlock(Block.Parse("(0,0) [3x3]")).Build(),
        ExpectedNodes =
        [
            new ConstraintGraphNodeDatum<Block, Colour>
            {
                Variable = Block.Parse("(0,0) [3x3]"), Degree = 1, SumTightness = 0.5, Domain = [Colour.Black, Colour.White]
            },
            new ConstraintGraphNodeDatum<Block, Colour>
            {
                Variable = Block.Parse("(3,0) [3x3]"), Degree = 1, SumTightness = 0.5, Domain = [Colour.Black, Colour.White]
            }
        ],
        ExpectedEdges =
        [
            new ConstraintGraphEdgeDatum<Block, Colour>
            {
                FirstVariable = Block.Parse("(0,0) [3x3]"),
                SecondVariable = Block.Parse("(3,0) [3x3]"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<Colour>(Colour.Black, Colour.Black, false),
                    new AssignmentPair<Colour>(Colour.Black, Colour.White, true),
                    new AssignmentPair<Colour>(Colour.White, Colour.Black, true),
                    new AssignmentPair<Colour>(Colour.White, Colour.White, false)
                ]
            }
        ]
    };

    private static readonly TestCase TestCaseThree = new()
    {
        Problem = MapColouringProblem.Create()
            .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
            .UseBlockSpecificColours()
            .AddBlockAndColours(Block.Parse("(0,0) [3x3]"))
            .AddBlockAndColours(Block.Parse("(3,0) [3x3]"), Colour.Black).Build(),
        ExpectedNodes =
        [
            new ConstraintGraphNodeDatum<Block, Colour>
            {
                Variable = Block.Parse("(0,0) [3x3]"), Degree = 0, SumTightness = 0.0, Domain = []
            },
            new ConstraintGraphNodeDatum<Block, Colour>
            {
                Variable = Block.Parse("(3,0) [3x3]"), Degree = 0, SumTightness = 0.0, Domain = [Colour.Black]
            }
        ],
        ExpectedEdges = []
    };


    private static readonly TestCase TestCaseFour = new()
    {
        Problem = MapColouringProblem.Create()
            .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
            .UseGlobalColours(Colour.Black, Colour.White)
            .AddBlock(Block.Parse("(0,0) [3x3]"))
            .AddBlock(Block.Parse("(3,0) [3x3]"))
            .AddBlock(Block.Parse("(3,3) [3x3]")).Build(),
        ExpectedNodes =
        [
            new ConstraintGraphNodeDatum<Block, Colour>
            {
                Variable = Block.Parse("(0,0) [3x3]"), Degree = 1, SumTightness = 0.5, Domain = [Colour.Black, Colour.White]
            },
            new ConstraintGraphNodeDatum<Block, Colour>
            {
                Variable = Block.Parse("(3,0) [3x3]"), Degree = 2, SumTightness = 1.0, Domain = [Colour.Black, Colour.White]
            },
            new ConstraintGraphNodeDatum<Block, Colour>
            {
                Variable = Block.Parse("(3,3) [3x3]"), Degree = 1, SumTightness = 0.5, Domain = [Colour.Black, Colour.White]
            }
        ],
        ExpectedEdges =
        [
            new ConstraintGraphEdgeDatum<Block, Colour>
            {
                FirstVariable = Block.Parse("(0,0) [3x3]"),
                SecondVariable = Block.Parse("(3,0) [3x3]"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<Colour>(Colour.Black, Colour.Black, false),
                    new AssignmentPair<Colour>(Colour.Black, Colour.White, true),
                    new AssignmentPair<Colour>(Colour.White, Colour.Black, true),
                    new AssignmentPair<Colour>(Colour.White, Colour.White, false)
                ]
            },
            new ConstraintGraphEdgeDatum<Block, Colour>
            {
                FirstVariable = Block.Parse("(3,0) [3x3]"),
                SecondVariable = Block.Parse("(3,3) [3x3]"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<Colour>(Colour.Black, Colour.Black, false),
                    new AssignmentPair<Colour>(Colour.Black, Colour.White, true),
                    new AssignmentPair<Colour>(Colour.White, Colour.Black, true),
                    new AssignmentPair<Colour>(Colour.White, Colour.White, false)
                ]
            }
        ]
    };


    private static readonly TestCase TestCaseFive = new()
    {
        Problem = MapColouringProblem.Create()
            .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
            .UseBlockSpecificColours()
            .AddBlockAndColours(Block.Parse("(0,6) [3x1]"), Colour.Red, Colour.Blue)
            .AddBlockAndColours(Block.Parse("(1,1) [2x3]"), Colour.Red, Colour.Blue)
            .AddBlockAndColours(Block.Parse("(3,1) [4x2]"), Colour.Green, Colour.Red, Colour.Blue)
            .AddBlockAndColours(Block.Parse("(3,3) [2x3]"), Colour.Red)
            .AddBlockAndColours(Block.Parse("(5,3) [2x2]"), Colour.Yellow, Colour.Blue).Build(),
        ExpectedNodes =
        [
            new ConstraintGraphNodeDatum<Block, Colour>
            {
                Variable = Block.Parse("(0,6) [3x1]"), Degree = 0, SumTightness = 0.0, Domain = [Colour.Red, Colour.Blue]
            },
            new ConstraintGraphNodeDatum<Block, Colour>
            {
                Variable = Block.Parse("(1,1) [2x3]"),
                Degree = 2,
                SumTightness = 0.833333333,
                Domain = [Colour.Red, Colour.Blue]
            },
            new ConstraintGraphNodeDatum<Block, Colour>
            {
                Variable = Block.Parse("(3,1) [4x2]"),
                Degree = 3,
                SumTightness = 0.833333333,
                Domain = [Colour.Green, Colour.Red, Colour.Blue]
            },
            new ConstraintGraphNodeDatum<Block, Colour>
            {
                Variable = Block.Parse("(3,3) [2x3]"), Degree = 2, SumTightness = 0.833333333, Domain = [Colour.Red]
            },
            new ConstraintGraphNodeDatum<Block, Colour>
            {
                Variable = Block.Parse("(5,3) [2x2]"),
                Degree = 1,
                SumTightness = 0.166666667,
                Domain = [Colour.Yellow, Colour.Blue]
            }
        ],
        ExpectedEdges =
        [
            new ConstraintGraphEdgeDatum<Block, Colour>
            {
                FirstVariable = Block.Parse("(1,1) [2x3]"),
                SecondVariable = Block.Parse("(3,1) [4x2]"),
                Tightness = 0.333333333,
                AssignmentPairs =
                [
                    new AssignmentPair<Colour>(Colour.Red, Colour.Green, true),
                    new AssignmentPair<Colour>(Colour.Red, Colour.Red, false),
                    new AssignmentPair<Colour>(Colour.Red, Colour.Blue, true),
                    new AssignmentPair<Colour>(Colour.Blue, Colour.Green, true),
                    new AssignmentPair<Colour>(Colour.Blue, Colour.Red, true),
                    new AssignmentPair<Colour>(Colour.Blue, Colour.Blue, false)
                ]
            },
            new ConstraintGraphEdgeDatum<Block, Colour>
            {
                FirstVariable = Block.Parse("(1,1) [2x3]"),
                SecondVariable = Block.Parse("(3,3) [2x3]"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<Colour>(Colour.Red, Colour.Red, false),
                    new AssignmentPair<Colour>(Colour.Blue, Colour.Red, true)
                ]
            },
            new ConstraintGraphEdgeDatum<Block, Colour>
            {
                FirstVariable = Block.Parse("(3,1) [4x2]"),
                SecondVariable = Block.Parse("(3,3) [2x3]"),
                Tightness = 0.333333333,
                AssignmentPairs =
                [
                    new AssignmentPair<Colour>(Colour.Green, Colour.Red, true),
                    new AssignmentPair<Colour>(Colour.Red, Colour.Red, false),
                    new AssignmentPair<Colour>(Colour.Blue, Colour.Red, true)
                ]
            },
            new ConstraintGraphEdgeDatum<Block, Colour>
            {
                FirstVariable = Block.Parse("(3,1) [4x2]"),
                SecondVariable = Block.Parse("(5,3) [2x2]"),
                Tightness = 0.166666667,
                AssignmentPairs =
                [
                    new AssignmentPair<Colour>(Colour.Green, Colour.Yellow, true),
                    new AssignmentPair<Colour>(Colour.Green, Colour.Blue, true),
                    new AssignmentPair<Colour>(Colour.Red, Colour.Yellow, true),
                    new AssignmentPair<Colour>(Colour.Red, Colour.Blue, true),
                    new AssignmentPair<Colour>(Colour.Blue, Colour.Yellow, true),
                    new AssignmentPair<Colour>(Colour.Blue, Colour.Blue, false)
                ]
            }
        ]
    };

    private sealed record TestCase
    {
        public required MapColouringProblem Problem { get; init; }

        public required IList<ConstraintGraphNodeDatum<Block, Colour>> ExpectedNodes { get; init; }

        public required IList<ConstraintGraphEdgeDatum<Block, Colour>> ExpectedEdges { get; init; }

        public void Deconstruct(out MapColouringProblem problem,
            out IList<ConstraintGraphNodeDatum<Block, Colour>> expectedNodes,
            out IList<ConstraintGraphEdgeDatum<Block, Colour>> expectedEdges)
        {
            problem = Problem;
            expectedNodes = ExpectedNodes;
            expectedEdges = ExpectedEdges;
        }
    }
}
