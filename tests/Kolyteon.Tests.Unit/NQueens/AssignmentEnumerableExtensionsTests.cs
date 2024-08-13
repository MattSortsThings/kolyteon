using Kolyteon.Common;
using Kolyteon.Modelling;
using Kolyteon.NQueens;

namespace Kolyteon.Tests.Unit.NQueens;

public static class AssignmentEnumerableExtensionsTests
{
    [UnitTest]
    public sealed class ToNQueensSolutionMethod
    {
        [Fact]
        public void ToNQueensSolution_ReturnsArrayOfSquares()
        {
            // Arrange
            Assignment<int, Square>[] sut =
            [
                new Assignment<int, Square>(0, Square.Parse("(0,1)")),
                new Assignment<int, Square>(1, Square.Parse("(1,3)")),
                new Assignment<int, Square>(3, Square.Parse("(3,2)")),
                new Assignment<int, Square>(2, Square.Parse("(2,0)"))
            ];

            // Act
            Square[] result = sut.ToNQueensSolution();

            // Assert
            result.Should().Equal(Square.Parse("(0,1)"),
                Square.Parse("(1,3)"),
                Square.Parse("(3,2)"),
                Square.Parse("(2,0)"));
        }
    }
}
