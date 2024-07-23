using Kolyteon.Common;
using Kolyteon.Common.Internals;

namespace Kolyteon.MapColouring.Internals;

internal static class ProblemValidation
{
    internal static IProblemValidator<MapColouringProblem> AtLeastOneBlock =>
        new AtLeastOneBlockValidator();

    internal static IProblemValidator<MapColouringProblem> AllBlocksInCanvas =>
        new AllBlocksInCanvasValidator();

    internal static IProblemValidator<MapColouringProblem> NoOverlappingBlocks =>
        new NoOverlappingBlocksValidator();

    private sealed class AtLeastOneBlockValidator : IProblemValidator<MapColouringProblem>
    {
        public CheckingResult Validate(MapColouringProblem problem) => problem.BlockData.Count == 0
            ? CheckingResult.Failure("Problem has zero blocks.")
            : CheckingResult.Success();
    }

    private sealed class AllBlocksInCanvasValidator : IProblemValidator<MapColouringProblem>
    {
        public CheckingResult Validate(MapColouringProblem problem)
        {
            Block canvas = problem.Canvas;

            return problem.BlockData.Select(datum => datum.Block)
                .Where(block => !canvas.Contains(block))
                .Select(block => CheckingResult.Failure($"Block {block} is not inside canvas {canvas}."))
                .FirstOrDefault(CheckingResult.Success());
        }
    }

    private sealed class NoOverlappingBlocksValidator : IProblemValidator<MapColouringProblem>
    {
        public CheckingResult Validate(MapColouringProblem problem) =>
            problem.BlockData.Select(datum => datum.Block)
                .SelectMany((blockAtI, i) => problem.BlockData.Take(i).Select(pastDatum => pastDatum.Block)
                    .Where(pastBlock => pastBlock.Overlaps(blockAtI)).Select(pastBlock =>
                        CheckingResult.Failure($"Blocks {pastBlock} and {blockAtI} overlap.")))
                .FirstOrDefault(CheckingResult.Success());
    }
}
