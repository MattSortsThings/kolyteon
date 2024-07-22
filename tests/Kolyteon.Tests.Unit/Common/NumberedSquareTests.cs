using System.Text.Json;
using Kolyteon.Common;

namespace Kolyteon.Tests.Unit.Common;

public static class NumberedSquareTests
{
    [UnitTest]
    public sealed class ParameterlessConstructor
    {
        [Fact]
        public void ParameterlessConstructor_Initializes_HasSquareWithColumnAndRowZeroAndNumberZero()
        {
            // Act
            NumberedSquare result = new();

            // Assert
            using (new AssertionScope())
            {
                result.Square.Column.Should().Be(0);
                result.Square.Row.Should().Be(0);
                result.Number.Should().Be(0);
            }
        }
    }

    [UnitTest]
    public sealed class CompareToMethod
    {
        [Fact]
        public void CompareTo_InstanceSquarePrecedesOther_ReturnsNegativeValue()
        {
            // Arrange
            const int sharedNumber = 5;

            NumberedSquare sut = Square.FromColumnAndRow(0, 0).ToNumberedSquare(sharedNumber);
            NumberedSquare other = Square.FromColumnAndRow(99, 99).ToNumberedSquare(sharedNumber);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveSameSquareAndInstanceNumberPrecedesOther_ReturnsNegativeValue()
        {
            // Arrange
            Square sharedSquare = Square.FromColumnAndRow(5, 5);

            NumberedSquare sut = sharedSquare.ToNumberedSquare(0);
            NumberedSquare other = sharedSquare.ToNumberedSquare(99);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveEqualSquareAndEqualNumberValues_ReturnsZero()
        {
            // Arrange
            Square sharedSquare = Square.FromColumnAndRow(5, 5);
            const int sharedNumber = 5;

            NumberedSquare sut = sharedSquare.ToNumberedSquare(sharedNumber);
            NumberedSquare other = sharedSquare.ToNumberedSquare(sharedNumber);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void CompareTo_InstanceSquareFollowsOther_ReturnsPositiveValue()
        {
            // Arrange
            const int sharedNumber = 5;

            NumberedSquare sut = Square.FromColumnAndRow(99, 99).ToNumberedSquare(sharedNumber);
            NumberedSquare other = Square.FromColumnAndRow(0, 0).ToNumberedSquare(sharedNumber);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveSameSquareAndInstanceNumberFollowsOther_ReturnsPositiveValue()
        {
            // Arrange
            Square sharedSquare = Square.FromColumnAndRow(5, 5);

            NumberedSquare sut = sharedSquare.ToNumberedSquare(99);
            NumberedSquare other = sharedSquare.ToNumberedSquare(0);

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
        public void Equals_InstanceAndOtherHaveEqualSquareAndEqualNumberValues_ReturnsTrue()
        {
            // Arrange
            Square sharedSquare = Square.FromColumnAndRow(5, 5);
            const int sharedNumber = 5;

            NumberedSquare sut = sharedSquare.ToNumberedSquare(sharedNumber);
            NumberedSquare other = sharedSquare.ToNumberedSquare(sharedNumber);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalSquareValues_ReturnsFalse()
        {
            // Arrange
            const int sharedNumber = 5;

            NumberedSquare sut = Square.FromColumnAndRow(99, 99).ToNumberedSquare(sharedNumber);
            NumberedSquare other = Square.FromColumnAndRow(0, 0).ToNumberedSquare(sharedNumber);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalNumberValues_ReturnsFalse()
        {
            // Arrange
            Square sharedSquare = Square.FromColumnAndRow(5, 5);

            NumberedSquare sut = sharedSquare.ToNumberedSquare(99);
            NumberedSquare other = sharedSquare.ToNumberedSquare(0);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ToStringMethod
    {
        public static TheoryData<Square, int, string> TestCases => new()
        {
            { Square.FromColumnAndRow(0, 0), 0, "(0,0) [0]" },
            { Square.FromColumnAndRow(1, 2), 3, "(1,2) [3]" },
            { Square.FromColumnAndRow(10, 17), 25, "(10,17) [25]" },
            { Square.FromColumnAndRow(100, 200), 300, "(100,200) [300]" }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(ToStringMethod))]
        public void ToString_ReturnsFormattedString(Square square, int number, string expected)
        {
            // Arrange
            NumberedSquare sut = square.ToNumberedSquare(number);

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
        public void Equality_InstanceAndOtherHaveEqualSquareAndEqualNumberValues_ReturnsTrue()
        {
            // Arrange
            Square sharedSquare = Square.FromColumnAndRow(5, 5);
            const int sharedNumber = 5;

            NumberedSquare sut = sharedSquare.ToNumberedSquare(sharedNumber);
            NumberedSquare other = sharedSquare.ToNumberedSquare(sharedNumber);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalSquareValues_ReturnsFalse()
        {
            // Arrange
            const int sharedNumber = 5;

            NumberedSquare sut = Square.FromColumnAndRow(99, 99).ToNumberedSquare(sharedNumber);
            NumberedSquare other = Square.FromColumnAndRow(0, 0).ToNumberedSquare(sharedNumber);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalNumberValues_ReturnsFalse()
        {
            // Arrange
            Square sharedSquare = Square.FromColumnAndRow(5, 5);

            NumberedSquare sut = sharedSquare.ToNumberedSquare(99);
            NumberedSquare other = sharedSquare.ToNumberedSquare(0);

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
        public void Inequality_InstanceAndOtherHaveEqualSquareAndEqualNumberValues_ReturnsFalse()
        {
            // Arrange
            Square sharedSquare = Square.FromColumnAndRow(5, 5);
            const int sharedNumber = 5;

            NumberedSquare sut = sharedSquare.ToNumberedSquare(sharedNumber);
            NumberedSquare other = sharedSquare.ToNumberedSquare(sharedNumber);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalSquareValues_ReturnsTrue()
        {
            // Arrange
            const int sharedNumber = 5;

            NumberedSquare sut = Square.FromColumnAndRow(99, 99).ToNumberedSquare(sharedNumber);
            NumberedSquare other = Square.FromColumnAndRow(0, 0).ToNumberedSquare(sharedNumber);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalNumberValues_ReturnsTrue()
        {
            // Arrange
            Square sharedSquare = Square.FromColumnAndRow(5, 5);

            NumberedSquare sut = sharedSquare.ToNumberedSquare(99);
            NumberedSquare other = sharedSquare.ToNumberedSquare(0);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        public static TheoryData<Square, int> TestCases => new()
        {
            { Square.FromColumnAndRow(0, 0), 0 },
            { Square.FromColumnAndRow(1, 2), 3 },
            { Square.FromColumnAndRow(10, 17), 25 },
            { Square.FromColumnAndRow(100, 200), 300 }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(Serialization))]
        public void CanSerializeToJson_ThenDeserializeToInstanceWithEqualValue(Square square, int number)
        {
            // Arrange
            NumberedSquare originalNumberedSquare = square.ToNumberedSquare(number);

            string json = JsonSerializer.Serialize(originalNumberedSquare, JsonSerializerOptions.Default);

            // Act
            NumberedSquare deserializedNumberedSquare =
                JsonSerializer.Deserialize<NumberedSquare>(json, JsonSerializerOptions.Default);

            // Assert
            deserializedNumberedSquare.Should().Be(originalNumberedSquare);
        }
    }
}
