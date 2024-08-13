using Kolyteon.Common;
using Kolyteon.MapColouring;
using Kolyteon.Modelling;

namespace Kolyteon.Tests.Unit.MapColouring;

public static class AssignmentEnumerableExtensionsTests
{
    [UnitTest]
    public sealed class ToMapColouringSolutionMethod
    {
        [Fact]
        public void ToMapColouringSolution_ReturnsDictionaryOfBlocksAndColours()
        {
            // Arrange
            Assignment<Block, Colour>[] sut =
            [
                new Assignment<Block, Colour>(Block.Parse("(0,0) [5x1]"), Colour.Black),
                new Assignment<Block, Colour>(Block.Parse("(0,1) [5x1]"), Colour.Red),
                new Assignment<Block, Colour>(Block.Parse("(0,2) [5x3]"), Colour.White)
            ];

            // Act
            Dictionary<Block, Colour> result = sut.ToMapColouringSolution();

            // Assert
            result.Should().BeEquivalentTo(new Dictionary<Block, Colour>
            {
                [Block.Parse("(0,0) [5x1]")] = Colour.Black,
                [Block.Parse("(0,1) [5x1]")] = Colour.Red,
                [Block.Parse("(0,2) [5x3]")] = Colour.White
            });
        }
    }
}
