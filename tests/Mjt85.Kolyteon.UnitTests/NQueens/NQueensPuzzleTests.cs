using System.Text.Json;
using Mjt85.Kolyteon.NQueens;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.NQueens;

/// <summary>
///     Unit tests for the <see cref="NQueensPuzzle" /> record type.
/// </summary>
public sealed class NQueensPuzzleTests
{
    [UnitTest]
    public sealed class Equals_Method
    {
        [Fact]
        public void InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            NQueensPuzzle sut = NQueensPuzzle.FromN(1);

            // Act
            var result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualNValues_ReturnsTrue()
        {
            // Arrange
            const int sharedN = 1;

            NQueensPuzzle sut = NQueensPuzzle.FromN(sharedN);
            NQueensPuzzle other = NQueensPuzzle.FromN(sharedN);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalNValues_ReturnsFalse()
        {
            // Arrange
            NQueensPuzzle sut = NQueensPuzzle.FromN(1);
            NQueensPuzzle other = NQueensPuzzle.FromN(99);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void OtherArgIsNull_ReturnsFalse()
        {
            // Arrange
            NQueensPuzzle sut = NQueensPuzzle.FromN(1);

            // Act
            var result = sut.Equals(null);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class FromN_StaticMethod
    {
        [Theory]
        [InlineData(1)]
        [InlineData(8)]
        public void ReturnsInstanceWithSpecifiedNValue(int n)
        {
            // Act
            NQueensPuzzle result = NQueensPuzzle.FromN(n);

            // Assert
            result.Should().BeOfType<NQueensPuzzle>().Which.N.Should().Be(n);
        }

        [Fact]
        public void NArgIsLessThanOne_Throws()
        {
            // Act
            Action act = () => NQueensPuzzle.FromN(0);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value of N must be greater than or equal to 1. (Parameter 'n')\nActual value was 0.");
        }
    }

    [UnitTest]
    public sealed class Equality_Operator
    {
        [Fact]
        public void InstanceAndOtherHaveEqualNValues_ReturnsTrue()
        {
            // Arrange
            const int sharedN = 1;

            NQueensPuzzle sut = NQueensPuzzle.FromN(sharedN);
            NQueensPuzzle other = NQueensPuzzle.FromN(sharedN);

            // Act
            var result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalNValues_ReturnsFalse()
        {
            // Arrange
            NQueensPuzzle sut = NQueensPuzzle.FromN(1);
            NQueensPuzzle other = NQueensPuzzle.FromN(99);

            // Act
            var result = sut == other;

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class Inequality_Operator
    {
        [Fact]
        public void InstanceAndOtherHaveEqualNValues_ReturnsFalse()
        {
            // Arrange
            const int sharedN = 1;

            NQueensPuzzle sut = NQueensPuzzle.FromN(sharedN);
            NQueensPuzzle other = NQueensPuzzle.FromN(sharedN);

            // Act
            var result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalNValues_ReturnsTrue()
        {
            // Arrange
            NQueensPuzzle sut = NQueensPuzzle.FromN(1);
            NQueensPuzzle other = NQueensPuzzle.FromN(99);

            // Act
            var result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        [Theory]
        [InlineData(8)]
        [InlineData(22)]
        public void CanSerializeToJson_ThenDeserialize(int n)
        {
            // Arrange
            NQueensPuzzle original = NQueensPuzzle.FromN(n);

            JsonSerializerOptions jsonOptions = Invariants.JsonSerializerOptions();

            // Act
            var json = JsonSerializer.Serialize(original, jsonOptions);
            var deserialized = JsonSerializer.Deserialize<NQueensPuzzle>(json, jsonOptions);

            // Assert
            deserialized.Should().NotBeNull().And.Be(original);
        }
    }
}
