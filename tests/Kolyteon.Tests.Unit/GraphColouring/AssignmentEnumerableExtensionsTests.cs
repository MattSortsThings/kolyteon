using Kolyteon.Common;
using Kolyteon.GraphColouring;
using Kolyteon.Modelling;

namespace Kolyteon.Tests.Unit.GraphColouring;

public static class AssignmentEnumerableExtensionsTests
{
    [UnitTest]
    public sealed class ToGraphColouringSolutionMethod
    {
        [Fact]
        public void ToGraphColouringSolution_ReturnsDictionaryOfNodesAndColours()
        {
            // Arrange
            Assignment<Node, Colour>[] sut =
            [
                new(Node.FromName("Lundy"), Colour.Black),
                new(Node.FromName("Fastnet"), Colour.Red),
                new(Node.FromName("Irish Sea"), Colour.White)
            ];

            // Act
            Dictionary<Node, Colour> result = sut.ToGraphColouringSolution();

            // Assert
            result.Should().BeEquivalentTo(new Dictionary<Node, Colour>
            {
                [Node.FromName("Lundy")] = Colour.Black,
                [Node.FromName("Fastnet")] = Colour.Red,
                [Node.FromName("Irish Sea")] = Colour.White
            });
        }
    }
}
