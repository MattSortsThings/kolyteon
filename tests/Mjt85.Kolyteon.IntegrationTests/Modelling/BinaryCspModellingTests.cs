using FluentAssertions.Equivalency;
using Mjt85.Kolyteon.IntegrationTests.Helpers;
using Mjt85.Kolyteon.IntegrationTests.Modelling.TestCases;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.NQueens;

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
    public sealed class Modelling_NQueensPuzzle
    {
        [Theory]
        [ClassData(typeof(NQueensVariables))]
        public void BinaryCspHasExpectedVariables(NQueensPuzzle puzzle, IEnumerable<int> expected)
        {
            // Arrange
            NQueensBinaryCsp binaryCsp = new(puzzle.N);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetAllVariables().Should().Equal(expected);
        }

        [Theory]
        [ClassData(typeof(NQueensDomains))]
        public void BinaryCspHasExpectedDomains(NQueensPuzzle puzzle, IEnumerable<IReadOnlyList<Queen>> expected)
        {
            // Arrange
            NQueensBinaryCsp binaryCsp = new(puzzle.N);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetAllDomains().Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
        }

        [Theory]
        [ClassData(typeof(NQueensAdjacentVariables))]
        public void BinaryCspHasExpectedAdjacentVariables(NQueensPuzzle puzzle, IEnumerable<Pair<int>> expected)
        {
            // Arrange
            NQueensBinaryCsp binaryCsp = new(puzzle.N);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetAllAdjacentVariables().Should().Equal(expected);
        }

        [Theory]
        [ClassData(typeof(NQueensDomainSizeStatistics))]
        public void BinaryCspHasExpectedDomainSizeStatistics(NQueensPuzzle puzzle, DomainSizeStatistics expected)
        {
            // Arrange
            NQueensBinaryCsp binaryCsp = new(puzzle.N);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetDomainSizeStatistics().Should()
                .BeEquivalentTo(expected, ConfigureEquivalencyOptions<DomainSizeStatistics>());
        }


        [Theory]
        [ClassData(typeof(NQueensDegreeStatistics))]
        public void BinaryCspHasExpectedDegreeStatistics(NQueensPuzzle puzzle, DegreeStatistics expected)
        {
            // Arrange
            NQueensBinaryCsp binaryCsp = new(puzzle.N);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetDegreeStatistics().Should().BeEquivalentTo(expected, ConfigureEquivalencyOptions<DegreeStatistics>());
        }

        [Theory]
        [ClassData(typeof(NQueensSumTightnessStatistics))]
        public void BinaryCspHasExpectedSumTightnessStatistics(NQueensPuzzle puzzle, SumTightnessStatistics expected)
        {
            // Arrange
            NQueensBinaryCsp binaryCsp = new(puzzle.N);

            // Act
            binaryCsp.Model(puzzle);

            // Assert
            binaryCsp.GetSumTightnessStatistics().Should()
                .BeEquivalentTo(expected, ConfigureEquivalencyOptions<SumTightnessStatistics>());
        }
    }
}
