using Kolyteon.Common;
using Kolyteon.Futoshiki;
using Kolyteon.GraphColouring;
using Kolyteon.MapColouring;
using Kolyteon.Modelling;
using Kolyteon.NQueens;
using Kolyteon.Shikaku;
using Kolyteon.Solving;
using Kolyteon.Sudoku;
using Kolyteon.Tests.Integration.ProblemSolving.TestData;
using Kolyteon.Tests.Integration.ProblemSolving.TestUtils;
using Kolyteon.Tests.Utils.TestAssertions;

namespace Kolyteon.Tests.Integration.ProblemSolving;

[IntegrationTest]
public abstract partial class SilentProblemSolvingTests
{
    private protected abstract CheckingStrategy CheckingStrategy { get; }

    private protected abstract OrderingStrategy OrderingStrategy { get; }

    private SilentBinaryCspSolver<TVariable, TDomainValue> ConfigureSolver<TVariable, TDomainValue>(
        IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp)
        where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
        where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue> =>
        SilentBinaryCspSolver<TVariable, TDomainValue>.Create()
            .WithCapacity(binaryCsp.Variables)
            .AndCheckingStrategy(CheckingStrategy)
            .AndOrderingStrategy(OrderingStrategy)
            .Build();

    [Theory]
    [ClassData(typeof(ExampleProblems.Futoshiki.Solvable))]
    public void GivenBinaryCspModellingSolvableFutoshikiProblem_FindsCorrectSolution(FutoshikiProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Square, int> binaryCsp = FutoshikiConstraintGraph.ModellingProblem(problem);

        SilentBinaryCspSolver<Square, int> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<Square, int> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            result.VerifyCorrectSolution(problem);
            result.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And.HaveOrderingStrategy(OrderingStrategy);
            result.TotalSteps.Should().BeGreaterThan(binaryCsp.Variables);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.Futoshiki.Unsolvable))]
    public void GivenBinaryCspModellingUnsolvableFutoshikiProblem_FindsNoSolution(FutoshikiProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Square, int> binaryCsp = FutoshikiConstraintGraph.ModellingProblem(problem);

        SilentBinaryCspSolver<Square, int> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<Square, int> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            result.Assignments.Should().BeEmpty();
            result.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And.HaveOrderingStrategy(OrderingStrategy);
            result.TotalSteps.Should().BePositive();
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.GraphColouring.Solvable))]
    public void GivenBinaryCspModellingSolvableGraphColouringProblem_FindsCorrectSolution(GraphColouringProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Node, Colour> binaryCsp = GraphColouringConstraintGraph.ModellingProblem(problem);

        SilentBinaryCspSolver<Node, Colour> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<Node, Colour> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            result.VerifyCorrectSolution(problem);
            result.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And.HaveOrderingStrategy(OrderingStrategy);
            result.TotalSteps.Should().BeGreaterThan(binaryCsp.Variables);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.GraphColouring.Unsolvable))]
    public void GivenBinaryCspModellingUnsolvableGraphColouringProblem_FindsNoSolution(GraphColouringProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Node, Colour> binaryCsp = GraphColouringConstraintGraph.ModellingProblem(problem);

        SilentBinaryCspSolver<Node, Colour> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<Node, Colour> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            result.Assignments.Should().BeEmpty();
            result.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And.HaveOrderingStrategy(OrderingStrategy);
            result.TotalSteps.Should().BePositive();
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.MapColouring.Solvable))]
    public void GivenBinaryCspModellingSolvableMapColouringProblem_FindsCorrectSolution(MapColouringProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Block, Colour> binaryCsp = MapColouringConstraintGraph.ModellingProblem(problem);

        SilentBinaryCspSolver<Block, Colour> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<Block, Colour> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            result.VerifyCorrectSolution(problem);
            result.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And.HaveOrderingStrategy(OrderingStrategy);
            result.TotalSteps.Should().BeGreaterThan(binaryCsp.Variables);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.MapColouring.Unsolvable))]
    public void GivenBinaryCspModellingUnsolvableMapColouringProblem_FindsNoSolution(MapColouringProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Block, Colour> binaryCsp = MapColouringConstraintGraph.ModellingProblem(problem);

        SilentBinaryCspSolver<Block, Colour> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<Block, Colour> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            result.Assignments.Should().BeEmpty();
            result.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And.HaveOrderingStrategy(OrderingStrategy);
            result.TotalSteps.Should().BePositive();
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.NQueens.Solvable))]
    public void GivenBinaryCspModellingSolvableNQueensProblem_FindsCorrectSolution(NQueensProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<int, Square> binaryCsp = NQueensConstraintGraph.ModellingProblem(problem);

        SilentBinaryCspSolver<int, Square> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<int, Square> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            result.VerifyCorrectSolution(problem);
            result.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And.HaveOrderingStrategy(OrderingStrategy);
            result.TotalSteps.Should().BeGreaterThan(binaryCsp.Variables);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.NQueens.Unsolvable))]
    public void GivenBinaryCspModellingUnsolvableNQueensProblem_FindsNoSolution(NQueensProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<int, Square> binaryCsp = NQueensConstraintGraph.ModellingProblem(problem);

        SilentBinaryCspSolver<int, Square> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<int, Square> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            result.Assignments.Should().BeEmpty();
            result.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And.HaveOrderingStrategy(OrderingStrategy);
            result.TotalSteps.Should().BePositive();
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.Shikaku.Solvable))]
    public void GivenBinaryCspModellingSolvableShikakuProblem_FindsCorrectSolution(ShikakuProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<NumberedSquare, Block> binaryCsp = ShikakuConstraintGraph.ModellingProblem(problem);

        SilentBinaryCspSolver<NumberedSquare, Block> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<NumberedSquare, Block> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            result.VerifyCorrectSolution(problem);
            result.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And.HaveOrderingStrategy(OrderingStrategy);
            result.TotalSteps.Should().BeGreaterThan(binaryCsp.Variables);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.Shikaku.Unsolvable))]
    public void GivenBinaryCspModellingUnsolvableShikakuProblem_FindsNoSolution(ShikakuProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<NumberedSquare, Block> binaryCsp = ShikakuConstraintGraph.ModellingProblem(problem);

        SilentBinaryCspSolver<NumberedSquare, Block> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<NumberedSquare, Block> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            result.Assignments.Should().BeEmpty();
            result.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And.HaveOrderingStrategy(OrderingStrategy);
            result.TotalSteps.Should().BePositive();
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.Sudoku.Solvable))]
    public void GivenBinaryCspModellingSolvableSudokuProblem_FindsCorrectSolution(SudokuProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Square, int> binaryCsp = SudokuConstraintGraph.ModellingProblem(problem);

        SilentBinaryCspSolver<Square, int> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<Square, int> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            result.VerifyCorrectSolution(problem);
            result.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And.HaveOrderingStrategy(OrderingStrategy);
            result.TotalSteps.Should().BeGreaterThan(binaryCsp.Variables);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.Sudoku.Unsolvable))]
    public void GivenBinaryCspModellingUnsolvableSudokuProblem_FindsNoSolution(SudokuProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Square, int> binaryCsp = SudokuConstraintGraph.ModellingProblem(problem);

        SilentBinaryCspSolver<Square, int> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<Square, int> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            result.Assignments.Should().BeEmpty();
            result.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And.HaveOrderingStrategy(OrderingStrategy);
            result.TotalSteps.Should().BePositive();
        }
    }

    [Fact]
    public void CanModelAndSolveProblemsSequentially()
    {
        // Arrange
        NQueensProblem solvableProblem1 = NQueensProblem.FromN(4);
        NQueensProblem solvableProblem2 = NQueensProblem.FromN(5);
        NQueensProblem unsolvableProblem = NQueensProblem.FromN(3);

        NQueensConstraintGraph binaryCsp = NQueensConstraintGraph.ModellingProblem(solvableProblem1);

        SilentBinaryCspSolver<int, Square> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<int, Square> resultForSolvableProblem1 = solver.Solve(binaryCsp);

        // Assert
        resultForSolvableProblem1.VerifyCorrectSolution(solvableProblem1);

        // Act
        binaryCsp.Clear();
        binaryCsp.Model(unsolvableProblem);
        SolvingResult<int, Square> resultForUnsolvableProblem = solver.Solve(binaryCsp);

        // Assert
        resultForUnsolvableProblem.Assignments.Should().BeEmpty();

        // Act
        binaryCsp.Clear();
        binaryCsp.Model(solvableProblem2);
        SolvingResult<int, Square> resultForSolvableProblem2 = solver.Solve(binaryCsp);

        // Assert
        resultForSolvableProblem2.VerifyCorrectSolution(solvableProblem2);
    }
}
