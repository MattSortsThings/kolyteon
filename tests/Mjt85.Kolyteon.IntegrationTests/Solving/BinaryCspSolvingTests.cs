using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Mjt85.Kolyteon.IntegrationTests.Solving.TestCases;
using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.NQueens;
using Mjt85.Kolyteon.Shikaku;
using Mjt85.Kolyteon.Solving;
using Mjt85.Kolyteon.Sudoku;

namespace Mjt85.Kolyteon.IntegrationTests.Solving;

[IntegrationTest]
public abstract class BinaryCspSolvingTests
{
    protected abstract Search Search { get; }

    protected abstract Ordering Ordering { get; }

    private BinaryCspSolver<V, D> ConfigureSolver<V, D>(int capacity)
        where V : struct, IComparable<V>, IEquatable<V>
        where D : struct, IComparable<D>, IEquatable<D> => BinaryCspSolver<V, D>.Create()
        .WithInitialCapacity(capacity)
        .AndInitialSearchStrategy(Search)
        .AndInitialOrderingStrategy(Ordering)
        .Build();

    [Theory]
    [ClassData(typeof(MapColouringPuzzles.Solvable))]
    public void BinaryCspModelsSolvableMapColouringPuzzle_SolverFindsValidSolutionToPuzzle(MapColouringPuzzle puzzle)
    {
        // Arrange
        MapColouringBinaryCsp binaryCsp = new(puzzle.RegionData.Count);
        BinaryCspSolver<Region, Colour> solver = ConfigureSolver<Region, Colour>(puzzle.RegionData.Count);

        binaryCsp.Model(puzzle);

        // Act
        Result<Region, Colour> result = solver.Solve(binaryCsp);
        IReadOnlyDictionary<Region, Colour> solution = result.Assignments.ToPuzzleSolution();

        // Assert
        puzzle.ValidSolution(solution).Should().Be(ValidationResult.Success);
    }

    [Theory]
    [ClassData(typeof(MapColouringPuzzles.Unsolvable))]
    public void BinaryCspModelsUnsolvableMapColouringPuzzle_SolverReturnsResultWithEmptyAssignments(MapColouringPuzzle puzzle)
    {
        // Arrange
        MapColouringBinaryCsp binaryCsp = new(puzzle.RegionData.Count);
        BinaryCspSolver<Region, Colour> solver = ConfigureSolver<Region, Colour>(puzzle.RegionData.Count);

        binaryCsp.Model(puzzle);

        // Act
        Result<Region, Colour> result = solver.Solve(binaryCsp);

        // Assert
        result.Assignments.Should().BeEmpty();
    }

    [Theory]
    [ClassData(typeof(NQueensPuzzles.Solvable))]
    public void BinaryCspModelsSolvableNQueensPuzzle_SolverFindsValidSolutionToPuzzle(NQueensPuzzle puzzle)
    {
        // Arrange
        NQueensBinaryCsp binaryCsp = new(puzzle.N);
        BinaryCspSolver<int, Queen> solver = ConfigureSolver<int, Queen>(puzzle.N);

        binaryCsp.Model(puzzle);

        // Act
        Result<int, Queen> result = solver.Solve(binaryCsp);
        IReadOnlyList<Queen> solution = result.Assignments.ToPuzzleSolution();

        // Assert
        puzzle.ValidSolution(solution).Should().Be(ValidationResult.Success);
    }

    [Theory]
    [ClassData(typeof(NQueensPuzzles.Unsolvable))]
    public void BinaryCspModelsUnsolvableNQueensPuzzle_SolverReturnsResultWithEmptyAssignments(NQueensPuzzle puzzle)
    {
        // Arrange
        NQueensBinaryCsp binaryCsp = new(puzzle.N);
        BinaryCspSolver<int, Queen> solver = ConfigureSolver<int, Queen>(puzzle.N);

        binaryCsp.Model(puzzle);

        // Act
        Result<int, Queen> result = solver.Solve(binaryCsp);

        // Assert
        result.Assignments.Should().BeEmpty();
    }

    [Theory]
    [ClassData(typeof(ShikakuPuzzles.Solvable))]
    public void BinaryCspModelsSolvableShikakuPuzzle_SolverFindsValidSolutionToPuzzle(ShikakuPuzzle puzzle)
    {
        // Arrange
        ShikakuBinaryCsp binaryCsp = new(puzzle.Hints.Count);
        BinaryCspSolver<Hint, Rectangle> solver = ConfigureSolver<Hint, Rectangle>(puzzle.Hints.Count);

        binaryCsp.Model(puzzle);

        // Act
        Result<Hint, Rectangle> result = solver.Solve(binaryCsp);
        IReadOnlyList<Rectangle> solution = result.Assignments.ToPuzzleSolution();

        // Assert
        puzzle.ValidSolution(solution).Should().Be(ValidationResult.Success);
    }

    [Theory]
    [ClassData(typeof(ShikakuPuzzles.Unsolvable))]
    public void BinaryCspModelsUnsolvableShikakuPuzzle_SolverReturnsResultWithEmptyAssignments(ShikakuPuzzle puzzle)
    {
        // Arrange
        ShikakuBinaryCsp binaryCsp = new(puzzle.Hints.Count);
        BinaryCspSolver<Hint, Rectangle> solver = ConfigureSolver<Hint, Rectangle>(puzzle.Hints.Count);

        binaryCsp.Model(puzzle);

        // Act
        Result<Hint, Rectangle> result = solver.Solve(binaryCsp);

        // Assert
        result.Assignments.Should().BeEmpty();
    }

    [Theory]
    [ClassData(typeof(SudokuPuzzles.Solvable))]
    public void BinaryCspModelsSolvableSudokuPuzzle_SolverFindsValidSolutionToPuzzle(SudokuPuzzle puzzle)
    {
        // Arrange
        SudokuBinaryCsp binaryCsp = new(20);
        BinaryCspSolver<EmptyCell, int> solver = ConfigureSolver<EmptyCell, int>(20);

        binaryCsp.Model(puzzle);

        // Act
        Result<EmptyCell, int> result = solver.Solve(binaryCsp);
        IReadOnlyList<FilledCell> solution = result.Assignments.ToPuzzleSolution();

        // Assert
        puzzle.ValidSolution(solution).Should().Be(ValidationResult.Success);
    }

    [Theory]
    [ClassData(typeof(SudokuPuzzles.Unsolvable))]
    public void BinaryCspModelsUnsolvableSudokuPuzzle_SolverReturnsResultWithEmptyAssignments(SudokuPuzzle puzzle)
    {
        // Arrange
        SudokuBinaryCsp binaryCsp = new(20);
        BinaryCspSolver<EmptyCell, int> solver = ConfigureSolver<EmptyCell, int>(20);

        binaryCsp.Model(puzzle);

        // Act
        Result<EmptyCell, int> result = solver.Solve(binaryCsp);

        // Assert
        result.Assignments.Should().BeEmpty();
    }

    [Fact]
    public void CanModelAndSolveMultipleProblemsInSequence()
    {
        // Arrange
        NQueensBinaryCsp binaryCsp = new(5);
        BinaryCspSolver<int, Queen> solver = ConfigureSolver<int, Queen>(5);

        NQueensPuzzle n3puzzle = NQueensPuzzle.FromN(3);
        NQueensPuzzle n4Puzzle = NQueensPuzzle.FromN(4);
        NQueensPuzzle n5Puzzle = NQueensPuzzle.FromN(5);

        // Act
        binaryCsp.Model(n4Puzzle);
        Result<int, Queen> n4Result = solver.Solve(binaryCsp);

        // Assert
        n4Puzzle.ValidSolution(n4Result.Assignments.ToPuzzleSolution()).Should().Be(ValidationResult.Success);

        // Act
        binaryCsp.Clear();
        binaryCsp.Model(n3puzzle);
        Result<int, Queen> n3Result = solver.Solve(binaryCsp);

        // Assert
        n3Result.Assignments.Should().BeEmpty();

        // Act
        binaryCsp.Clear();
        binaryCsp.Model(n5Puzzle);
        Result<int, Queen> n5Result = solver.Solve(binaryCsp);

        // Assert
        n5Puzzle.ValidSolution(n5Result.Assignments.ToPuzzleSolution()).Should().Be(ValidationResult.Success);
    }

    [Fact]
    public void CanReuseSameSolverAfterSolvingOperationCancelledAndExceptionCaught()
    {
        // Arrange
        NQueensBinaryCsp binaryCsp = new(4);
        BinaryCspSolver<int, Queen> solver = ConfigureSolver<int, Queen>(4);

        NQueensPuzzle n3puzzle = NQueensPuzzle.FromN(3);
        NQueensPuzzle n4Puzzle = NQueensPuzzle.FromN(4);

        binaryCsp.Model(n3puzzle);

        // Act
        Action act = () =>
        {
            using var cts = new CancellationTokenSource(TimeSpan.Zero);
            _ = solver.Solve(binaryCsp, cts.Token);
        };

        // Assert
        act.Should().Throw<OperationCanceledException>();

        // Act
        binaryCsp.Clear();
        binaryCsp.Model(n4Puzzle);
        Result<int, Queen> result = solver.Solve(binaryCsp);

        // Assert
        n4Puzzle.ValidSolution(result.Assignments.ToPuzzleSolution()).Should().Be(ValidationResult.Success);
    }

    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public sealed class Algorithms
    {
        public sealed class BT_plus_NO : BinaryCspSolvingTests
        {
            protected override Search Search => Search.Backtracking;

            protected override Ordering Ordering => Ordering.None;
        }

        public sealed class BT_plus_BZ : BinaryCspSolvingTests
        {
            protected override Search Search => Search.Backtracking;

            protected override Ordering Ordering => Ordering.Brelaz;
        }

        public sealed class BT_plus_MC : BinaryCspSolvingTests
        {
            protected override Search Search => Search.Backtracking;

            protected override Ordering Ordering => Ordering.MaxCardinality;
        }
    }
}
