using Kolyteon.Common;
using Kolyteon.NQueens;

namespace Kolyteon.Tests.Unit.NQueens;

public static class SquareExtensionsTests
{
    [UnitTest]
    public sealed class CapturesMethod
    {
        public static TheoryData<Square, Square> PositiveTestCases => new()
        {
            // same column
            { Square.FromColumnAndRow(0, 0), Square.FromColumnAndRow(1, 0) },
            { Square.FromColumnAndRow(1, 1), Square.FromColumnAndRow(4, 1) },
            { Square.FromColumnAndRow(2, 2), Square.FromColumnAndRow(500, 2) },
            { Square.FromColumnAndRow(5, 10), Square.FromColumnAndRow(5, 15) },

            // same row
            { Square.FromColumnAndRow(0, 0), Square.FromColumnAndRow(0, 1) },
            { Square.FromColumnAndRow(1, 1), Square.FromColumnAndRow(1, 4) },
            { Square.FromColumnAndRow(2, 2), Square.FromColumnAndRow(2, 500) },
            { Square.FromColumnAndRow(5, 10), Square.FromColumnAndRow(7, 10) },

            // same diagonal
            { Square.FromColumnAndRow(0, 0), Square.FromColumnAndRow(1, 1) },
            { Square.FromColumnAndRow(0, 0), Square.FromColumnAndRow(99, 99) },
            { Square.FromColumnAndRow(4, 5), Square.FromColumnAndRow(5, 6) },
            { Square.FromColumnAndRow(4, 5), Square.FromColumnAndRow(5, 4) },
            { Square.FromColumnAndRow(4, 5), Square.FromColumnAndRow(6, 7) },
            { Square.FromColumnAndRow(4, 5), Square.FromColumnAndRow(6, 3) }
        };

        public static TheoryData<Square, Square> NegativeTestCases => new()
        {
            { Square.FromColumnAndRow(0, 0), Square.FromColumnAndRow(1, 2) },
            { Square.FromColumnAndRow(0, 0), Square.FromColumnAndRow(2, 1) },
            { Square.FromColumnAndRow(0, 0), Square.FromColumnAndRow(1, 99) },
            { Square.FromColumnAndRow(0, 0), Square.FromColumnAndRow(99, 1) },
            { Square.FromColumnAndRow(0, 0), Square.FromColumnAndRow(7, 3) },
            { Square.FromColumnAndRow(0, 0), Square.FromColumnAndRow(3, 7) },
            { Square.FromColumnAndRow(1, 2), Square.FromColumnAndRow(9, 4) },
            { Square.FromColumnAndRow(1, 12), Square.FromColumnAndRow(2, 50) },
            { Square.FromColumnAndRow(4, 5), Square.FromColumnAndRow(33, 111) },
            { Square.FromColumnAndRow(142, 839), Square.FromColumnAndRow(381, 399) }
        };

        [Fact]
        public void Captures_InstanceAndOtherAreEqual_ReturnsTrue()
        {
            // Arrange
            const int sharedColumn = 1;
            const int sharedRow = 2;

            Square sut = Square.FromColumnAndRow(sharedColumn, sharedRow);
            Square other = Square.FromColumnAndRow(sharedColumn, sharedRow);

            // Act
            bool result = sut.Captures(other);
            bool reciprocalResult = other.Captures(sut);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeTrue();
                reciprocalResult.Should().BeTrue();
            }
        }

        [Theory]
        [MemberData(nameof(PositiveTestCases), MemberType = typeof(CapturesMethod))]
        public void Captures_InstanceAndOtherHaveSameColumnOrRowOrDiagonal_ReturnsTrue(Square firstQueen, Square secondQueen)
        {
            // Act
            bool result = firstQueen.Captures(secondQueen);
            bool reciprocalResult = secondQueen.Captures(firstQueen);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeTrue();
                reciprocalResult.Should().BeTrue();
            }
        }

        [Theory]
        [MemberData(nameof(NegativeTestCases), MemberType = typeof(CapturesMethod))]
        public void Captures_InstanceAndOtherDoNotHaveSameColumnOrRowOrDiagonal_ReturnsFalse(Square firstQueen,
            Square secondQueen)
        {
            // Act
            bool result = firstQueen.Captures(secondQueen);
            bool reciprocalResult = secondQueen.Captures(firstQueen);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeFalse();
                reciprocalResult.Should().BeFalse();
            }
        }
    }
}
