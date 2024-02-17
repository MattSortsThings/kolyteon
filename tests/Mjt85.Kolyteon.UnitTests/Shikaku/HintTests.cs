using System.Text.Json;
using FluentAssertions.Execution;
using Mjt85.Kolyteon.Shikaku;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.Shikaku;

/// <summary>
///     Unit tests for the <see cref="Hint" /> struct type.
/// </summary>
public sealed class HintTests
{
    [UnitTest]
    public sealed class Constructor_ThreeArgs
    {
        [Fact]
        public void Initializes_SetsColumnAndRowAndNumberFromArgs()
        {
            // Arrange
            const int column = 2;
            const int row = 3;
            const int number = 10;

            // Act
            Hint result = new(column, row, number);

            // Assert
            using (new AssertionScope())
            {
                result.Column.Should().Be(column);
                result.Row.Should().Be(row);
                result.Number.Should().Be(number);
            }
        }

        [Fact]
        public void ColumnArgIsNegative_Throws()
        {
            // Arrange
            const int arbitraryRow = 0;
            const int arbitraryNumber = 2;

            // Act
            Action act = () => _ = new Hint(-1, arbitraryRow, arbitraryNumber);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must not be negative. (Parameter 'column')\nActual value was -1.");
        }

        [Fact]
        public void RowArgIsNegative_Throws()
        {
            // Arrange
            const int arbitraryColumn = 0;
            const int arbitraryNumber = 2;

            // Act
            Action act = () => _ = new Hint(arbitraryColumn, -2, arbitraryNumber);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must not be negative. (Parameter 'row')\nActual value was -2.");
        }

        [Theory]
        [InlineData(1, "Actual value was 1.")]
        [InlineData(0, "Actual value was 0.")]
        [InlineData(-1, "Actual value was -1.")]
        public void NumberArgIsLessThanTwo_Throws(int number, string expectedSuffix)
        {
            // Arrange
            const int arbitraryColumn = 0;
            const int arbitraryRow = 0;

            // Act
            Action act = () => _ = new Hint(arbitraryColumn, arbitraryRow, number);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must not be less than 2. (Parameter 'number')\n" + expectedSuffix);
        }
    }

    [UnitTest]
    public sealed class Constructor_ZeroArgs
    {
        [Fact]
        public void Initializes_ColumnIsZero_RowsIsZero_NumberIsTwo()
        {
            // Act
            Hint result = new();

            // Assert
            using (new AssertionScope())
            {
                result.Column.Should().Be(0);
                result.Row.Should().Be(0);
                result.Number.Should().Be(2);
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
            const int arbitraryRow = 5;
            const int arbitraryNumber = 5;

            Hint sut = new(0, arbitraryRow, arbitraryNumber);
            Hint other = new(99, arbitraryRow, arbitraryNumber);

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
            const int arbitraryNumber = 5;

            Hint sut = new(sharedColumn, 0, arbitraryNumber);
            Hint other = new(sharedColumn, 99, arbitraryNumber);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualColumnValuesAndEqualRowValues_InstanceHasSmallerNumberValue_ReturnsNegativeValue()
        {
            // Arrange
            const int sharedColumn = 5;
            const int sharedRow = 5;

            Hint sut = new(sharedColumn, sharedRow, 2);
            Hint other = new(sharedColumn, sharedRow, 99);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualColumnValuesAndEqualRowValuesAndEqualNumberValues_ReturnsZero()
        {
            // Arrange
            const int sharedColumn = 5;
            const int sharedRow = 5;
            const int sharedNumber = 5;

            Hint sut = new(sharedColumn, sharedRow, sharedNumber);
            Hint other = new(sharedColumn, sharedRow, sharedNumber);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void InstanceHasGreaterColumnValueThanOther_ReturnsPositiveValue()
        {
            // Arrange
            const int arbitraryRow = 5;
            const int arbitraryNumber = 5;

            Hint sut = new(99, arbitraryRow, arbitraryNumber);
            Hint other = new(0, arbitraryRow, arbitraryNumber);

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
            const int arbitraryNumber = 5;

            Hint sut = new(sharedColumn, 99, arbitraryNumber);
            Hint other = new(sharedColumn, 0, arbitraryNumber);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualColumnValuesAndEqualRowValues_InstanceHasGreaterNumberValue_ReturnsPositiveValue()
        {
            // Arrange
            const int sharedColumn = 5;
            const int sharedRow = 5;

            Hint sut = new(sharedColumn, sharedRow, 99);
            Hint other = new(sharedColumn, sharedRow, 2);

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
            Hint sut = new();

            // Act
            var result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualColumnValuesAndEqualRowValuesAndEqualNumberValues_ReturnsTrue()
        {
            // Arrange
            const int sharedColumn = 1;
            const int sharedRow = 2;
            const int sharedNumber = 9;

            Hint sut = new(sharedColumn, sharedRow, sharedNumber);
            Hint other = new(sharedColumn, sharedRow, sharedNumber);

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
            const int sharedNumber = 2;

            Hint sut = new(0, sharedRow, sharedNumber);
            Hint other = new(99, sharedRow, sharedNumber);

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
            const int sharedNumber = 2;

            Hint sut = new(sharedColumn, 0, sharedNumber);
            Hint other = new(sharedColumn, 99, sharedNumber);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalNumberValues_ReturnsFalse()
        {
            // Arrange
            const int sharedColumn = 0;
            const int sharedRow = 0;

            Hint sut = new(sharedColumn, sharedRow, 2);
            Hint other = new(sharedColumn, sharedRow, 99);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ToString_Method
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ReturnsFormattedString(Hint sut, string expected)
        {
            // Act
            var result = sut.ToString();

            // Assert
            result.Should().Be(expected);
        }

        private sealed class TestCases : TheoryData<Hint, string>
        {
            public TestCases()
            {
                Add(new Hint(), "(0,0) [2]");
                Add(new Hint(0, 0, 2), "(0,0) [2]");
                Add(new Hint(7, 2, 10), "(7,2) [10]");
                Add(new Hint(10, 20, 3), "(10,20) [3]");
            }
        }
    }

    [UnitTest]
    public sealed class Equality_Operator
    {
        [Fact]
        public void InstanceAndOtherHaveEqualColumnValuesAndEqualRowValuesAndEqualNumberValues_ReturnsTrue()
        {
            // Arrange
            const int sharedColumn = 1;
            const int sharedRow = 2;
            const int sharedNumber = 9;

            Hint sut = new(sharedColumn, sharedRow, sharedNumber);
            Hint other = new(sharedColumn, sharedRow, sharedNumber);

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
            const int sharedNumber = 2;

            Hint sut = new(0, sharedRow, sharedNumber);
            Hint other = new(99, sharedRow, sharedNumber);

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
            const int sharedNumber = 2;

            Hint sut = new(sharedColumn, 0, sharedNumber);
            Hint other = new(sharedColumn, 99, sharedNumber);

            // Act
            var result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalNumberValues_ReturnsFalse()
        {
            // Arrange
            const int sharedColumn = 0;
            const int sharedRow = 0;

            Hint sut = new(sharedColumn, sharedRow, 2);
            Hint other = new(sharedColumn, sharedRow, 99);

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
        public void InstanceAndOtherHaveEqualColumnValuesAndEqualRowValuesAndEqualNumberValues_ReturnsFalse()
        {
            // Arrange
            const int sharedColumn = 1;
            const int sharedRow = 2;
            const int sharedNumber = 9;

            Hint sut = new(sharedColumn, sharedRow, sharedNumber);
            Hint other = new(sharedColumn, sharedRow, sharedNumber);

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
            const int sharedNumber = 2;

            Hint sut = new(0, sharedRow, sharedNumber);
            Hint other = new(99, sharedRow, sharedNumber);

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
            const int sharedNumber = 2;

            Hint sut = new(sharedColumn, 0, sharedNumber);
            Hint other = new(sharedColumn, 99, sharedNumber);

            // Act
            var result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalNumberValues_ReturnsTrue()
        {
            // Arrange
            const int sharedColumn = 0;
            const int sharedRow = 0;

            Hint sut = new(sharedColumn, sharedRow, 2);
            Hint other = new(sharedColumn, sharedRow, 99);

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
        [InlineData(0, 0, 2)]
        [InlineData(10, 0, 7)]
        [InlineData(7, 3, 15)]
        public void CanSerializeToJson_ThenDeserialize(int column, int row, int number)
        {
            // Arrange
            Hint original = new(column, row, number);

            JsonSerializerOptions jsonOptions = Invariants.GetJsonSerializerOptions();

            // Act
            var json = JsonSerializer.Serialize(original, jsonOptions);
            var deserialized = JsonSerializer.Deserialize<Hint>(json, jsonOptions);

            // Assert
            deserialized.Should().NotBeNull().And.Be(original);
        }
    }
}
