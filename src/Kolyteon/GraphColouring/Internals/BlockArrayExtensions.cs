using Kolyteon.Common;
using Kolyteon.MapColouring;

namespace Kolyteon.GraphColouring.Internals;

internal static class BlockArrayExtensions
{
    internal static IEnumerable<Node> ToNodes(this Block[] blocks)
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            yield return Node.FromName($"N{i:D2}");
        }
    }

    internal static IEnumerable<Edge> ToEdges(this Block[] blocks)
    {
        for (int i = 1; i < blocks.Length; i++)
        {
            Block blockAtI = blocks[i];
            for (int h = 0; h < i; h++)
            {
                if (blocks[h].AdjacentTo(blockAtI))
                {
                    yield return Edge.Between(Node.FromName($"N{h:D2}"), Node.FromName($"N{i:D2}"));
                }
            }
        }
    }
}
