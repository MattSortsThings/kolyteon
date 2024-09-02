using Kolyteon.Common;
using Kolyteon.GraphColouring;
using Kolyteon.Modelling.Testing;

namespace Kolyteon.Tests.Unit.GraphColouring;

public static partial class GraphColouringConstraintGraphTests
{
    private static readonly TestCase TestCaseOne = new()
    {
        Problem = GraphColouringProblem.Create()
            .UseGlobalColours(Colour.White, Colour.White, Colour.Black, Colour.Red)
            .AddNode(Node.FromName("N0"))
            .Build(),
        ExpectedNodes =
        [
            new ConstraintGraphNodeDatum<Node, Colour>
            {
                Variable = Node.FromName("N0"),
                Degree = 0,
                SumTightness = 0.0,
                Domain = [Colour.Black, Colour.Red, Colour.White]
            }
        ],
        ExpectedEdges = []
    };

    private static readonly TestCase TestCaseTwo = new()
    {
        Problem = GraphColouringProblem.Create()
            .UseGlobalColours(Colour.Black, Colour.White)
            .AddNode(Node.FromName("N1"))
            .AddNode(Node.FromName("N0")).Build(),
        ExpectedNodes =
        [
            new ConstraintGraphNodeDatum<Node, Colour>
            {
                Variable = Node.FromName("N0"), Degree = 0, SumTightness = 0.0, Domain = [Colour.Black, Colour.White]
            },
            new ConstraintGraphNodeDatum<Node, Colour>
            {
                Variable = Node.FromName("N1"), Degree = 0, SumTightness = 0.0, Domain = [Colour.Black, Colour.White]
            }
        ],
        ExpectedEdges = []
    };

    private static readonly TestCase TestCaseThree = new()
    {
        Problem = GraphColouringProblem.Create()
            .UseGlobalColours(Colour.Black, Colour.White)
            .AddNode(Node.FromName("N0"))
            .AddNode(Node.FromName("N1"))
            .AddEdge(Edge.Between(Node.FromName("N0"), Node.FromName("N1")))
            .Build(),
        ExpectedNodes =
        [
            new ConstraintGraphNodeDatum<Node, Colour>
            {
                Variable = Node.FromName("N0"), Degree = 1, SumTightness = 0.5, Domain = [Colour.Black, Colour.White]
            },
            new ConstraintGraphNodeDatum<Node, Colour>
            {
                Variable = Node.FromName("N1"), Degree = 1, SumTightness = 0.5, Domain = [Colour.Black, Colour.White]
            }
        ],
        ExpectedEdges =
        [
            new ConstraintGraphEdgeDatum<Node, Colour>
            {
                FirstVariable = Node.FromName("N0"),
                SecondVariable = Node.FromName("N1"),
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

    private static readonly TestCase TestCaseFour = new()
    {
        Problem = GraphColouringProblem.Create()
            .UseNodeSpecificColours()
            .AddNodeAndColours(Node.FromName("N0"), Colour.Black, Colour.White)
            .AddNodeAndColours(Node.FromName("N1"), Colour.Green, Colour.Red)
            .AddEdge(Edge.Between(Node.FromName("N0"), Node.FromName("N1")))
            .Build(),
        ExpectedNodes =
        [
            new ConstraintGraphNodeDatum<Node, Colour>
            {
                Variable = Node.FromName("N0"), Degree = 0, SumTightness = 0.0, Domain = [Colour.Black, Colour.White]
            },
            new ConstraintGraphNodeDatum<Node, Colour>
            {
                Variable = Node.FromName("N1"), Degree = 0, SumTightness = 0.0, Domain = [Colour.Green, Colour.Red]
            }
        ],
        ExpectedEdges = []
    };

    private static readonly TestCase TestCaseFive = new()
    {
        Problem = GraphColouringProblem.Create()
            .UseNodeSpecificColours()
            .AddNodeAndColours(Node.FromName("N0"), Colour.Black, Colour.White)
            .AddNodeAndColours(Node.FromName("N1"), Colour.Green, Colour.Red)
            .AddNodeAndColours(Node.FromName("N2"), Colour.Black, Colour.Green, Colour.Red, Colour.White)
            .AddEdge(Edge.Between(Node.FromName("N0"), Node.FromName("N1")))
            .AddEdge(Edge.Between(Node.FromName("N0"), Node.FromName("N2")))
            .AddEdge(Edge.Between(Node.FromName("N1"), Node.FromName("N2")))
            .Build(),
        ExpectedNodes =
        [
            new ConstraintGraphNodeDatum<Node, Colour>
            {
                Variable = Node.FromName("N0"), Degree = 1, SumTightness = 0.25, Domain = [Colour.Black, Colour.White]
            },
            new ConstraintGraphNodeDatum<Node, Colour>
            {
                Variable = Node.FromName("N1"), Degree = 1, SumTightness = 0.25, Domain = [Colour.Green, Colour.Red]
            },
            new ConstraintGraphNodeDatum<Node, Colour>
            {
                Variable = Node.FromName("N2"),
                Degree = 2,
                SumTightness = 0.5,
                Domain = [Colour.Black, Colour.Green, Colour.Red, Colour.White]
            }
        ],
        ExpectedEdges =
        [
            new ConstraintGraphEdgeDatum<Node, Colour>
            {
                FirstVariable = Node.FromName("N0"),
                SecondVariable = Node.FromName("N2"),
                Tightness = 0.25,
                AssignmentPairs =
                [
                    new AssignmentPair<Colour>(Colour.Black, Colour.Black, false),
                    new AssignmentPair<Colour>(Colour.Black, Colour.Green, true),
                    new AssignmentPair<Colour>(Colour.Black, Colour.Red, true),
                    new AssignmentPair<Colour>(Colour.Black, Colour.White, true),
                    new AssignmentPair<Colour>(Colour.White, Colour.Black, true),
                    new AssignmentPair<Colour>(Colour.White, Colour.Green, true),
                    new AssignmentPair<Colour>(Colour.White, Colour.Red, true),
                    new AssignmentPair<Colour>(Colour.White, Colour.White, false)
                ]
            },
            new ConstraintGraphEdgeDatum<Node, Colour>
            {
                FirstVariable = Node.FromName("N1"),
                SecondVariable = Node.FromName("N2"),
                Tightness = 0.25,
                AssignmentPairs =
                [
                    new AssignmentPair<Colour>(Colour.Green, Colour.Black, true),
                    new AssignmentPair<Colour>(Colour.Green, Colour.Green, false),
                    new AssignmentPair<Colour>(Colour.Green, Colour.Red, true),
                    new AssignmentPair<Colour>(Colour.Green, Colour.White, true),
                    new AssignmentPair<Colour>(Colour.Red, Colour.Black, true),
                    new AssignmentPair<Colour>(Colour.Red, Colour.Green, true),
                    new AssignmentPair<Colour>(Colour.Red, Colour.Red, false),
                    new AssignmentPair<Colour>(Colour.Red, Colour.White, true)
                ]
            }
        ]
    };


    private sealed record TestCase
    {
        public required GraphColouringProblem Problem { get; init; }

        public required IList<ConstraintGraphNodeDatum<Node, Colour>> ExpectedNodes { get; init; }

        public required IList<ConstraintGraphEdgeDatum<Node, Colour>> ExpectedEdges { get; init; }

        public void Deconstruct(out GraphColouringProblem problem,
            out IList<ConstraintGraphNodeDatum<Node, Colour>> expectedNodes,
            out IList<ConstraintGraphEdgeDatum<Node, Colour>> expectedEdges)
        {
            problem = Problem;
            expectedNodes = ExpectedNodes;
            expectedEdges = ExpectedEdges;
        }
    }
}
