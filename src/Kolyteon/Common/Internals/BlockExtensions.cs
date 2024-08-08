namespace Kolyteon.Common.Internals;

internal static class BlockExtensions
{
    internal static BlockPair DivideOnColumn(this Block block, int offset)
    {
        ((int originColumn, int originRow), (int width, int height)) = block;

        Block firstBlock = Square.FromColumnAndRow(originColumn, originRow)
            .ToBlock(Dimensions.FromWidthAndHeight(offset, height));

        Block secondBlock = Square.FromColumnAndRow(originColumn + offset, originRow)
            .ToBlock(Dimensions.FromWidthAndHeight(width - offset, height));

        return new BlockPair(firstBlock, secondBlock);
    }

    internal static BlockPair DivideOnRow(this Block block, int offset)
    {
        ((int originColumn, int originRow), (int width, int height)) = block;

        Block firstBlock = Square.FromColumnAndRow(originColumn, originRow)
            .ToBlock(Dimensions.FromWidthAndHeight(width, offset));

        Block secondBlock = Square.FromColumnAndRow(originColumn, originRow + offset)
            .ToBlock(Dimensions.FromWidthAndHeight(width, height - offset));

        return new BlockPair(firstBlock, secondBlock);
    }
}
