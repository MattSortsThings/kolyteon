using Kolyteon.Common;
using Kolyteon.Sudoku;

namespace Kolyteon.Tests.Integration;

[IntegrationTest]
public abstract partial class ProblemGenerationTests
{
    private protected abstract int Seed { get; }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(10)]
    [InlineData(11)]
    [InlineData(23)]
    [InlineData(30)]
    [InlineData(55)]
    [InlineData(63)]
    [InlineData(79)]
    [InlineData(80)]
    public void CanGenerateSudokuProblemFromEmptySquares(int emptySquares)
    {
        // Arrange
        SudokuGenerator generator = new(Seed);

        // Act
        SudokuProblem result = generator.Generate(emptySquares);

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
