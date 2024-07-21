using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.NQueens;

namespace Kolyteon.Tests.Unit.NQueens;

public static class NQueensProblemTests
{
    [UnitTest]
    public sealed class EqualsMethod
    {
        [Fact]
        public void Equals_InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            NQueensProblem sut = NQueensProblem.FromN(5);

            // Act
            bool result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveEqualQueensValues_ReturnsTrue()
        {
            // Arrange
            const int sharedN = 5;

            NQueensProblem sut = NQueensProblem.FromN(sharedN);
            NQueensProblem other = NQueensProblem.FromN(sharedN);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalQueensValues_ReturnsFalse()
        {
            // Arrange
            NQueensProblem sut = NQueensProblem.FromN(1);
            NQueensProblem other = NQueensProblem.FromN(99);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_OtherArgIsNull_ReturnsFalse()
        {
            // Arrange
            NQueensProblem sut = NQueensProblem.FromN(5);

            // Act
            bool result = sut.Equals(null);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class EqualityOperator
    {
        [Fact]
        public void Equality_InstanceAndOtherHaveEqualQueensValues_ReturnsTrue()
        {
            // Arrange
            const int sharedN = 5;

            NQueensProblem sut = NQueensProblem.FromN(sharedN);
            NQueensProblem other = NQueensProblem.FromN(sharedN);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalQueensValues_ReturnsFalse()
        {
            // Arrange
            NQueensProblem sut = NQueensProblem.FromN(1);
            NQueensProblem other = NQueensProblem.FromN(99);

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
        public void Inequality_InstanceAndOtherHaveEqualQueensValues_ReturnsFalse()
        {
            // Arrange
            const int sharedN = 5;

            NQueensProblem sut = NQueensProblem.FromN(sharedN);
            NQueensProblem other = NQueensProblem.FromN(sharedN);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalQueensValues_ReturnsTrue()
        {
            // Arrange
            NQueensProblem sut = NQueensProblem.FromN(1);
            NQueensProblem other = NQueensProblem.FromN(99);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class FromNStaticFactoryMethod
    {
        public static TheoryData<int, Block, int> TestCases => new()
        {
            { 1, Dimensions.FromWidthAndHeight(1, 1).ToBlock(), 1 },
            { 4, Dimensions.FromWidthAndHeight(4, 4).ToBlock(), 4 },
            { 100, Dimensions.FromWidthAndHeight(100, 100).ToBlock(), 100 }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(FromNStaticFactoryMethod))]
        public void FromN_GivenN_ReturnsInstanceWithNByNChessBoardAndNQueens(int n, Block expectedChessBoard, int expectedQueens)
        {
            // Act
            NQueensProblem result = NQueensProblem.FromN(n);

            // Assert
            using (new AssertionScope())
            {
                result.ChessBoard.Should().Be(expectedChessBoard);
                result.Queens.Should().Be(expectedQueens);
            }
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(100)]
        public void CanSerializeToJson_ThenDeserializeToInstanceWithEqualValue(int n)
        {
            // Arrange
            NQueensProblem originalProblem = NQueensProblem.FromN(n);

            string json = JsonSerializer.Serialize(originalProblem, JsonSerializerOptions.Default);

            // Act
            NQueensProblem? deserializedProblem =
                JsonSerializer.Deserialize<NQueensProblem>(json, JsonSerializerOptions.Default);

            // Assert
            deserializedProblem.Should().NotBeNull().And.Be(originalProblem);
        }
    }
}
