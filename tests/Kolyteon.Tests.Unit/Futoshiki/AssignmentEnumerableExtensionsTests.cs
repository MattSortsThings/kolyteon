using Kolyteon.Common;
using Kolyteon.Futoshiki;
using Kolyteon.Modelling;

namespace Kolyteon.Tests.Unit.Futoshiki;

public static class AssignmentEnumerableExtensionsTests
{
    [UnitTest]
    public sealed class ToFutoshikiSolutionMethod
    {
        [Fact]
        public void ToFutoshikiSolution_ReturnsArrayOfNumberedSquares()
        {
            // Arrange
            Assignment<Square, int>[] sut =
            [
                new(Square.Parse("(0,0)"), 1),
                new(Square.Parse("(1,0)"), 2),
                new(Square.Parse("(3,3)"), 1)
            ];

            // Act
            NumberedSquare[] result = sut.ToFutoshikiSolution();

            // Assert
            result.Should().Equal(NumberedSquare.Parse("(0,0) [1]"),
                NumberedSquare.Parse("(1,0) [2]"),
                NumberedSquare.Parse("(3,3) [1]"));
        }
    }
}
