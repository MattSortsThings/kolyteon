using Kolyteon.Common;
using Kolyteon.Sudoku;
using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.Sudoku;

public static class SudokuGeneratorTests
{
    [UnitTest]
    [ClearBoxTest]
    public sealed class GenerateMethodUsingMinimalFakeRandom
    {
        [Theory]
        [InlineData(1)]
        [InlineData(29)]
        [InlineData(30)]
        [InlineData(78)]
        [InlineData(80)]
        public void Generate_GivenEmptySquares_ReturnsInstanceWithFilledSquaresInAscendingOrder(int emptySquares)
        {
            // Arrange
            SudokuGenerator sut = new(MinimalFakeRandom.Instance);

            // Act
            SudokuProblem result = sut.Generate(emptySquares);

            // Assert
            using (new AssertionScope())
            {
                result.FilledSquares.Should().BeInAscendingOrder()
                    .And.HaveCount(81 - emptySquares);

                result.Grid.Should().Be(Block.Parse("(0,0) [9x9]"));

                result.Sectors.Should().Equal(Block.Parse("(0,0) [3x3]"),
                    Block.Parse("(0,3) [3x3]"),
                    Block.Parse("(0,6) [3x3]"),
                    Block.Parse("(3,0) [3x3]"),
                    Block.Parse("(3,3) [3x3]"),
                    Block.Parse("(3,6) [3x3]"),
                    Block.Parse("(6,0) [3x3]"),
                    Block.Parse("(6,3) [3x3]"),
                    Block.Parse("(6,6) [3x3]"));
            }
        }

        [Fact]
        public void Generate_EmptySquaresArgIsLessThanOne_Throws()
        {
            // Arrange
            SudokuGenerator sut = new(MinimalFakeRandom.Instance);

            // Act
            Action act = () => sut.Generate(0);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must be greater than 0 and less than 81. (Parameter 'emptySquares')\n" +
                             "Actual value was 0.");
        }

        [Fact]
        public void Generate_EmptySquaresArgIsGreaterThanEighty_Throws()
        {
            // Arrange
            SudokuGenerator sut = new(MinimalFakeRandom.Instance);

            // Act
            Action act = () => sut.Generate(81);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must be greater than 0 and less than 81. (Parameter 'emptySquares')\n" +
                             "Actual value was 81.");
        }
    }

    [UnitTest]
    [ClearBoxTest]
    public sealed class GenerateMethodUsingMaximalFakeRandom
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(30)]
        [InlineData(78)]
        [InlineData(80)]
        public void Generate_GivenEmptySquares_ReturnsInstanceWithFilledSquaresInAscendingOrder(int emptySquares)
        {
            // Arrange
            SudokuGenerator sut = new(MaximalFakeRandom.Instance);

            // Act
            SudokuProblem result = sut.Generate(emptySquares);

            // Assert
            using (new AssertionScope())
            {
                result.FilledSquares.Should().BeInAscendingOrder()
                    .And.HaveCount(81 - emptySquares);

                result.Grid.Should().Be(Block.Parse("(0,0) [9x9]"));

                result.Sectors.Should().Equal(Block.Parse("(0,0) [3x3]"),
                    Block.Parse("(0,3) [3x3]"),
                    Block.Parse("(0,6) [3x3]"),
                    Block.Parse("(3,0) [3x3]"),
                    Block.Parse("(3,3) [3x3]"),
                    Block.Parse("(3,6) [3x3]"),
                    Block.Parse("(6,0) [3x3]"),
                    Block.Parse("(6,3) [3x3]"),
                    Block.Parse("(6,6) [3x3]"));
            }
        }
    }
}
