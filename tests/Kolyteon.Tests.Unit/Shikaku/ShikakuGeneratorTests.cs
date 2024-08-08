using Kolyteon.Common;
using Kolyteon.Shikaku;
using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.Shikaku;

public static class ShikakuGeneratorTests
{
    [UnitTest]
    [ClearBoxTest]
    public sealed class GenerateMethodUsingMinimalFakeRandom
    {
        [Theory]
        [InlineData(5, 1)]
        [InlineData(5, 5)]
        [InlineData(5, 9)]
        [InlineData(5, 10)]
        [InlineData(6, 1)]
        [InlineData(6, 2)]
        [InlineData(6, 11)]
        [InlineData(6, 12)]
        public void Generate_GivenGridSideLengthAndHints_ReturnsInstanceWithHintsInAscendingOrder(int gridSideLength, int hints)
        {
            // Arrange
            ShikakuGenerator sut = new(MinimalFakeRandom.Instance);

            // Act
            ShikakuProblem result = sut.Generate(gridSideLength, hints);

            // Assert
            Block expectedGrid = Dimensions.FromWidthAndHeight(gridSideLength, gridSideLength).ToBlock();

            using (new AssertionScope())
            {
                result.Grid.Should().Be(expectedGrid);

                result.Hints.Should().BeInAscendingOrder()
                    .And.HaveCount(hints)
                    .And.AllSatisfy(hint => result.Grid.Contains(hint).Should().BeTrue());

                result.Hints.Sum(hint => hint.Number).Should().Be(result.Grid.AreaInSquares);
            }
        }

        [Fact]
        public void Generate_GridSideLengthArgIsLessThanFive_Throws()
        {
            // Arrange
            ShikakuGenerator sut = new(MinimalFakeRandom.Instance);

            const int arbitraryHints = 1;

            // Act
            Action act = () => sut.Generate(4, arbitraryHints);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must be not less than 5 and not greater than 20. (Parameter 'gridSideLength')\n" +
                             "Actual value was 4.");
        }

        [Fact]
        public void Generate_GridSideLengthArgIsGreaterThanTwenty_Throws()
        {
            // Arrange
            ShikakuGenerator sut = new(MinimalFakeRandom.Instance);

            const int arbitraryHints = 1;

            // Act
            Action act = () => sut.Generate(21, arbitraryHints);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must be not less than 5 and not greater than 20. (Parameter 'gridSideLength')\n" +
                             "Actual value was 21.");
        }

        [Fact]
        public void Generate_HintsArgIsLessThanOne_Throws()
        {
            // Arrange
            ShikakuGenerator sut = new(MinimalFakeRandom.Instance);

            const int arbitraryGridSideLength = 5;

            // Act
            Action act = () => sut.Generate(arbitraryGridSideLength, 0);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must be greater than 0 and not greater than twice the specified grid side length. " +
                             "(Parameter 'hints')\n" +
                             "Actual value was 0.");
        }

        [Theory]
        [InlineData(5, 11)]
        [InlineData(10, 21)]
        public void Generate_HintsArgIsLessThanGreaterThanTwiceGridSideLengthArg_Throws(int gridSideLength, int hints)
        {
            // Arrange
            ShikakuGenerator sut = new(MinimalFakeRandom.Instance);

            // Act
            Action act = () => sut.Generate(gridSideLength, hints);

            // Assert
            string expectedMessageSuffix = $"Actual value was {hints}.";

            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must be greater than 0 and not greater than twice the specified grid side length. " +
                             "(Parameter 'hints')\n" +
                             expectedMessageSuffix);
        }
    }

    [UnitTest]
    [ClearBoxTest]
    public sealed class GenerateMethodUsingMaximalFakeRandom
    {
        [Theory]
        [InlineData(5, 1)]
        [InlineData(5, 5)]
        [InlineData(5, 9)]
        [InlineData(5, 10)]
        [InlineData(6, 1)]
        [InlineData(6, 2)]
        [InlineData(6, 11)]
        [InlineData(6, 12)]
        public void Generate_GivenGridSideLengthAndHints_ReturnsInstanceWithHintsInAscendingOrder(int gridSideLength, int hints)
        {
            // Arrange
            ShikakuGenerator sut = new(MaximalFakeRandom.Instance);

            // Act
            ShikakuProblem result = sut.Generate(gridSideLength, hints);

            // Assert
            Block expectedGrid = Dimensions.FromWidthAndHeight(gridSideLength, gridSideLength).ToBlock();

            using (new AssertionScope())
            {
                result.Grid.Should().Be(expectedGrid);

                result.Hints.Should().BeInAscendingOrder()
                    .And.HaveCount(hints)
                    .And.AllSatisfy(hint => result.Grid.Contains(hint).Should().BeTrue());

                result.Hints.Sum(hint => hint.Number).Should().Be(result.Grid.AreaInSquares);
            }
        }
    }
}
