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
using Kolyteon.Tests.Utils.TestAssertions;

namespace Kolyteon.Tests.Integration.ProblemSolving;

[IntegrationTest]
public abstract partial class ProblemSolvingTests
{
    private protected abstract CheckingStrategy CheckingStrategy { get; }

    private protected abstract OrderingStrategy OrderingStrategy { get; }

    private SearchAlgorithm ExpectedSearchAlgorithm => new(CheckingStrategy, OrderingStrategy);

    private BinaryCspSolver<TVariable, TDomainValue> ConfigureSolver<TVariable, TDomainValue>(
        IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp)
        where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
        where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue> =>
        BinaryCspSolver<TVariable, TDomainValue>.Create()
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

        BinaryCspSolver<Square, int> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<Square, int> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            problem.VerifyCorrect(result.Assignments.ToFutoshikiSolution()).Should().BeSuccessful();

            result.SearchAlgorithm.Should().Be(ExpectedSearchAlgorithm);

            result.TotalSteps.Should().BeGreaterThan(binaryCsp.Variables);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.Futoshiki.Unsolvable))]
    public void GivenBinaryCspModellingUnsolvableFutoshikiProblem_FindsNoSolution(FutoshikiProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Square, int> binaryCsp = FutoshikiConstraintGraph.ModellingProblem(problem);

        BinaryCspSolver<Square, int> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<Square, int> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            result.Assignments.Should().BeEmpty();

            result.SearchAlgorithm.Should().Be(ExpectedSearchAlgorithm);

            result.TotalSteps.Should().BePositive();
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.GraphColouring.Solvable))]
    public void GivenBinaryCspModellingSolvableGraphColouringProblem_FindsCorrectSolution(GraphColouringProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Node, Colour> binaryCsp = GraphColouringConstraintGraph.ModellingProblem(problem);

        BinaryCspSolver<Node, Colour> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<Node, Colour> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            problem.VerifyCorrect(result.Assignments.ToGraphColouringSolution()).Should().BeSuccessful();

            result.SearchAlgorithm.Should().Be(ExpectedSearchAlgorithm);

            result.TotalSteps.Should().BeGreaterThan(binaryCsp.Variables);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.GraphColouring.Unsolvable))]
    public void GivenBinaryCspModellingUnsolvableGraphColouringProblem_FindsNoSolution(GraphColouringProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Node, Colour> binaryCsp = GraphColouringConstraintGraph.ModellingProblem(problem);

        BinaryCspSolver<Node, Colour> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<Node, Colour> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            result.Assignments.Should().BeEmpty();

            result.SearchAlgorithm.Should().Be(ExpectedSearchAlgorithm);

            result.TotalSteps.Should().BePositive();
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.MapColouring.Solvable))]
    public void GivenBinaryCspModellingSolvableMapColouringProblem_FindsCorrectSolution(MapColouringProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Block, Colour> binaryCsp = MapColouringConstraintGraph.ModellingProblem(problem);

        BinaryCspSolver<Block, Colour> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<Block, Colour> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            problem.VerifyCorrect(result.Assignments.ToMapColouringSolution()).Should().BeSuccessful();

            result.SearchAlgorithm.Should().Be(ExpectedSearchAlgorithm);

            result.TotalSteps.Should().BeGreaterThan(binaryCsp.Variables);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.MapColouring.Unsolvable))]
    public void GivenBinaryCspModellingUnsolvableMapColouringProblem_FindsNoSolution(MapColouringProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Block, Colour> binaryCsp = MapColouringConstraintGraph.ModellingProblem(problem);

        BinaryCspSolver<Block, Colour> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<Block, Colour> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            result.Assignments.Should().BeEmpty();

            result.SearchAlgorithm.Should().Be(ExpectedSearchAlgorithm);

            result.TotalSteps.Should().BePositive();
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.NQueens.Solvable))]
    public void GivenBinaryCspModellingSolvableNQueensProblem_FindsCorrectSolution(NQueensProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<int, Square> binaryCsp = NQueensConstraintGraph.ModellingProblem(problem);

        BinaryCspSolver<int, Square> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<int, Square> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            problem.VerifyCorrect(result.Assignments.ToNQueensSolution()).Should().BeSuccessful();

            result.SearchAlgorithm.Should().Be(ExpectedSearchAlgorithm);

            result.TotalSteps.Should().BeGreaterThan(binaryCsp.Variables);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.NQueens.Unsolvable))]
    public void GivenBinaryCspModellingUnsolvableNQueensProblem_FindsNoSolution(NQueensProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<int, Square> binaryCsp = NQueensConstraintGraph.ModellingProblem(problem);

        BinaryCspSolver<int, Square> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<int, Square> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            result.Assignments.Should().BeEmpty();

            result.SearchAlgorithm.Should().Be(ExpectedSearchAlgorithm);

            result.TotalSteps.Should().BePositive();
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.Shikaku.Solvable))]
    public void GivenBinaryCspModellingSolvableShikakuProblem_FindsCorrectSolution(ShikakuProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<NumberedSquare, Block> binaryCsp = ShikakuConstraintGraph.ModellingProblem(problem);

        BinaryCspSolver<NumberedSquare, Block> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<NumberedSquare, Block> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            problem.VerifyCorrect(result.Assignments.ToShikakuSolution()).Should().BeSuccessful();

            result.SearchAlgorithm.Should().Be(ExpectedSearchAlgorithm);

            result.TotalSteps.Should().BeGreaterThan(binaryCsp.Variables);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.Shikaku.Unsolvable))]
    public void GivenBinaryCspModellingUnsolvableShikakuProblem_FindsNoSolution(ShikakuProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<NumberedSquare, Block> binaryCsp = ShikakuConstraintGraph.ModellingProblem(problem);

        BinaryCspSolver<NumberedSquare, Block> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<NumberedSquare, Block> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            result.Assignments.Should().BeEmpty();

            result.SearchAlgorithm.Should().Be(ExpectedSearchAlgorithm);

            result.TotalSteps.Should().BePositive();
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.Sudoku.Solvable))]
    public void GivenBinaryCspModellingSolvableSudokuProblem_FindsCorrectSolution(SudokuProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Square, int> binaryCsp = SudokuConstraintGraph.ModellingProblem(problem);

        BinaryCspSolver<Square, int> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<Square, int> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            problem.VerifyCorrect(result.Assignments.ToSudokuSolution()).Should().BeSuccessful();

            result.SearchAlgorithm.Should().Be(ExpectedSearchAlgorithm);

            result.TotalSteps.Should().BeGreaterThan(binaryCsp.Variables);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.Sudoku.Unsolvable))]
    public void GivenBinaryCspModellingUnsolvableSudokuProblem_FindsNoSolution(SudokuProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Square, int> binaryCsp = SudokuConstraintGraph.ModellingProblem(problem);

        BinaryCspSolver<Square, int> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<Square, int> result = solver.Solve(binaryCsp);

        // Assert
        using (new AssertionScope())
        {
            result.Assignments.Should().BeEmpty();

            result.SearchAlgorithm.Should().Be(ExpectedSearchAlgorithm);

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

        IBinaryCsp<int, Square, NQueensProblem> binaryCsp = NQueensConstraintGraph.ModellingProblem(solvableProblem1);

        BinaryCspSolver<int, Square> solver = ConfigureSolver(binaryCsp);

        // Act
        SolvingResult<int, Square> firstResult = solver.Solve(binaryCsp);

        // Assert
        solvableProblem1.VerifyCorrect(firstResult.Assignments.ToNQueensSolution()).Should().BeSuccessful();

        // Act
        binaryCsp.Clear();
        binaryCsp.Model(unsolvableProblem);
        SolvingResult<int, Square> secondResult = solver.Solve(binaryCsp);

        // Assert
        secondResult.Assignments.Should().BeEmpty();

        // Act
        binaryCsp.Clear();
        binaryCsp.Model(solvableProblem2);
        SolvingResult<int, Square> thirdResult = solver.Solve(binaryCsp);

        // Assert
        solvableProblem2.VerifyCorrect(thirdResult.Assignments.ToNQueensSolution()).Should().BeSuccessful();
    }
}