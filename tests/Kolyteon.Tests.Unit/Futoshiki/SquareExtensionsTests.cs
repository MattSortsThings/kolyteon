using Kolyteon.Common;
using Kolyteon.Futoshiki;

namespace Kolyteon.Tests.Unit.Futoshiki;

public static class SquareExtensionsTests
{
    [UnitTest]
    public sealed class AdjacentToMethod
    {
        [Fact]
        public void AdjacentTo_InstanceAndOtherHaveSameColumnAndAdjacentRows_ReturnsTrue()
        {
            // Arrange
            const int sharedColumn = 0;

            Square sut = Square.FromColumnAndRow(sharedColumn, 0);
            Square other = Square.FromColumnAndRow(sharedColumn, 1);

            // Act
            bool result = sut.AdjacentTo(other);
            bool reciprocalResult = other.AdjacentTo(sut);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeTrue();
                reciprocalResult.Should().BeTrue();
            }
        }

        [Fact]
        public void AdjacentTo_InstanceAndOtherHaveSameRowAndAdjacentColumns_ReturnsTrue()
        {
            // Arrange
            const int sharedRow = 0;

            Square sut = Square.FromColumnAndRow(0, sharedRow);
            Square other = Square.FromColumnAndRow(1, sharedRow);

            // Act
            bool result = sut.AdjacentTo(other);
            bool reciprocalResult = other.AdjacentTo(sut);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeTrue();
                reciprocalResult.Should().BeTrue();
            }
        }

        [Fact]
        public void AdjacentTo_InstanceAndOtherHaveEqualColumnAndRowValues_ReturnsFalse()
        {
            // Arrange
            const int sharedColumn = 4;
            const int sharedRow = 2;

            Square sut = Square.FromColumnAndRow(sharedColumn, sharedRow);
            Square other = Square.FromColumnAndRow(sharedColumn, sharedRow);

            // Act
            bool result = sut.AdjacentTo(other);
            bool reciprocalResult = other.AdjacentTo(sut);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeFalse();
                reciprocalResult.Should().BeFalse();
            }
        }

        [Theory]
        [InlineData(0, 2)]
        [InlineData(2, 0)]
        [InlineData(1, 1)]
        [InlineData(44, 91)]
        public void AdjacentTo_InstanceAndOtherAreNotAdjacent_ReturnsFalse(int otherColumn, int otherRow)
        {
            // Arrange
            Square sut = Square.FromColumnAndRow(0, 0);
            Square other = Square.FromColumnAndRow(otherColumn, otherRow);

            // Act
            bool result = sut.AdjacentTo(other);
            bool reciprocalResult = other.AdjacentTo(sut);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeFalse();
                reciprocalResult.Should().BeFalse();
            }
        }
    }
}
