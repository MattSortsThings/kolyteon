using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.Shikaku;

namespace Kolyteon.Tests.Unit.Shikaku;

public static class ShikakuProblemTests
{
    [UnitTest]
    public sealed class EqualsMethod
    {
        [Fact]
        public void Equals_InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            ShikakuProblem sut = ShikakuProblem.FromGrid(new int?[,]
            {
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0025 }
            });

            // Act
            bool result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveSameGridDimensionsAndSameHints_ReturnsTrue()
        {
            // Arrange
            int?[,] sharedGrid =
            {
                { 0005, null, null, null, null },
                { null, null, null, null, null },
                { null, null, 0010, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0010 }
            };

            ShikakuProblem sut = ShikakuProblem.FromGrid(sharedGrid);
            ShikakuProblem other = ShikakuProblem.FromGrid(sharedGrid);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveDifferentHints_ReturnsFalse()
        {
            // Arrange
            ShikakuProblem sut = ShikakuProblem.FromGrid(new int?[,]
            {
                { 0005, null, null, null, null },
                { null, null, null, null, null },
                { null, null, 0010, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0010 }
            });

            ShikakuProblem other = ShikakuProblem.FromGrid(new int?[,]
            {
                { 0003, null, null, null, null },
                { null, null, null, null, null },
                { null, null, 0012, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0010 }
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
            ShikakuProblem? sut = ShikakuProblem.FromGrid(new int?[,]
            {
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0025 }
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
        public void Equality_InstanceAndOtherHaveSameGridDimensionsAndSameHints_ReturnsTrue()
        {
            // Arrange
            int?[,] sharedGrid =
            {
                { 0005, null, null, null, null },
                { null, null, null, null, null },
                { null, null, 0010, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0010 }
            };

            ShikakuProblem sut = ShikakuProblem.FromGrid(sharedGrid);
            ShikakuProblem other = ShikakuProblem.FromGrid(sharedGrid);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveDifferentHints_ReturnsFalse()
        {
            // Arrange
            ShikakuProblem sut = ShikakuProblem.FromGrid(new int?[,]
            {
                { 0005, null, null, null, null },
                { null, null, null, null, null },
                { null, null, 0010, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0010 }
            });

            ShikakuProblem other = ShikakuProblem.FromGrid(new int?[,]
            {
                { 0003, null, null, null, null },
                { null, null, null, null, null },
                { null, null, 0012, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0010 }
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
        public void Inequality_InstanceAndOtherHaveSameGridDimensionsAndSameHints_ReturnsFalse()
        {
            // Arrange
            int?[,] sharedGrid =
            {
                { 0005, null, null, null, null },
                { null, null, null, null, null },
                { null, null, 0010, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0010 }
            };

            ShikakuProblem sut = ShikakuProblem.FromGrid(sharedGrid);
            ShikakuProblem other = ShikakuProblem.FromGrid(sharedGrid);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveDifferentHints_ReturnsTrue()
        {
            // Arrange
            ShikakuProblem sut = ShikakuProblem.FromGrid(new int?[,]
            {
                { 0005, null, null, null, null },
                { null, null, null, null, null },
                { null, null, 0010, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0010 }
            });

            ShikakuProblem other = ShikakuProblem.FromGrid(new int?[,]
            {
                { 0003, null, null, null, null },
                { null, null, null, null, null },
                { null, null, 0012, null, null },
                { null, null, null, null, null },
                { null, null, null, null, 0010 }
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
        public static TheoryData<int?[,], Block, IReadOnlyList<NumberedSquare>> HappyPathTestCases => new()
        {
            {
                new int?[,]
                {
                    { 0025, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null }
                },
                Dimensions.FromWidthAndHeight(5, 5).ToBlock(), [
                    Square.FromColumnAndRow(0, 0).ToNumberedSquare(25)
                ]
            },
            {
                new int?[,]
                {
                    { 0020, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, 0003, null, 0002 }
                },
                Dimensions.FromWidthAndHeight(5, 5).ToBlock(), [
                    Square.FromColumnAndRow(0, 0).ToNumberedSquare(20),
                    Square.FromColumnAndRow(2, 4).ToNumberedSquare(3),
                    Square.FromColumnAndRow(4, 4).ToNumberedSquare(2)
                ]
            },
            {
                new int?[,]
                {
                    { null, 0010, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, 0010, null },
                    { null, null, null, null, null },
                    { null, null, 0003, null, 0002 }
                },
                Dimensions.FromWidthAndHeight(5, 5).ToBlock(), [
                    Square.FromColumnAndRow(1, 0).ToNumberedSquare(10),
                    Square.FromColumnAndRow(2, 4).ToNumberedSquare(3),
                    Square.FromColumnAndRow(3, 2).ToNumberedSquare(10),
                    Square.FromColumnAndRow(4, 4).ToNumberedSquare(2)
                ]
            },
            {
                new int?[,]
                {
                    { 0007, null, null, null, null, null, null },
                    { null, 0007, null, null, null, null, null },
                    { null, null, 0007, null, null, null, null },
                    { null, null, null, 0007, null, null, null },
                    { null, null, null, null, 0007, null, null },
                    { null, null, null, null, null, 0007, null },
                    { 0002, null, null, null, null, null, 0005 }
                },
                Dimensions.FromWidthAndHeight(7, 7).ToBlock(), [
                    Square.FromColumnAndRow(0, 0).ToNumberedSquare(7),
                    Square.FromColumnAndRow(0, 6).ToNumberedSquare(2),
                    Square.FromColumnAndRow(1, 1).ToNumberedSquare(7),
                    Square.FromColumnAndRow(2, 2).ToNumberedSquare(7),
                    Square.FromColumnAndRow(3, 3).ToNumberedSquare(7),
                    Square.FromColumnAndRow(4, 4).ToNumberedSquare(7),
                    Square.FromColumnAndRow(5, 5).ToNumberedSquare(7),
                    Square.FromColumnAndRow(6, 6).ToNumberedSquare(5)
                ]
            }
        };

        [Theory]
        [MemberData(nameof(HappyPathTestCases), MemberType = typeof(FromGridStaticFactoryMethod))]
        public void FromGrid_GivenValidGrid_ReturnsProblemWithCorrectGridAndHints(int?[,] grid,
            Block expectedGrid,
            IReadOnlyList<NumberedSquare> expectedHints)
        {
            // Act
            ShikakuProblem result = ShikakuProblem.FromGrid(grid);

            // Assert
            using (new AssertionScope())
            {
                result.Grid.Should().Be(expectedGrid);
                result.Hints.Should().Equal(expectedHints);
            }
        }

        [Fact]
        public void FromGrid_GridIsNotSquare_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0005, null, null, null, null },
                { null, 0005, null, null, null },
                { null, null, 0005, null, null },
                { null, null, null, 0005, null },
                { null, null, null, null, 0005 },
                { 0005, null, null, null, null },
                { null, 0005, null, null, null },
                { null, null, 0005, null, null },
                { null, null, null, 0005, null },
                { null, null, null, null, 0005 }
            };

            // Act
            Action act = () => ShikakuProblem.FromGrid(grid);

            // Assert
            act.Should().Throw<InvalidProblemException>();
        }

        [Fact]
        public void FromGrid_GridIsSmallerThanFiveByFive_Throws()
        {
            // Arrange
            int?[,] grid = { { 0003, null, null }, { 0003, null, null }, { 0003, null, null } };

            // Act
            Action act = () => ShikakuProblem.FromGrid(grid);

            // Assert
            act.Should().Throw<InvalidProblemException>();
        }

        [Fact]
        public void FromGrid_HintNumberIsSmallerThanTwo_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0005, null, null, null, null },
                { null, 0005, null, null, 0001 },
                { null, null, 0005, null, null },
                { null, null, null, 0005, null },
                { null, null, null, null, 0004 }
            };

            // Act
            Action act = () => ShikakuProblem.FromGrid(grid);

            // Assert
            act.Should().Throw<InvalidProblemException>();
        }

        [Fact]
        public void FromGrid_ZeroHints_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null }
            };

            // Act
            Action act = () => ShikakuProblem.FromGrid(grid);

            // Assert
            act.Should().Throw<InvalidProblemException>();
        }

        [Fact]
        public void FromGrid_HintsSumToLessThanGridArea_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0005, null, null, null, null },
                { null, 0005, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null }
            };

            // Act
            Action act = () => ShikakuProblem.FromGrid(grid);

            // Assert
            act.Should().Throw<InvalidProblemException>();
        }

        [Fact]
        public void FromGrid_HintsSumToMoreThanGridArea_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0005, null, null, null, 0100 },
                { null, 0005, null, null, null },
                { null, null, 0005, null, null },
                { null, null, null, 0005, null },
                { null, null, null, null, 0005 }
            };

            // Act
            Action act = () => ShikakuProblem.FromGrid(grid);

            // Assert
            act.Should().Throw<InvalidProblemException>();
        }
    }

    [UnitTest]
    [SuppressMessage("ReSharper", "UseCollectionExpression")]
    public sealed class Serialization
    {
        public static TheoryData<int?[,]> TestCases() => new()
        {
            new int?[,]
            {
                { 0005, null, null, null, null },
                { null, 0005, null, null, null },
                { null, null, 0005, null, null },
                { null, null, null, 0005, null },
                { null, null, null, null, 0005 }
            },
            new int?[,]
            {
                { 0020, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, 0003, null, 0002 }
            },
            new int?[,]
            {
                { 0007, null, null, null, null, null, null },
                { null, 0007, null, null, null, null, null },
                { null, null, 0007, null, null, null, null },
                { null, null, null, 0007, null, null, null },
                { null, null, null, null, 0007, null, null },
                { null, null, null, null, null, 0007, null },
                { 0002, null, null, null, null, null, 0005 }
            }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(Serialization))]
        public void CanSerializeToJson_ThenDeserializeToInstanceWithEqualValue(int?[,] grid)
        {
            // Arrange
            ShikakuProblem originalProblem = ShikakuProblem.FromGrid(grid);

            string json = JsonSerializer.Serialize(originalProblem, JsonSerializerOptions.Default);

            // Act
            ShikakuProblem? deserializedProblem =
                JsonSerializer.Deserialize<ShikakuProblem>(json, JsonSerializerOptions.Default);

            // Assert
            deserializedProblem.Should().NotBeNull().And.Be(originalProblem);
        }
    }
}
