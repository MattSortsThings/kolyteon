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
public abstract partial class VerboseProblemSolvingTests
{
    private protected abstract CheckingStrategy CheckingStrategy { get; }

    private protected abstract OrderingStrategy OrderingStrategy { get; }

    private VerboseBinaryCspSolver<TVariable, TDomainValue> ConfigureSolver<TVariable, TDomainValue>(
        IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp)
        where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
        where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue> =>
        VerboseBinaryCspSolver<TVariable, TDomainValue>.Create()
            .WithCapacity(binaryCsp.Variables)
            .AndCheckingStrategy(CheckingStrategy)
            .AndOrderingStrategy(OrderingStrategy)
            .AndStepDelay(TimeSpan.Zero)
            .Build();

    [Theory]
    [ClassData(typeof(ExampleProblems.Futoshiki.Solvable))]
    public async Task SolveAsync_GivenBinaryCspModellingSolvableFutoshikiProblem_FindsCorrectSolution(FutoshikiProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Square, int> binaryCsp = FutoshikiConstraintGraph.ModellingProblem(problem);

        VerboseBinaryCspSolver<Square, int> solver = ConfigureSolver(binaryCsp);

        TestSolvingProgressReporter<Square, int> progressReporter = new();

        // Act
        SolvingResult<Square, int> result = await solver.SolveAsync(binaryCsp, progressReporter);

        // Assert
        using (new AssertionScope())
        {
            result.VerifyCorrectSolution(problem);
            result.SearchMetrics.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And
                .HaveOrderingStrategy(OrderingStrategy);
            result.SearchMetrics.TotalSteps.Should().BeGreaterThan(binaryCsp.Variables);
            progressReporter.VerifyEndStateMatchesResult(result);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.Futoshiki.Unsolvable))]
    public async Task SolveAsync_GivenBinaryCspModellingUnsolvableFutoshikiProblem_FindsNoSolution(FutoshikiProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Square, int> binaryCsp = FutoshikiConstraintGraph.ModellingProblem(problem);

        VerboseBinaryCspSolver<Square, int> solver = ConfigureSolver(binaryCsp);

        TestSolvingProgressReporter<Square, int> progressReporter = new();

        // Act
        SolvingResult<Square, int> result = await solver.SolveAsync(binaryCsp, progressReporter);

        // Assert
        using (new AssertionScope())
        {
            result.Solution.Should().BeEmpty();
            result.SearchMetrics.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And
                .HaveOrderingStrategy(OrderingStrategy);
            result.SearchMetrics.TotalSteps.Should().BePositive();
            progressReporter.VerifyEndStateMatchesResult(result);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.GraphColouring.Solvable))]
    public async Task SolveAsync_GivenBinaryCspModellingSolvableGraphColouringProblem_FindsCorrectSolution(
        GraphColouringProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Node, Colour> binaryCsp = GraphColouringConstraintGraph.ModellingProblem(problem);

        VerboseBinaryCspSolver<Node, Colour> solver = ConfigureSolver(binaryCsp);

        TestSolvingProgressReporter<Node, Colour> progressReporter = new();

        // Act
        SolvingResult<Node, Colour> result = await solver.SolveAsync(binaryCsp, progressReporter);

        // Assert
        using (new AssertionScope())
        {
            result.VerifyCorrectSolution(problem);
            result.SearchMetrics.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And
                .HaveOrderingStrategy(OrderingStrategy);
            result.SearchMetrics.TotalSteps.Should().BeGreaterThan(binaryCsp.Variables);
            progressReporter.VerifyEndStateMatchesResult(result);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.GraphColouring.Unsolvable))]
    public async Task SolveAsync_GivenBinaryCspModellingUnsolvableGraphColouringProblem_FindsNoSolution(
        GraphColouringProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Node, Colour> binaryCsp = GraphColouringConstraintGraph.ModellingProblem(problem);

        VerboseBinaryCspSolver<Node, Colour> solver = ConfigureSolver(binaryCsp);

        TestSolvingProgressReporter<Node, Colour> progressReporter = new();

        // Act
        SolvingResult<Node, Colour> result = await solver.SolveAsync(binaryCsp, progressReporter);

        // Assert
        using (new AssertionScope())
        {
            result.Solution.Should().BeEmpty();
            result.SearchMetrics.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And
                .HaveOrderingStrategy(OrderingStrategy);
            result.SearchMetrics.TotalSteps.Should().BePositive();
            progressReporter.VerifyEndStateMatchesResult(result);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.MapColouring.Solvable))]
    public async Task SolveAsync_GivenBinaryCspModellingSolvableMapColouringProblem_FindsCorrectSolution(
        MapColouringProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Block, Colour> binaryCsp = MapColouringConstraintGraph.ModellingProblem(problem);

        VerboseBinaryCspSolver<Block, Colour> solver = ConfigureSolver(binaryCsp);

        TestSolvingProgressReporter<Block, Colour> progressReporter = new();

        // Act
        SolvingResult<Block, Colour> result = await solver.SolveAsync(binaryCsp, progressReporter);

        // Assert
        using (new AssertionScope())
        {
            result.VerifyCorrectSolution(problem);
            result.SearchMetrics.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And
                .HaveOrderingStrategy(OrderingStrategy);
            result.SearchMetrics.TotalSteps.Should().BeGreaterThan(binaryCsp.Variables);
            progressReporter.VerifyEndStateMatchesResult(result);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.MapColouring.Unsolvable))]
    public async Task SolveAsync_GivenBinaryCspModellingUnsolvableMapColouringProblem_FindsNoSolution(
        MapColouringProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Block, Colour> binaryCsp = MapColouringConstraintGraph.ModellingProblem(problem);

        VerboseBinaryCspSolver<Block, Colour> solver = ConfigureSolver(binaryCsp);

        TestSolvingProgressReporter<Block, Colour> progressReporter = new();

        // Act
        SolvingResult<Block, Colour> result = await solver.SolveAsync(binaryCsp, progressReporter);

        // Assert
        using (new AssertionScope())
        {
            result.Solution.Should().BeEmpty();
            result.SearchMetrics.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And
                .HaveOrderingStrategy(OrderingStrategy);
            result.SearchMetrics.TotalSteps.Should().BePositive();
            progressReporter.VerifyEndStateMatchesResult(result);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.NQueens.Solvable))]
    public async Task SolveAsync_GivenBinaryCspModellingSolvableNQueensProblem_FindsCorrectSolution(NQueensProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<int, Square> binaryCsp = NQueensConstraintGraph.ModellingProblem(problem);

        VerboseBinaryCspSolver<int, Square> solver = ConfigureSolver(binaryCsp);

        TestSolvingProgressReporter<int, Square> progressReporter = new();

        // Act
        SolvingResult<int, Square> result = await solver.SolveAsync(binaryCsp, progressReporter);

        // Assert
        using (new AssertionScope())
        {
            result.VerifyCorrectSolution(problem);
            result.SearchMetrics.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And
                .HaveOrderingStrategy(OrderingStrategy);
            result.SearchMetrics.TotalSteps.Should().BeGreaterThan(binaryCsp.Variables);
            progressReporter.VerifyEndStateMatchesResult(result);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.NQueens.Unsolvable))]
    public async Task SolveAsync_GivenBinaryCspModellingUnsolvableNQueensProblem_FindsNoSolution(NQueensProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<int, Square> binaryCsp = NQueensConstraintGraph.ModellingProblem(problem);

        VerboseBinaryCspSolver<int, Square> solver = ConfigureSolver(binaryCsp);

        TestSolvingProgressReporter<int, Square> progressReporter = new();

        // Act
        SolvingResult<int, Square> result = await solver.SolveAsync(binaryCsp, progressReporter);

        // Assert
        using (new AssertionScope())
        {
            result.Solution.Should().BeEmpty();
            result.SearchMetrics.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And
                .HaveOrderingStrategy(OrderingStrategy);
            result.SearchMetrics.TotalSteps.Should().BePositive();
            progressReporter.VerifyEndStateMatchesResult(result);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.Shikaku.Solvable))]
    public async Task SolveAsync_GivenBinaryCspModellingSolvableShikakuProblem_FindsCorrectSolution(ShikakuProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<NumberedSquare, Block> binaryCsp = ShikakuConstraintGraph.ModellingProblem(problem);

        VerboseBinaryCspSolver<NumberedSquare, Block> solver = ConfigureSolver(binaryCsp);

        TestSolvingProgressReporter<NumberedSquare, Block> progressReporter = new();

        // Act
        SolvingResult<NumberedSquare, Block> result = await solver.SolveAsync(binaryCsp, progressReporter);

        // Assert
        using (new AssertionScope())
        {
            result.VerifyCorrectSolution(problem);
            result.SearchMetrics.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And
                .HaveOrderingStrategy(OrderingStrategy);
            result.SearchMetrics.TotalSteps.Should().BeGreaterThan(binaryCsp.Variables);
            progressReporter.VerifyEndStateMatchesResult(result);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.Shikaku.Unsolvable))]
    public async Task SolveAsync_GivenBinaryCspModellingUnsolvableShikakuProblem_FindsNoSolution(ShikakuProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<NumberedSquare, Block> binaryCsp = ShikakuConstraintGraph.ModellingProblem(problem);

        VerboseBinaryCspSolver<NumberedSquare, Block> solver = ConfigureSolver(binaryCsp);

        TestSolvingProgressReporter<NumberedSquare, Block> progressReporter = new();

        // Act
        SolvingResult<NumberedSquare, Block> result = await solver.SolveAsync(binaryCsp, progressReporter);

        // Assert
        using (new AssertionScope())
        {
            result.Solution.Should().BeEmpty();
            result.SearchMetrics.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And
                .HaveOrderingStrategy(OrderingStrategy);
            result.SearchMetrics.TotalSteps.Should().BePositive();
            progressReporter.VerifyEndStateMatchesResult(result);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.Sudoku.Solvable))]
    public async Task SolveAsync_GivenBinaryCspModellingSolvableSudokuProblem_FindsCorrectSolution(SudokuProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Square, int> binaryCsp = SudokuConstraintGraph.ModellingProblem(problem);

        VerboseBinaryCspSolver<Square, int> solver = ConfigureSolver(binaryCsp);

        TestSolvingProgressReporter<Square, int> progressReporter = new();

        // Act
        SolvingResult<Square, int> result = await solver.SolveAsync(binaryCsp, progressReporter);

        // Assert
        using (new AssertionScope())
        {
            result.VerifyCorrectSolution(problem);
            result.SearchMetrics.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And
                .HaveOrderingStrategy(OrderingStrategy);
            result.SearchMetrics.TotalSteps.Should().BeGreaterThan(binaryCsp.Variables);
            progressReporter.VerifyEndStateMatchesResult(result);
        }
    }

    [Theory]
    [ClassData(typeof(ExampleProblems.Sudoku.Unsolvable))]
    public async Task SolveAsync_GivenBinaryCspModellingUnsolvableSudokuProblem_FindsNoSolution(SudokuProblem problem)
    {
        // Arrange
        IReadOnlyBinaryCsp<Square, int> binaryCsp = SudokuConstraintGraph.ModellingProblem(problem);

        VerboseBinaryCspSolver<Square, int> solver = ConfigureSolver(binaryCsp);

        TestSolvingProgressReporter<Square, int> progressReporter = new();

        // Act
        SolvingResult<Square, int> result = await solver.SolveAsync(binaryCsp, progressReporter);

        // Assert
        using (new AssertionScope())
        {
            result.Solution.Should().BeEmpty();
            result.SearchMetrics.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy).And
                .HaveOrderingStrategy(OrderingStrategy);
            result.SearchMetrics.TotalSteps.Should().BePositive();
            progressReporter.VerifyEndStateMatchesResult(result);
        }
    }

    [Fact]
    public async Task CanModelAndSolveProblemsSequentially()
    {
        // Arrange
        NQueensProblem solvableProblem1 = NQueensProblem.FromN(4);
        NQueensProblem solvableProblem2 = NQueensProblem.FromN(5);
        NQueensProblem unsolvableProblem = NQueensProblem.FromN(3);

        NQueensConstraintGraph binaryCsp = NQueensConstraintGraph.ModellingProblem(solvableProblem1);

        VerboseBinaryCspSolver<int, Square> solver = ConfigureSolver(binaryCsp);

        TestSolvingProgressReporter<int, Square> progressReporter = new();

        // Act
        SolvingResult<int, Square> resultForSolvableProblem1 = await solver.SolveAsync(binaryCsp, progressReporter);

        // Assert
        using (new AssertionScope())
        {
            resultForSolvableProblem1.VerifyCorrectSolution(solvableProblem1);
            progressReporter.VerifyEndStateMatchesResult(resultForSolvableProblem1);
        }

        // Act
        binaryCsp.Clear();
        binaryCsp.Model(unsolvableProblem);
        SolvingResult<int, Square> resultForUnsolvableProblem = await solver.SolveAsync(binaryCsp, progressReporter);

        // Assert
        using (new AssertionScope())
        {
            resultForUnsolvableProblem.Solution.Should().BeEmpty();
            progressReporter.VerifyEndStateMatchesResult(resultForUnsolvableProblem);
        }

        // Act
        binaryCsp.Clear();
        binaryCsp.Model(solvableProblem2);
        SolvingResult<int, Square> resultForSolvableProblem2 = await solver.SolveAsync(binaryCsp, progressReporter);

        // Assert
        using (new AssertionScope())
        {
            resultForSolvableProblem2.VerifyCorrectSolution(solvableProblem2);
            progressReporter.VerifyEndStateMatchesResult(resultForSolvableProblem2);
        }
    }
}
