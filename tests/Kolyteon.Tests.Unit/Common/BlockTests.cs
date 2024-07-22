using System.Text.Json;
using Kolyteon.Common;

namespace Kolyteon.Tests.Unit.Common;

public static class BlockTests
{
    [UnitTest]
    public sealed class ParameterlessConstructor
    {
        [Fact]
        public void ParameterlessConstructor_Initializes_HasOriginSquareInColumnZeroRowZeroAndDimensionsOfOneByOne()
        {
            // Act
            Block result = new();

            // Assert
            using (new AssertionScope())
            {
                result.OriginSquare.Column.Should().Be(0);
                result.OriginSquare.Row.Should().Be(0);
                result.Dimensions.WidthInSquares.Should().Be(1);
                result.Dimensions.HeightInSquares.Should().Be(1);
            }
        }
    }

    [UnitTest]
    public sealed class AreaInSquaresProperty
    {
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(2, 4, 8)]
        [InlineData(10, 1, 10)]
        [InlineData(5, 25, 125)]
        public void AreaInSquares_ReturnsProductOfDimensionsWidthInSquaresAndHeightInSquares(int width, int height, int expected)
        {
            // Arrange
            Square arbitraryOrigin = Square.FromColumnAndRow(0, 0);
            Dimensions dimensions = Dimensions.FromWidthAndHeight(width, height);

            Block sut = arbitraryOrigin.ToBlock(dimensions);

            // Act
            int result = sut.AreaInSquares;

            // Assert
            result.Should().Be(expected);
        }
    }

    [UnitTest]
    public sealed class TerminusSquareProperty
    {
        public static TheoryData<Square, Dimensions, Square> TestCases => new()
        {
            { Square.FromColumnAndRow(0, 0), Dimensions.FromWidthAndHeight(1, 1), Square.FromColumnAndRow(0, 0) },
            { Square.FromColumnAndRow(2, 3), Dimensions.FromWidthAndHeight(2, 4), Square.FromColumnAndRow(3, 6) },
            { Square.FromColumnAndRow(7, 0), Dimensions.FromWidthAndHeight(5, 10), Square.FromColumnAndRow(11, 9) },
            { Square.FromColumnAndRow(4, 2), Dimensions.FromWidthAndHeight(1, 2), Square.FromColumnAndRow(4, 3) },
            { Square.FromColumnAndRow(4, 2), Dimensions.FromWidthAndHeight(2, 1), Square.FromColumnAndRow(5, 2) }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(TerminusSquareProperty))]
        public void TerminusSquare_ReturnsBottomRightSquareInBlock(Square originSquare,
            Dimensions dimensions,
            Square expectedTerminusSquare)
        {
            // Arrange
            Block sut = originSquare.ToBlock(dimensions);

            // Act
            Square result = sut.TerminusSquare;

            // Assert
            result.Should().Be(expectedTerminusSquare);
        }
    }

    [UnitTest]
    public sealed class CompareToMethod
    {
        [Fact]
        public void CompareTo_InstanceOriginSquarePrecedesOther_ReturnsNegativeValue()
        {
            // Arrange
            Dimensions sharedDimensions = Dimensions.FromWidthAndHeight(5, 5);

            Block sut = Square.FromColumnAndRow(0, 0).ToBlock(sharedDimensions);
            Block other = Square.FromColumnAndRow(99, 99).ToBlock(sharedDimensions);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveSameOriginSquareInstanceDimensionsPrecedesOther_ReturnsNegativeValue()
        {
            // Arrange
            Square sharedOrigin = Square.FromColumnAndRow(5, 5);

            Block sut = sharedOrigin.ToBlock(Dimensions.FromWidthAndHeight(1, 1));
            Block other = sharedOrigin.ToBlock(Dimensions.FromWidthAndHeight(99, 99));

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveEqualOriginSquareAndDimensionsValues_ReturnsZero()
        {
            // Arrange
            Square sharedOrigin = Square.FromColumnAndRow(5, 5);
            Dimensions sharedDimensions = Dimensions.FromWidthAndHeight(5, 5);

            Block sut = sharedOrigin.ToBlock(sharedDimensions);
            Block other = sharedOrigin.ToBlock(sharedDimensions);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void CompareTo_InstanceOriginSquareFollowsOther_ReturnsPositiveValue()
        {
            // Arrange
            Dimensions sharedDimensions = Dimensions.FromWidthAndHeight(5, 5);

            Block sut = Square.FromColumnAndRow(99, 99).ToBlock(sharedDimensions);
            Block other = Square.FromColumnAndRow(0, 0).ToBlock(sharedDimensions);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveSameOriginSquareInstanceDimensionsFollowOther_ReturnsNegativeValue()
        {
            // Arrange
            Square sharedOrigin = Square.FromColumnAndRow(5, 5);

            Block sut = sharedOrigin.ToBlock(Dimensions.FromWidthAndHeight(99, 99));
            Block other = sharedOrigin.ToBlock(Dimensions.FromWidthAndHeight(1, 1));

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }
    }

    [UnitTest]
    public sealed class EqualsMethod
    {
        [Fact]
        public void Equals_InstanceAndOtherHaveEqualOriginSquareAndDimensionsValues_ReturnsTrue()
        {
            // Arrange
            Square sharedOrigin = Square.FromColumnAndRow(5, 5);
            Dimensions sharedDimensions = Dimensions.FromWidthAndHeight(5, 5);

            Block sut = sharedOrigin.ToBlock(sharedDimensions);
            Block other = sharedOrigin.ToBlock(sharedDimensions);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalOriginSquareValues_ReturnsFalse()
        {
            // Arrange
            Dimensions sharedDimensions = Dimensions.FromWidthAndHeight(5, 5);

            Block sut = Square.FromColumnAndRow(99, 99).ToBlock(sharedDimensions);
            Block other = Square.FromColumnAndRow(0, 0).ToBlock(sharedDimensions);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalDimensionsValues_ReturnsFalse()
        {
            // Arrange
            Square sharedOrigin = Square.FromColumnAndRow(5, 5);

            Block sut = sharedOrigin.ToBlock(Dimensions.FromWidthAndHeight(99, 99));
            Block other = sharedOrigin.ToBlock(Dimensions.FromWidthAndHeight(1, 1));

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ContainsMethodSquareOverload
    {
        public static TheoryData<Block, Square> PositiveTestCases => new()
        {
            { Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1)), Square.FromColumnAndRow(0, 0) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(2, 2) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(2, 3) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(2, 4) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(3, 2) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(3, 3) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(3, 4) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(4, 2) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(4, 3) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(4, 4) }
        };

        public static TheoryData<Block, Square> NegativeTestCases => new()
        {
            { Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1)), Square.FromColumnAndRow(0, 1) },
            { Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1)), Square.FromColumnAndRow(1, 0) },
            { Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1)), Square.FromColumnAndRow(1, 1) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(1, 1) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(1, 2) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(1, 3) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(1, 4) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(1, 5) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(1, 1) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(2, 1) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(3, 1) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(4, 1) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(5, 1) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(5, 2) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(5, 3) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(5, 4) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(5, 5) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(2, 5) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(3, 5) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(4, 5) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(0, 0) },
            { Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)), Square.FromColumnAndRow(99, 99) }
        };

        [Theory]
        [MemberData(nameof(PositiveTestCases), MemberType = typeof(ContainsMethodSquareOverload))]
        public void Contains_GivenSquareInsideBlock_ReturnsTrue(Block sut, Square square)
        {
            // Act
            bool result = sut.Contains(in square);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(NegativeTestCases), MemberType = typeof(ContainsMethodSquareOverload))]
        public void Contains_GivenSquareNotInsideBlock_ReturnsFalse(Block sut, Square square)
        {
            // Act
            bool result = sut.Contains(in square);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ContainsMethodNumberedSquareOverload
    {
        [Fact]
        public void Contains_GivenNumberedSquareWithSquareInsideBlock_ReturnsTrue()
        {
            // Arrange
            Block sut = Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3));
            NumberedSquare numberedSquare = Square.FromColumnAndRow(2, 2).ToNumberedSquare(0);

            // Act
            bool result = sut.Contains(in numberedSquare);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Contains_GivenNumberedSquareWithSquareOutsideBlock_ReturnsFalse()
        {
            // Arrange
            Block sut = Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3));
            NumberedSquare numberedSquare = Square.FromColumnAndRow(99, 99).ToNumberedSquare(0);

            // Act
            bool result = sut.Contains(in numberedSquare);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ContainsMethodBlockOverload
    {
        public static TheoryData<Block, Block> PositiveTestCases => new()
        {
            {
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1)),
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 3).ToBlock(Dimensions.FromWidthAndHeight(3, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(2, 3))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(3, 2).ToBlock(Dimensions.FromWidthAndHeight(2, 3))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 3).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(3, 2).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(3, 3).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 3).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 4).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(3, 2).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(3, 3).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(3, 4).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(4, 2).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(4, 3).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(4, 4).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            }
        };

        public static TheoryData<Block, Block> NegativeTestCases => new()
        {
            {
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1)),
                Square.FromColumnAndRow(99, 99).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(99, 99).ToBlock(Dimensions.FromWidthAndHeight(3, 3))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(5, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(5, 0).ToBlock(Dimensions.FromWidthAndHeight(2, 5))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(3, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(3, 3).ToBlock(Dimensions.FromWidthAndHeight(3, 3))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(3, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 3).ToBlock(Dimensions.FromWidthAndHeight(3, 3))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(1, 3).ToBlock(Dimensions.FromWidthAndHeight(3, 3))
            }
        };

        [Theory]
        [MemberData(nameof(PositiveTestCases), MemberType = typeof(ContainsMethodBlockOverload))]
        public void Contains_GivenBlockEntirelyContainedInsideInstance_ReturnsTrue(Block sut, Block block)
        {
            // Act
            bool result = sut.Contains(in block);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(NegativeTestCases), MemberType = typeof(ContainsMethodBlockOverload))]
        public void Contains_GivenBlockPartiallyOrEntirelyOutsideInstance_ReturnsFalse(Block sut, Block block)
        {
            // Act
            bool result = sut.Contains(in block);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class OverlapsMethod
    {
        public static TheoryData<Block, Block> PositiveTestCases => new()
        {
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(2, 3).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(2, 4).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(3, 2).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(3, 3).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(3, 4).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(4, 2).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(4, 3).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(4, 4).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(1, 4))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(2, 3).ToBlock(Dimensions.FromWidthAndHeight(1, 4))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(2, 4).ToBlock(Dimensions.FromWidthAndHeight(1, 4))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(3, 2).ToBlock(Dimensions.FromWidthAndHeight(1, 4))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(3, 3).ToBlock(Dimensions.FromWidthAndHeight(1, 4))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(3, 4).ToBlock(Dimensions.FromWidthAndHeight(1, 4))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(4, 2).ToBlock(Dimensions.FromWidthAndHeight(1, 4))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(4, 3).ToBlock(Dimensions.FromWidthAndHeight(1, 4))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(4, 4).ToBlock(Dimensions.FromWidthAndHeight(1, 4))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(2, 3).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(3, 3).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(9, 9))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(3, 3))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(3, 9))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(1, 3).ToBlock(Dimensions.FromWidthAndHeight(4, 7))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(1, 0).ToBlock(Dimensions.FromWidthAndHeight(4, 6))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(3, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 9))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(0, 5).ToBlock(Dimensions.FromWidthAndHeight(4, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(4, 5).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            }
        };

        public static TheoryData<Block, Block> NegativeTestCases => new()
        {
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(1, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(2, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(3, 1).ToBlock(Dimensions.FromWidthAndHeight(4, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(9, 9).ToBlock(Dimensions.FromWidthAndHeight(9, 9))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(0, 2).ToBlock(Dimensions.FromWidthAndHeight(2, 5))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(5, 3).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(5, 3).ToBlock(Dimensions.FromWidthAndHeight(4, 4))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(3, 6).ToBlock(Dimensions.FromWidthAndHeight(2, 1))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(1, 0).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(2, 0).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(3, 0).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(4, 0).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(5, 0).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(6, 0).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(0, 6).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(1, 6).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(2, 6).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(3, 6).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(4, 6).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(5, 6).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(6, 6).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(0, 1).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(0, 2).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(0, 3).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(0, 4).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(0, 5).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(5, 1).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(5, 2).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(5, 3).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(5, 4).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 4)),
                Square.FromColumnAndRow(5, 5).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            }
        };

        [Theory]
        [MemberData(nameof(PositiveTestCases), MemberType = typeof(OverlapsMethod))]
        public void Overlaps_GivenOverlappingBlock_ReturnsTrue(Block firstBlock, Block secondBlock)
        {
            // Act
            bool result = firstBlock.Overlaps(in secondBlock);
            bool reciprocalResult = secondBlock.Overlaps(firstBlock);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeTrue();
                reciprocalResult.Should().BeTrue();
            }
        }

        [Theory]
        [MemberData(nameof(NegativeTestCases), MemberType = typeof(OverlapsMethod))]
        public void Overlaps_GivenNonOverlappingBlock_ReturnsFalse(Block firstBlock, Block secondBlock)
        {
            // Act
            bool result = firstBlock.Overlaps(in secondBlock);
            bool reciprocalResult = secondBlock.Overlaps(firstBlock);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeFalse();
                reciprocalResult.Should().BeFalse();
            }
        }
    }

    [UnitTest]
    public sealed class ToStringMethod
    {
        public static TheoryData<Square, Dimensions, string> TestCases => new()
        {
            { Square.FromColumnAndRow(0, 0), Dimensions.FromWidthAndHeight(1, 1), "(0,0) [1x1]" },
            { Square.FromColumnAndRow(7, 3), Dimensions.FromWidthAndHeight(2, 4), "(7,3) [2x4]" },
            { Square.FromColumnAndRow(10, 20), Dimensions.FromWidthAndHeight(100, 100), "(10,20) [100x100]" }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(ToStringMethod))]
        public void ToString_ReturnsFormattedString(Square originSquare, Dimensions dimensions, string expected)
        {
            // Arrange
            Block sut = originSquare.ToBlock(dimensions);

            // Act
            string result = sut.ToString();

            // Assert
            result.Should().Be(expected);
        }
    }

    [UnitTest]
    public sealed class EqualityOperator
    {
        [Fact]
        public void Equality_InstanceAndOtherHaveEqualOriginSquareAndDimensionsValues_ReturnsTrue()
        {
            // Arrange
            Square sharedOrigin = Square.FromColumnAndRow(5, 5);
            Dimensions sharedDimensions = Dimensions.FromWidthAndHeight(5, 5);

            Block sut = sharedOrigin.ToBlock(sharedDimensions);
            Block other = sharedOrigin.ToBlock(sharedDimensions);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalOriginSquareValues_ReturnsFalse()
        {
            // Arrange
            Dimensions sharedDimensions = Dimensions.FromWidthAndHeight(5, 5);

            Block sut = Square.FromColumnAndRow(99, 99).ToBlock(sharedDimensions);
            Block other = Square.FromColumnAndRow(0, 0).ToBlock(sharedDimensions);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalDimensionsValues_ReturnsFalse()
        {
            // Arrange
            Square sharedOrigin = Square.FromColumnAndRow(5, 5);

            Block sut = sharedOrigin.ToBlock(Dimensions.FromWidthAndHeight(99, 99));
            Block other = sharedOrigin.ToBlock(Dimensions.FromWidthAndHeight(1, 1));

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
        public void Inequality_InstanceAndOtherHaveEqualOriginSquareAndDimensionsValues_ReturnsFalse()
        {
            // Arrange
            Square sharedOrigin = Square.FromColumnAndRow(5, 5);
            Dimensions sharedDimensions = Dimensions.FromWidthAndHeight(5, 5);

            Block sut = sharedOrigin.ToBlock(sharedDimensions);
            Block other = sharedOrigin.ToBlock(sharedDimensions);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalOriginSquareValues_ReturnsTrue()
        {
            // Arrange
            Dimensions sharedDimensions = Dimensions.FromWidthAndHeight(5, 5);

            Block sut = Square.FromColumnAndRow(99, 99).ToBlock(sharedDimensions);
            Block other = Square.FromColumnAndRow(0, 0).ToBlock(sharedDimensions);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalDimensionsValues_ReturnsTrue()
        {
            // Arrange
            Square sharedOrigin = Square.FromColumnAndRow(5, 5);

            Block sut = sharedOrigin.ToBlock(Dimensions.FromWidthAndHeight(99, 99));
            Block other = sharedOrigin.ToBlock(Dimensions.FromWidthAndHeight(1, 1));

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        public static TheoryData<Square, Dimensions> TestCases => new()
        {
            { Square.FromColumnAndRow(0, 0), Dimensions.FromWidthAndHeight(1, 1) },
            { Square.FromColumnAndRow(7, 3), Dimensions.FromWidthAndHeight(2, 4) },
            { Square.FromColumnAndRow(10, 20), Dimensions.FromWidthAndHeight(100, 100) }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(Serialization))]
        public void CanSerializeToJson_ThenDeserializeToInstanceWithEqualValue(Square originSquare, Dimensions dimensions)
        {
            // Arrange
            Block originalBlock = originSquare.ToBlock(dimensions);

            string json = JsonSerializer.Serialize(originalBlock, JsonSerializerOptions.Default);

            // Act
            Block deserializedBlock = JsonSerializer.Deserialize<Block>(json, JsonSerializerOptions.Default);

            // Assert
            deserializedBlock.Should().Be(originalBlock);
        }
    }
}
