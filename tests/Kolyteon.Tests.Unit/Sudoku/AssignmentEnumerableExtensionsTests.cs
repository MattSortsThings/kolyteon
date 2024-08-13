using Kolyteon.Common;
using Kolyteon.Modelling;
using Kolyteon.Sudoku;

namespace Kolyteon.Tests.Unit.Sudoku;

public static class AssignmentEnumerableExtensionsTests
{
    [UnitTest]
    public sealed class ToSudokuSolutionMethod
    {
        [Fact]
        public void ToSudokuSolution_ReturnsArrayOfNumberedSquares()
        {
            // Arrange
            Assignment<Square, int>[] sut =
            [
                new Assignment<Square, int>(Square.Parse("(0,0)"), 1),
                new Assignment<Square, int>(Square.Parse("(1,0)"), 2),
                new Assignment<Square, int>(Square.Parse("(3,3)"), 1)
            ];

            // Act
            NumberedSquare[] result = sut.ToSudokuSolution();

            // Assert
            result.Should().Equal(NumberedSquare.Parse("(0,0) [1]"),
                NumberedSquare.Parse("(1,0) [2]"),
                NumberedSquare.Parse("(3,3) [1]"));
        }
    }
}
