using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.Sudoku;

namespace Kolyteon.Tests.Unit.Sudoku;

public static class SudokuProblemTests
{
    [UnitTest]
    public sealed class EqualsMethod
    {
        [Fact]
        public void Equals_InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            SudokuProblem sut = SudokuProblem.FromGrid(new int?[,]
            {
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, 0008, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, 0002 },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { 0001, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            });

            // Act
            bool result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveEqualFilledSquares_ReturnsTrue()
        {
            // Arrange
            int?[,] sharedGrid =
            {
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, 0008, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, 0002 },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { 0001, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            };

            SudokuProblem sut = SudokuProblem.FromGrid(sharedGrid);
            SudokuProblem other = SudokuProblem.FromGrid(sharedGrid);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalFilledSquares_ReturnsFalse()
        {
            // Arrange
            SudokuProblem sut = SudokuProblem.FromGrid(new int?[,]
            {
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, 0008, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, 0002 },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { 0001, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            });

            SudokuProblem other = SudokuProblem.FromGrid(new int?[,]
            {
                { 0001, null, null, null, null, null, null, null, 0002 },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, 0008, null, null, null, null, null }
            });


            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_OtherArgIsNull_ReturnsFalse()
        {
            // Arrange
            SudokuProblem sut = SudokuProblem.FromGrid(new int?[,]
            {
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, 0008, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, 0002 },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { 0001, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            });

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
        public void Equality_InstanceAndOtherHaveEqualFilledSquares_ReturnsTrue()
        {
            // Arrange
            int?[,] sharedGrid =
            {
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, 0008, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, 0002 },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { 0001, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            };

            SudokuProblem sut = SudokuProblem.FromGrid(sharedGrid);
            SudokuProblem other = SudokuProblem.FromGrid(sharedGrid);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalFilledSquares_ReturnsFalse()
        {
            // Arrange
            SudokuProblem sut = SudokuProblem.FromGrid(new int?[,]
            {
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, 0008, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, 0002 },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { 0001, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            });

            SudokuProblem other = SudokuProblem.FromGrid(new int?[,]
            {
                { 0001, null, null, null, null, null, null, null, 0002 },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, 0008, null, null, null, null, null }
            });

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
        public void Inequality_InstanceAndOtherHaveEqualFilledSquares_ReturnsFalse()
        {
            // Arrange
            int?[,] sharedGrid =
            {
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, 0008, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, 0002 },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { 0001, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            };

            SudokuProblem sut = SudokuProblem.FromGrid(sharedGrid);
            SudokuProblem other = SudokuProblem.FromGrid(sharedGrid);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalFilledSquares_ReturnsTrue()
        {
            // Arrange
            SudokuProblem sut = SudokuProblem.FromGrid(new int?[,]
            {
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, 0008, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, 0002 },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { 0001, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            });

            SudokuProblem other = SudokuProblem.FromGrid(new int?[,]
            {
                { 0001, null, null, null, null, null, null, null, 0002 },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, 0008, null, null, null, null, null }
            });

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class FromGridStaticFactoryMethod
    {
        public static TheoryData<int?[,], IReadOnlyList<NumberedSquare>> FilledSquaresTestCases => new()
        {
            {
                new int?[,]
                {
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null }
                },
                Array.Empty<NumberedSquare>()
            },
            {
                new int?[,]
                {
                    { 0001, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null }
                },
                [
                    Square.FromColumnAndRow(0, 0).ToNumberedSquare(1)
                ]
            },
            {
                new int?[,]
                {
                    { 0001, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, 0002 }
                },
                [
                    Square.FromColumnAndRow(0, 0).ToNumberedSquare(1),
                    Square.FromColumnAndRow(8, 8).ToNumberedSquare(2)
                ]
            },
            {
                new int?[,]
                {
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, 0008, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, 0002 },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { 0001, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null }
                },
                [
                    Square.FromColumnAndRow(0, 7).ToNumberedSquare(1),
                    Square.FromColumnAndRow(3, 1).ToNumberedSquare(8),
                    Square.FromColumnAndRow(8, 3).ToNumberedSquare(2)
                ]
            }
        };

        [Theory]
        [MemberData(nameof(FilledSquaresTestCases), MemberType = typeof(FromGridStaticFactoryMethod))]
        public void FromGrid_GivenGrid_ReturnsInstanceWithCorrectFilledSquares(int?[,] grid,
            IReadOnlyList<NumberedSquare> expectedFilledSquares)
        {
            // Act
            SudokuProblem result = SudokuProblem.FromGrid(grid);

            // Assert
            result.FilledSquares.Should().Equal(expectedFilledSquares);
        }

        [Fact]
        public void FromGrid_GivenGrid_ReturnsInstanceWithNineByNineGrid()
        {
            // Arrange
            int?[,] grid =
            {
                { 0001, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            };

            // Act
            SudokuProblem result = SudokuProblem.FromGrid(grid);

            // Assert
            result.Grid.Should().Be(Dimensions.FromWidthAndHeight(9, 9).ToBlock());
        }

        [Fact]
        public void FromGrid_GivenGrid_ReturnsInstanceWithNineThreeByThreeSectors()
        {
            // Arrange
            int?[,] grid =
            {
                { 0001, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            };

            // Act
            SudokuProblem result = SudokuProblem.FromGrid(grid);

            // Assert
            result.Sectors.Should().Equal(
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 3).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 6).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(3, 0).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(3, 3).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(3, 6).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(6, 0).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(6, 3).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(6, 6).ToBlock(Dimensions.FromWidthAndHeight(3, 3))
            );
        }

        [Fact]
        public void FromGrid_ZeroEmptySquares_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                { 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
                { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
                { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
                { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
                { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
                { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 },
                { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
                { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 }
            };

            // Act
            Action act = () => SudokuProblem.FromGrid(grid);

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Problem has zero empty squares.");
        }


        [Fact]
        public void FromGrid_FilledSquareHasNumberLessThanOne_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, 0000 }
            };

            // Act
            Action act = () => SudokuProblem.FromGrid(grid);

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Invalid filled square (8,8) [0]. Number must be in the range [1,9].");
        }

        [Fact]
        public void FromGrid_FilledSquareHasNumberGreaterThanNine_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, 0010 }
            };

            // Act
            Action act = () => SudokuProblem.FromGrid(grid);

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Invalid filled square (8,8) [10]. Number must be in the range [1,9].");
        }

        [Fact]
        public void FromGrid_DuplicateNumberInColumn_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, 0005, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            };

            // Act
            Action act = () => SudokuProblem.FromGrid(grid);

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Number 5 occurs more than once in column 4.");
        }

        [Fact]
        public void FromGrid_DuplicateNumberInRow_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0001, 0002, 0003, 0004, 0005, 0006, 0007, null, 0001 },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            };

            // Act
            Action act = () => SudokuProblem.FromGrid(grid);

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Number 1 occurs more than once in row 0.");
        }

        [Fact]
        public void FromGrid_DuplicateNumberInSector_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, 0009, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            };

            // Act
            Action act = () => SudokuProblem.FromGrid(grid);

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Number 9 occurs more than once in sector 6.");
        }

        [Fact]
        public void FromGrid_GridArgIsNull_Throws()
        {
            // Act
            Action act = () => SudokuProblem.FromGrid(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'grid')");
        }

        [Fact]
        public void FromGrid_GridArgRankZeroLengthIsLessThanNine_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                { null, null, null, null, null, null, null, null, null }
            };

            // Act
            Action act = () => SudokuProblem.FromGrid(grid);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Rank-0 length is 2, must be 9.");
        }

        [Fact]
        public void FromGrid_GridArgRankZeroLengthIsGreaterThanNine_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            };

            // Act
            Action act = () => SudokuProblem.FromGrid(grid);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Rank-0 length is 12, must be 9.");
        }

        [Fact]
        public void FromGrid_GridArgRankOneLengthIsLessThanNine_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0001, 0002, 0003 },
                { null, null, null },
                { null, null, null },
                { null, null, null },
                { null, null, null },
                { null, null, null },
                { null, null, null },
                { null, null, null },
                { null, null, null }
            };

            // Act
            Action act = () => SudokuProblem.FromGrid(grid);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Rank-1 length is 3, must be 9.");
        }

        [Fact]
        public void FromGrid_GridArgRankOneLengthIsGreaterThanNine_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null }
            };

            // Act
            Action act = () => SudokuProblem.FromGrid(grid);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Rank-1 length is 10, must be 9.");
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        [SuppressMessage("ReSharper", "UseCollectionExpression")]
        public static TheoryData<int?[,]> TestCases => new()
        {
            new int?[,]
            {
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            },
            new int?[,]
            {
                { 0001, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            },
            new int?[,]
            {
                { 0001, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, 0002 }
            },
            new int?[,]
            {
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, 0008, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, 0002 },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { 0001, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(Serialization))]
        public void CanSerializeToJson_ThenDeserializeToInstanceWithEqualValue(int?[,] grid)
        {
            // Arrange
            SudokuProblem originalProblem = SudokuProblem.FromGrid(grid);

            string json = JsonSerializer.Serialize(originalProblem, JsonSerializerOptions.Default);

            // Act
            SudokuProblem? deserializedProblem = JsonSerializer.Deserialize<SudokuProblem>(json, JsonSerializerOptions.Default);

            // Assert
            deserializedProblem.Should().NotBeNull().And.Be(originalProblem);
        }
    }
}
