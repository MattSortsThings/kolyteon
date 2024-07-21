using System.Text.Json;
using Kolyteon.Common;

namespace Kolyteon.Tests.Unit.Common;

public static class BlockTests
{
    [UnitTest]
    public sealed class ParameterlessConstructor
    {
        [Fact]
        public void ParameterlessConstructor_Initializes_HasOriginSquareInColumnZeroRowZeroAndDimensionsOfOneByOne()
        {
            // Act
            Block result = new();

            // Assert
            using (new AssertionScope())
            {
                result.OriginSquare.Column.Should().Be(0);
                result.OriginSquare.Row.Should().Be(0);
                result.Dimensions.WidthInSquares.Should().Be(1);
                result.Dimensions.HeightInSquares.Should().Be(1);
            }
        }
    }

    [UnitTest]
    public sealed class CompareToMethod
    {
        [Fact]
        public void CompareTo_InstanceOriginSquarePrecedesOther_ReturnsNegativeValue()
        {
            // Arrange
            Dimensions sharedDimensions = Dimensions.FromWidthAndHeight(5, 5);

            Block sut = Square.FromColumnAndRow(0, 0).ToBlock(sharedDimensions);
            Block other = Square.FromColumnAndRow(99, 99).ToBlock(sharedDimensions);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveSameOriginSquareInstanceDimensionsPrecedesOther_ReturnsNegativeValue()
        {
            // Arrange
            Square sharedOrigin = Square.FromColumnAndRow(5, 5);

            Block sut = sharedOrigin.ToBlock(Dimensions.FromWidthAndHeight(1, 1));
            Block other = sharedOrigin.ToBlock(Dimensions.FromWidthAndHeight(99, 99));

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveEqualOriginSquareAndDimensionsValues_ReturnsZero()
        {
            // Arrange
            Square sharedOrigin = Square.FromColumnAndRow(5, 5);
            Dimensions sharedDimensions = Dimensions.FromWidthAndHeight(5, 5);

            Block sut = sharedOrigin.ToBlock(sharedDimensions);
            Block other = sharedOrigin.ToBlock(sharedDimensions);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void CompareTo_InstanceOriginSquareFollowsOther_ReturnsPositiveValue()
        {
            // Arrange
            Dimensions sharedDimensions = Dimensions.FromWidthAndHeight(5, 5);

            Block sut = Square.FromColumnAndRow(99, 99).ToBlock(sharedDimensions);
            Block other = Square.FromColumnAndRow(0, 0).ToBlock(sharedDimensions);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveSameOriginSquareInstanceDimensionsFollowOther_ReturnsNegativeValue()
        {
            // Arrange
            Square sharedOrigin = Square.FromColumnAndRow(5, 5);

            Block sut = sharedOrigin.ToBlock(Dimensions.FromWidthAndHeight(99, 99));
            Block other = sharedOrigin.ToBlock(Dimensions.FromWidthAndHeight(1, 1));

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }
    }

    [UnitTest]
    public sealed class EqualsMethod
    {
        [Fact]
        public void Equals_InstanceAndOtherHaveEqualOriginSquareAndDimensionsValues_ReturnsTrue()
        {
            // Arrange
            Square sharedOrigin = Square.FromColumnAndRow(5, 5);
            Dimensions sharedDimensions = Dimensions.FromWidthAndHeight(5, 5);

            Block sut = sharedOrigin.ToBlock(sharedDimensions);
            Block other = sharedOrigin.ToBlock(sharedDimensions);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalOriginSquareValues_ReturnsFalse()
        {
            // Arrange
            Dimensions sharedDimensions = Dimensions.FromWidthAndHeight(5, 5);

            Block sut = Square.FromColumnAndRow(99, 99).ToBlock(sharedDimensions);
            Block other = Square.FromColumnAndRow(0, 0).ToBlock(sharedDimensions);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalDimensionsValues_ReturnsFalse()
        {
            // Arrange
            Square sharedOrigin = Square.FromColumnAndRow(5, 5);

            Block sut = sharedOrigin.ToBlock(Dimensions.FromWidthAndHeight(99, 99));
            Block other = sharedOrigin.ToBlock(Dimensions.FromWidthAndHeight(1, 1));

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ContainsMethod
    {
        public static TheoryData<Block, Square> PositiveTestCases => new()
        {
            { Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1)), Square.FromColumnAndRow(0, 0) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(2, 2) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(2, 3) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(2, 4) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(3, 2) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(3, 3) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(3, 4) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(4, 2) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(4, 3) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(4, 4) }
        };

        public static TheoryData<Block, Square> NegativeTestCases => new()
        {
            { Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1)), Square.FromColumnAndRow(0, 1) },
            { Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1)), Square.FromColumnAndRow(1, 0) },
            { Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1)), Square.FromColumnAndRow(1, 1) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(1, 1) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(1, 2) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(1, 3) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(1, 4) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(1, 5) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(1, 1) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(2, 1) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(3, 1) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(4, 1) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(5, 1) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(5, 2) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(5, 3) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(5, 4) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(5, 5) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(2, 5) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(3, 5) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(4, 5) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(0, 0) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(99, 99) }
        };

        [Theory]
        [MemberData(nameof(PositiveTestCases), MemberType = typeof(ContainsMethod))]
        public void Contains_GivenSquareInsideBlock_ReturnsTrue(Block sut, Square square)
        {
            // Act
            bool result = sut.Contains(in square);

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class ToStringMethod
    {
        public static TheoryData<Square, Dimensions, string> TestCases => new()
        {
            { Square.FromColumnAndRow(0, 0), Dimensions.FromWidthAndHeight(1, 1), "(0,0) [1x1]" },
            { Square.FromColumnAndRow(7, 3), Dimensions.FromWidthAndHeight(2, 4), "(7,3) [2x4]" },
            { Square.FromColumnAndRow(10, 20), Dimensions.FromWidthAndHeight(100, 100), "(10,20) [100x100]" }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(ToStringMethod))]
        public void ToString_ReturnsFormattedString(Square originSquare, Dimensions dimensions, string expected)
        {
            // Arrange
            Block sut = originSquare.ToBlock(dimensions);

            // Act
            string result = sut.ToString();

            // Assert
            result.Should().Be(expected);
        }
    }

    [UnitTest]
    public sealed class EqualityOperator
    {
        [Fact]
        public void Equality_InstanceAndOtherHaveEqualOriginSquareAndDimensionsValues_ReturnsTrue()
        {
            // Arrange
            Square sharedOrigin = Square.FromColumnAndRow(5, 5);
            Dimensions sharedDimensions = Dimensions.FromWidthAndHeight(5, 5);

            Block sut = sharedOrigin.ToBlock(sharedDimensions);
            Block other = sharedOrigin.ToBlock(sharedDimensions);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalOriginSquareValues_ReturnsFalse()
        {
            // Arrange
            Dimensions sharedDimensions = Dimensions.FromWidthAndHeight(5, 5);

            Block sut = Square.FromColumnAndRow(99, 99).ToBlock(sharedDimensions);
            Block other = Square.FromColumnAndRow(0, 0).ToBlock(sharedDimensions);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalDimensionsValues_ReturnsFalse()
        {
            // Arrange
            Square sharedOrigin = Square.FromColumnAndRow(5, 5);

            Block sut = sharedOrigin.ToBlock(Dimensions.FromWidthAndHeight(99, 99));
            Block other = sharedOrigin.ToBlock(Dimensions.FromWidthAndHeight(1, 1));

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class InequalityOperator
    {
        [Fact]
        public void Inequality_InstanceAndOtherHaveEqualOriginSquareAndDimensionsValues_ReturnsFalse()
        {
            // Arrange
            Square sharedOrigin = Square.FromColumnAndRow(5, 5);
            Dimensions sharedDimensions = Dimensions.FromWidthAndHeight(5, 5);

            Block sut = sharedOrigin.ToBlock(sharedDimensions);
            Block other = sharedOrigin.ToBlock(sharedDimensions);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalOriginSquareValues_ReturnsTrue()
        {
            // Arrange
            Dimensions sharedDimensions = Dimensions.FromWidthAndHeight(5, 5);

            Block sut = Square.FromColumnAndRow(99, 99).ToBlock(sharedDimensions);
            Block other = Square.FromColumnAndRow(0, 0).ToBlock(sharedDimensions);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalDimensionsValues_ReturnsTrue()
        {
            // Arrange
            Square sharedOrigin = Square.FromColumnAndRow(5, 5);

            Block sut = sharedOrigin.ToBlock(Dimensions.FromWidthAndHeight(99, 99));
            Block other = sharedOrigin.ToBlock(Dimensions.FromWidthAndHeight(1, 1));

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        public static TheoryData<Square, Dimensions> TestCases => new()
        {
            { Square.FromColumnAndRow(0, 0), Dimensions.FromWidthAndHeight(1, 1) },
            { Square.FromColumnAndRow(7, 3), Dimensions.FromWidthAndHeight(2, 4) },
            { Square.FromColumnAndRow(10, 20), Dimensions.FromWidthAndHeight(100, 100) }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(Serialization))]
        public void CanSerializeToJson_ThenDeserializeToInstanceWithEqualValue(Square originSquare, Dimensions dimensions)
        {
            // Arrange
            Block originalBlock = originSquare.ToBlock(dimensions);

            string json = JsonSerializer.Serialize(originalBlock, JsonSerializerOptions.Default);

            // Act
            Block deserializedBlock = JsonSerializer.Deserialize<Block>(json, JsonSerializerOptions.Default);

            // Assert
            deserializedBlock.Should().Be(originalBlock);
        }
    }
}
