using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions.Execution;
using Mjt85.Kolyteon.IntegrationTests.Helpers;
using Mjt85.Kolyteon.IntegrationTests.Solving.TestCases;
using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.NQueens;
using Mjt85.Kolyteon.Shikaku;
using Mjt85.Kolyteon.Solving.Common;
using Mjt85.Kolyteon.Solving.Verbose;
using Mjt85.Kolyteon.Sudoku;

namespace Mjt85.Kolyteon.IntegrationTests.Solving;

[IntegrationTest]
public abstract class VerboseBinaryCspSolvingTests
{
    protected abstract Search SearchStrategy { get; }

    protected abstract Ordering OrderingStrategy { get; }

    private VerboseBinaryCspSolver<V, D> ConfigureSolver<V, D>(int capacity)
        where V : struct, IComparable<V>, IEquatable<V>
        where D : struct, IComparable<D>, IEquatable<D> => CreateBinaryCspSolver.WithInitialCapacity(capacity)
        .AndInitialSearchStrategy(SearchStrategy)
        .AndInitialOrderingStrategy(OrderingStrategy)
        .Verbose()
        .WithInitialStepDelay(TimeSpan.Zero)
        .Build<V, D>();

    [Theory]
    [ClassData(typeof(MapColouringPuzzles.Solvable))]
    public async Task BinaryCspModelsSolvableMapColouringPuzzle_SolverFindsValidSolutionToPuzzle_UpdatesProgress(
        MapColouringPuzzle puzzle)
    {
        // Arrange
        MapColouringBinaryCsp binaryCsp = MapColouringBinaryCsp.WithInitialCapacity(puzzle.RegionData.Count);
        VerboseBinaryCspSolver<Region, Colour> solver = ConfigureSolver<Region, Colour>(puzzle.RegionData.Count);
        OneLineSummaryProgress<Region, Colour> progress = new();

        binaryCsp.Model(puzzle);

        // Act
        Result<Region, Colour> result = await solver.SolveAsync(binaryCsp, progress);
        IReadOnlyDictionary<Region, Colour> solution = result.Assignments.ToPuzzleSolution();

        // Assert
        using (new AssertionScope())
        {
            puzzle.ValidSolution(solution).Should().Be(ValidationResult.Success);
            progress.OneLineSummary.Should().Be("finished at leaf level");
        }
    }

    [Theory]
    [ClassData(typeof(MapColouringPuzzles.Unsolvable))]
    public async Task BinaryCspModelsUnsolvableMapColouringPuzzle_SolverReturnsResultWithEmptyAssignments_UpdatesProgress(
        MapColouringPuzzle puzzle)
    {
        // Arrange
        MapColouringBinaryCsp binaryCsp = MapColouringBinaryCsp.WithInitialCapacity(puzzle.RegionData.Count);
        VerboseBinaryCspSolver<Region, Colour> solver = ConfigureSolver<Region, Colour>(puzzle.RegionData.Count);
        OneLineSummaryProgress<Region, Colour> progress = new();

        binaryCsp.Model(puzzle);

        // Act
        Result<Region, Colour> result = await solver.SolveAsync(binaryCsp, progress);

        // Assert
        using (new AssertionScope())
        {
            result.Assignments.Should().BeEmpty();
            progress.OneLineSummary.Should().Be("finished at root level");
        }
    }

    [Theory]
    [ClassData(typeof(NQueensPuzzles.Solvable))]
    public async Task BinaryCspModelsSolvableNQueensPuzzle_SolverFindsValidSolutionToPuzzle_UpdatesProgress(NQueensPuzzle puzzle)
    {
        // Arrange
        NQueensBinaryCsp binaryCsp = NQueensBinaryCsp.WithInitialCapacity(puzzle.N);
        VerboseBinaryCspSolver<int, Queen> solver = ConfigureSolver<int, Queen>(puzzle.N);
        OneLineSummaryProgress<int, Queen> progress = new();

        binaryCsp.Model(puzzle);

        // Act
        Result<int, Queen> result = await solver.SolveAsync(binaryCsp, progress);
        IReadOnlyList<Queen> solution = result.Assignments.ToPuzzleSolution();

        // Assert
        using (new AssertionScope())
        {
            puzzle.ValidSolution(solution).Should().Be(ValidationResult.Success);
            progress.OneLineSummary.Should().Be("finished at leaf level");
        }
    }

    [Theory]
    [ClassData(typeof(NQueensPuzzles.Unsolvable))]
    public async Task BinaryCspModelsUnsolvableNQueensPuzzle_SolverReturnsResultWithEmptyAssignments_UpdatesProgress(
        NQueensPuzzle puzzle)
    {
        // Arrange
        NQueensBinaryCsp binaryCsp = NQueensBinaryCsp.WithInitialCapacity(puzzle.N);
        VerboseBinaryCspSolver<int, Queen> solver = ConfigureSolver<int, Queen>(puzzle.N);
        OneLineSummaryProgress<int, Queen> progress = new();

        binaryCsp.Model(puzzle);

        // Act
        Result<int, Queen> result = await solver.SolveAsync(binaryCsp, progress);

        // Assert
        using (new AssertionScope())
        {
            result.Assignments.Should().BeEmpty();
            progress.OneLineSummary.Should().Be("finished at root level");
        }
    }

    [Theory]
    [ClassData(typeof(ShikakuPuzzles.Solvable))]
    public async Task BinaryCspModelsSolvableShikakuPuzzle_SolverFindsValidSolutionToPuzzle_UpdatesProgress(ShikakuPuzzle puzzle)
    {
        // Arrange
        ShikakuBinaryCsp binaryCsp = new(puzzle.Hints.Count);
        VerboseBinaryCspSolver<Hint, Rectangle> solver = ConfigureSolver<Hint, Rectangle>(puzzle.Hints.Count);
        OneLineSummaryProgress<Hint, Rectangle> progress = new();

        binaryCsp.Model(puzzle);

        // Act
        Result<Hint, Rectangle> result = await solver.SolveAsync(binaryCsp, progress);
        IReadOnlyList<Rectangle> solution = result.Assignments.ToPuzzleSolution();

        // Assert
        using (new AssertionScope())
        {
            puzzle.ValidSolution(solution).Should().Be(ValidationResult.Success);
            progress.OneLineSummary.Should().Be("finished at leaf level");
        }
    }

    [Theory]
    [ClassData(typeof(ShikakuPuzzles.Unsolvable))]
    public async Task BinaryCspModelsUnsolvableShikakuPuzzle_SolverReturnsResultWithEmptyAssignments_UpdatesProgress(
        ShikakuPuzzle puzzle)
    {
        // Arrange
        ShikakuBinaryCsp binaryCsp = new(puzzle.Hints.Count);
        VerboseBinaryCspSolver<Hint, Rectangle> solver = ConfigureSolver<Hint, Rectangle>(puzzle.Hints.Count);
        OneLineSummaryProgress<Hint, Rectangle> progress = new();

        binaryCsp.Model(puzzle);

        // Act
        Result<Hint, Rectangle> result = await solver.SolveAsync(binaryCsp, progress);

        // Assert
        using (new AssertionScope())
        {
            result.Assignments.Should().BeEmpty();
            progress.OneLineSummary.Should().Be("finished at root level");
        }
    }

    [Theory]
    [ClassData(typeof(SudokuPuzzles.Solvable))]
    public async Task BinaryCspModelsSolvableSudokuPuzzle_SolverFindsValidSolutionToPuzzle_UpdatesProgress(SudokuPuzzle puzzle)
    {
        // Arrange
        SudokuBinaryCsp binaryCsp = new(20);
        VerboseBinaryCspSolver<EmptyCell, int> solver = ConfigureSolver<EmptyCell, int>(20);
        OneLineSummaryProgress<EmptyCell, int> progress = new();

        binaryCsp.Model(puzzle);

        // Act
        Result<EmptyCell, int> result = await solver.SolveAsync(binaryCsp, progress);
        IReadOnlyList<FilledCell> solution = result.Assignments.ToPuzzleSolution();

        // Assert
        using (new AssertionScope())
        {
            puzzle.ValidSolution(solution).Should().Be(ValidationResult.Success);
            progress.OneLineSummary.Should().Be("finished at leaf level");
        }
    }

    [Theory]
    [ClassData(typeof(SudokuPuzzles.Unsolvable))]
    public async Task BinaryCspModelsUnsolvableSudokuPuzzle_SolverReturnsResultWithEmptyAssignments_UpdatesProgress(
        SudokuPuzzle puzzle)
    {
        // Arrange
        SudokuBinaryCsp binaryCsp = new(20);
        VerboseBinaryCspSolver<EmptyCell, int> solver = ConfigureSolver<EmptyCell, int>(20);
        OneLineSummaryProgress<EmptyCell, int> progress = new();

        binaryCsp.Model(puzzle);

        // Act
        Result<EmptyCell, int> result = await solver.SolveAsync(binaryCsp, progress);

        // Assert
        using (new AssertionScope())
        {
            result.Assignments.Should().BeEmpty();
            progress.OneLineSummary.Should().Be("finished at root level");
        }
    }

    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public sealed class Algorithms
    {
        public sealed class BT_plus_NO : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.Backtracking;

            protected override Ordering OrderingStrategy => Ordering.None;
        }

        public sealed class BT_plus_BZ : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.Backtracking;

            protected override Ordering OrderingStrategy => Ordering.Brelaz;
        }

        public sealed class BT_plus_MC : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.Backtracking;

            protected override Ordering OrderingStrategy => Ordering.MaxCardinality;
        }

        public sealed class BT_plus_MT : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.Backtracking;

            protected override Ordering OrderingStrategy => Ordering.MaxTightness;
        }

        public sealed class BJ_plus_NO : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.Backjumping;

            protected override Ordering OrderingStrategy => Ordering.None;
        }

        public sealed class BJ_plus_BZ : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.Backjumping;

            protected override Ordering OrderingStrategy => Ordering.Brelaz;
        }

        public sealed class BJ_plus_MC : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.Backjumping;

            protected override Ordering OrderingStrategy => Ordering.MaxCardinality;
        }

        public sealed class BJ_plus_MT : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.Backjumping;

            protected override Ordering OrderingStrategy => Ordering.MaxTightness;
        }

        public sealed class GBJ_plus_NO : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.GraphBasedBackjumping;

            protected override Ordering OrderingStrategy => Ordering.None;
        }

        public sealed class GBJ_plus_BZ : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.GraphBasedBackjumping;

            protected override Ordering OrderingStrategy => Ordering.Brelaz;
        }

        public sealed class GBJ_plus_MC : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.GraphBasedBackjumping;

            protected override Ordering OrderingStrategy => Ordering.MaxCardinality;
        }

        public sealed class GBJ_plus_MT : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.GraphBasedBackjumping;

            protected override Ordering OrderingStrategy => Ordering.MaxTightness;
        }

        public sealed class CBJ_plus_NO : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.ConflictDirectedBackjumping;

            protected override Ordering OrderingStrategy => Ordering.None;
        }

        public sealed class CBJ_plus_BZ : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.ConflictDirectedBackjumping;

            protected override Ordering OrderingStrategy => Ordering.Brelaz;
        }

        public sealed class CBJ_plus_MC : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.ConflictDirectedBackjumping;

            protected override Ordering OrderingStrategy => Ordering.MaxCardinality;
        }

        public sealed class CBJ_plus_MT : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.ConflictDirectedBackjumping;

            protected override Ordering OrderingStrategy => Ordering.MaxTightness;
        }

        public sealed class FC_plus_NO : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.ForwardChecking;

            protected override Ordering OrderingStrategy => Ordering.None;
        }

        public sealed class FC_plus_BZ : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.ForwardChecking;

            protected override Ordering OrderingStrategy => Ordering.Brelaz;
        }

        public sealed class FC_plus_MC : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.ForwardChecking;

            protected override Ordering OrderingStrategy => Ordering.MaxCardinality;
        }

        public sealed class FC_plus_MT : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.ForwardChecking;

            protected override Ordering OrderingStrategy => Ordering.MaxTightness;
        }

        public sealed class PLA_plus_NO : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.PartialLookingAhead;

            protected override Ordering OrderingStrategy => Ordering.None;
        }

        public sealed class PLA_plus_BZ : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.PartialLookingAhead;

            protected override Ordering OrderingStrategy => Ordering.Brelaz;
        }

        public sealed class PLA_plus_MC : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.PartialLookingAhead;

            protected override Ordering OrderingStrategy => Ordering.MaxCardinality;
        }

        public sealed class PLA_plus_MT : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.PartialLookingAhead;

            protected override Ordering OrderingStrategy => Ordering.MaxTightness;
        }

        public sealed class FLA_plus_NO : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.FullLookingAhead;

            protected override Ordering OrderingStrategy => Ordering.None;
        }

        public sealed class FLA_plus_BZ : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.FullLookingAhead;

            protected override Ordering OrderingStrategy => Ordering.Brelaz;
        }

        public sealed class FLA_plus_MC : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.FullLookingAhead;

            protected override Ordering OrderingStrategy => Ordering.MaxCardinality;
        }

        public sealed class FLA_plus_MT : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.FullLookingAhead;

            protected override Ordering OrderingStrategy => Ordering.MaxTightness;
        }

        public sealed class MAC_plus_NO : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.MaintainingArcConsistency;

            protected override Ordering OrderingStrategy => Ordering.None;
        }

        public sealed class MAC_plus_BZ : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.MaintainingArcConsistency;

            protected override Ordering OrderingStrategy => Ordering.Brelaz;
        }

        public sealed class MAC_plus_MC : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.MaintainingArcConsistency;

            protected override Ordering OrderingStrategy => Ordering.MaxCardinality;
        }

        public sealed class MAC_plus_MT : VerboseBinaryCspSolvingTests
        {
            protected override Search SearchStrategy => Search.MaintainingArcConsistency;

            protected override Ordering OrderingStrategy => Ordering.MaxTightness;
        }
    }
}
