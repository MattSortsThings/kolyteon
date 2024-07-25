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
        internal override CheckingResult Validate(MapColouringProblem problem) =>
            problem.BlockData.Count == 0
                ? CheckingResult.Failure("Problem has zero blocks.")
                : CheckingResult.Success();
    }

    private sealed class AllBlocksInCanvasValidator : MapColouringProblemValidator
    {
        internal override CheckingResult Validate(MapColouringProblem problem)
        {
            Block canvas = problem.Canvas;

            return problem.BlockData.Select(datum => datum.Block)
                .Where(block => !canvas.Contains(block))
                .Select(block => CheckingResult.Failure($"Block {block} is not inside canvas {canvas}."))
                .FirstOrDefault(CheckingResult.Success());
        }
    }

    private sealed class NoOverlappingBlocksValidator : MapColouringProblemValidator
    {
        internal override CheckingResult Validate(MapColouringProblem problem) =>
            problem.BlockData.Select(datum => datum.Block)
                .SelectMany((blockAtI, i) => problem.BlockData.Take(i).Select(pastDatum => pastDatum.Block)
                    .Where(pastBlock => pastBlock.Overlaps(blockAtI)).Select(pastBlock =>
                        CheckingResult.Failure($"Blocks {pastBlock} and {blockAtI} overlap.")))
                .FirstOrDefault(CheckingResult.Success());
    }
}
