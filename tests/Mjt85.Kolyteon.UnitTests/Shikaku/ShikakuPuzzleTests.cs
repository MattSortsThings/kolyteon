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
                { 5, null, null, null, null },
                { null, 5, null, null, null },
                { null, null, 5, null, null },
                { null, null, null, 5, null },
                { null, null, null, null, 5 }
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
                { 5, null, null, null, null },
                { null, 5, null, null, null },
                { null, null, 5, null, null },
                { null, null, null, 5, null },
                { null, null, null, null, 5 }
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
                { 5, null, null, null, null },
                { null, 5, null, null, null },
                { null, null, 5, null, null },
                { null, null, null, 5, null },
                { null, null, null, null, 5 }
            });

            ShikakuPuzzle other = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 2, null, null, null, null },
                { null, 5, null, null, null },
                { null, null, 5, null, null },
                { null, null, null, 5, null },
                { null, null, null, null, 8 }
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
                { 5, null, null, null, null },
                { null, 5, null, null, null },
                { null, null, 5, null, null },
                { null, null, null, 5, null },
                { null, null, null, null, 5 }
            });

            // Act
            var result = sut.Equals(null);

            // Assert
            result.Should().BeFalse();
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
                { 3, null, null },
                { null, 3, null },
                { null, null, 3 }
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
                { 3, null, null },
                { null, 3, null },
                { null, null, 3 },
                { 3, null, null },
                { null, 3, null },
                { null, null, 3 },
                { 3, null, null },
                { null, 3, null },
                { null, null, 3 }
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
                { 24, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, 1, null }
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
                { 10, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 10 }
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
                { 25, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 2 }
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
                    { 25, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null }
                }, 5, [
                    new Hint(0, 0, 25)
                ]);

                Add(new int?[,]
                {
                    { 5, null, null, null, null },
                    { null, 5, null, null, null },
                    { null, null, 5, null, null },
                    { null, null, null, 5, null },
                    { null, null, null, null, 5 }
                }, 5, [
                    new Hint(0, 0, 5),
                    new Hint(1, 1, 5),
                    new Hint(2, 2, 5),
                    new Hint(3, 3, 5),
                    new Hint(4, 4, 5)
                ]);

                Add(new int?[,]
                {
                    { 7, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, 18 }
                }, 5, [
                    new Hint(0, 0, 7),
                    new Hint(4, 4, 18)
                ]);

                Add(new int?[,]
                {
                    { null, null, null, null, null },
                    { null, 10, null, null, null },
                    { null, null, 3, 6, null },
                    { null, null, null, null, null },
                    { null, null, null, null, 6 }
                }, 5, [
                    new Hint(1, 1, 10),
                    new Hint(2, 2, 3),
                    new Hint(3, 2, 6),
                    new Hint(4, 4, 6)
                ]);

                Add(new int?[,]
                {
                    { null, null, 7, null, null, null, null },
                    { null, null, null, null, null, 7, null },
                    { 8, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null },
                    { 7, null, null, null, null, null, null },
                    { null, null, 14, null, null, null, null },
                    { null, null, null, null, 6, null, null }
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
                { 5, null, null, null, null },
                { null, 5, null, null, null },
                { null, null, 5, null, null },
                { null, null, null, 5, null },
                { null, null, null, null, 5 }
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
                { 5, null, null, null, null },
                { null, 5, null, null, null },
                { null, null, 5, null, null },
                { null, null, null, 5, null },
                { null, null, null, null, 5 }
            });

            ShikakuPuzzle other = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 2, null, null, null, null },
                { null, 5, null, null, null },
                { null, null, 5, null, null },
                { null, null, null, 5, null },
                { null, null, null, null, 8 }
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
                { 5, null, null, null, null },
                { null, 5, null, null, null },
                { null, null, 5, null, null },
                { null, null, null, 5, null },
                { null, null, null, null, 5 }
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
                { 5, null, null, null, null },
                { null, 5, null, null, null },
                { null, null, 5, null, null },
                { null, null, null, 5, null },
                { null, null, null, null, 5 }
            });

            ShikakuPuzzle other = ShikakuPuzzle.FromGrid(new int?[,]
            {
                { 2, null, null, null, null },
                { null, 5, null, null, null },
                { null, null, 5, null, null },
                { null, null, null, 5, null },
                { null, null, null, null, 8 }
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
                    { 25, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null }
                });

                Add(new int?[,]
                {
                    { 5, null, null, null, null },
                    { null, 5, null, null, null },
                    { null, null, 5, null, null },
                    { null, null, null, 5, null },
                    { null, null, null, null, 5 }
                });

                Add(new int?[,]
                {
                    { 7, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, 18 }
                });

                Add(new int?[,]
                {
                    { null, null, null, null, null },
                    { null, 10, null, null, null },
                    { null, null, 3, 6, null },
                    { null, null, null, null, null },
                    { null, null, null, null, 6 }
                });
            }
        }
    }
}
