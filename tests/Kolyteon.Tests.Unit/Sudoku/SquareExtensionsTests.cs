using Kolyteon.Common;
using Kolyteon.Sudoku;

namespace Kolyteon.Tests.Unit.Sudoku;

public static class SquareExtensionsTests
{
    [UnitTest]
    public sealed class GetSectorMethod
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void GetSector_ReturnsSectorCalculatedFromColumnAndRow(int column, int row, int expectedSector)
        {
            // Arrange
            Square sut = Square.FromColumnAndRow(column, row);

            // Act
            int result = sut.GetSector();

            // Assert
            result.Should().Be(expectedSector);
        }

        private sealed class TestCases : TheoryData<int, int, int>
        {
            public TestCases()
            {
                Add(0, 0, 0);
                Add(0, 1, 0);
                Add(0, 2, 0);
                Add(0, 3, 1);
                Add(0, 4, 1);
                Add(0, 5, 1);
                Add(0, 6, 2);
                Add(0, 7, 2);
                Add(0, 8, 2);
                Add(1, 0, 0);
                Add(1, 1, 0);
                Add(1, 2, 0);
                Add(1, 3, 1);
                Add(1, 4, 1);
                Add(1, 5, 1);
                Add(1, 6, 2);
                Add(1, 7, 2);
                Add(1, 8, 2);
                Add(2, 0, 0);
                Add(2, 1, 0);
                Add(2, 2, 0);
                Add(2, 3, 1);
                Add(2, 4, 1);
                Add(2, 5, 1);
                Add(2, 6, 2);
                Add(2, 7, 2);
                Add(2, 8, 2);
                Add(3, 0, 3);
                Add(3, 1, 3);
                Add(3, 2, 3);
                Add(3, 3, 4);
                Add(3, 4, 4);
                Add(3, 5, 4);
                Add(3, 6, 5);
                Add(3, 7, 5);
                Add(3, 8, 5);
                Add(4, 0, 3);
                Add(4, 1, 3);
                Add(4, 2, 3);
                Add(4, 3, 4);
                Add(4, 4, 4);
                Add(4, 5, 4);
                Add(4, 6, 5);
                Add(4, 7, 5);
                Add(4, 8, 5);
                Add(5, 0, 3);
                Add(5, 1, 3);
                Add(5, 2, 3);
                Add(5, 3, 4);
                Add(5, 4, 4);
                Add(5, 5, 4);
                Add(5, 6, 5);
                Add(5, 7, 5);
                Add(5, 8, 5);
                Add(6, 0, 6);
                Add(6, 1, 6);
                Add(6, 2, 6);
                Add(6, 3, 7);
                Add(6, 4, 7);
                Add(6, 5, 7);
                Add(6, 6, 8);
                Add(6, 7, 8);
                Add(6, 8, 8);
                Add(7, 0, 6);
                Add(7, 1, 6);
                Add(7, 2, 6);
                Add(7, 3, 7);
                Add(7, 4, 7);
                Add(7, 5, 7);
                Add(7, 6, 8);
                Add(7, 7, 8);
                Add(7, 8, 8);
                Add(8, 0, 6);
                Add(8, 1, 6);
                Add(8, 2, 6);
                Add(8, 3, 7);
                Add(8, 4, 7);
                Add(8, 5, 7);
                Add(8, 6, 8);
                Add(8, 7, 8);
                Add(8, 8, 8);
            }
        }
    }
}
