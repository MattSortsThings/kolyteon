using Kolyteon.Common;
using Kolyteon.Common.Internals;

namespace Kolyteon.MapColouring.Internals;

internal static class ProblemValidation
{
    internal static MapColouringProblemValidator AtLeastOneBlock => new AtLeastOneBlockValidator();

    internal static MapColouringProblemValidator AllBlocksInCanvas => new AllBlocksInCanvasValidator();

    internal static MapColouringProblemValidator NoOverlappingBlocks => new NoOverlappingBlocksValidator();

    internal abstract class MapColouringProblemValidator : ProblemValidator<MapColouringProblem>;

    private sealed class AtLeastOneBlockValidator : MapColouringProblemValidator
    {
        internal override Result Validate(MapColouringProblem problem) =>
            problem.BlockData.Count == 0
                ? Result.Failure("Problem has zero blocks.")
                : Result.Success();
    }

    private sealed class AllBlocksInCanvasValidator : MapColouringProblemValidator
    {
        internal override Result Validate(MapColouringProblem problem)
        {
            Block canvas = problem.Canvas;

            return problem.BlockData.Select(datum => datum.Block)
                .Where(block => !canvas.Contains(block))
                .Select(block => Result.Failure($"Block {block} is not inside canvas {canvas}."))
                .FirstOrDefault(Result.Success());
        }
    }

    private sealed class NoOverlappingBlocksValidator : MapColouringProblemValidator
    {
        internal override Result Validate(MapColouringProblem problem) =>
            problem.BlockData.Select(datum => datum.Block)
                .SelectMany((blockAtI, i) => problem.BlockData.Take(i).Select(pastDatum => pastDatum.Block)
                    .Where(pastBlock => pastBlock.Overlaps(blockAtI)).Select(pastBlock =>
                        Result.Failure($"Blocks {pastBlock} and {blockAtI} overlap.")))
                .FirstOrDefault(Result.Success());
    }
}
