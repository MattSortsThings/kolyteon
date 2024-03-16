using FluentAssertions.Execution;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.NQueens;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.NQueens;

/// <summary>
///     Unit tests for the <see cref="NQueensBinaryCsp" /> class.
/// </summary>
public sealed class NQueensBinaryCspTests
{
    [UnitTest]
    public sealed class Model_Method
    {
        [Fact]
        public void Models_VariablesAreColumnIndexesFromZeroInclusiveToNExclusive()
        {
            // Arrange
            NQueensBinaryCsp sut = NQueensBinaryCsp.WithInitialCapacity(4);

            NQueensPuzzle puzzle = NQueensPuzzle.FromN(4);

            // Act
            sut.Model(puzzle);

            // Assert
            sut.GetAllVariables().Should().Equal(0, 1, 2, 3);
        }

        [Fact]
        public void Models_DomainsAreAllQueensInColumnMatchingColumnIndexValue()
        {
            // Arrange
            NQueensBinaryCsp sut = NQueensBinaryCsp.WithInitialCapacity(4);

            NQueensPuzzle puzzle = NQueensPuzzle.FromN(4);

            // Act
            sut.Model(puzzle);

            // Assert
            IEnumerable<Queen[]> expectedDomains =
            [
                [new Queen(0, 0), new Queen(0, 1), new Queen(0, 2), new Queen(0, 3)],
                [new Queen(1, 0), new Queen(1, 1), new Queen(1, 2), new Queen(1, 3)],
                [new Queen(2, 0), new Queen(2, 1), new Queen(2, 2), new Queen(2, 3)],
                [new Queen(3, 0), new Queen(3, 1), new Queen(3, 2), new Queen(3, 3)]
            ];

            sut.GetAllDomains().Should().BeEquivalentTo(expectedDomains, options => options.WithStrictOrdering());
        }

        [Fact]
        public void Models_AllVariablePairsAdjacent()
        {
            // Arrange
            NQueensBinaryCsp sut = NQueensBinaryCsp.WithInitialCapacity(4);

            NQueensPuzzle puzzle = NQueensPuzzle.FromN(4);

            // Act
            sut.Model(puzzle);

            // Act
            IEnumerable<Pair<int>> expectedAdjacentVariables =
            [
                new Pair<int>(0, 1), new Pair<int>(0, 2), new Pair<int>(0, 3),
                new Pair<int>(1, 2), new Pair<int>(1, 3),
                new Pair<int>(2, 3)
            ];

            sut.GetAllAdjacentVariables().Should().Equal(expectedAdjacentVariables);
        }

        [Fact]
        public void Models_UpdatesAllProblemMetricsProperties()
        {
            // Arrange
            NQueensBinaryCsp sut = NQueensBinaryCsp.WithInitialCapacity(4);

            NQueensPuzzle puzzle = NQueensPuzzle.FromN(4);

            // Act
            sut.Model(puzzle);

            // Assert
            using (new AssertionScope())
            {
                sut.Variables.Should().Be(4);
                sut.Constraints.Should().Be(6);
                sut.ConstraintDensity.Should().BeApproximately(1.0, Invariants.SixDecimalPlacesPrecision);
                sut.ConstraintTightness.Should().BeApproximately(0.541667, Invariants.SixDecimalPlacesPrecision);
            }
        }
    }
}
