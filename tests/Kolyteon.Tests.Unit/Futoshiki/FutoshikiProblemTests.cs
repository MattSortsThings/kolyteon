using Kolyteon.Common;
using Kolyteon.Futoshiki;

namespace Kolyteon.Tests.Unit.Futoshiki;

public static class FutoshikiProblemTests
{
    [UnitTest]
    public sealed class EqualsMethod
    {
        private static readonly GreaterThanSign FixedGreaterThanSign =
            GreaterThanSign.Between(Square.FromColumnAndRow(0, 0), Square.FromColumnAndRow(0, 1));

        private static readonly LessThanSign FixedLessThanSign =
            LessThanSign.Between(Square.FromColumnAndRow(3, 2), Square.FromColumnAndRow(3, 3));

        [Fact]
        public void Equals_InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            FutoshikiProblem sut = FutoshikiProblem.Create()
                .FromGrid(new int?[,]
                {
                    { 0001, null, null, null },
                    { null, 0002, null, null },
                    { null, null, 0003, null },
                    { null, null, null, 0004 }
                })
                .AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            // Act
            bool result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveEqualGridValuesAndSameFilledSquaresAndSameSigns_ReturnsTrue()
        {
            // Arrange
            int?[,] sharedGrid =
            {
                { 0001, null, null, null },
                { null, 0002, null, null },
                { null, null, 0003, null },
                { null, null, null, 0004 }
            };

            FutoshikiProblem sut = FutoshikiProblem.Create()
                .FromGrid(sharedGrid)
                .AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            FutoshikiProblem other = FutoshikiProblem.Create()
                .FromGrid(sharedGrid)
                .AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalGridValues_ReturnsFalse()
        {
            // Arrange
            FutoshikiProblem sut = FutoshikiProblem.Create()
                .FromGrid(new int?[,]
                {
                    { 0001, null, null, null },
                    { null, 0002, null, null },
                    { null, null, 0003, null },
                    { null, null, null, 0004 }
                })
                .AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            FutoshikiProblem other = FutoshikiProblem.Create()
                .FromGrid(new int?[,]
                {
                    { 0001, null, null, null, null },
                    { null, 0002, null, null, null },
                    { null, null, 0003, null, null },
                    { null, null, null, 0004, null },
                    { null, null, null, null, null }
                })
                .AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalFilledSquares_ReturnsFalse()
        {
            // Arrange
            FutoshikiProblem sut = FutoshikiProblem.Create()
                .FromGrid(new int?[,]
                {
                    { 0001, null, null, null },
                    { null, 0002, null, null },
                    { null, null, 0003, null },
                    { null, null, null, 0004 }
                }).AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            FutoshikiProblem other = FutoshikiProblem.Create()
                .FromGrid(new int?[,]
                {
                    { 0001, null, null, null },
                    { null, 0002, null, null },
                    { null, null, 0004, null },
                    { null, null, null, 0003 }
                }).AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalGreaterThanSigns_ReturnsFalse()
        {
            // Arrange
            int?[,] sharedGrid =
            {
                { 0001, null, null, null },
                { null, 0002, null, null },
                { null, null, 0003, null },
                { null, null, null, 0004 }
            };

            FutoshikiProblem sut = FutoshikiProblem.Create()
                .FromGrid(sharedGrid)
                .AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            FutoshikiProblem other = FutoshikiProblem.Create()
                .FromGrid(sharedGrid)
                .AddSign(FixedLessThanSign)
                .Build();

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalLessThanSigns_ReturnsFalse()
        {
            // Arrange
            int?[,] sharedGrid =
            {
                { 0001, null, null, null },
                { null, 0002, null, null },
                { null, null, 0003, null },
                { null, null, null, 0004 }
            };

            FutoshikiProblem sut = FutoshikiProblem.Create()
                .FromGrid(sharedGrid)
                .AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            FutoshikiProblem other = FutoshikiProblem.Create()
                .FromGrid(sharedGrid)
                .AddSign(FixedGreaterThanSign)
                .Build();

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_OtherArgIsNull_ReturnsFalse()
        {
            // Arrange
            FutoshikiProblem sut = FutoshikiProblem.Create()
                .FromGrid(new int?[,]
                {
                    { null, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null }
                })
                .Build();

            // Act
            bool result = sut.Equals(null);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class EqualityOperator
    {
        private static readonly GreaterThanSign FixedGreaterThanSign =
            GreaterThanSign.Between(Square.FromColumnAndRow(0, 0), Square.FromColumnAndRow(0, 1));

        private static readonly LessThanSign FixedLessThanSign =
            LessThanSign.Between(Square.FromColumnAndRow(3, 2), Square.FromColumnAndRow(3, 3));

        [Fact]
        public void Equality_InstanceAndOtherHaveEqualGridValuesAndSameFilledSquaresAndSameSigns_ReturnsTrue()
        {
            // Arrange
            int?[,] sharedGrid =
            {
                { 0001, null, null, null },
                { null, 0002, null, null },
                { null, null, 0003, null },
                { null, null, null, 0004 }
            };

            FutoshikiProblem sut = FutoshikiProblem.Create()
                .FromGrid(sharedGrid)
                .AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            FutoshikiProblem other = FutoshikiProblem.Create()
                .FromGrid(sharedGrid)
                .AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalGridValues_ReturnsFalse()
        {
            // Arrange
            FutoshikiProblem sut = FutoshikiProblem.Create()
                .FromGrid(new int?[,]
                {
                    { 0001, null, null, null },
                    { null, 0002, null, null },
                    { null, null, 0003, null },
                    { null, null, null, 0004 }
                })
                .AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            FutoshikiProblem other = FutoshikiProblem.Create()
                .FromGrid(new int?[,]
                {
                    { 0001, null, null, null, null },
                    { null, 0002, null, null, null },
                    { null, null, 0003, null, null },
                    { null, null, null, 0004, null },
                    { null, null, null, null, null }
                })
                .AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalFilledSquares_ReturnsFalse()
        {
            // Arrange
            FutoshikiProblem sut = FutoshikiProblem.Create()
                .FromGrid(new int?[,]
                {
                    { 0001, null, null, null },
                    { null, 0002, null, null },
                    { null, null, 0003, null },
                    { null, null, null, 0004 }
                }).AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            FutoshikiProblem other = FutoshikiProblem.Create()
                .FromGrid(new int?[,]
                {
                    { 0001, null, null, null },
                    { null, 0002, null, null },
                    { null, null, 0004, null },
                    { null, null, null, 0003 }
                }).AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalGreaterThanSigns_ReturnsFalse()
        {
            // Arrange
            int?[,] sharedGrid =
            {
                { 0001, null, null, null },
                { null, 0002, null, null },
                { null, null, 0003, null },
                { null, null, null, 0004 }
            };

            FutoshikiProblem sut = FutoshikiProblem.Create()
                .FromGrid(sharedGrid)
                .AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            FutoshikiProblem other = FutoshikiProblem.Create()
                .FromGrid(sharedGrid)
                .AddSign(FixedLessThanSign)
                .Build();

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalLessThanSigns_ReturnsFalse()
        {
            // Arrange
            int?[,] sharedGrid =
            {
                { 0001, null, null, null },
                { null, 0002, null, null },
                { null, null, 0003, null },
                { null, null, null, 0004 }
            };

            FutoshikiProblem sut = FutoshikiProblem.Create()
                .FromGrid(sharedGrid)
                .AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            FutoshikiProblem other = FutoshikiProblem.Create()
                .FromGrid(sharedGrid)
                .AddSign(FixedGreaterThanSign)
                .Build();

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class InequalityOperator
    {
        private static readonly GreaterThanSign FixedGreaterThanSign =
            GreaterThanSign.Between(Square.FromColumnAndRow(0, 0), Square.FromColumnAndRow(0, 1));

        private static readonly LessThanSign FixedLessThanSign =
            LessThanSign.Between(Square.FromColumnAndRow(3, 2), Square.FromColumnAndRow(3, 3));

        [Fact]
        public void Inequality_InstanceAndOtherHaveEqualGridValuesAndSameFilledSquaresAndSameSigns_ReturnsFalse()
        {
            // Arrange
            int?[,] sharedGrid =
            {
                { 0001, null, null, null },
                { null, 0002, null, null },
                { null, null, 0003, null },
                { null, null, null, 0004 }
            };

            FutoshikiProblem sut = FutoshikiProblem.Create()
                .FromGrid(sharedGrid)
                .AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            FutoshikiProblem other = FutoshikiProblem.Create()
                .FromGrid(sharedGrid)
                .AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalGridValues_ReturnsTrue()
        {
            // Arrange
            FutoshikiProblem sut = FutoshikiProblem.Create()
                .FromGrid(new int?[,]
                {
                    { 0001, null, null, null },
                    { null, 0002, null, null },
                    { null, null, 0003, null },
                    { null, null, null, 0004 }
                })
                .AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            FutoshikiProblem other = FutoshikiProblem.Create()
                .FromGrid(new int?[,]
                {
                    { 0001, null, null, null, null },
                    { null, 0002, null, null, null },
                    { null, null, 0003, null, null },
                    { null, null, null, 0004, null },
                    { null, null, null, null, null }
                })
                .AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalFilledSquares_ReturnsTrue()
        {
            // Arrange
            FutoshikiProblem sut = FutoshikiProblem.Create()
                .FromGrid(new int?[,]
                {
                    { 0001, null, null, null },
                    { null, 0002, null, null },
                    { null, null, 0003, null },
                    { null, null, null, 0004 }
                }).AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            FutoshikiProblem other = FutoshikiProblem.Create()
                .FromGrid(new int?[,]
                {
                    { 0001, null, null, null },
                    { null, 0002, null, null },
                    { null, null, 0004, null },
                    { null, null, null, 0003 }
                }).AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalGreaterThanSigns_ReturnsTrue()
        {
            // Arrange
            int?[,] sharedGrid =
            {
                { 0001, null, null, null },
                { null, 0002, null, null },
                { null, null, 0003, null },
                { null, null, null, 0004 }
            };

            FutoshikiProblem sut = FutoshikiProblem.Create()
                .FromGrid(sharedGrid)
                .AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            FutoshikiProblem other = FutoshikiProblem.Create()
                .FromGrid(sharedGrid)
                .AddSign(FixedLessThanSign)
                .Build();

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalLessThanSigns_ReturnsTrue()
        {
            // Arrange
            int?[,] sharedGrid =
            {
                { 0001, null, null, null },
                { null, 0002, null, null },
                { null, null, 0003, null },
                { null, null, null, 0004 }
            };

            FutoshikiProblem sut = FutoshikiProblem.Create()
                .FromGrid(sharedGrid)
                .AddSign(FixedGreaterThanSign)
                .AddSign(FixedLessThanSign)
                .Build();

            FutoshikiProblem other = FutoshikiProblem.Create()
                .FromGrid(sharedGrid)
                .AddSign(FixedGreaterThanSign)
                .Build();

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class FluentBuilder
    {
        [Fact]
        public void FluentBuilder_GivenGridWithAllNullValuesAndNoSigns_ReturnsInstanceWithNoFilledSquaresAndNoSigns()
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
            FutoshikiProblem result = FutoshikiProblem.Create().FromGrid(grid).Build();

            // Assert
            using (new AssertionScope())
            {
                result.MaxNumber.Should().Be(5);
                result.Grid.Should().Be(Dimensions.FromWidthAndHeight(5, 5).ToBlock());
                result.FilledSquares.Should().BeEmpty();
                result.GreaterThanSigns.Should().BeEmpty();
                result.LessThanSigns.Should().BeEmpty();
            }
        }

        [Fact]
        public void FluentBuilder_GivenGridAndSigns_ReturnsInstanceWithCorrectFilledSquaresAndSignsIgnoringDuplicateSigns()
        {
            // Arrange
            int?[,] grid =
            {
                { 0001, null, null, null },
                { null, 0002, null, null },
                { null, null, null, null },
                { null, null, null, null }
            };

            GreaterThanSign greaterThanA =
                GreaterThanSign.Between(Square.FromColumnAndRow(1, 1), Square.FromColumnAndRow(1, 2));
            GreaterThanSign greaterThanB =
                GreaterThanSign.Between(Square.FromColumnAndRow(1, 1), Square.FromColumnAndRow(2, 1));

            LessThanSign lessThan = LessThanSign.Between(Square.FromColumnAndRow(0, 0), Square.FromColumnAndRow(0, 1));

            // Act
            FutoshikiProblem result = FutoshikiProblem.Create().FromGrid(grid)
                .AddSign(greaterThanB)
                .AddSign(greaterThanA)
                .AddSign(lessThan)
                .AddSign(greaterThanA)
                .Build();

            // Assert
            using (new AssertionScope())
            {
                result.MaxNumber.Should().Be(4);
                result.Grid.Should().Be(Dimensions.FromWidthAndHeight(4, 4).ToBlock());
                result.FilledSquares.Should().Equal(
                    Square.FromColumnAndRow(0, 0).ToNumberedSquare(1),
                    Square.FromColumnAndRow(1, 1).ToNumberedSquare(2)
                );
                result.GreaterThanSigns.Should().Equal(greaterThanA, greaterThanB);
                result.LessThanSigns.Should().Equal(lessThan);
            }
        }

        [Fact]
        public void FluentBuilder_ZeroEmptySquares_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0001, 0002, 0003, 0004 },
                { 0002, 0003, 0004, 0001 },
                { 0003, 0004, 0001, 0002 },
                { 0004, 0001, 0002, 0003 }
            };

            // Act
            Action act = () => FutoshikiProblem.Create().FromGrid(grid).Build();

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Problem has zero empty squares.");
        }

        [Fact]
        public void FluentBuilder_FilledSquareHasNumberLessThanOne_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0001, null, null, 0000 },
                { null, null, null, null },
                { null, null, null, null },
                { null, null, null, null }
            };

            // Act
            Action act = () => FutoshikiProblem.Create().FromGrid(grid).Build();

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Invalid filled square (3,0) [0]. Number must be in the range [1,4].");
        }

        [Fact]
        public void FluentBuilder_FilledSquareHasNumberGreaterThanGridSideLength_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0001, null, null, 0005 },
                { null, null, null, null },
                { null, null, null, null },
                { null, null, null, null }
            };

            // Act
            Action act = () => FutoshikiProblem.Create().FromGrid(grid).Build();

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Invalid filled square (3,0) [5]. Number must be in the range [1,4].");
        }

        [Fact]
        public void FluentBuilder_DuplicateNumberInColumn_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0001, null, null, 0004 },
                { null, null, null, null },
                { null, null, null, null },
                { null, null, null, 0004 }
            };

            // Act
            Action act = () => FutoshikiProblem.Create().FromGrid(grid).Build();

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Number 4 occurs more than once in column 3.");
        }

        [Fact]
        public void FluentBuilder_DuplicateNumberInRow_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 0001, null, null, 0001 },
                { null, null, null, null },
                { null, null, null, null },
                { null, null, null, null }
            };

            // Act
            Action act = () => FutoshikiProblem.Create().FromGrid(grid).Build();

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Number 1 occurs more than once in row 0.");
        }

        [Fact]
        public void FluentBuilder_GreaterThanSignHasFirstSquareOutsideGrid_Throws()
        {
            // Act
            Action act = () => FutoshikiProblem.Create()
                .FromGrid(new int?[,]
                {
                    { 0001, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null }
                }).AddSign(GreaterThanSign.Between(Square.FromColumnAndRow(4, 4), Square.FromColumnAndRow(4, 5)))
                .Build();

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Sign (4,4)>(4,5) is not inside grid (0,0) [4x4].");
        }

        [Fact]
        public void FluentBuilder_GreaterThanSignHasSecondSquareOutsideGrid_Throws()
        {
            // Act
            Action act = () => FutoshikiProblem.Create()
                .FromGrid(new int?[,]
                {
                    { 0001, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null }
                }).AddSign(GreaterThanSign.Between(Square.FromColumnAndRow(3, 3), Square.FromColumnAndRow(3, 4)))
                .Build();

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Sign (3,3)>(3,4) is not inside grid (0,0) [4x4].");
        }

        [Fact]
        public void FluentBuilder_LessThanSignHasFirstSquareOutsideGrid_Throws()
        {
            // Act
            Action act = () => FutoshikiProblem.Create()
                .FromGrid(new int?[,]
                {
                    { 0001, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null }
                }).AddSign(LessThanSign.Between(Square.FromColumnAndRow(4, 4), Square.FromColumnAndRow(4, 5)))
                .Build();

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Sign (4,4)<(4,5) is not inside grid (0,0) [4x4].");
        }

        [Fact]
        public void FluentBuilder_LessThanSignHasSecondSquareOutsideGrid_Throws()
        {
            // Act
            Action act = () => FutoshikiProblem.Create()
                .FromGrid(new int?[,]
                {
                    { 0001, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null }
                }).AddSign(LessThanSign.Between(Square.FromColumnAndRow(3, 3), Square.FromColumnAndRow(3, 4)))
                .Build();

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Sign (3,3)<(3,4) is not inside grid (0,0) [4x4].");
        }

        [Fact]
        public void FluentBuilder_GreaterThanSignAndLessThanSignInSameLocation_Throws()
        {
            // Act
            Action act = () => FutoshikiProblem.Create()
                .FromGrid(new int?[,]
                {
                    { 0001, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null },
                    { null, null, null, null }
                }).AddSign(GreaterThanSign.Between(Square.FromColumnAndRow(0, 0), Square.FromColumnAndRow(0, 1)))
                .AddSign(LessThanSign.Between(Square.FromColumnAndRow(0, 0), Square.FromColumnAndRow(0, 1)))
                .Build();

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Signs (0,0)>(0,1) and (0,0)<(0,1) have same location.");
        }

        [Fact]
        public void FluentBuilder_GridArgIsNull_Throws()
        {
            // Act
            Action act = () => FutoshikiProblem.Create().FromGrid(null!).Build();

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'grid')");
        }

        [Fact]
        public void FluentBuilder_GridArgHasRankOneLengthNotEqualToRankOneLength_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null },
                { null, null, null, null, null }
            };

            // Act
            Action act = () => FutoshikiProblem.Create().FromGrid(grid).Build();

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Invalid grid dimensions. Rank-0 and rank-1 lengths must be equal to each other, " +
                             "not less than 4, and not greater than 9.");
        }

        [Fact]
        public void FluentBuilder_GridArgHasRankZeroLengthLessThanFour_Throws()
        {
            // Arrange
            int?[,] grid = { { null, null, null, null }, { null, null, null, null }, { null, null, null, null } };

            // Act
            Action act = () => FutoshikiProblem.Create().FromGrid(grid).Build();

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Invalid grid dimensions. Rank-0 and rank-1 lengths must be equal to each other, " +
                             "not less than 4, and not greater than 9.");
        }

        [Fact]
        public void FluentBuilder_GridArgHasRankZeroLengthGreaterThanNine_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { null, null, null, null },
                { null, null, null, null },
                { null, null, null, null },
                { null, null, null, null },
                { null, null, null, null },
                { null, null, null, null },
                { null, null, null, null },
                { null, null, null, null },
                { null, null, null, null },
                { null, null, null, null }
            };

            // Act
            Action act = () => FutoshikiProblem.Create().FromGrid(grid).Build();

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Invalid grid dimensions. Rank-0 and rank-1 lengths must be equal to each other, " +
                             "not less than 4, and not greater than 9.");
        }

        [Fact]
        public void FluentBuilder_GridArgHasRankOneLengthLessThanFour_Throws()
        {
            // Arrange
            int?[,] grid = { { null, null, null }, { null, null, null }, { null, null, null }, { null, null, null } };

            // Act
            Action act = () => FutoshikiProblem.Create().FromGrid(grid).Build();

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Invalid grid dimensions. Rank-0 and rank-1 lengths must be equal to each other, " +
                             "not less than 4, and not greater than 9.");
        }

        [Fact]
        public void FluentBuilder_GridArgHasRankOneLengthGreaterThanNine_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null }
            };

            // Act
            Action act = () => FutoshikiProblem.Create().FromGrid(grid).Build();

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Invalid grid dimensions. Rank-0 and rank-1 lengths must be equal to each other, " +
                             "not less than 4, and not greater than 9.");
        }

        [Fact]
        public void FluentBuilder_AddingNullGreaterThanSign_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { null, null, null, null },
                { null, null, null, null },
                { null, null, null, null },
                { null, null, null, null }
            };

            // Act
            Action act = () => FutoshikiProblem.Create().FromGrid(grid).AddSign((GreaterThanSign)null!).Build();

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'sign')");
        }

        [Fact]
        public void FluentBuilder_AddingNullLessThanSign_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { null, null, null, null },
                { null, null, null, null },
                { null, null, null, null },
                { null, null, null, null }
            };

            // Act
            Action act = () => FutoshikiProblem.Create().FromGrid(grid).AddSign((LessThanSign)null!).Build();

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'sign')");
        }
    }
}