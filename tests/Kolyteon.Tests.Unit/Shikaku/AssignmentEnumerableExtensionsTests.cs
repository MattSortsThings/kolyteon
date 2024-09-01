using Kolyteon.Common;
using Kolyteon.Modelling;
using Kolyteon.Shikaku;

namespace Kolyteon.Tests.Unit.Shikaku;

public static class AssignmentEnumerableExtensionsTests
{
    [UnitTest]
    public sealed class ToShikakuSolutionMethod
    {
        [Fact]
        public void ToShikakuSolution_ReturnsArrayOfBlocks()
        {
            // Arrange
            Assignment<NumberedSquare, Block>[] sut =
            [
                new(NumberedSquare.Parse("(0,0) [5]"), Block.Parse("(0,0) [5x1]")),
                new(NumberedSquare.Parse("(0,1) [5]"), Block.Parse("(0,1) [5x1]")),
                new(NumberedSquare.Parse("(0,2) [15]"), Block.Parse("(0,2) [5x3]"))
            ];

            // Act
            Block[] result = sut.ToShikakuSolution();

            // Assert
            result.Should().Equal(Block.Parse("(0,0) [5x1]"),
                Block.Parse("(0,1) [5x1]"),
                Block.Parse("(0,2) [5x3]"));
        }
    }
}
