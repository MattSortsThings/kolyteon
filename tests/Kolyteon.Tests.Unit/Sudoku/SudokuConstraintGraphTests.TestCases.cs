using Kolyteon.Common;
using Kolyteon.Modelling.Testing;
using Kolyteon.Sudoku;

namespace Kolyteon.Tests.Unit.Sudoku;

public static partial class SudokuConstraintGraphTests
{
    private static readonly TestCase TestCaseOne = new()
    {
        Problem = SudokuProblem.FromGrid(new int?[,]
        {
            { null, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
            { 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
            { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
            { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
            { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
            { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
            { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 },
            { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
            { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 }
        }),
        ExpectedNodes =
        [
            new ConstraintGraphNode<Square, int>
            {
                Variable = Square.Parse("(0,0)"), Degree = 0, SumTightness = 0.0, Domain = [1]
            }
        ],
        ExpectedEdges = []
    };

    private static readonly TestCase TestCaseTwo = new()
    {
        Problem = SudokuProblem.FromGrid(new int?[,]
        {
            { null, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
            { 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
            { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
            { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
            { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
            { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
            { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 },
            { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
            { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, null }
        }),
        ExpectedNodes =
        [
            new ConstraintGraphNode<Square, int>
            {
                Variable = Square.Parse("(0,0)"), Degree = 0, SumTightness = 0.0, Domain = [1]
            },
            new ConstraintGraphNode<Square, int>
            {
                Variable = Square.Parse("(8,8)"), Degree = 0, SumTightness = 0.0, Domain = [8]
            }
        ],
        ExpectedEdges = []
    };

    private static readonly TestCase TestCaseThree = new()
    {
        Problem = SudokuProblem.FromGrid(new int?[,]
        {
            { null, null, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
            { 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
            { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
            { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
            { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
            { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
            { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 },
            { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
            { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, null }
        }),
        ExpectedNodes =
        [
            new ConstraintGraphNode<Square, int>
            {
                Variable = Square.Parse("(0,0)"), Degree = 0, SumTightness = 0.0, Domain = [1]
            },
            new ConstraintGraphNode<Square, int>
            {
                Variable = Square.Parse("(1,0)"), Degree = 0, SumTightness = 0.0, Domain = [2]
            },
            new ConstraintGraphNode<Square, int>
            {
                Variable = Square.Parse("(8,8)"), Degree = 0, SumTightness = 0.0, Domain = [8]
            }
        ],
        ExpectedEdges = []
    };

    private static readonly TestCase TestCaseFour = new()
    {
        Problem = SudokuProblem.FromGrid(new int?[,]
        {
            { null, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
            { 0004, null, null, 0007, 0008, 0009, 0001, 0002, 0003 },
            { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
            { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
            { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
            { null, null, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
            { null, 0007, 0008, 0009, 0001, 0002, 0003, null, 0005 },
            { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
            { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, null }
        }),
        ExpectedNodes =
        [
            new ConstraintGraphNode<Square, int>
            {
                Variable = Square.Parse("(0,0)"), Degree = 0, SumTightness = 0.0, Domain = [1]
            },
            new ConstraintGraphNode<Square, int>
            {
                Variable = Square.Parse("(0,5)"), Degree = 2, SumTightness = 1.0, Domain = [5, 6]
            },
            new ConstraintGraphNode<Square, int>
            {
                Variable = Square.Parse("(0,6)"), Degree = 1, SumTightness = 0.5, Domain = [6]
            },
            new ConstraintGraphNode<Square, int>
            {
                Variable = Square.Parse("(1,1)"), Degree = 2, SumTightness = 1.0, Domain = [5, 6]
            },
            new ConstraintGraphNode<Square, int>
            {
                Variable = Square.Parse("(1,5)"), Degree = 2, SumTightness = 1.0, Domain = [5, 6]
            },
            new ConstraintGraphNode<Square, int>
            {
                Variable = Square.Parse("(2,1)"), Degree = 1, SumTightness = 0.5, Domain = [6]
            },
            new ConstraintGraphNode<Square, int>
            {
                Variable = Square.Parse("(7,6)"), Degree = 0, SumTightness = 0.0, Domain = [4]
            },
            new ConstraintGraphNode<Square, int>
            {
                Variable = Square.Parse("(8,8)"), Degree = 0, SumTightness = 0.0, Domain = [2]
            }
        ],
        ExpectedEdges =
        [
            new ConstraintGraphEdge<Square, int>
            {
                FirstVariable = Square.Parse("(0,5)"),
                SecondVariable = Square.Parse("(0,6)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(5, 6, true),
                    new AssignmentPair<int>(6, 6, false)
                ]
            },
            new ConstraintGraphEdge<Square, int>
            {
                FirstVariable = Square.Parse("(0,5)"),
                SecondVariable = Square.Parse("(1,5)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(5, 5, false),
                    new AssignmentPair<int>(5, 6, true),
                    new AssignmentPair<int>(6, 5, true),
                    new AssignmentPair<int>(6, 6, false)
                ]
            },
            new ConstraintGraphEdge<Square, int>
            {
                FirstVariable = Square.Parse("(1,1)"),
                SecondVariable = Square.Parse("(1,5)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(5, 5, false),
                    new AssignmentPair<int>(5, 6, true),
                    new AssignmentPair<int>(6, 5, true),
                    new AssignmentPair<int>(6, 6, false)
                ]
            },
            new ConstraintGraphEdge<Square, int>
            {
                FirstVariable = Square.Parse("(1,1)"),
                SecondVariable = Square.Parse("(2,1)"),
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(5, 6, true),
                    new AssignmentPair<int>(6, 6, false)
                ]
            }
        ]
    };


    private sealed record TestCase
    {
        public required SudokuProblem Problem { get; init; }

        public required IList<ConstraintGraphNode<Square, int>> ExpectedNodes { get; init; }

        public required IList<ConstraintGraphEdge<Square, int>> ExpectedEdges { get; init; }

        public void Deconstruct(out SudokuProblem problem,
            out IList<ConstraintGraphNode<Square, int>> expectedNodes,
            out IList<ConstraintGraphEdge<Square, int>> expectedEdges)
        {
            problem = Problem;
            expectedNodes = ExpectedNodes;
            expectedEdges = ExpectedEdges;
        }
    }
}
