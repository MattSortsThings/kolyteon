using System.Text.Json;
using FluentAssertions.Execution;
using Mjt85.Kolyteon.Shikaku;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.Shikaku;

/// <summary>
///     Unit tests for the <see cref="Rectangle" /> struct type.
/// </summary>
public sealed class RectangleTests
{
    [UnitTest]
    public sealed class Constructor_FourArgs
    {
        [Fact]
        public void Initializes_SetsOriginColumnAndOriginRowAndWidthInCellsAndHeightInCellsFromArgs()
        {
            // Arrange
            const int originColumn = 2;
            const int originRow = 1;
            const int widthInCells = 10;
            const int heightInCells = 3;

            // Act
            Rectangle result = new(originColumn, originRow, widthInCells, heightInCells);

            // Assert
            using (new AssertionScope())
            {
                result.OriginColumn.Should().Be(originColumn);
                result.OriginRow.Should().Be(originRow);
                result.WidthInCells.Should().Be(widthInCells);
                result.HeightInCells.Should().Be(heightInCells);
            }
        }

        [Fact]
        public void OriginColumnArgIsNegative_Throws()
        {
            // Arrange
            const int arbitraryOriginRow = 0;
            const int arbitraryWidthInCells = 1;
            const int arbitraryHeightInCells = 1;

            // Act
            Action act = () => _ = new Rectangle(-1, arbitraryOriginRow, arbitraryWidthInCells, arbitraryHeightInCells);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must not be negative. (Parameter 'originColumn')\nActual value was -1.");
        }

        [Fact]
        public void OriginRowArgIsNegative_Throws()
        {
            // Arrange
            const int arbitraryOriginColumn = 0;
            const int arbitraryWidthInCells = 1;
            const int arbitraryHeightInCells = 1;

            // Act
            Action act = () => _ = new Rectangle(arbitraryOriginColumn, -2, arbitraryWidthInCells, arbitraryHeightInCells);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must not be negative. (Parameter 'originRow')\nActual value was -2.");
        }

        [Fact]
        public void WidthInCellsArgIsLessThanOne_Throws()
        {
            // Arrange
            const int arbitraryOriginColumn = 0;
            const int arbitraryOriginRow = 0;
            const int arbitraryHeightInCells = 1;

            // Act
            Action act = () => _ = new Rectangle(arbitraryOriginColumn, arbitraryOriginRow, 0, arbitraryHeightInCells);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must not be less than 1. (Parameter 'widthInCells')\nActual value was 0.");
        }

        [Fact]
        public void HeightInCellsArgIsLessThanOne_Throws()
        {
            // Arrange
            const int arbitraryOriginColumn = 0;
            const int arbitraryOriginRow = 0;
            const int arbitraryWidthInCells = 1;

            // Act
            Action act = () => _ = new Rectangle(arbitraryOriginColumn, arbitraryOriginRow, arbitraryWidthInCells, 0);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must not be less than 1. (Parameter 'heightInCells')\nActual value was 0.");
        }
    }

    [UnitTest]
    public sealed class Constructor_ZeroArgs
    {
        [Fact]
        public void Initializes_OriginColumnValueAndOriginRowValueAreZero_WidthInCellsValueAndHeightInCellsValueAreOne()
        {
            // Act
            Rectangle result = new();

            // Assert
            using (new AssertionScope())
            {
                result.OriginColumn.Should().Be(0);
                result.OriginRow.Should().Be(0);
                result.WidthInCells.Should().Be(1);
                result.HeightInCells.Should().Be(1);
            }
        }
    }

    [UnitTest]
    public sealed class AreaInCells_Property
    {
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 2, 2)]
        [InlineData(2, 1, 2)]
        [InlineData(3, 2, 6)]
        [InlineData(2, 3, 6)]
        [InlineData(11, 31, 341)]
        public void ReturnsProductOfWidthInCellsAndHeightInCells(int widthInCells, int heightInCells, int expected)
        {
            // Arrange
            const int arbitraryOriginColumn = 0;
            const int arbitraryOriginRow = 0;

            Rectangle sut = new(arbitraryOriginColumn, arbitraryOriginRow, widthInCells, heightInCells);

            // Act
            var result = sut.AreaInCells;

            // Assert
            result.Should().Be(expected);
        }
    }

    [UnitTest]
    public sealed class CompareTo_Method
    {
        [Fact]
        public void InstanceHasSmallerOriginColumnThanOther_ReturnsNegativeValue()
        {
            // Arrange
            const int sharedOriginRow = 5;
            const int sharedWidthInCells = 5;
            const int sharedHeightInCells = 5;

            Rectangle sut = new(0, sharedOriginRow, sharedWidthInCells, sharedHeightInCells);
            Rectangle other = new(99, sharedOriginRow, sharedWidthInCells, sharedHeightInCells);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualOriginColumn_InstanceHasSmallerOriginRow_ReturnsNegativeValue()
        {
            // Arrange
            const int sharedOriginColumn = 5;
            const int sharedWidthInCells = 5;
            const int sharedHeightInCells = 5;

            Rectangle sut = new(sharedOriginColumn, 0, sharedWidthInCells, sharedHeightInCells);
            Rectangle other = new(sharedOriginColumn, 99, sharedWidthInCells, sharedHeightInCells);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualOriginColumnAndOriginRow_InstanceHasSmallerWidthInCells_ReturnsNegativeValue()
        {
            // Arrange
            const int sharedOriginColumn = 5;
            const int sharedOriginRow = 5;
            const int sharedHeightInCells = 5;

            Rectangle sut = new(sharedOriginColumn, sharedOriginRow, 1, sharedHeightInCells);
            Rectangle other = new(sharedOriginColumn, sharedOriginRow, 99, sharedHeightInCells);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualOriginColumnAndRowAndWidth_InstanceHasSmallerHeightInCells_ReturnsNegativeValue()
        {
            // Arrange
            const int sharedOriginColumn = 5;
            const int sharedOriginRow = 5;
            const int sharedWidthInCells = 5;

            Rectangle sut = new(sharedOriginColumn, sharedOriginRow, sharedWidthInCells, 1);
            Rectangle other = new(sharedOriginColumn, sharedOriginRow, sharedWidthInCells, 99);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualOriginColumnAndOriginRowAndWidthInCellsAndHeightInCellsValues_ReturnsZero()
        {
            // Arrange
            const int sharedOriginColumn = 5;
            const int sharedOriginRow = 5;
            const int sharedWidthInCells = 5;
            const int sharedHeightInCells = 5;

            Rectangle sut = new(sharedOriginColumn, sharedOriginRow, sharedWidthInCells, sharedHeightInCells);
            Rectangle other = new(sharedOriginColumn, sharedOriginRow, sharedWidthInCells, sharedHeightInCells);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void InstanceHasGreaterOriginColumnThanOther_ReturnsPositiveValue()
        {
            // Arrange
            const int sharedOriginRow = 5;
            const int sharedWidthInCells = 5;
            const int sharedHeightInCells = 5;

            Rectangle sut = new(99, sharedOriginRow, sharedWidthInCells, sharedHeightInCells);
            Rectangle other = new(0, sharedOriginRow, sharedWidthInCells, sharedHeightInCells);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualOriginColumn_InstanceHasGreaterOriginRow_ReturnsPositiveValue()
        {
            // Arrange
            const int sharedOriginColumn = 5;
            const int sharedWidthInCells = 5;
            const int sharedHeightInCells = 5;

            Rectangle sut = new(sharedOriginColumn, 99, sharedWidthInCells, sharedHeightInCells);
            Rectangle other = new(sharedOriginColumn, 0, sharedWidthInCells, sharedHeightInCells);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualOriginColumnAndOriginRow_InstanceHasGreaterWidthInCells_ReturnsPositiveValue()
        {
            // Arrange
            const int sharedOriginColumn = 5;
            const int sharedOriginRow = 5;
            const int sharedHeightInCells = 5;

            Rectangle sut = new(sharedOriginColumn, sharedOriginRow, 99, sharedHeightInCells);
            Rectangle other = new(sharedOriginColumn, sharedOriginRow, 1, sharedHeightInCells);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualOriginColumnAndRowAndWidth_InstanceHasGreaterHeightInCells_ReturnsPositiveValue()
        {
            // Arrange
            const int sharedOriginColumn = 5;
            const int sharedOriginRow = 5;
            const int sharedWidthInCells = 5;

            Rectangle sut = new(sharedOriginColumn, sharedOriginRow, sharedWidthInCells, 99);
            Rectangle other = new(sharedOriginColumn, sharedOriginRow, sharedWidthInCells, 1);

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
            Rectangle sut = new();

            // Act
            var result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualOriginColumnAndOriginRowAndWidthInCellsAndHeightInCellsValues_ReturnsTrue()
        {
            // Arrange
            const int sharedOriginColumn = 1;
            const int sharedOriginRow = 2;
            const int sharedWidthInCells = 3;
            const int sharedHeightInCells = 10;

            Rectangle sut = new(sharedOriginColumn, sharedOriginRow, sharedWidthInCells, sharedHeightInCells);
            Rectangle other = new(sharedOriginColumn, sharedOriginRow, sharedWidthInCells, sharedHeightInCells);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalOriginColumnValues_ReturnsFalse()
        {
            // Arrange
            const int arbitraryOriginRow = 2;
            const int arbitraryWidthInCells = 3;
            const int arbitraryHeightInCells = 10;

            Rectangle sut = new(0, arbitraryOriginRow, arbitraryWidthInCells, arbitraryHeightInCells);
            Rectangle other = new(99, arbitraryOriginRow, arbitraryWidthInCells, arbitraryHeightInCells);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalOriginRowValues_ReturnsFalse()
        {
            // Arrange
            const int sharedOriginColumn = 1;
            const int arbitraryWidthInCells = 3;
            const int arbitraryHeightInCells = 10;

            Rectangle sut = new(sharedOriginColumn, 0, arbitraryWidthInCells, arbitraryHeightInCells);
            Rectangle other = new(sharedOriginColumn, 99, arbitraryWidthInCells, arbitraryHeightInCells);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalWidthInCellsValues_ReturnsFalse()
        {
            // Arrange
            const int sharedOriginColumn = 1;
            const int sharedOriginRow = 2;
            const int arbitraryHeightInCells = 10;

            Rectangle sut = new(sharedOriginColumn, sharedOriginRow, 1, arbitraryHeightInCells);
            Rectangle other = new(sharedOriginColumn, sharedOriginRow, 9, arbitraryHeightInCells);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalHeightInCellsValues_ReturnsFalse()
        {
            // Arrange
            const int sharedOriginColumn = 1;
            const int sharedOriginRow = 2;
            const int sharedWidthInCells = 3;

            Rectangle sut = new(sharedOriginColumn, sharedOriginRow, sharedWidthInCells, 1);
            Rectangle other = new(sharedOriginColumn, sharedOriginRow, sharedWidthInCells, 99);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class Overlaps_Method
    {
        [Fact]
        public void InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            Rectangle sut = new();

            // Act
            var result = sut.Overlaps(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void InstanceAndOtherOverlap_ReturnsTrue(Rectangle rectangleA, Rectangle rectangleB)
        {
            // Act
            var aOverlapsB = rectangleA.Overlaps(rectangleB);
            var bOverlapsA = rectangleB.Overlaps(rectangleA);

            // Assert
            aOverlapsB.Should().Be(bOverlapsA).And.BeTrue();
        }

        [Theory]
        [ClassData(typeof(NegativeTestCases))]
        public void InstanceAndOtherDoNotOverlap_ReturnsFalse(Rectangle rectangleA, Rectangle rectangleB)
        {
            // Act
            var aOverlapsB = rectangleA.Overlaps(rectangleB);
            var bOverlapsA = rectangleB.Overlaps(rectangleA);

            // Assert
            aOverlapsB.Should().Be(bOverlapsA).And.BeFalse();
        }

        private sealed class PositiveTestCases : TheoryData<Rectangle, Rectangle>
        {
            public PositiveTestCases()
            {
                Rectangle fixedRectangle = new(2, 2, 3, 4);

                Add(fixedRectangle, new Rectangle(2, 2, 1, 1));
                Add(fixedRectangle, new Rectangle(2, 3, 1, 1));
                Add(fixedRectangle, new Rectangle(2, 4, 1, 1));
                Add(fixedRectangle, new Rectangle(3, 2, 1, 1));
                Add(fixedRectangle, new Rectangle(3, 3, 1, 1));
                Add(fixedRectangle, new Rectangle(3, 4, 1, 1));
                Add(fixedRectangle, new Rectangle(4, 2, 1, 1));
                Add(fixedRectangle, new Rectangle(4, 3, 1, 1));
                Add(fixedRectangle, new Rectangle(4, 4, 1, 1));
                Add(fixedRectangle, new Rectangle(2, 2, 1, 4));
                Add(fixedRectangle, new Rectangle(2, 3, 1, 4));
                Add(fixedRectangle, new Rectangle(2, 4, 1, 4));
                Add(fixedRectangle, new Rectangle(3, 2, 1, 4));
                Add(fixedRectangle, new Rectangle(3, 3, 1, 4));
                Add(fixedRectangle, new Rectangle(3, 4, 1, 4));
                Add(fixedRectangle, new Rectangle(4, 2, 1, 4));
                Add(fixedRectangle, new Rectangle(4, 3, 1, 4));
                Add(fixedRectangle, new Rectangle(4, 4, 1, 1));
                Add(fixedRectangle, new Rectangle(2, 3, 2, 2));
                Add(fixedRectangle, new Rectangle(3, 3, 1, 1));
                Add(fixedRectangle, new Rectangle(0, 0, 9, 9));
                Add(fixedRectangle, new Rectangle(0, 0, 3, 3));
                Add(fixedRectangle, new Rectangle(0, 0, 3, 9));
                Add(fixedRectangle, new Rectangle(1, 3, 4, 7));
                Add(fixedRectangle, new Rectangle(1, 1, 3, 3));
                Add(fixedRectangle, new Rectangle(1, 0, 4, 6));
                Add(fixedRectangle, new Rectangle(3, 0, 1, 9));
                Add(fixedRectangle, new Rectangle(0, 5, 4, 2));
                Add(fixedRectangle, new Rectangle(4, 5, 1, 1));
            }
        }

        private sealed class NegativeTestCases : TheoryData<Rectangle, Rectangle>
        {
            public NegativeTestCases()
            {
                Rectangle fixedRectangle = new(2, 2, 3, 4);

                Add(fixedRectangle, new Rectangle(0, 0, 1, 1));
                Add(fixedRectangle, new Rectangle(1, 1, 1, 2));
                Add(fixedRectangle, new Rectangle(1, 1, 2, 1));
                Add(fixedRectangle, new Rectangle(1, 1, 3, 1));
                Add(fixedRectangle, new Rectangle(3, 1, 4, 1));
                Add(fixedRectangle, new Rectangle(9, 9, 9, 9));
                Add(fixedRectangle, new Rectangle(0, 2, 2, 5));
                Add(fixedRectangle, new Rectangle(5, 3, 1, 1));
                Add(fixedRectangle, new Rectangle(5, 3, 4, 4));
                Add(fixedRectangle, new Rectangle(3, 6, 2, 1));
                Add(fixedRectangle, new Rectangle(0, 0, 2, 2));
                Add(fixedRectangle, new Rectangle(1, 0, 2, 2));
                Add(fixedRectangle, new Rectangle(2, 0, 2, 2));
                Add(fixedRectangle, new Rectangle(3, 0, 2, 2));
                Add(fixedRectangle, new Rectangle(4, 0, 2, 2));
                Add(fixedRectangle, new Rectangle(5, 0, 2, 2));
                Add(fixedRectangle, new Rectangle(6, 0, 2, 2));
                Add(fixedRectangle, new Rectangle(0, 6, 2, 2));
                Add(fixedRectangle, new Rectangle(1, 6, 2, 2));
                Add(fixedRectangle, new Rectangle(2, 6, 2, 2));
                Add(fixedRectangle, new Rectangle(3, 6, 2, 2));
                Add(fixedRectangle, new Rectangle(4, 6, 2, 2));
                Add(fixedRectangle, new Rectangle(5, 6, 2, 2));
                Add(fixedRectangle, new Rectangle(6, 6, 2, 2));
                Add(fixedRectangle, new Rectangle(0, 1, 2, 2));
                Add(fixedRectangle, new Rectangle(0, 2, 2, 2));
                Add(fixedRectangle, new Rectangle(0, 3, 2, 2));
                Add(fixedRectangle, new Rectangle(0, 4, 2, 2));
                Add(fixedRectangle, new Rectangle(0, 5, 2, 2));
                Add(fixedRectangle, new Rectangle(0, 6, 2, 2));
                Add(fixedRectangle, new Rectangle(5, 1, 2, 2));
                Add(fixedRectangle, new Rectangle(5, 2, 2, 2));
                Add(fixedRectangle, new Rectangle(5, 3, 2, 2));
                Add(fixedRectangle, new Rectangle(5, 4, 2, 2));
                Add(fixedRectangle, new Rectangle(5, 5, 2, 2));
                Add(fixedRectangle, new Rectangle(5, 6, 2, 2));
            }
        }
    }

    [UnitTest]
    public sealed class Encloses_Method
    {
        [Theory]
        [InlineData(2, 2)]
        [InlineData(2, 3)]
        [InlineData(2, 4)]
        [InlineData(2, 5)]
        [InlineData(3, 2)]
        [InlineData(3, 3)]
        [InlineData(3, 4)]
        [InlineData(3, 5)]
        [InlineData(4, 2)]
        [InlineData(4, 3)]
        [InlineData(4, 4)]
        [InlineData(4, 5)]
        public void HintOccupiesCellInsideGrid_ReturnsTrue(int hintColumn, int hintRow)
        {
            // Arrange
            Rectangle sut = new(2, 2, 3, 4);

            const int arbitraryNumber = 2;
            Hint hint = new(hintColumn, hintRow, arbitraryNumber);

            // Act
            var result = sut.Encloses(hint);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(1, 3)]
        [InlineData(1, 4)]
        [InlineData(1, 5)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        [InlineData(4, 1)]
        [InlineData(2, 6)]
        [InlineData(3, 6)]
        [InlineData(4, 6)]
        [InlineData(5, 1)]
        [InlineData(5, 2)]
        [InlineData(5, 3)]
        [InlineData(5, 4)]
        [InlineData(5, 5)]
        [InlineData(9, 9)]
        public void HintOccupiesCellOutsideGrid_ReturnsFalse(int hintColumn, int hintRow)
        {
            // Arrange
            Rectangle sut = new(2, 2, 3, 4);

            const int arbitraryNumber = 2;
            Hint hint = new(hintColumn, hintRow, arbitraryNumber);

            // Act
            var result = sut.Encloses(hint);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ToString_Method
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ReturnsFormattedString(Rectangle sut, string expected)
        {
            // Act
            var result = sut.ToString();

            // Assert
            result.Should().Be(expected);
        }

        private sealed class TestCases : TheoryData<Rectangle, string>
        {
            public TestCases()
            {
                Add(new Rectangle(), "(0,0) [1x1]");
                Add(new Rectangle(0, 0, 1, 1), "(0,0) [1x1]");
                Add(new Rectangle(7, 2, 10, 4), "(7,2) [10x4]");
                Add(new Rectangle(10, 20, 11, 3), "(10,20) [11x3]");
            }
        }
    }

    [UnitTest]
    public sealed class Equality_Operator
    {
        [Fact]
        public void InstanceAndOtherHaveEqualOriginColumnAndOriginRowAndWidthInCellsAndHeightInCellsValues_ReturnsTrue()
        {
            // Arrange
            const int sharedOriginColumn = 1;
            const int sharedOriginRow = 2;
            const int sharedWidthInCells = 3;
            const int sharedHeightInCells = 10;

            Rectangle sut = new(sharedOriginColumn, sharedOriginRow, sharedWidthInCells, sharedHeightInCells);
            Rectangle other = new(sharedOriginColumn, sharedOriginRow, sharedWidthInCells, sharedHeightInCells);

            // Act
            var result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalOriginColumnValues_ReturnsFalse()
        {
            // Arrange
            const int arbitraryOriginRow = 2;
            const int arbitraryWidthInCells = 3;
            const int arbitraryHeightInCells = 10;

            Rectangle sut = new(0, arbitraryOriginRow, arbitraryWidthInCells, arbitraryHeightInCells);
            Rectangle other = new(99, arbitraryOriginRow, arbitraryWidthInCells, arbitraryHeightInCells);

            // Act
            var result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalOriginRowValues_ReturnsFalse()
        {
            // Arrange
            const int sharedOriginColumn = 1;
            const int arbitraryWidthInCells = 3;
            const int arbitraryHeightInCells = 10;

            Rectangle sut = new(sharedOriginColumn, 0, arbitraryWidthInCells, arbitraryHeightInCells);
            Rectangle other = new(sharedOriginColumn, 99, arbitraryWidthInCells, arbitraryHeightInCells);

            // Act
            var result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalWidthInCellsValues_ReturnsFalse()
        {
            // Arrange
            const int sharedOriginColumn = 1;
            const int sharedOriginRow = 2;
            const int arbitraryHeightInCells = 10;

            Rectangle sut = new(sharedOriginColumn, sharedOriginRow, 1, arbitraryHeightInCells);
            Rectangle other = new(sharedOriginColumn, sharedOriginRow, 9, arbitraryHeightInCells);

            // Act
            var result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalHeightInCellsValues_ReturnsFalse()
        {
            // Arrange
            const int sharedOriginColumn = 1;
            const int sharedOriginRow = 2;
            const int sharedWidthInCells = 3;

            Rectangle sut = new(sharedOriginColumn, sharedOriginRow, sharedWidthInCells, 1);
            Rectangle other = new(sharedOriginColumn, sharedOriginRow, sharedWidthInCells, 99);

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
        public void InstanceAndOtherHaveEqualOriginColumnAndOriginRowAndWidthInCellsAndHeightInCellsValues_ReturnsFalse()
        {
            // Arrange
            const int sharedOriginColumn = 1;
            const int sharedOriginRow = 2;
            const int sharedWidthInCells = 3;
            const int sharedHeightInCells = 10;

            Rectangle sut = new(sharedOriginColumn, sharedOriginRow, sharedWidthInCells, sharedHeightInCells);
            Rectangle other = new(sharedOriginColumn, sharedOriginRow, sharedWidthInCells, sharedHeightInCells);

            // Act
            var result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalOriginColumnValues_ReturnsTrue()
        {
            // Arrange
            const int arbitraryOriginRow = 2;
            const int arbitraryWidthInCells = 3;
            const int arbitraryHeightInCells = 10;

            Rectangle sut = new(0, arbitraryOriginRow, arbitraryWidthInCells, arbitraryHeightInCells);
            Rectangle other = new(99, arbitraryOriginRow, arbitraryWidthInCells, arbitraryHeightInCells);

            // Act
            var result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalOriginRowValues_ReturnsTrue()
        {
            // Arrange
            const int sharedOriginColumn = 1;
            const int arbitraryWidthInCells = 3;
            const int arbitraryHeightInCells = 10;

            Rectangle sut = new(sharedOriginColumn, 0, arbitraryWidthInCells, arbitraryHeightInCells);
            Rectangle other = new(sharedOriginColumn, 99, arbitraryWidthInCells, arbitraryHeightInCells);

            // Act
            var result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalWidthInCellsValues_ReturnsTrue()
        {
            // Arrange
            const int sharedOriginColumn = 1;
            const int sharedOriginRow = 2;
            const int arbitraryHeightInCells = 10;

            Rectangle sut = new(sharedOriginColumn, sharedOriginRow, 1, arbitraryHeightInCells);
            Rectangle other = new(sharedOriginColumn, sharedOriginRow, 9, arbitraryHeightInCells);

            // Act
            var result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalHeightInCellsValues_ReturnsTrue()
        {
            // Arrange
            const int sharedOriginColumn = 1;
            const int sharedOriginRow = 2;
            const int sharedWidthInCells = 3;

            Rectangle sut = new(sharedOriginColumn, sharedOriginRow, sharedWidthInCells, 1);
            Rectangle other = new(sharedOriginColumn, sharedOriginRow, sharedWidthInCells, 99);

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
        [InlineData(0, 0, 2, 1)]
        [InlineData(10, 0, 7, 19)]
        [InlineData(7, 3, 15, 3)]
        public void CanSerializeToJson_ThenDeserialize(int originColumn, int originRow, int widthInCells, int heightInCells)
        {
            // Arrange
            Rectangle original = new(originColumn, originRow, widthInCells, heightInCells);

            JsonSerializerOptions jsonOptions = Invariants.GetJsonSerializerOptions();

            // Act
            var json = JsonSerializer.Serialize(original, jsonOptions);
            var deserialized = JsonSerializer.Deserialize<Rectangle>(json, jsonOptions);

            // Assert
            deserialized.Should().NotBeNull().And.Be(original);
        }
    }
}
