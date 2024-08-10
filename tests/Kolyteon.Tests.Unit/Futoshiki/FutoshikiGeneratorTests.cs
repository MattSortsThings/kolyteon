using System.Linq.Expressions;
using Kolyteon.Common;
using Kolyteon.Futoshiki;
using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.Futoshiki;

public static class FutoshikiGeneratorTests
{
    private static Expression<Func<FutoshikiProblem, bool>> ProblemHasAtLeastOneSign() =>
        problem => problem.LessThanSigns.Count > 0 || problem.GreaterThanSigns.Count > 0;

    [UnitTest]
    [ClearBoxTest]
    public sealed class GenerateMethodUsingMinimalFakeRandom
    {
        [Theory]
        [InlineData(5, 1)]
        [InlineData(5, 5)]
        [InlineData(5, 21)]
        [InlineData(5, 24)]
        [InlineData(6, 1)]
        [InlineData(6, 2)]
        [InlineData(6, 31)]
        [InlineData(6, 35)]
        public void Generate_GivenGridSideLengthAndEmptySquares_ReturnsInstanceWithFilledSquares(int gridSideLength,
            int emptySquares)
        {
            // Arrange
            FutoshikiGenerator sut = new(MinimalFakeRandom.Instance);

            // Act
            FutoshikiProblem result = sut.Generate(gridSideLength, emptySquares);

            // Assert
            Block expectedGrid = Dimensions.FromWidthAndHeight(gridSideLength, gridSideLength).ToBlock();

            int expectedFilledSquares = result.Grid.AreaInSquares - emptySquares;

            using (new AssertionScope())
            {
                result.Grid.Should().Be(expectedGrid);

                result.FilledSquares.Should().BeInAscendingOrder()
                    .And.HaveCount(expectedFilledSquares)
                    .And.AllSatisfy(filledSquare =>
                        result.Grid.Contains(filledSquare).Should().BeTrue())
                    .And.AllSatisfy(filledSquare =>
                        filledSquare.Number.Should().BeGreaterOrEqualTo(1).And.BeLessThanOrEqualTo(gridSideLength));

                result.Should().Match(ProblemHasAtLeastOneSign());
            }
        }

        [Fact]
        public void Generate_GridSideLengthArgIsLessThanFour_Throws()
        {
            // Arrange
            FutoshikiGenerator sut = new(MinimalFakeRandom.Instance);

            const int arbitraryEmptySquares = 1;

            // Act
            Action act = () => sut.Generate(3, arbitraryEmptySquares);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must be not less than 4 and not greater than 9. (Parameter 'gridSideLength')\n" +
                             "Actual value was 3.");
        }

        [Fact]
        public void Generate_GridSideLengthArgIsGreaterThanNine_Throws()
        {
            // Arrange
            FutoshikiGenerator sut = new(MinimalFakeRandom.Instance);

            const int arbitraryEmptySquares = 1;

            // Act
            Action act = () => sut.Generate(10, arbitraryEmptySquares);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must be not less than 4 and not greater than 9. (Parameter 'gridSideLength')\n" +
                             "Actual value was 10.");
        }

        [Fact]
        public void Generate_EmptySquaresArgIsLessThanOne_Throws()
        {
            // Arrange
            FutoshikiGenerator sut = new(MinimalFakeRandom.Instance);

            const int arbitraryGridSideLength = 4;

            // Act
            Action act = () => sut.Generate(arbitraryGridSideLength, 0);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must be greater than 0 and less than the square of the specified grid side length. " +
                             "(Parameter 'emptySquares')\n" +
                             "Actual value was 0.");
        }

        [Theory]
        [InlineData(4, 16)]
        [InlineData(5, 25)]
        [InlineData(5, 26)]
        public void Generate_EmptySquaresArgIsGreaterThanOrEqualToGridArea_Throws(int gridSideLength, int emptySquares)
        {
            // Arrange
            FutoshikiGenerator sut = new(MinimalFakeRandom.Instance);

            // Act
            Action act = () => sut.Generate(gridSideLength, emptySquares);

            // Assert
            string expectedMessageSuffix = $"Actual value was {emptySquares}.";

            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must be greater than 0 and less than the square of the specified grid side length. " +
                             "(Parameter 'emptySquares')\n" +
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
        [InlineData(5, 21)]
        [InlineData(5, 24)]
        [InlineData(6, 1)]
        [InlineData(6, 2)]
        [InlineData(6, 31)]
        [InlineData(6, 35)]
        public void Generate_GivenGridSideLengthAndEmptySquares_ReturnsInstanceWithFilledSquares(int gridSideLength,
            int emptySquares)
        {
            // Arrange
            FutoshikiGenerator sut = new(MaximalFakeRandom.Instance);

            // Act
            FutoshikiProblem result = sut.Generate(gridSideLength, emptySquares);

            // Assert
            Block expectedGrid = Dimensions.FromWidthAndHeight(gridSideLength, gridSideLength).ToBlock();

            int expectedFilledSquares = result.Grid.AreaInSquares - emptySquares;

            using (new AssertionScope())
            {
                result.Grid.Should().Be(expectedGrid);

                result.FilledSquares.Should().BeInAscendingOrder()
                    .And.HaveCount(expectedFilledSquares)
                    .And.AllSatisfy(filledSquare =>
                        result.Grid.Contains(filledSquare).Should().BeTrue())
                    .And.AllSatisfy(filledSquare =>
                        filledSquare.Number.Should().BeGreaterOrEqualTo(1).And.BeLessThanOrEqualTo(gridSideLength));

                result.Should().Match(ProblemHasAtLeastOneSign());
            }
        }
    }
}
