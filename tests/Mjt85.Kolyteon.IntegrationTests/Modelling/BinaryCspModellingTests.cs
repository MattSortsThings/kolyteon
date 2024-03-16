using FluentAssertions.Equivalency;
using Mjt85.Kolyteon.IntegrationTests.Helpers;
using Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;
using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.NQueens;
using Mjt85.Kolyteon.Shikaku;
using Mjt85.Kolyteon.Sudoku;

namespace Mjt85.Kolyteon.IntegrationTests.Modelling;

/// <summary>
///     Integration tests for binary CSP modelling.
/// </summary>
public sealed class BinaryCspModellingTests
{
    private static Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> ConfigureEquivalencyOptions<T>()
    {
        return options => options.Using<double>(e =>
            e.Subject.Should().BeApproximately(e.Expectation, Invariants.SixDecimalPlacesPrecision)).WhenTypeIs<double>();
    }

    [IntegrationTest]
    public sealed class Modelling_MapColouringPuzzle
    {
        [Theory]
        [ClassData(typeof(MapColouringPuzzles.ExpectedVariables))]
        public void BinaryCspHasExpectedVariables(MapColouringPuzzle puzzle, IEnumerable<Region> expected)
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = MapColouringBinaryCsp.WithInitialCapacity(puzzle.RegionData.Count);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetAllVariables().Should().Equal(expected);
        }

        [Theory]
        [ClassData(typeof(MapColouringPuzzles.ExpectedDomains))]
        public void BinaryCspHasExpectedDomains(MapColouringPuzzle puzzle, IEnumerable<IReadOnlyList<Colour>> expected)
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = MapColouringBinaryCsp.WithInitialCapacity(puzzle.RegionData.Count);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetAllDomains().Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
        }

        [Theory]
        [ClassData(typeof(MapColouringPuzzles.ExpectedAdjacentVariables))]
        public void BinaryCspHasExpectedAdjacentVariables(MapColouringPuzzle puzzle, IEnumerable<Pair<Region>> expected)
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = MapColouringBinaryCsp.WithInitialCapacity(puzzle.RegionData.Count);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetAllAdjacentVariables().Should().Equal(expected);
        }

        [Theory]
        [ClassData(typeof(MapColouringPuzzles.ExpectedProblemMetrics))]
        public void BinaryCspHasExpectedProblemMetrics(MapColouringPuzzle puzzle, ProblemMetrics expected)
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = MapColouringBinaryCsp.WithInitialCapacity(puzzle.RegionData.Count);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetProblemMetrics().Should()
                .BeEquivalentTo(expected, ConfigureEquivalencyOptions<ProblemMetrics>());
        }

        [Theory]
        [ClassData(typeof(MapColouringPuzzles.ExpectedDomainSizeStatistics))]
        public void BinaryCspHasExpectedDomainSizeStatistics(MapColouringPuzzle puzzle, DomainSizeStatistics expected)
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = MapColouringBinaryCsp.WithInitialCapacity(puzzle.RegionData.Count);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetDomainSizeStatistics().Should()
                .BeEquivalentTo(expected, ConfigureEquivalencyOptions<DomainSizeStatistics>());
        }

        [Theory]
        [ClassData(typeof(MapColouringPuzzles.ExpectedDegreeStatistics))]
        public void BinaryCspHasExpectedDegreeStatistics(MapColouringPuzzle puzzle, DegreeStatistics expected)
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = MapColouringBinaryCsp.WithInitialCapacity(puzzle.RegionData.Count);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetDegreeStatistics().Should()
                .BeEquivalentTo(expected, ConfigureEquivalencyOptions<DegreeStatistics>());
        }

        [Theory]
        [ClassData(typeof(MapColouringPuzzles.ExpectedSumTightnessStatistics))]
        public void BinaryCspHasExpectedSumTightnessStatistics(MapColouringPuzzle puzzle, SumTightnessStatistics expected)
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = MapColouringBinaryCsp.WithInitialCapacity(puzzle.RegionData.Count);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetSumTightnessStatistics().Should()
                .BeEquivalentTo(expected, ConfigureEquivalencyOptions<SumTightnessStatistics>());
        }
    }

    [IntegrationTest]
    public sealed class Modelling_NQueensPuzzle
    {
        [Theory]
        [ClassData(typeof(NQueensPuzzles.ExpectedVariables))]
        public void BinaryCspHasExpectedVariables(NQueensPuzzle puzzle, IEnumerable<int> expected)
        {
            // Arrange
            NQueensBinaryCsp binaryCsp = NQueensBinaryCsp.WithInitialCapacity(puzzle.N);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetAllVariables().Should().Equal(expected);
        }

        [Theory]
        [ClassData(typeof(NQueensPuzzles.ExpectedDomains))]
        public void BinaryCspHasExpectedDomains(NQueensPuzzle puzzle, IEnumerable<IReadOnlyList<Queen>> expected)
        {
            // Arrange
            NQueensBinaryCsp binaryCsp = NQueensBinaryCsp.WithInitialCapacity(puzzle.N);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetAllDomains().Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
        }

        [Theory]
        [ClassData(typeof(NQueensPuzzles.ExpectedAdjacentVariables))]
        public void BinaryCspHasExpectedAdjacentVariables(NQueensPuzzle puzzle, IEnumerable<Pair<int>> expected)
        {
            // Arrange
            NQueensBinaryCsp binaryCsp = NQueensBinaryCsp.WithInitialCapacity(puzzle.N);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetAllAdjacentVariables().Should().Equal(expected);
        }


        [Theory]
        [ClassData(typeof(NQueensPuzzles.ExpectedProblemMetrics))]
        public void BinaryCspHasExpectedProblemMetrics(NQueensPuzzle puzzle, ProblemMetrics expected)
        {
            // Arrange
            NQueensBinaryCsp binaryCsp = NQueensBinaryCsp.WithInitialCapacity(puzzle.N);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetProblemMetrics().Should()
                .BeEquivalentTo(expected, ConfigureEquivalencyOptions<ProblemMetrics>());
        }

        [Theory]
        [ClassData(typeof(NQueensPuzzles.ExpectedDomainSizeStatistics))]
        public void BinaryCspHasExpectedDomainSizeStatistics(NQueensPuzzle puzzle, DomainSizeStatistics expected)
        {
            // Arrange
            NQueensBinaryCsp binaryCsp = NQueensBinaryCsp.WithInitialCapacity(puzzle.N);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetDomainSizeStatistics().Should()
                .BeEquivalentTo(expected, ConfigureEquivalencyOptions<DomainSizeStatistics>());
        }

        [Theory]
        [ClassData(typeof(NQueensPuzzles.ExpectedDegreeStatistics))]
        public void BinaryCspHasExpectedDegreeStatistics(NQueensPuzzle puzzle, DegreeStatistics expected)
        {
            // Arrange
            NQueensBinaryCsp binaryCsp = NQueensBinaryCsp.WithInitialCapacity(puzzle.N);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetDegreeStatistics().Should().BeEquivalentTo(expected, ConfigureEquivalencyOptions<DegreeStatistics>());
        }

        [Theory]
        [ClassData(typeof(NQueensPuzzles.ExpectedSumTightnessStatistics))]
        public void BinaryCspHasExpectedSumTightnessStatistics(NQueensPuzzle puzzle, SumTightnessStatistics expected)
        {
            // Arrange
            NQueensBinaryCsp binaryCsp = NQueensBinaryCsp.WithInitialCapacity(puzzle.N);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetSumTightnessStatistics().Should()
                .BeEquivalentTo(expected, ConfigureEquivalencyOptions<SumTightnessStatistics>());
        }
    }

    [IntegrationTest]
    public sealed class Modelling_ShikakuPuzzle
    {
        [Theory]
        [ClassData(typeof(ShikakuPuzzles.ExpectedVariables))]
        public void BinaryCspHasExpectedVariables(ShikakuPuzzle puzzle, IEnumerable<Hint> expected)
        {
            // Arrange
            ShikakuBinaryCsp binaryCsp = ShikakuBinaryCsp.WithInitialCapacity(puzzle.Hints.Count);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetAllVariables().Should().Equal(expected);
        }

        [Theory]
        [ClassData(typeof(ShikakuPuzzles.ExpectedDomains))]
        public void BinaryCspHasExpectedDomains(ShikakuPuzzle puzzle, IEnumerable<IReadOnlyList<Rectangle>> expected)
        {
            // Arrange
            ShikakuBinaryCsp binaryCsp = ShikakuBinaryCsp.WithInitialCapacity(puzzle.Hints.Count);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetAllDomains().Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
        }

        [Theory]
        [ClassData(typeof(ShikakuPuzzles.ExpectedAdjacentVariables))]
        public void BinaryCspHasExpectedAdjacentVariables(ShikakuPuzzle puzzle, IEnumerable<Pair<Hint>> expected)
        {
            // Arrange
            ShikakuBinaryCsp binaryCsp = ShikakuBinaryCsp.WithInitialCapacity(puzzle.Hints.Count);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetAllAdjacentVariables().Should().Equal(expected);
        }

        [Theory]
        [ClassData(typeof(ShikakuPuzzles.ExpectedProblemMetrics))]
        public void BinaryCspHasExpectedProblemMetrics(ShikakuPuzzle puzzle, ProblemMetrics expected)
        {
            // Arrange
            ShikakuBinaryCsp binaryCsp = ShikakuBinaryCsp.WithInitialCapacity(puzzle.Hints.Count);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetProblemMetrics().Should().BeEquivalentTo(expected, ConfigureEquivalencyOptions<ProblemMetrics>());
        }

        [Theory]
        [ClassData(typeof(ShikakuPuzzles.ExpectedDomainSizeStatistics))]
        public void BinaryCspHasExpectedDomainSizeStatistics(ShikakuPuzzle puzzle, DomainSizeStatistics expected)
        {
            // Arrange
            ShikakuBinaryCsp binaryCsp = ShikakuBinaryCsp.WithInitialCapacity(puzzle.Hints.Count);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetDomainSizeStatistics().Should()
                .BeEquivalentTo(expected, ConfigureEquivalencyOptions<DomainSizeStatistics>());
        }

        [Theory]
        [ClassData(typeof(ShikakuPuzzles.ExpectedDegreeStatistics))]
        public void BinaryCspHasExpectedDegreeStatistics(ShikakuPuzzle puzzle, DegreeStatistics expected)
        {
            // Arrange
            ShikakuBinaryCsp binaryCsp = ShikakuBinaryCsp.WithInitialCapacity(puzzle.Hints.Count);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetDegreeStatistics().Should().BeEquivalentTo(expected, ConfigureEquivalencyOptions<DegreeStatistics>());
        }

        [Theory]
        [ClassData(typeof(ShikakuPuzzles.ExpectedSumTightnessStatistics))]
        public void BinaryCspHasExpectedSumTightnessStatistics(ShikakuPuzzle puzzle, SumTightnessStatistics expected)
        {
            // Arrange
            ShikakuBinaryCsp binaryCsp = ShikakuBinaryCsp.WithInitialCapacity(puzzle.Hints.Count);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetSumTightnessStatistics().Should()
                .BeEquivalentTo(expected, ConfigureEquivalencyOptions<SumTightnessStatistics>());
        }
    }

    [IntegrationTest]
    public sealed class Modelling_SudokuPuzzle
    {
        [Theory]
        [ClassData(typeof(SudokuPuzzles.ExpectedVariables))]
        public void BinaryCspHasExpectedVariables(SudokuPuzzle puzzle, IEnumerable<EmptyCell> expected)
        {
            // Arrange
            SudokuBinaryCsp binaryCsp = SudokuBinaryCsp.WithInitialCapacity(10);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetAllVariables().Should().Equal(expected);
        }

        [Theory]
        [ClassData(typeof(SudokuPuzzles.ExpectedDomains))]
        public void BinaryCspHasExpectedDomains(SudokuPuzzle puzzle, IEnumerable<IReadOnlyList<int>> expected)
        {
            // Arrange
            SudokuBinaryCsp binaryCsp = SudokuBinaryCsp.WithInitialCapacity(10);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetAllDomains().Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
        }

        [Theory]
        [ClassData(typeof(SudokuPuzzles.ExpectedAdjacentVariables))]
        public void BinaryCspHasExpectedAdjacentVariables(SudokuPuzzle puzzle, IEnumerable<Pair<EmptyCell>> expected)
        {
            // Arrange
            SudokuBinaryCsp binaryCsp = SudokuBinaryCsp.WithInitialCapacity(10);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetAllAdjacentVariables().Should().Equal(expected);
        }

        [Theory]
        [ClassData(typeof(SudokuPuzzles.ExpectedProblemMetrics))]
        public void BinaryCspHasExpectedProblemMetrics(SudokuPuzzle puzzle, ProblemMetrics expected)
        {
            // Arrange
            SudokuBinaryCsp binaryCsp = SudokuBinaryCsp.WithInitialCapacity(10);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetProblemMetrics().Should().BeEquivalentTo(expected, ConfigureEquivalencyOptions<ProblemMetrics>());
        }

        [Theory]
        [ClassData(typeof(SudokuPuzzles.ExpectedDomainSizeStatistics))]
        public void BinaryCspHasExpectedDomainSizeStatistics(SudokuPuzzle puzzle, DomainSizeStatistics expected)
        {
            // Arrange
            SudokuBinaryCsp binaryCsp = SudokuBinaryCsp.WithInitialCapacity(10);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetDomainSizeStatistics().Should()
                .BeEquivalentTo(expected, ConfigureEquivalencyOptions<DomainSizeStatistics>());
        }

        [Theory]
        [ClassData(typeof(SudokuPuzzles.ExpectedDegreeStatistics))]
        public void BinaryCspHasExpectedDegreeStatistics(SudokuPuzzle puzzle, DegreeStatistics expected)
        {
            // Arrange
            SudokuBinaryCsp binaryCsp = SudokuBinaryCsp.WithInitialCapacity(10);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetDegreeStatistics().Should().BeEquivalentTo(expected, ConfigureEquivalencyOptions<DegreeStatistics>());
        }

        [Theory]
        [ClassData(typeof(SudokuPuzzles.ExpectedSumTightnessStatistics))]
        public void BinaryCspHasExpectedSumTightnessStatistics(SudokuPuzzle puzzle, SumTightnessStatistics expected)
        {
            // Arrange
            SudokuBinaryCsp binaryCsp = SudokuBinaryCsp.WithInitialCapacity(10);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetSumTightnessStatistics().Should()
                .BeEquivalentTo(expected, ConfigureEquivalencyOptions<SumTightnessStatistics>());
        }
    }
}
