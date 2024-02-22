using FluentAssertions.Execution;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Shikaku;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.Shikaku;

/// <summary>
///     Unit tests for the <see cref="ShikakuBinaryCsp" /> class.
/// </summary>
public sealed class ShikakuBinaryCspTests
{
    [UnitTest]
    public sealed class Model_Method
    {
        private static readonly Hint Col0Row0Num2 = new(0, 0, 2);
        private static readonly Hint Col0Row4Num16 = new(0, 4, 16);
        private static readonly Hint Col4Row0Num3 = new(4, 0, 3);
        private static readonly Hint Col4Row4Num4 = new(4, 4, 4);

        [Fact]
        public void Models_VariablesAreHintsOrderedByColumnThenByRow()
        {
            // Arrange
            ShikakuBinaryCsp sut = new(4);

            ShikakuPuzzle puzzle = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0002, null, null, null, 0003 },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { 0016, null, null, null, 0004 }
            });

            // Act
            sut.Model(puzzle);

            // Assert
            sut.GetAllVariables().Should().Equal(Col0Row0Num2, Col0Row4Num16, Col4Row0Num3, Col4Row4Num4);
        }

        [Fact]
        public void Models_DomainsAreAllRectanglesOfAreaEqualToHintNumberInsideGridEnclosingNoOtherHint_OrderedByRectangleSort()
        {
            // Arrange
            ShikakuBinaryCsp sut = new(4);

            ShikakuPuzzle puzzle = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0002, null, null, null, 0003 },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { 0016, null, null, null, 0004 }
            });

            // Act
            sut.Model(puzzle);

            // Assert
            IEnumerable<Rectangle>[] expectedDomains =
            [
                [
                    new Rectangle(0, 0, 1, 2),
                    new Rectangle(0, 0, 2, 1)
                ],
                [
                    new Rectangle(0, 1, 4, 4)
                ],
                [
                    new Rectangle(2, 0, 3, 1),
                    new Rectangle(4, 0, 1, 3)
                ],
                [
                    new Rectangle(1, 4, 4, 1),
                    new Rectangle(3, 3, 2, 2),
                    new Rectangle(4, 1, 1, 4)
                ]
            ];

            sut.GetAllDomains().Should().BeEquivalentTo(expectedDomains, options => options.WithStrictOrdering());
        }

        [Fact]
        public void Models_AddsConstraintForEveryHintPairWithAtLeastOnePairOfOverlappingRectanglesInDomains()
        {
            // Arrange
            ShikakuBinaryCsp sut = new(4);

            ShikakuPuzzle puzzle = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0002, null, null, null, 0003 },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { 0016, null, null, null, 0004 }
            });

            // Act
            sut.Model(puzzle);

            // Assert
            IEnumerable<Pair<Hint>> expectedAdjacentVariables =
            [
                new Pair<Hint>(Col0Row0Num2, Col0Row4Num16),
                new Pair<Hint>(Col0Row4Num16, Col4Row4Num4),
                new Pair<Hint>(Col4Row0Num3, Col4Row4Num4)
            ];

            sut.GetAllAdjacentVariables().Should().Equal(expectedAdjacentVariables);
        }

        [Fact]
        public void Models_UpdatesAllProblemMetricsProperties()
        {
            // Arrange
            ShikakuBinaryCsp sut = new(4);

            ShikakuPuzzle puzzle = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0002, null, null, null, 0003 },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { 0016, null, null, null, 0004 }
            });

            // Act
            sut.Model(puzzle);

            // Assert
            using (new AssertionScope())
            {
                sut.Variables.Should().Be(4);
                sut.Constraints.Should().Be(3);
                sut.ConstraintDensity.Should().BeApproximately(0.5, Invariants.SixDecimalPlacesPrecision);
                sut.ConstraintTightness.Should().BeApproximately(0.363636, Invariants.SixDecimalPlacesPrecision);
            }
        }
    }
}
