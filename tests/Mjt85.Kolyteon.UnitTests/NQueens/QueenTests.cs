using System.Text.Json;
using FluentAssertions.Execution;
using Mjt85.Kolyteon.NQueens;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.NQueens;

/// <summary>
///     Unit tests for the <see cref="Queen" /> struct type.
/// </summary>
public sealed class QueenTests
{
    [UnitTest]
    public sealed class Constructor_TwoArgs
    {
        [Fact]
        public void Initializes_SetsColumnAndRowFromArgs()
        {
            // Arrange
            const int column = 2;
            const int row = 3;

            // Act
            Queen result = new(column, row);

            // Assert
            using (new AssertionScope())
            {
                result.Column.Should().Be(column);
                result.Row.Should().Be(row);
            }
        }

        [Fact]
        public void ColumnArgIsNegative_Throws()
        {
            // Arrange
            const int arbitraryRow = 0;

            // Act
            Action act = () => _ = new Queen(-1, arbitraryRow);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must not be negative. (Parameter 'column')\nActual value was -1.");
        }

        [Fact]
        public void RowArgIsNegative_Throws()
        {
            // Arrange
            const int arbitraryColumn = 0;

            // Act
            Action act = () => _ = new Queen(arbitraryColumn, -2);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must not be negative. (Parameter 'row')\nActual value was -2.");
        }
    }

    [UnitTest]
    public sealed class Constructor_ZeroArgs
    {
        [Fact]
        public void Initializes_ColumnIsZero_RowIsZero()
        {
            // Act
            Queen result = new();

            // Assert
            using (new AssertionScope())
            {
                result.Column.Should().Be(0);
                result.Row.Should().Be(0);
            }
        }
    }

    [UnitTest]
    public sealed class CompareTo_Method
    {
        [Fact]
        public void InstanceHasSmallerColumnValueThanOther_ReturnsNegativeValue()
        {
            // Arrange
            const int arbitraryRow = 0;
            Queen sut = new(5, arbitraryRow);
            Queen other = new(99, arbitraryRow);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualColumnValues_InstanceHasSmallerRowValueThanOther_ReturnsNegativeValue()
        {
            // Arrange
            const int sharedColumn = 5;
            Queen sut = new(sharedColumn, 5);
            Queen other = new(sharedColumn, 99);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualColumnValuesAndEqualRowValues_ReturnsZero()
        {
            // Arrange
            const int sharedColumn = 5;
            const int sharedRow = 5;
            Queen sut = new(sharedColumn, sharedRow);
            Queen other = new(sharedColumn, sharedRow);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void InstanceHasGreaterColumnValueThanOther_ReturnsPositiveValue()
        {
            // Arrange
            const int arbitraryRow = 0;
            Queen sut = new(5, arbitraryRow);
            Queen other = new(0, arbitraryRow);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualColumnValues_InstanceHasGreaterRowValueThanOther_ReturnsPositiveValue()
        {
            // Arrange
            const int sharedColumn = 5;
            Queen sut = new(sharedColumn, 5);
            Queen other = new(sharedColumn, 0);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }
    }

    [UnitTest]
    public sealed class Equals_Method
    {
        [Fact]
        public void InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            Queen sut = new();

            // Act
            var result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualColumnValuesAndEqualRowValues_ReturnsTrue()
        {
            // Arrange
            const int sharedColumn = 1;
            const int sharedRow = 2;
            Queen sut = new(sharedColumn, sharedRow);
            Queen other = new(sharedColumn, sharedRow);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalColumnValues_ReturnsFalse()
        {
            // Arrange
            const int sharedRow = 0;
            Queen sut = new(0, sharedRow);
            Queen other = new(99, sharedRow);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalRowValues_ReturnsFalse()
        {
            // Arrange
            const int sharedColumn = 0;
            Queen sut = new(sharedColumn, 0);
            Queen other = new(sharedColumn, 99);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class CanCapture_Method
    {
        [Fact]
        public void InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            Queen sut = new();

            // Act
            var result = sut.CanCapture(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [ClassData(typeof(SameColumnTestCases))]
        public void InstanceAndOtherAreOnSameColumn_ReturnsTrue(Queen sut, Queen other)
        {
            // Act
            var result = sut.CanCapture(other);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [ClassData(typeof(SameRowTestCases))]
        public void InstanceAndOtherAreOnSameRow_ReturnsTrue(Queen sut, Queen other)
        {
            // Act
            var result = sut.CanCapture(other);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [ClassData(typeof(SameDiagonalTestCases))]
        public void InstanceAndOtherAreOnSameDiagonal_ReturnsTrue(Queen sut, Queen other)
        {
            // Act
            var result = sut.CanCapture(other);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [ClassData(typeof(NegativeTestCases))]
        public void InstanceAndOtherAreOnDifferentColumnAndRowAndDiagonal_ReturnsFalse(Queen sut, Queen other)
        {
            // Act
            var result = sut.CanCapture(other);

            // Assert
            result.Should().BeFalse();
        }

        private sealed class SameColumnTestCases : TheoryData<Queen, Queen>
        {
            public SameColumnTestCases()
            {
                Add(new Queen(0, 2), new Queen(0, 1));
                Add(new Queen(0, 2), new Queen(0, 3));
                Add(new Queen(4, 2), new Queen(4, 0));
                Add(new Queen(4, 2), new Queen(4, 4));
            }
        }

        private sealed class SameRowTestCases : TheoryData<Queen, Queen>
        {
            public SameRowTestCases()
            {
                Add(new Queen(2, 0), new Queen(1, 0));
                Add(new Queen(2, 0), new Queen(3, 0));
                Add(new Queen(2, 4), new Queen(0, 4));
                Add(new Queen(2, 4), new Queen(4, 4));
            }
        }

        private sealed class SameDiagonalTestCases : TheoryData<Queen, Queen>
        {
            public SameDiagonalTestCases()
            {
                Add(new Queen(3, 3), new Queen(0, 0));
                Add(new Queen(3, 3), new Queen(1, 1));
                Add(new Queen(3, 3), new Queen(2, 2));
                Add(new Queen(3, 3), new Queen(2, 4));
                Add(new Queen(3, 3), new Queen(1, 5));
                Add(new Queen(3, 3), new Queen(0, 6));

                Add(new Queen(0, 0), new Queen(3, 3));
                Add(new Queen(1, 1), new Queen(3, 3));
                Add(new Queen(2, 2), new Queen(3, 3));
                Add(new Queen(2, 4), new Queen(3, 3));
                Add(new Queen(1, 5), new Queen(3, 3));
                Add(new Queen(0, 6), new Queen(3, 3));

                Add(new Queen(13, 1), new Queen(14, 2));
                Add(new Queen(13, 1), new Queen(12, 0));
            }
        }

        private sealed class NegativeTestCases : TheoryData<Queen, Queen>
        {
            public NegativeTestCases()
            {
                Add(new Queen(0, 0), new Queen(1, 2));
                Add(new Queen(0, 0), new Queen(1, 99));
                Add(new Queen(0, 0), new Queen(99, 1));
                Add(new Queen(0, 0), new Queen(99, 2));

                Add(new Queen(1, 2), new Queen(0, 0));
                Add(new Queen(1, 99), new Queen(0, 0));
                Add(new Queen(99, 1), new Queen(0, 0));
                Add(new Queen(99, 2), new Queen(0, 0));

                Add(new Queen(4, 2), new Queen(7, 8));
                Add(new Queen(9, 10), new Queen(3, 0));
            }
        }
    }


    [UnitTest]
    public sealed class ToString_Method
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ReturnsFormattedString(Queen sut, string expected)
        {
            // Act
            var result = sut.ToString();

            // Assert
            result.Should().Be(expected);
        }

        private sealed class TestCases : TheoryData<Queen, string>
        {
            public TestCases()
            {
                Add(new Queen(), "(0,0)");
                Add(new Queen(0, 0), "(0,0)");
                Add(new Queen(7, 2), "(7,2)");
                Add(new Queen(10, 20), "(10,20)");
            }
        }
    }

    [UnitTest]
    public sealed class Equality_Operator
    {
        [Fact]
        public void InstanceAndOtherHaveEqualColumnValuesAndEqualRowValues_ReturnsTrue()
        {
            // Arrange
            const int sharedColumn = 1;
            const int sharedRow = 2;
            Queen sut = new(sharedColumn, sharedRow);
            Queen other = new(sharedColumn, sharedRow);

            // Act
            var result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalColumnValues_ReturnsFalse()
        {
            // Arrange
            const int sharedRow = 0;
            Queen sut = new(0, sharedRow);
            Queen other = new(99, sharedRow);

            // Act
            var result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalRowValues_ReturnsFalse()
        {
            // Arrange
            const int sharedColumn = 0;
            Queen sut = new(sharedColumn, 0);
            Queen other = new(sharedColumn, 99);

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
        public void InstanceAndOtherHaveEqualColumnValuesAndEqualRowValues_ReturnsFalse()
        {
            // Arrange
            const int sharedColumn = 1;
            const int sharedRow = 2;
            Queen sut = new(sharedColumn, sharedRow);
            Queen other = new(sharedColumn, sharedRow);

            // Act
            var result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalColumnValues_ReturnsTrue()
        {
            // Arrange
            const int sharedRow = 0;
            Queen sut = new(0, sharedRow);
            Queen other = new(99, sharedRow);

            // Act
            var result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalRowValues_ReturnsTrue()
        {
            // Arrange
            const int sharedColumn = 0;
            Queen sut = new(sharedColumn, 0);
            Queen other = new(sharedColumn, 99);

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
        [InlineData(0, 0)]
        [InlineData(10, 0)]
        [InlineData(7, 3)]
        public void CanSerializeToJson_ThenDeserialize(int column, int row)
        {
            // Arrange
            Queen original = new(column, row);

            JsonSerializerOptions jsonOptions = Invariants.JsonSerializerOptions();

            // Act
            var json = JsonSerializer.Serialize(original, jsonOptions);
            var deserialized = JsonSerializer.Deserialize<Queen>(json, jsonOptions);

            // Assert
            deserialized.Should().NotBeNull().And.Be(original);
        }
    }
}
