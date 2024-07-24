using Kolyteon.Common;
using Kolyteon.MapColouring;

namespace Kolyteon.Tests.Unit.MapColouring;

public static class BlockExtensionsTests
{
    [UnitTest]
    public sealed class AdjacentToMethod
    {
        public static TheoryData<Block, Block> PositiveTestCases => new()
        {
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 1).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 2).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 3).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(4, 1).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(4, 2).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(4, 3).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(1, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(3, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(1, 4).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 4).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(3, 4).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 2))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 1).ToBlock(Dimensions.FromWidthAndHeight(1, 2))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 2).ToBlock(Dimensions.FromWidthAndHeight(1, 2))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 3).ToBlock(Dimensions.FromWidthAndHeight(1, 2))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(4, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 2))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(4, 1).ToBlock(Dimensions.FromWidthAndHeight(1, 2))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(4, 2).ToBlock(Dimensions.FromWidthAndHeight(1, 2))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(4, 3).ToBlock(Dimensions.FromWidthAndHeight(1, 2))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(2, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(1, 0).ToBlock(Dimensions.FromWidthAndHeight(2, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 0).ToBlock(Dimensions.FromWidthAndHeight(2, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(3, 0).ToBlock(Dimensions.FromWidthAndHeight(2, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 4).ToBlock(Dimensions.FromWidthAndHeight(2, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(1, 4).ToBlock(Dimensions.FromWidthAndHeight(2, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 4).ToBlock(Dimensions.FromWidthAndHeight(2, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(3, 4).ToBlock(Dimensions.FromWidthAndHeight(2, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 3))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 1).ToBlock(Dimensions.FromWidthAndHeight(1, 3))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 2).ToBlock(Dimensions.FromWidthAndHeight(1, 3))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(4, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 3))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(4, 1).ToBlock(Dimensions.FromWidthAndHeight(1, 3))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(4, 2).ToBlock(Dimensions.FromWidthAndHeight(1, 3))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(3, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(1, 0).ToBlock(Dimensions.FromWidthAndHeight(3, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 0).ToBlock(Dimensions.FromWidthAndHeight(3, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 4).ToBlock(Dimensions.FromWidthAndHeight(3, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(1, 4).ToBlock(Dimensions.FromWidthAndHeight(3, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 4).ToBlock(Dimensions.FromWidthAndHeight(3, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 6))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(4, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 6))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(6, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 4).ToBlock(Dimensions.FromWidthAndHeight(6, 1))
            }
        };

        public static TheoryData<Block, Block> NegativeTestCases => new()
        {
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 4).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 5).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(4, 0).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(4, 4).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(4, 5).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(5, 1).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(5, 2).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(5, 3).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(1, 5).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 5).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(3, 5).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 5).ToBlock(Dimensions.FromWidthAndHeight(1, 3))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(1, 5).ToBlock(Dimensions.FromWidthAndHeight(1, 3))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 5).ToBlock(Dimensions.FromWidthAndHeight(1, 3))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(3, 5).ToBlock(Dimensions.FromWidthAndHeight(1, 3))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(4, 5).ToBlock(Dimensions.FromWidthAndHeight(1, 3))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(5, 5).ToBlock(Dimensions.FromWidthAndHeight(1, 3))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(5, 0).ToBlock(Dimensions.FromWidthAndHeight(3, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(5, 01).ToBlock(Dimensions.FromWidthAndHeight(3, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(5, 2).ToBlock(Dimensions.FromWidthAndHeight(3, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(5, 3).ToBlock(Dimensions.FromWidthAndHeight(3, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(5, 4).ToBlock(Dimensions.FromWidthAndHeight(3, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(5, 5).ToBlock(Dimensions.FromWidthAndHeight(3, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(99, 99).ToBlock(Dimensions.FromWidthAndHeight(3, 3))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(1, 2).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 1).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 1).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(3, 1).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(1, 2).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 2).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(3, 2).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(1, 3).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(2, 3).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(3, 3).ToBlock(Dimensions.FromWidthAndHeight(1, 1))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(2, 2))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(3, 3))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(4, 4))
            },
            {
                Square.FromColumnAndRow(1, 1).ToBlock(Dimensions.FromWidthAndHeight(3, 3)),
                Square.FromColumnAndRow(0, 0).ToBlock(Dimensions.FromWidthAndHeight(5, 5))
            }
        };

        [Theory]
        [MemberData(nameof(PositiveTestCases), MemberType = typeof(AdjacentToMethod))]
        public void AdjacentTo_InstanceAndOtherTouchButDoNotOverlap_ReturnsTrue(Block firstBlock, Block secondBlock)
        {
            // Act
            bool result = firstBlock.AdjacentTo(secondBlock);
            bool reciprocalResult = secondBlock.AdjacentTo(firstBlock);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeTrue();
                reciprocalResult.Should().BeTrue();
            }
        }


        [Theory]
        [MemberData(nameof(NegativeTestCases), MemberType = typeof(AdjacentToMethod))]
        public void AdjacentTo_InstanceAndOtherOverlapOrDoNotTouch_ReturnsFalse(Block firstBlock, Block secondBlock)
        {
            // Act
            bool result = firstBlock.AdjacentTo(secondBlock);
            bool reciprocalResult = secondBlock.AdjacentTo(firstBlock);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeFalse();
                reciprocalResult.Should().BeFalse();
            }
        }
    }
}
