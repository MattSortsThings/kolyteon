using System.Collections;
using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.Shikaku;
using Kolyteon.Tests.Utils.TestAssertions;

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
    public sealed class VerifyCorrectMethod
    {
        public static TheoryData<ShikakuProblem, IReadOnlyList<Block>> PositiveTestCases => new()
        {
            {
                ShikakuProblem.FromGrid(new int?[,]
                {
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, 0025, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null }
                }),
                [
                    Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(5, 5))
                ]
            },
            {
                ShikakuProblem.FromGrid(new int?[,]
                {
                    { 0010, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, 0005, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, 0010 }
                }),
                [
                    Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(5, 2)),
                    Square.FromColumnAndRow(0, 2).ToBlock(Dimensions.FromWidthAndHeight(5, 1)),
                    Square.FromColumnAndRow(0, 3).ToBlock(Dimensions.FromWidthAndHeight(5, 2))
                ]
            },
            {
                ShikakuProblem.FromGrid(new int?[,]
                {
                    { 0010, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, 0005, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, 0010 }
                }),
                [
                    Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(2, 5)),
                    Square.FromColumnAndRow(2, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 5)),
                    Square.FromColumnAndRow(3, 0).ToBlock(Dimensions.FromWidthAndHeight(2, 5))
                ]
            },
            {
                ShikakuProblem.FromGrid(new int?[,]
                {
                    { 0010, null, 0003, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, 0012 }
                }),
                [
                    Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(2, 5)),
                    Square.FromColumnAndRow(2, 0).ToBlock(Dimensions.FromWidthAndHeight(3, 1)),
                    Square.FromColumnAndRow(2, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 4))
                ]
            },
            {
                ShikakuProblem.FromGrid(new int?[,]
                {
                    { 0005, null, 0004, null, null },
                    { null, null, null, 0004, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, 0012 }
                }),
                [
                    Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 5)),
                    Square.FromColumnAndRow(1, 0).ToBlock(Dimensions.FromWidthAndHeight(4, 1)),
                    Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(4, 1)),
                    Square.FromColumnAndRow(1, 2).ToBlock(Dimensions.FromWidthAndHeight(4, 3))
                ]
            },
            {
                ShikakuProblem.FromGrid(new int?[,]
                {
                    { 0005, null, 0004, null, null },
                    { null, null, null, 0004, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, 0012 }
                }),
                [
                    Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 5)),
                    Square.FromColumnAndRow(1, 0).ToBlock(Dimensions.FromWidthAndHeight(2, 2)),
                    Square.FromColumnAndRow(1, 2).ToBlock(Dimensions.FromWidthAndHeight(4, 3)),
                    Square.FromColumnAndRow(3, 0).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
                ]
            },
            {
                ShikakuProblem.FromGrid(new int?[,]
                {
                    { 0005, 0002, 0002, null, null },
                    { null, null, null, 0004, null },
                    { null, 0008, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, 0004 }
                }),
                [
                    Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 5)),
                    Square.FromColumnAndRow(1, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 2)),
                    Square.FromColumnAndRow(1, 2).ToBlock(Dimensions.FromWidthAndHeight(4, 2)),
                    Square.FromColumnAndRow(1, 4).ToBlock(Dimensions.FromWidthAndHeight(4, 1)),
                    Square.FromColumnAndRow(2, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 2)),
                    Square.FromColumnAndRow(3, 0).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
                ]
            }
        };

        [Theory]
        [MemberData(nameof(PositiveTestCases), MemberType = typeof(VerifyCorrectMethod))]
        public void VerifyCorrect_GivenCorrectSolution_ReturnsSuccessfulResult(ShikakuProblem sut, IReadOnlyList<Block> solution)
        {
            // Act
            Result result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeSuccessful().And.HaveNullFirstError();
        }

        [Fact]
        public void VerifyCorrect_SolutionIsEmptyList_ReturnsUnsuccessfulResult()
        {
            // Arrange
            ShikakuProblem sut = ShikakuProblem.FromGrid(new int?[,]
            {
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, 0025, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null }
            });

            IReadOnlyList<Block> solution = [];

            // Act
            Result result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeUnsuccessful()
                .And.HaveFirstError("Solution has 0 blocks, but problem has 1 hint.");
        }

        [Fact]
        public void VerifyCorrect_SolutionHasTooFewItems_ReturnsUnsuccessfulResult()
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

            IReadOnlyList<Block> solution =
            [
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(5, 1)),
                Square.FromColumnAndRow(0, 1).ToBlock(Dimensions.FromWidthAndHeight(5, 2))
            ];

            // Act
            Result result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeUnsuccessful()
                .And.HaveFirstError("Solution has 2 blocks, but problem has 3 hints.");
        }

        [Fact]
        public void VerifyCorrect_SolutionHasTooManyItems_ReturnsUnsuccessfulResult()
        {
            // Arrange
            ShikakuProblem sut = ShikakuProblem.FromGrid(new int?[,]
            {
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, 0025, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null }
            });

            IReadOnlyList<Block> solution =
            [
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(5, 1)),
                Square.FromColumnAndRow(0, 1).ToBlock(Dimensions.FromWidthAndHeight(5, 2))
            ];

            // Act
            Result result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeUnsuccessful()
                .And.HaveFirstError("Solution has 2 blocks, but problem has 1 hint.");
        }

        [Fact]
        public void VerifyCorrect_BlockAreasDoNotSumToGridArea_ReturnsUnsuccessfulResult()
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

            IReadOnlyList<Block> solution =
            [
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(3, 1)),
                Square.FromColumnAndRow(0, 1).ToBlock(Dimensions.FromWidthAndHeight(5, 2)),
                Square.FromColumnAndRow(3, 3).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            ];

            // Act
            Result result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeUnsuccessful()
                .And.HaveFirstError("Block areas sum to 17, but grid area is 25.");
        }

        [Fact]
        public void VerifyCorrect_BlockIsNotInsideGrid_ReturnsUnsuccessfulResult()
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

            IReadOnlyList<Block> solution =
            [
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(5, 1)),
                Square.FromColumnAndRow(0, 1).ToBlock(Dimensions.FromWidthAndHeight(5, 2)),
                Square.FromColumnAndRow(3, 3).ToBlock(Dimensions.FromWidthAndHeight(5, 2))
            ];

            // Act
            Result result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeUnsuccessful()
                .And.HaveFirstError("Block (3,3) [5x2] is not inside grid (0,0) [5x5].");
        }

        [Fact]
        public void VerifyCorrect_OverlappingBlocks_ReturnsUnsuccessfulResult()
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

            IReadOnlyList<Block> solution =
            [
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(5, 1)),
                Square.FromColumnAndRow(0, 1).ToBlock(Dimensions.FromWidthAndHeight(5, 2)),
                Square.FromColumnAndRow(3, 0).ToBlock(Dimensions.FromWidthAndHeight(2, 5))
            ];

            // Act
            Result result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeUnsuccessful()
                .And.HaveFirstError("Blocks (0,0) [5x1] and (3,0) [2x5] overlap.");
        }

        [Fact]
        public void VerifyCorrect_BlockContainsMoreThanOneHint_ReturnsUnsuccessfulResult()
        {
            // Arrange
            ShikakuProblem sut = ShikakuProblem.FromGrid(new int?[,]
            {
                { 0005, null, null, null, null },
                { null, null, null, null, null },
                { null, null, 0010, 0010, null },
                { null, null, null, null, null },
                { null, null, null, null, null }
            });

            IReadOnlyList<Block> solution =
            [
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(5, 1)),
                Square.FromColumnAndRow(0, 1).ToBlock(Dimensions.FromWidthAndHeight(5, 2)),
                Square.FromColumnAndRow(0, 3).ToBlock(Dimensions.FromWidthAndHeight(5, 2))
            ];

            // Act
            Result result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeUnsuccessful()
                .And.HaveFirstError("Block (0,1) [5x2] contains more than one hint.");
        }

        [Fact]
        public void VerifyCorrect_BlockContainsHintWithNumberUnequalToItsArea_ReturnsUnsuccessfulResult()
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

            IReadOnlyList<Block> solution =
            [
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(5, 1)),
                Square.FromColumnAndRow(0, 1).ToBlock(Dimensions.FromWidthAndHeight(5, 3)),
                Square.FromColumnAndRow(0, 4).ToBlock(Dimensions.FromWidthAndHeight(5, 1))
            ];

            // Act
            Result result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeUnsuccessful()
                .And.HaveFirstError("Block (0,1) [5x3] contains hint (2,2) [10] with number not equal to its area.");
        }

        [Fact]
        public void VerifyCorrect_SolutionArgIsNull_Throws()
        {
            // Arrange
            ShikakuProblem sut = ShikakuProblem.FromGrid(new int?[,]
            {
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, 0025, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null }
            });

            // Act
            Action act = () => sut.VerifyCorrect(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'solution')");
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
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Invalid problem grid dimensions [5x10]. Grid must be a square no smaller than 5x5.");
        }

        [Fact]
        public void FromGrid_GridIsSmallerThanFiveByFive_Throws()
        {
            // Arrange
            int?[,] grid = { { 0003, null, null }, { 0003, null, null }, { 0003, null, null } };

            // Act
            Action act = () => ShikakuProblem.FromGrid(grid);

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Invalid problem grid dimensions [3x3]. Grid must be a square no smaller than 5x5.");
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
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Invalid hint (4,1) [1]. Hint number must be not less than 2.");
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
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Problem has zero hints.");
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
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Hint numbers sum to 10, but grid area is 25. Hint numbers must sum to grid area.");
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
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Hint numbers sum to 125, but grid area is 25. Hint numbers must sum to grid area.");
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        [Theory]
        [ClassData(typeof(TestCases))]
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

        public sealed class TestCases : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return
                [
                    new int?[,]
                    {
                        { 0005, null, null, null, null },
                        { null, 0005, null, null, null },
                        { null, null, 0005, null, null },
                        { null, null, null, 0005, null },
                        { null, null, null, null, 0005 }
                    }
                ];

                yield return
                [
                    new int?[,]
                    {
                        { 0020, null, null, null, null },
                        { null, null, null, null, null },
                        { null, null, null, null, null },
                        { null, null, null, null, null },
                        { null, null, 0003, null, 0002 }
                    }
                ];

                yield return
                [
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
                ];
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
