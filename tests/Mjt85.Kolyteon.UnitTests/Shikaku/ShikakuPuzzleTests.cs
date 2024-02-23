using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using FluentAssertions.Execution;
using Mjt85.Kolyteon.Shikaku;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.Shikaku;

/// <summary>
///     Unit tests for the <see cref="ShikakuPuzzle" /> record type.
/// </summary>
public sealed class ShikakuPuzzleTests
{
    [UnitTest]
    public sealed class Equals_Method
    {
        [Fact]
        public void InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            ShikakuPuzzle sut = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0005, null, null, null, null },
                { null, 0005, null, null, null },
                { null, null, 0005, null, null },
                { null, null, null, 0005, null },
                { null, null, null, null, 0005 }
            });

            // Act
            var result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveSameHints_ReturnsTrue()
        {
            // Arrange
            var sharedGrid = new int?[,]
            {
                { 0005, null, null, null, null },
                { null, 0005, null, null, null },
                { null, null, 0005, null, null },
                { null, null, null, 0005, null },
                { null, null, null, null, 0005 }
            };

            ShikakuPuzzle sut = ShikakuPuzzle.FromGrid(sharedGrid);
            ShikakuPuzzle other = ShikakuPuzzle.FromGrid(sharedGrid);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveDifferentHints_ReturnsFalse()
        {
            // Arrange
            ShikakuPuzzle sut = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0005, null, null, null, null },
                { null, 0005, null, null, null },
                { null, null, 0005, null, null },
                { null, null, null, 0005, null },
                { null, null, null, null, 0005 }
            });

            ShikakuPuzzle other = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0002, null, null, null, null },
                { null, 0005, null, null, null },
                { null, null, 0005, null, null },
                { null, null, null, 0005, null },
                { null, null, null, null, 0008 }
            });

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void OtherArgIsNull_ReturnsFalse()
        {
            // Arrange
            ShikakuPuzzle sut = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0005, null, null, null, null },
                { null, 0005, null, null, null },
                { null, null, 0005, null, null },
                { null, null, null, 0005, null },
                { null, null, null, null, 0005 }
            });

            // Act
            var result = sut.Equals(null);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ValidSolution_Method
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ValidSolution_ReturnsSuccess(int?[,] grid, IReadOnlyList<Rectangle> solution)
        {
            // Arrange
            ShikakuPuzzle sut = ShikakuPuzzle.FromGrid(grid);

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().Be(ValidationResult.Success);
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void SolutionHasTooFewItems_ReturnsFailure()
        {
            // Arrange
            ShikakuPuzzle sut = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0025, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null }
            });

            IReadOnlyList<Rectangle> solution = Array.Empty<Rectangle>();

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should().Be("Solution size is 0, should be 1.");
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void SolutionHasTooManyItems_ReturnsFailure()
        {
            // Arrange
            ShikakuPuzzle sut = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0025, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null }
            });

            IReadOnlyList<Rectangle> solution =
            [
                new Rectangle(0, 0, 5, 5),
                new Rectangle(0, 0, 5, 5)
            ];

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should().Be("Solution size is 2, should be 1.");
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void RectangleAreasSumToLessThanGridArea_ReturnsFailure()
        {
            // Arrange
            ShikakuPuzzle sut = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0010, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0015 }
            });

            IReadOnlyList<Rectangle> solution =
            [
                new Rectangle(0, 0, 5, 1),
                new Rectangle(5, 0, 1, 5)
            ];
            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should().Be("Sum of rectangle areas is 10, grid area is 25.");
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void RectangleAreasSumToMoreThanGridArea_ReturnsFailure()
        {
            // Arrange
            ShikakuPuzzle sut = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0010, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0015 }
            });

            IReadOnlyList<Rectangle> solution =
            [
                new Rectangle(0, 0, 5, 2),
                new Rectangle(0, 1, 4, 5)
            ];
            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should().Be("Sum of rectangle areas is 30, grid area is 25.");
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void RectangleOutsideGrid_ReturnsFailure()
        {
            // Arrange
            ShikakuPuzzle sut = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0010, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0015 }
            });

            IReadOnlyList<Rectangle> solution =
            [
                new Rectangle(0, 0, 5, 2),
                new Rectangle(4, 4, 3, 5)
            ];

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should().Be("Rectangle (4,4) [3x5] outside grid.");
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void RectanglesOverlap_ReturnsFailure()
        {
            // Arrange
            ShikakuPuzzle sut = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0010, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0015 }
            });

            IReadOnlyList<Rectangle> solution =
            [
                new Rectangle(0, 0, 5, 2),
                new Rectangle(2, 0, 3, 5)
            ];

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should().Be("Rectangles (0,0) [5x2] and (2,0) [3x5] overlap.");
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void RectangleEnclosesZeroHints_ReturnsFailure()
        {
            // Arrange
            ShikakuPuzzle sut = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, 0010, null, 0015 }
            });

            IReadOnlyList<Rectangle> solution =
            [
                new Rectangle(0, 0, 5, 2),
                new Rectangle(0, 2, 5, 3)
            ];

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should().Be("Rectangle (0,0) [5x2] encloses zero hints.");
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void RectangleEnclosesMultipleHints_ReturnsFailure()
        {
            // Arrange
            ShikakuPuzzle sut = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0010, null, 0015, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null }
            });

            IReadOnlyList<Rectangle> solution =
            [
                new Rectangle(0, 0, 5, 2),
                new Rectangle(0, 2, 5, 3)
            ];

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should().Be("Rectangle (0,0) [5x2] encloses multiple hints.");
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void RectangleAreaNotEqualToEnclosedHintNumber_ReturnsFailure()
        {
            // Arrange
            ShikakuPuzzle sut = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0015, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0010 }
            });

            IReadOnlyList<Rectangle> solution =
            [
                new Rectangle(0, 0, 5, 2),
                new Rectangle(0, 2, 5, 3)
            ];

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should().Be("Rectangle (0,0) [5x2] encloses hint (0,0) [15] with incorrect number.");
        }

        [Fact]
        public void SolutionArgIsNull_Throws()
        {
            // Arrange
            ShikakuPuzzle sut = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0025, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null }
            });

            // Act
            Action act = () => sut.ValidSolution(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'solution')");
        }

        private sealed class TestCases : TheoryData<int?[,], IReadOnlyList<Rectangle>>
        {
            public TestCases()
            {
                Add(new int?[,]
                {
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, 0025, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null }
                }, [
                    new Rectangle(0, 0, 5, 5)
                ]);

                Add(new int?[,]
                {
                    { 0025, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null }
                }, [
                    new Rectangle(0, 0, 5, 5)
                ]);

                Add(new int?[,]
                {
                    { 0005, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, 0020 }
                }, [
                    new Rectangle(0, 0, 5, 1),
                    new Rectangle(0, 1, 5, 4)
                ]);

                Add(new int?[,]
                {
                    { 0005, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, 0020 }
                }, [
                    new Rectangle(0, 0, 1, 5),
                    new Rectangle(1, 0, 4, 5)
                ]);

                Add(new int?[,]
                {
                    { null, null, 0005, null, null },
                    { null, null, 0020, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null }
                }, [
                    new Rectangle(0, 0, 5, 1),
                    new Rectangle(0, 1, 5, 4)
                ]);

                Add(new int?[,]
                {
                    { 0005, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, 0010 },
                    { null, null, null, null, null },
                    { null, null, null, null, 0010 }
                }, [
                    new Rectangle(0, 0, 5, 1),
                    new Rectangle(0, 1, 5, 2),
                    new Rectangle(0, 3, 5, 2)
                ]);

                Add(new int?[,]
                {
                    { null, null, 0005, null, null },
                    { null, null, null, null, null },
                    { 0004, null, null, null, 0006 },
                    { null, null, null, null, null },
                    { null, null, 0010, null, null }
                }, [
                    new Rectangle(0, 0, 5, 1),
                    new Rectangle(0, 1, 2, 2),
                    new Rectangle(2, 1, 3, 2),
                    new Rectangle(0, 3, 5, 2)
                ]);

                Add(new int?[,]
                {
                    { 0005, null, null, null, null },
                    { null, 0005, null, null, null },
                    { null, null, 0005, null, null },
                    { null, null, null, 0005, null },
                    { null, null, null, null, 0005 }
                }, [
                    new Rectangle(0, 0, 5, 1),
                    new Rectangle(0, 1, 5, 1),
                    new Rectangle(0, 2, 5, 1),
                    new Rectangle(0, 3, 5, 1),
                    new Rectangle(0, 4, 5, 1)
                ]);

                Add(new int?[,]
                {
                    { 0005, null, null, null, null },
                    { null, 0005, null, null, null },
                    { null, null, 0005, null, null },
                    { null, null, null, 0005, null },
                    { null, null, null, null, 0005 }
                }, [
                    new Rectangle(0, 0, 1, 5),
                    new Rectangle(1, 0, 1, 5),
                    new Rectangle(2, 0, 1, 5),
                    new Rectangle(3, 0, 1, 5),
                    new Rectangle(4, 0, 1, 5)
                ]);

                Add(new int?[,]
                {
                    { 0005, null, null, null, null },
                    { null, 0003, null, null, null },
                    { null, null, null, null, 0009 },
                    { null, 0002, null, null, null },
                    { null, null, null, null, 0006 }
                }, [
                    new Rectangle(0, 0, 1, 5),
                    new Rectangle(1, 0, 1, 3),
                    new Rectangle(1, 3, 1, 2),
                    new Rectangle(2, 0, 3, 3),
                    new Rectangle(2, 3, 3, 2)
                ]);
            }
        }
    }

    [UnitTest]
    public sealed class FromGrid_StaticMethod
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ReturnsInstanceWithExpectedGridSideLengthAndHints(int?[,] grid,
            int expectedGridSideLength,
            IReadOnlyList<Hint> expectedHints)
        {
            // Act
            ShikakuPuzzle result = ShikakuPuzzle.FromGrid(grid);

            // Assert
            using (new AssertionScope())
            {
                result.GridSideLength.Should().Be(expectedGridSideLength);
                result.Hints.Should().BeEquivalentTo(expectedHints, options => options.WithoutStrictOrdering());
            }
        }

        [Fact]
        public void GridArgIsNull_Throws()
        {
            // Act
            Action act = () => ShikakuPuzzle.FromGrid(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'grid')");
        }

        [Fact]
        public void GridIsSmallerThanFiveByFive_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0003, null, null },
                { null, 0003, null },
                { null, null, 0003 }
            };

            // Act
            Action act = () => ShikakuPuzzle.FromGrid(grid);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Grid must be a square no smaller than 5x5 in size.");
        }

        [Fact]
        public void GridIsNotSquare_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0003, null, null },
                { null, 0003, null },
                { null, null, 0003 },
                { 0003, null, null },
                { null, 0003, null },
                { null, null, 0003 },
                { 0003, null, null },
                { null, 0003, null },
                { null, null, 0003 }
            };

            // Act
            Action act = () => ShikakuPuzzle.FromGrid(grid);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Grid must be a square no smaller than 5x5 in size.");
        }

        [Fact]
        public void GridContainsHintNumberLessThanTwo_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0024, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, 0001, null }
            };

            // Act
            Action act = () => ShikakuPuzzle.FromGrid(grid);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Grid has illegal hint number at index [4,3].");
        }

        [Fact]
        public void HintNumbersSumToLessThanGridArea_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0010, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0010 }
            };

            // Act
            Action act = () => ShikakuPuzzle.FromGrid(grid);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Hint numbers sum to 20, grid area is 25.");
        }

        [Fact]
        public void HintNumbersSumToMoreThanGridArea_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0025, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0002 }
            };

            // Act
            Action act = () => ShikakuPuzzle.FromGrid(grid);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Hint numbers sum to 27, grid area is 25.");
        }

        private sealed class TestCases : TheoryData<int?[,], int, IReadOnlyList<Hint>>
        {
            public TestCases()
            {
                Add(new int?[,]
                {
                    { 0025, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null }
                }, 5, [
                    new Hint(0, 0, 25)
                ]);

                Add(new int?[,]
                {
                    { 0005, null, null, null, null },
                    { null, 0005, null, null, null },
                    { null, null, 0005, null, null },
                    { null, null, null, 0005, null },
                    { null, null, null, null, 0005 }
                }, 5, [
                    new Hint(0, 0, 5),
                    new Hint(1, 1, 5),
                    new Hint(2, 2, 5),
                    new Hint(3, 3, 5),
                    new Hint(4, 4, 5)
                ]);

                Add(new int?[,]
                {
                    { 0007, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, 0018 }
                }, 5, [
                    new Hint(0, 0, 7),
                    new Hint(4, 4, 18)
                ]);

                Add(new int?[,]
                {
                    { null, null, null, null, null },
                    { null, 0010, null, null, null },
                    { null, null, 0003, 0006, null },
                    { null, null, null, null, null },
                    { null, null, null, null, 0006 }
                }, 5, [
                    new Hint(1, 1, 10),
                    new Hint(2, 2, 3),
                    new Hint(3, 2, 6),
                    new Hint(4, 4, 6)
                ]);

                Add(new int?[,]
                {
                    { null, null, 0007, null, null, null, null },
                    { null, null, null, null, null, 0007, null },
                    { 0008, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null },
                    { 0007, null, null, null, null, null, null },
                    { null, null, 0014, null, null, null, null },
                    { null, null, null, null, 0006, null, null }
                }, 7, [
                    new Hint(0, 2, 8),
                    new Hint(0, 4, 7),
                    new Hint(2, 0, 7),
                    new Hint(2, 5, 14),
                    new Hint(4, 6, 6),
                    new Hint(5, 1, 7)
                ]);
            }
        }
    }

    [UnitTest]
    public sealed class Equality_Operator
    {
        [Fact]
        public void InstanceAndOtherHaveSameHints_ReturnsTrue()
        {
            // Arrange
            var sharedGrid = new int?[,]
            {
                { 0005, null, null, null, null },
                { null, 0005, null, null, null },
                { null, null, 0005, null, null },
                { null, null, null, 0005, null },
                { null, null, null, null, 0005 }
            };

            ShikakuPuzzle sut = ShikakuPuzzle.FromGrid(sharedGrid);
            ShikakuPuzzle other = ShikakuPuzzle.FromGrid(sharedGrid);

            // Act
            var result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveDifferentHints_ReturnsFalse()
        {
            // Arrange
            ShikakuPuzzle sut = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0005, null, null, null, null },
                { null, 0005, null, null, null },
                { null, null, 0005, null, null },
                { null, null, null, 0005, null },
                { null, null, null, null, 0005 }
            });

            ShikakuPuzzle other = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0002, null, null, null, null },
                { null, 0005, null, null, null },
                { null, null, 0005, null, null },
                { null, null, null, 0005, null },
                { null, null, null, null, 0008 }
            });

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
        public void InstanceAndOtherHaveSameHints_ReturnsFalse()
        {
            // Arrange
            var sharedGrid = new int?[,]
            {
                { 0005, null, null, null, null },
                { null, 0005, null, null, null },
                { null, null, 0005, null, null },
                { null, null, null, 0005, null },
                { null, null, null, null, 0005 }
            };

            ShikakuPuzzle sut = ShikakuPuzzle.FromGrid(sharedGrid);
            ShikakuPuzzle other = ShikakuPuzzle.FromGrid(sharedGrid);

            // Act
            var result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveDifferentHints_ReturnsTrue()
        {
            // Arrange
            ShikakuPuzzle sut = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0005, null, null, null, null },
                { null, 0005, null, null, null },
                { null, null, 0005, null, null },
                { null, null, null, 0005, null },
                { null, null, null, null, 0005 }
            });

            ShikakuPuzzle other = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 0002, null, null, null, null },
                { null, 0005, null, null, null },
                { null, null, 0005, null, null },
                { null, null, null, 0005, null },
                { null, null, null, null, 0008 }
            });

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
        [ClassData(typeof(TestCases))]
        public void CanSerializeToJson_ThenDeserialize(int?[,] grid)
        {
            // Arrange
            ShikakuPuzzle original = ShikakuPuzzle.FromGrid(grid);
            JsonSerializerOptions jsonOptions = Invariants.GetJsonSerializerOptions();

            // Act
            var json = JsonSerializer.Serialize(original, jsonOptions);
            var deserialized = JsonSerializer.Deserialize<ShikakuPuzzle>(json, jsonOptions);

            // Assert
            deserialized.Should().NotBeNull().And.Be(original);
        }

        private sealed class TestCases : TheoryData<int?[,]>
        {
            public TestCases()
            {
                Add(new int?[,]
                {
                    { 0025, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null }
                });

                Add(new int?[,]
                {
                    { 0005, null, null, null, null },
                    { null, 0005, null, null, null },
                    { null, null, 0005, null, null },
                    { null, null, null, 0005, null },
                    { null, null, null, null, 0005 }
                });

                Add(new int?[,]
                {
                    { 0007, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, 0018 }
                });

                Add(new int?[,]
                {
                    { null, null, null, null, null },
                    { null, 0010, null, null, null },
                    { null, null, 0003, 0006, null },
                    { null, null, null, null, null },
                    { null, null, null, null, 0006 }
                });
            }
        }
    }
}
