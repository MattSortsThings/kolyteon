using System.Text.Json;
using FluentAssertions.Execution;
using Mjt85.Kolyteon.Sudoku;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.Sudoku;

/// <summary>
///     Unit tests for the <see cref="FilledCell" /> struct type.
/// </summary>
public sealed class FilledCellTests
{
    [UnitTest]
    public sealed class Constructor_ThreeArgs
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void Initializes_SetsColumnAndRowAndNumberFromArgs_CalculatesSector(int column, int row, int expectedSector)
        {
            // Arrange
            const int number = 5;

            // Act
            var result = new FilledCell(column, row, number);

            // Assert
            using (new AssertionScope())
            {
                result.Column.Should().Be(column);
                result.Row.Should().Be(row);
                result.Sector.Should().Be(expectedSector);
                result.Number.Should().Be(number);
            }
        }

        [Fact]
        public void ColumnArgIsNegative_Throws()
        {
            // Arrange
            const int arbitraryRow = 0;
            const int arbitraryNumber = 1;

            // Act
            Action act = () => _ = new FilledCell(-1, arbitraryRow, arbitraryNumber);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must be non-negative and less than 9. (Parameter 'column')\nActual value was -1.");
        }

        [Fact]
        public void ColumnArgIsGreaterThanEight_Throws()
        {
            // Arrange
            const int arbitraryRow = 0;
            const int arbitraryNumber = 1;

            // Act
            Action act = () => _ = new FilledCell(9, arbitraryRow, arbitraryNumber);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must be non-negative and less than 9. (Parameter 'column')\nActual value was 9.");
        }

        [Fact]
        public void RowArgIsNegative_Throws()
        {
            // Arrange
            const int arbitraryColumn = 0;
            const int arbitraryNumber = 1;

            // Act
            Action act = () => _ = new FilledCell(arbitraryColumn, -1, arbitraryNumber);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must be non-negative and less than 9. (Parameter 'row')\nActual value was -1.");
        }

        [Fact]
        public void RowArgIsGreaterThanEight_Throws()
        {
            // Arrange
            const int arbitraryColumn = 0;
            const int arbitraryNumber = 1;

            // Act
            Action act = () => _ = new FilledCell(arbitraryColumn, 9, arbitraryNumber);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must be non-negative and less than 9. (Parameter 'row')\nActual value was 9.");
        }

        [Fact]
        public void NumberArgIsLessThanOne_Throws()
        {
            // Arrange
            const int arbitraryColumn = 0;
            const int arbitraryRow = 0;

            // Act
            Action act = () => _ = new FilledCell(arbitraryColumn, arbitraryRow, 0);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must be greater than or equal to 1 and less than or equal to 9. (Parameter 'number')\n" +
                             "Actual value was 0.");
        }

        [Fact]
        public void NumberArgIsGreaterThanNine_Throws()
        {
            // Arrange
            const int arbitraryColumn = 0;
            const int arbitraryRow = 0;

            // Act
            Action act = () => _ = new FilledCell(arbitraryColumn, arbitraryRow, 10);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must be greater than or equal to 1 and less than or equal to 9. (Parameter 'number')\n" +
                             "Actual value was 10.");
        }

        [UnitTest]
        public sealed class Constructor_ZeroArgs
        {
            [Fact]
            public void Initializes_ColumnIsZero_RowIsZero_SectorIsZero_NumberIsOne()
            {
                // Act
                FilledCell result = new();

                // Assert
                using (new AssertionScope())
                {
                    result.Column.Should().Be(0);
                    result.Row.Should().Be(0);
                    result.Sector.Should().Be(0);
                    result.Number.Should().Be(1);
                }
            }
        }

        private sealed class TestCases : TheoryData<int, int, int>
        {
            public TestCases()
            {
                Add(0, 0, 0);
                Add(0, 1, 0);
                Add(0, 2, 0);
                Add(0, 3, 1);
                Add(0, 4, 1);
                Add(0, 5, 1);
                Add(0, 6, 2);
                Add(0, 7, 2);
                Add(0, 8, 2);
                Add(1, 0, 0);
                Add(1, 1, 0);
                Add(1, 2, 0);
                Add(1, 3, 1);
                Add(1, 4, 1);
                Add(1, 5, 1);
                Add(1, 6, 2);
                Add(1, 7, 2);
                Add(1, 8, 2);
                Add(2, 0, 0);
                Add(2, 1, 0);
                Add(2, 2, 0);
                Add(2, 3, 1);
                Add(2, 4, 1);
                Add(2, 5, 1);
                Add(2, 6, 2);
                Add(2, 7, 2);
                Add(2, 8, 2);
                Add(3, 0, 3);
                Add(3, 1, 3);
                Add(3, 2, 3);
                Add(3, 3, 4);
                Add(3, 4, 4);
                Add(3, 5, 4);
                Add(3, 6, 5);
                Add(3, 7, 5);
                Add(3, 8, 5);
                Add(4, 0, 3);
                Add(4, 1, 3);
                Add(4, 2, 3);
                Add(4, 3, 4);
                Add(4, 4, 4);
                Add(4, 5, 4);
                Add(4, 6, 5);
                Add(4, 7, 5);
                Add(4, 8, 5);
                Add(5, 0, 3);
                Add(5, 1, 3);
                Add(5, 2, 3);
                Add(5, 3, 4);
                Add(5, 4, 4);
                Add(5, 5, 4);
                Add(5, 6, 5);
                Add(5, 7, 5);
                Add(5, 8, 5);
                Add(6, 0, 6);
                Add(6, 1, 6);
                Add(6, 2, 6);
                Add(6, 3, 7);
                Add(6, 4, 7);
                Add(6, 5, 7);
                Add(6, 6, 8);
                Add(6, 7, 8);
                Add(6, 8, 8);
                Add(7, 0, 6);
                Add(7, 1, 6);
                Add(7, 2, 6);
                Add(7, 3, 7);
                Add(7, 4, 7);
                Add(7, 5, 7);
                Add(7, 6, 8);
                Add(7, 7, 8);
                Add(7, 8, 8);
                Add(8, 0, 6);
                Add(8, 1, 6);
                Add(8, 2, 6);
                Add(8, 3, 7);
                Add(8, 4, 7);
                Add(8, 5, 7);
                Add(8, 6, 8);
                Add(8, 7, 8);
                Add(8, 8, 8);
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

            FilledCell sut = new(0, arbitraryRow, arbitraryNumber);
            FilledCell other = new(8, arbitraryRow, arbitraryNumber);

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

            FilledCell sut = new(sharedColumn, 0, arbitraryNumber);
            FilledCell other = new(sharedColumn, 8, arbitraryNumber);

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

            FilledCell sut = new(sharedColumn, sharedRow, 2);
            FilledCell other = new(sharedColumn, sharedRow, 8);

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

            FilledCell sut = new(sharedColumn, sharedRow, sharedNumber);
            FilledCell other = new(sharedColumn, sharedRow, sharedNumber);

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

            FilledCell sut = new(8, arbitraryRow, arbitraryNumber);
            FilledCell other = new(0, arbitraryRow, arbitraryNumber);

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

            FilledCell sut = new(sharedColumn, 8, arbitraryNumber);
            FilledCell other = new(sharedColumn, 0, arbitraryNumber);

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

            FilledCell sut = new(sharedColumn, sharedRow, 8);
            FilledCell other = new(sharedColumn, sharedRow, 2);

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
            FilledCell sut = new();

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

            FilledCell sut = new(sharedColumn, sharedRow, sharedNumber);
            FilledCell other = new(sharedColumn, sharedRow, sharedNumber);

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

            FilledCell sut = new(0, sharedRow, sharedNumber);
            FilledCell other = new(8, sharedRow, sharedNumber);

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

            FilledCell sut = new(sharedColumn, 0, sharedNumber);
            FilledCell other = new(sharedColumn, 8, sharedNumber);

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

            FilledCell sut = new(sharedColumn, sharedRow, 2);
            FilledCell other = new(sharedColumn, sharedRow, 8);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class Obstructs_Method
    {
        [Fact]
        public void InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            FilledCell sut = new();

            // Act
            var result = sut.Obstructs(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualNumberValuesAndEqualColumnValues_ReturnsTrue()
        {
            // Arrange
            const int sharedNumber = 1;
            const int sharedColumn = 5;

            FilledCell sut = new(sharedColumn, 8, sharedNumber);
            FilledCell other = new(sharedColumn, 0, sharedNumber);

            // Act
            var result = sut.Obstructs(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualNumberValuesAndEqualRowValues_ReturnsTrue()
        {
            // Arrange
            const int sharedNumber = 1;
            const int sharedRow = 5;

            FilledCell sut = new(8, sharedRow, sharedNumber);
            FilledCell other = new(0, sharedRow, sharedNumber);

            // Act
            var result = sut.Obstructs(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualNumberValuesAndEqualSectorValues_ReturnsTrue()
        {
            // Arrange
            const int sharedNumber = 1;

            FilledCell sut = new(1, 0, sharedNumber);
            FilledCell other = new(0, 1, sharedNumber);

            // Act
            var result = sut.Obstructs(other);

            // Assert
            result.Should().BeTrue("both are in Sector 0");
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalNumberValues_ReturnsFalse()
        {
            // Arrange
            const int arbitraryColumn = 0;
            const int arbitraryRow = 0;

            FilledCell sut = new(arbitraryColumn, arbitraryRow, 1);
            FilledCell other = new(arbitraryColumn, arbitraryRow, 9);

            // Act
            var result = sut.Obstructs(other);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ToString_Method
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ReturnsFormattedString(FilledCell sut, string expected)
        {
            // Act
            var result = sut.ToString();

            // Assert
            result.Should().Be(expected);
        }

        private sealed class TestCases : TheoryData<FilledCell, string>
        {
            public TestCases()
            {
                Add(new FilledCell(), "(0,0) [1]");
                Add(new FilledCell(0, 0, 2), "(0,0) [2]");
                Add(new FilledCell(7, 2, 9), "(7,2) [9]");
                Add(new FilledCell(1, 8, 3), "(1,8) [3]");
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

            FilledCell sut = new(sharedColumn, sharedRow, sharedNumber);
            FilledCell other = new(sharedColumn, sharedRow, sharedNumber);

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

            FilledCell sut = new(0, sharedRow, sharedNumber);
            FilledCell other = new(8, sharedRow, sharedNumber);

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

            FilledCell sut = new(sharedColumn, 0, sharedNumber);
            FilledCell other = new(sharedColumn, 8, sharedNumber);

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

            FilledCell sut = new(sharedColumn, sharedRow, 2);
            FilledCell other = new(sharedColumn, sharedRow, 8);

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

            FilledCell sut = new(sharedColumn, sharedRow, sharedNumber);
            FilledCell other = new(sharedColumn, sharedRow, sharedNumber);

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

            FilledCell sut = new(0, sharedRow, sharedNumber);
            FilledCell other = new(8, sharedRow, sharedNumber);

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

            FilledCell sut = new(sharedColumn, 0, sharedNumber);
            FilledCell other = new(sharedColumn, 8, sharedNumber);

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

            FilledCell sut = new(sharedColumn, sharedRow, 2);
            FilledCell other = new(sharedColumn, sharedRow, 8);

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
        [InlineData(8, 0, 7)]
        [InlineData(7, 3, 5)]
        public void CanSerializeToJson_ThenDeserialize(int column, int row, int number)
        {
            // Arrange
            FilledCell original = new(column, row, number);

            JsonSerializerOptions jsonOptions = Invariants.GetJsonSerializerOptions();

            // Act
            var json = JsonSerializer.Serialize(original, jsonOptions);
            var deserialized = JsonSerializer.Deserialize<FilledCell>(json, jsonOptions);

            // Assert
            deserialized.Should().NotBeNull().And.Be(original);
        }
    }
}
