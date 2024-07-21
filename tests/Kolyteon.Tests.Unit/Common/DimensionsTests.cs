using System.Text.Json;
using Kolyteon.Common;

namespace Kolyteon.Tests.Unit.Common;

public static class DimensionsTests
{
    [UnitTest]
    public sealed class ParameterlessConstructor
    {
        [Fact]
        public void ParameterlessConstructor_Initializes_HasWidthInSquaresAndHeightInSquaresOfOne()
        {
            // Act
            Dimensions result = new();

            // Assert
            using (new AssertionScope())
            {
                result.WidthInSquares.Should().Be(1);
                result.WidthInSquares.Should().Be(1);
            }
        }
    }

    [UnitTest]
    public sealed class CompareToMethod
    {
        [Fact]
        public void CompareTo_InstanceWidthInSquaresPrecedesOther_ReturnsNegativeValue()
        {
            // Arrange
            const int sharedHeight = 5;

            Dimensions sut = Dimensions.FromWidthAndHeight(1, sharedHeight);
            Dimensions other = Dimensions.FromWidthAndHeight(99, sharedHeight);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveEqualWidthInSquaresInstanceHeightInSquaresPrecedesOther_ReturnsNegativeValue()
        {
            // Arrange
            const int sharedWidth = 5;

            Dimensions sut = Dimensions.FromWidthAndHeight(sharedWidth, 1);
            Dimensions other = Dimensions.FromWidthAndHeight(sharedWidth, 99);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveEqualWidthInSquaresAndHeightInSquaresValues_ReturnsZero()
        {
            // Arrange
            const int sharedWidth = 5;
            const int sharedHeight = 6;

            Dimensions sut = Dimensions.FromWidthAndHeight(sharedWidth, sharedHeight);
            Dimensions other = Dimensions.FromWidthAndHeight(sharedWidth, sharedHeight);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void CompareTo_InstanceWidthInSquaresPrecedesOther_ReturnsPositiveValue()
        {
            // Arrange
            const int sharedHeight = 5;

            Dimensions sut = Dimensions.FromWidthAndHeight(99, sharedHeight);
            Dimensions other = Dimensions.FromWidthAndHeight(1, sharedHeight);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveEqualWidthInSquaresInstanceHeightInSquaresFollowsOther_ReturnsPositiveValue()
        {
            // Arrange
            const int sharedWidth = 5;

            Dimensions sut = Dimensions.FromWidthAndHeight(sharedWidth, 99);
            Dimensions other = Dimensions.FromWidthAndHeight(sharedWidth, 1);

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
        public void Equals_InstanceAndOtherHaveEqualWidthInSquaresAndHeightInSquaresValues_ReturnsTrue()
        {
            // Arrange
            const int sharedWidth = 5;
            const int sharedHeight = 6;

            Dimensions sut = Dimensions.FromWidthAndHeight(sharedWidth, sharedHeight);
            Dimensions other = Dimensions.FromWidthAndHeight(sharedWidth, sharedHeight);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalWidthInSquaresValues_ReturnsFalse()
        {
            // Arrange
            const int sharedHeight = 5;

            Dimensions sut = Dimensions.FromWidthAndHeight(99, sharedHeight);
            Dimensions other = Dimensions.FromWidthAndHeight(1, sharedHeight);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalHeightInSquaresValues_ReturnsFalse()
        {
            // Arrange
            const int sharedWidth = 5;

            Dimensions sut = Dimensions.FromWidthAndHeight(sharedWidth, 99);
            Dimensions other = Dimensions.FromWidthAndHeight(sharedWidth, 1);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ToStringMethod
    {
        [Theory]
        [InlineData(1, 1, "1x1")]
        [InlineData(2, 3, "2x3")]
        [InlineData(70, 30, "70x30")]
        [InlineData(100, 100, "100x100")]
        public void ToString_ReturnsFormattedString(int width, int height, string expected)
        {
            // Arrange
            Dimensions sut = Dimensions.FromWidthAndHeight(width, height);

            // Act
            string result = sut.ToString();

            // Assert
            result.Should().Be(expected);
        }
    }

    [UnitTest]
    public sealed class ToBlockMethod
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 3)]
        public void ToBlock_ReturnsBlockWithOriginSquareAtColumnZeroRowZeroAndSelfAsDimensions(int width, int height)
        {
            // Arrange
            Dimensions sut = Dimensions.FromWidthAndHeight(width, height);

            // Act
            Block result = sut.ToBlock();

            // Assert
            using (new AssertionScope())
            {
                result.OriginSquare.Should().Be(Square.FromColumnAndRow(0, 0));
                result.Dimensions.Should().Be(sut);
            }
        }
    }

    [UnitTest]
    public sealed class EqualityOperator
    {
        [Fact]
        public void Equality_InstanceAndOtherHaveEqualWidthInSquaresAndHeightInSquaresValues_ReturnsTrue()
        {
            // Arrange
            const int sharedWidth = 5;
            const int sharedHeight = 6;

            Dimensions sut = Dimensions.FromWidthAndHeight(sharedWidth, sharedHeight);
            Dimensions other = Dimensions.FromWidthAndHeight(sharedWidth, sharedHeight);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalWidthInSquaresValues_ReturnsFalse()
        {
            // Arrange
            const int sharedHeight = 5;

            Dimensions sut = Dimensions.FromWidthAndHeight(99, sharedHeight);
            Dimensions other = Dimensions.FromWidthAndHeight(1, sharedHeight);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalHeightInSquaresValues_ReturnsFalse()
        {
            // Arrange
            const int sharedWidth = 5;

            Dimensions sut = Dimensions.FromWidthAndHeight(sharedWidth, 99);
            Dimensions other = Dimensions.FromWidthAndHeight(sharedWidth, 1);

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
        public void Inequality_InstanceAndOtherHaveEqualWidthInSquaresAndHeightInSquaresValues_ReturnsFalse()
        {
            // Arrange
            const int sharedWidth = 5;
            const int sharedHeight = 6;

            Dimensions sut = Dimensions.FromWidthAndHeight(sharedWidth, sharedHeight);
            Dimensions other = Dimensions.FromWidthAndHeight(sharedWidth, sharedHeight);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalWidthInSquaresValues_ReturnsTrue()
        {
            // Arrange
            const int sharedHeight = 5;

            Dimensions sut = Dimensions.FromWidthAndHeight(99, sharedHeight);
            Dimensions other = Dimensions.FromWidthAndHeight(1, sharedHeight);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalHeightInSquaresValues_ReturnsTrue()
        {
            // Arrange
            const int sharedWidth = 5;

            Dimensions sut = Dimensions.FromWidthAndHeight(sharedWidth, 99);
            Dimensions other = Dimensions.FromWidthAndHeight(sharedWidth, 1);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class FromWidthAndHeightStaticFactoryMethod
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 3)]
        [InlineData(70, 30)]
        [InlineData(100, 100)]
        public void FromWidthAndHeight_GivenWidthAndHeight_ReturnsInstanceWithGivenWidthAndHeight(int width, int height)
        {
            // Act
            Dimensions result = Dimensions.FromWidthAndHeight(width, height);

            // Assert
            using (new AssertionScope())
            {
                result.WidthInSquares.Should().Be(width);
                result.HeightInSquares.Should().Be(height);
            }
        }

        [Fact]
        public void FromWidthAndHeight_WidthInSquaresArgIsLessThanOne_Throws()
        {
            // Arrange
            const int arbitraryHeight = 1;

            // Act
            Action act = () => Dimensions.FromWidthAndHeight(0, arbitraryHeight);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("widthInSquares ('0') must be greater than or equal to '1'. (Parameter 'widthInSquares')\n" +
                             "Actual value was 0.");
        }

        [Fact]
        public void FromWidthAndHeight_HeightInSquaresArgIsLessThanOne_Throws()
        {
            // Arrange
            const int arbitraryWidth = 1;

            // Act
            Action act = () => Dimensions.FromWidthAndHeight(arbitraryWidth, 0);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("heightInSquares ('0') must be greater than or equal to '1'. (Parameter 'heightInSquares')\n" +
                             "Actual value was 0.");
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 3)]
        [InlineData(100, 1)]
        public void CanSerializeToJson_ThenDeserializeToInstanceWithEqualValue(int width, int height)
        {
            // Arrange
            Dimensions originalDimensions = Dimensions.FromWidthAndHeight(width, height);

            string json = JsonSerializer.Serialize(originalDimensions, JsonSerializerOptions.Default);

            // Act
            Dimensions deserializedDimensions = JsonSerializer.Deserialize<Dimensions>(json, JsonSerializerOptions.Default);

            // Assert
            deserializedDimensions.Should().Be(originalDimensions);
        }
    }
}
