using System.Text.Json;
using Kolyteon.Common;

namespace Kolyteon.Tests.Unit.Common;

public static class SquareTests
{
    [UnitTest]
    public sealed class ParameterlessConstructor
    {
        [Fact]
        public void ParameterlessConstructor_Initializes_HasColumnZeroAndRowZero()
        {
            // Act
            Square result = new();

            // Assert
            using (new AssertionScope())
            {
                result.Column.Should().Be(0);
                result.Row.Should().Be(0);
            }
        }
    }

    [UnitTest]
    public sealed class CompareToMethod
    {
        [Fact]
        public void CompareTo_InstanceColumnPrecedesOther_ReturnsNegativeValue()
        {
            // Arrange
            const int sharedRow = 5;

            Square sut = Square.FromColumnAndRow(0, sharedRow);
            Square other = Square.FromColumnAndRow(99, sharedRow);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveSameColumnAndInstanceRowPrecedesOther_ReturnsNegativeValue()
        {
            // Arrange
            const int sharedColumn = 5;

            Square sut = Square.FromColumnAndRow(sharedColumn, 0);
            Square other = Square.FromColumnAndRow(sharedColumn, 99);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveEqualColumnAndRowValues_ReturnsZero()
        {
            // Arrange
            const int sharedColumn = 5;
            const int sharedRow = 6;

            Square sut = Square.FromColumnAndRow(sharedColumn, sharedRow);
            Square other = Square.FromColumnAndRow(sharedColumn, sharedRow);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void CompareTo_InstanceColumnFollowsOther_ReturnsPositiveValue()
        {
            // Arrange
            const int sharedRow = 5;

            Square sut = Square.FromColumnAndRow(99, sharedRow);
            Square other = Square.FromColumnAndRow(0, sharedRow);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveSameColumnAndInstanceRowFollowsOther_ReturnsPositiveValue()
        {
            // Arrange
            const int sharedColumn = 5;

            Square sut = Square.FromColumnAndRow(sharedColumn, 99);
            Square other = Square.FromColumnAndRow(sharedColumn, 0);

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
        public void Equals_InstanceAndOtherHaveEqualColumnAndRowValues_ReturnsTrue()
        {
            // Arrange
            const int sharedColumn = 5;
            const int sharedRow = 6;

            Square sut = Square.FromColumnAndRow(sharedColumn, sharedRow);
            Square other = Square.FromColumnAndRow(sharedColumn, sharedRow);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalColumnValues_ReturnsFalse()
        {
            // Arrange
            const int sharedRow = 5;

            Square sut = Square.FromColumnAndRow(99, sharedRow);
            Square other = Square.FromColumnAndRow(0, sharedRow);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalRowValues_ReturnsFalse()
        {
            // Arrange
            const int sharedColumn = 5;

            Square sut = Square.FromColumnAndRow(sharedColumn, 99);
            Square other = Square.FromColumnAndRow(sharedColumn, 0);

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
        [InlineData(0, 0, "(0,0)")]
        [InlineData(7, 3, "(7,3)")]
        [InlineData(10, 300, "(10,300)")]
        public void ToString_ReturnsFormattedString(int column, int row, string expected)
        {
            // Arrange
            Square sut = Square.FromColumnAndRow(column, row);

            // Act
            string result = sut.ToString();

            // Assert
            result.Should().Be(expected);
        }
    }

    [UnitTest]
    public sealed class ToBlockMethod
    {
        [Fact]
        public void ToBlock_GivenDimensions_ReturnsBlockWithSelfAsOriginSquareAndGivenDimensions()
        {
            // Arrange
            Square sut = Square.FromColumnAndRow(3, 10);

            Dimensions dimensions = Dimensions.FromWidthAndHeight(5, 4);

            // Act
            Block result = sut.ToBlock(dimensions);

            // Assert
            using (new AssertionScope())
            {
                result.OriginSquare.Should().Be(sut);
                result.Dimensions.Should().Be(dimensions);
            }
        }
    }

    [UnitTest]
    public sealed class ToNumberedSquareMethod
    {
        [Fact]
        public void ToNumberedSquare_GivenNumber_ReturnsNumberedSquareWithSelfAsSquareAndGivenNumber()
        {
            // Arrange
            Square sut = Square.FromColumnAndRow(3, 2);
            const int number = 4;

            // Act
            NumberedSquare result = sut.ToNumberedSquare(number);

            // Assert
            using (new AssertionScope())
            {
                result.Square.Should().Be(sut);
                result.Number.Should().Be(number);
            }
        }
    }

    [UnitTest]
    public sealed class EqualityOperator
    {
        [Fact]
        public void Equality_InstanceAndOtherHaveEqualColumnAndRowValues_ReturnsTrue()
        {
            // Arrange
            const int sharedColumn = 5;
            const int sharedRow = 6;

            Square sut = Square.FromColumnAndRow(sharedColumn, sharedRow);
            Square other = Square.FromColumnAndRow(sharedColumn, sharedRow);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalColumnValues_ReturnsFalse()
        {
            // Arrange
            const int sharedRow = 5;

            Square sut = Square.FromColumnAndRow(99, sharedRow);
            Square other = Square.FromColumnAndRow(0, sharedRow);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalRowValues_ReturnsFalse()
        {
            // Arrange
            const int sharedColumn = 5;

            Square sut = Square.FromColumnAndRow(sharedColumn, 99);
            Square other = Square.FromColumnAndRow(sharedColumn, 0);

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
        public void Inequality_InstanceAndOtherHaveEqualColumnAndRowValues_ReturnsFalse()
        {
            // Arrange
            const int sharedColumn = 5;
            const int sharedRow = 6;

            Square sut = Square.FromColumnAndRow(sharedColumn, sharedRow);
            Square other = Square.FromColumnAndRow(sharedColumn, sharedRow);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalColumnValues_ReturnsTrue()
        {
            // Arrange
            const int sharedRow = 5;

            Square sut = Square.FromColumnAndRow(99, sharedRow);
            Square other = Square.FromColumnAndRow(0, sharedRow);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalRowValues_ReturnsTrue()
        {
            // Arrange
            const int sharedColumn = 5;

            Square sut = Square.FromColumnAndRow(sharedColumn, 99);
            Square other = Square.FromColumnAndRow(sharedColumn, 0);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class FromColumnAndRowStaticFactoryMethod
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(7, 3)]
        [InlineData(10, 300)]
        [InlineData(1, 0)]
        public void FromColumnAndRow_GivenColumnAndRow_ReturnsInstanceWithColumnAndRow(int column, int row)
        {
            // Act
            Square result = Square.FromColumnAndRow(column, row);

            // Assert
            using (new AssertionScope())
            {
                result.Column.Should().Be(column);
                result.Row.Should().Be(row);
            }
        }

        [Fact]
        public void FromColumnAndRow_ColumnArgIsLessThanZero_Throws()
        {
            // Arrange
            const int arbitraryRow = 0;

            // Act
            Action act = () => Square.FromColumnAndRow(-1, arbitraryRow);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("column ('-1') must be greater than or equal to '0'. (Parameter 'column')\n" +
                             "Actual value was -1.");
        }

        [Fact]
        public void FromColumnAndRow_RowArgIsLessThanZero_Throws()
        {
            // Arrange
            const int arbitraryColumn = 0;

            // Act
            Action act = () => Square.FromColumnAndRow(arbitraryColumn, -1);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("row ('-1') must be greater than or equal to '0'. (Parameter 'row')\n" +
                             "Actual value was -1.");
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(7, 3)]
        [InlineData(10, 300)]
        [InlineData(1, 0)]
        public void CanSerializeToJson_ThenDeserializeToInstanceWithEqualValue(int column, int row)
        {
            // Arrange
            Square originalSquare = Square.FromColumnAndRow(column, row);

            string json = JsonSerializer.Serialize(originalSquare, JsonSerializerOptions.Default);

            // Act
            Square result = JsonSerializer.Deserialize<Square>(json, JsonSerializerOptions.Default);

            // Assert
            result.Should().Be(originalSquare);
        }
    }
}
