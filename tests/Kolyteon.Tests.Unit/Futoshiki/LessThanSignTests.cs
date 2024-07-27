using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.Futoshiki;

namespace Kolyteon.Tests.Unit.Futoshiki;

public static class LessThanSignTests
{
    [UnitTest]
    public sealed class CompareToMethod
    {
        [Fact]
        public void CompareTo_InstanceFirstSquarePrecedesOther_ReturnsNegativeValue()
        {
            // Arrange
            Square sharedSecondSquare = Square.FromColumnAndRow(1, 1);

            LessThanSign sut = LessThanSign.Between(Square.FromColumnAndRow(0, 1), sharedSecondSquare);
            LessThanSign other = LessThanSign.Between(Square.FromColumnAndRow(1, 0), sharedSecondSquare);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveEqualFirstSquareAndInstanceSecondSquarePrecedesOther_ReturnsNegativeValue()
        {
            // Arrange
            Square sharedFirstSquare = Square.FromColumnAndRow(0, 0);

            LessThanSign sut = LessThanSign.Between(sharedFirstSquare, Square.FromColumnAndRow(0, 1));
            LessThanSign other = LessThanSign.Between(sharedFirstSquare, Square.FromColumnAndRow(1, 0));

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveEqualFirstSquareAndSecondSquareValues_ReturnsZero()
        {
            // Arrange
            Square sharedFirstSquare = Square.FromColumnAndRow(0, 0);
            Square sharedSecondSquare = Square.FromColumnAndRow(1, 0);

            LessThanSign sut = LessThanSign.Between(sharedFirstSquare, sharedSecondSquare);
            LessThanSign other = LessThanSign.Between(sharedFirstSquare, sharedSecondSquare);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void CompareTo_InstanceFirstSquareFollowsOther_ReturnsPositiveValue()
        {
            // Arrange
            Square sharedSecondSquare = Square.FromColumnAndRow(1, 1);

            LessThanSign sut = LessThanSign.Between(Square.FromColumnAndRow(1, 0), sharedSecondSquare);
            LessThanSign other = LessThanSign.Between(Square.FromColumnAndRow(0, 1), sharedSecondSquare);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveEqualFirstSquareAndInstanceSecondSquareFollowsOther_ReturnsPositiveValue()
        {
            // Arrange
            Square sharedFirstSquare = Square.FromColumnAndRow(0, 0);

            LessThanSign sut = LessThanSign.Between(sharedFirstSquare, Square.FromColumnAndRow(1, 0));
            LessThanSign other = LessThanSign.Between(sharedFirstSquare, Square.FromColumnAndRow(0, 1));

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
        public void Equals_InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            LessThanSign sut = LessThanSign.Between(Square.FromColumnAndRow(0, 0), Square.FromColumnAndRow(0, 1));

            // Act
            bool result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveEqualFirstSquareAndSecondSquareValues_ReturnsTrue()
        {
            // Arrange
            Square sharedFirstSquare = Square.FromColumnAndRow(0, 0);
            Square sharedSecondSquare = Square.FromColumnAndRow(1, 0);

            LessThanSign sut = LessThanSign.Between(sharedFirstSquare, sharedSecondSquare);
            LessThanSign other = LessThanSign.Between(sharedFirstSquare, sharedSecondSquare);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalFirstSquareValues_ReturnsFalse()
        {
            // Arrange
            Square sharedSecondSquare = Square.FromColumnAndRow(1, 1);

            LessThanSign sut = LessThanSign.Between(Square.FromColumnAndRow(1, 0), sharedSecondSquare);
            LessThanSign other = LessThanSign.Between(Square.FromColumnAndRow(0, 1), sharedSecondSquare);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalSecondSquareValues_ReturnsFalse()
        {
            // Arrange
            Square sharedFirstSquare = Square.FromColumnAndRow(0, 0);

            LessThanSign sut = LessThanSign.Between(sharedFirstSquare, Square.FromColumnAndRow(1, 0));
            LessThanSign other = LessThanSign.Between(sharedFirstSquare, Square.FromColumnAndRow(0, 1));

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_OtherArgIsNull_ReturnsFalse()
        {
            // Arrange
            LessThanSign sut = LessThanSign.Between(Square.FromColumnAndRow(0, 0), Square.FromColumnAndRow(0, 1));

            // Act
            bool result = sut.Equals(null);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ToStringMethod
    {
        public static TheoryData<Square, Square, string> TestCases => new()
        {
            { Square.FromColumnAndRow(0, 0), Square.FromColumnAndRow(0, 1), "(0,0)<(0,1)" },
            { Square.FromColumnAndRow(10, 22), Square.FromColumnAndRow(11, 22), "(10,22)<(11,22)" },
            { Square.FromColumnAndRow(5, 4), Square.FromColumnAndRow(5, 5), "(5,4)<(5,5)" }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(ToStringMethod))]
        public void ToString_ReturnsFormattedString(Square firstSquare, Square secondSquare, string expected)
        {
            // Arrange
            LessThanSign sut = LessThanSign.Between(firstSquare, secondSquare);

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
        public void Equality_InstanceAndOtherHaveEqualFirstSquareAndSecondSquareValues_ReturnsTrue()
        {
            // Arrange
            Square sharedFirstSquare = Square.FromColumnAndRow(0, 0);
            Square sharedSecondSquare = Square.FromColumnAndRow(1, 0);

            LessThanSign sut = LessThanSign.Between(sharedFirstSquare, sharedSecondSquare);
            LessThanSign other = LessThanSign.Between(sharedFirstSquare, sharedSecondSquare);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalFirstSquareValues_ReturnsFalse()
        {
            // Arrange
            Square sharedSecondSquare = Square.FromColumnAndRow(1, 1);

            LessThanSign sut = LessThanSign.Between(Square.FromColumnAndRow(1, 0), sharedSecondSquare);
            LessThanSign other = LessThanSign.Between(Square.FromColumnAndRow(0, 1), sharedSecondSquare);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalSecondSquareValues_ReturnsFalse()
        {
            // Arrange
            Square sharedFirstSquare = Square.FromColumnAndRow(0, 0);

            LessThanSign sut = LessThanSign.Between(sharedFirstSquare, Square.FromColumnAndRow(1, 0));
            LessThanSign other = LessThanSign.Between(sharedFirstSquare, Square.FromColumnAndRow(0, 1));

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
        public void Inequality_InstanceAndOtherHaveEqualFirstSquareAndSecondSquareValues_ReturnsFalse()
        {
            // Arrange
            Square sharedFirstSquare = Square.FromColumnAndRow(0, 0);
            Square sharedSecondSquare = Square.FromColumnAndRow(1, 0);

            LessThanSign sut = LessThanSign.Between(sharedFirstSquare, sharedSecondSquare);
            LessThanSign other = LessThanSign.Between(sharedFirstSquare, sharedSecondSquare);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalFirstSquareValues_ReturnsTrue()
        {
            // Arrange
            Square sharedSecondSquare = Square.FromColumnAndRow(1, 1);

            LessThanSign sut = LessThanSign.Between(Square.FromColumnAndRow(1, 0), sharedSecondSquare);
            LessThanSign other = LessThanSign.Between(Square.FromColumnAndRow(0, 1), sharedSecondSquare);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalSecondSquareValues_ReturnsTrue()
        {
            // Arrange
            Square sharedFirstSquare = Square.FromColumnAndRow(0, 0);

            LessThanSign sut = LessThanSign.Between(sharedFirstSquare, Square.FromColumnAndRow(1, 0));
            LessThanSign other = LessThanSign.Between(sharedFirstSquare, Square.FromColumnAndRow(0, 1));

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class BetweenStaticFactoryMethod
    {
        [Fact]
        public void Between_SquareAArgPrecedesSquareBArg_ReturnsInstanceWithSquareAAsFirstSquareAndSquareBAsSecondSquare()
        {
            // Arrange
            Square squareA = Square.FromColumnAndRow(0, 0);
            Square squareB = Square.FromColumnAndRow(0, 1);

            // Act
            LessThanSign result = LessThanSign.Between(squareA, squareB);

            // Assert
            using (new AssertionScope())
            {
                result.FirstSquare.Should().Be(squareA);
                result.SecondSquare.Should().Be(squareB);
            }
        }

        [Fact]
        public void Between_SquareAArgFollowsSquareBArg_ReturnsInstanceWithSquareBAsFirstSquareAndSquareAAsSecondSquare()
        {
            // Arrange
            Square squareA = Square.FromColumnAndRow(0, 1);
            Square squareB = Square.FromColumnAndRow(0, 0);

            // Act
            LessThanSign result = LessThanSign.Between(squareA, squareB);

            // Assert
            using (new AssertionScope())
            {
                result.FirstSquare.Should().Be(squareB);
                result.SecondSquare.Should().Be(squareA);
            }
        }

        [Fact]
        public void Between_SquareAAndSquareBArgsAreEqual_Throws()
        {
            // Arrange
            Square square = Square.FromColumnAndRow(0, 0);

            // Act
            Action act = () => LessThanSign.Between(square, square);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Squares must be adjacent to each other.");
        }

        [Fact]
        public void Between_SquareAAndSquareBArgsAreNotAdjacentSquares_Throws()
        {
            // Arrange
            Square squareA = Square.FromColumnAndRow(0, 0);
            Square squareB = Square.FromColumnAndRow(99, 99);

            // Act
            Action act = () => LessThanSign.Between(squareA, squareB);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Squares must be adjacent to each other.");
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        public static TheoryData<Square, Square> TestCases => new()
        {
            { Square.FromColumnAndRow(0, 0), Square.FromColumnAndRow(0, 1) },
            { Square.FromColumnAndRow(10, 22), Square.FromColumnAndRow(11, 22) },
            { Square.FromColumnAndRow(5, 4), Square.FromColumnAndRow(5, 5) }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(Serialization))]
        public void CanSerializeToJson_ThenDeserializeToInstanceWithEqualValue(Square firstSquare, Square secondSquare)
        {
            // Arrange
            LessThanSign originalSign = LessThanSign.Between(firstSquare, secondSquare);

            string json = JsonSerializer.Serialize(originalSign, JsonSerializerOptions.Default);

            // Act
            LessThanSign? deserializedSign = JsonSerializer.Deserialize<LessThanSign>(json, JsonSerializerOptions.Default);

            // Assert
            deserializedSign.Should().NotBeNull().And.Be(originalSign);
        }
    }
}
