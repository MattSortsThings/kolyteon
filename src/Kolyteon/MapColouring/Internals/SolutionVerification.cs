using Kolyteon.Common;
using Kolyteon.Common.Internals;

namespace Kolyteon.MapColouring.Internals;

internal static class SolutionVerification
{
    internal static MapColouringSolutionVerifier OneEntryPerBlock => new OneEntryPerBlockVerifier();

    internal static MapColouringSolutionVerifier EveryBlockIsSolutionKey => new EveryBlockIsSolutionKeyVerifier();

    internal static MapColouringSolutionVerifier EveryBlockHasPermittedColour => new EveryBlockHasPermittedColourVerifier();

    internal static MapColouringSolutionVerifier NoAdjacentBlocksSameColour => new NoAdjacentBlocksSameColourVerifier();

    internal abstract class MapColouringSolutionVerifier :
        SolutionVerifier<IReadOnlyDictionary<Block, Colour>, MapColouringProblem>;

    private sealed class OneEntryPerBlockVerifier : MapColouringSolutionVerifier
    {
        internal override Result VerifyCorrect(IReadOnlyDictionary<Block, Colour> solution, MapColouringProblem problem)
        {
            int entries = solution.Count;
            int blocks = problem.BlockData.Count;

            return entries == blocks
                ? Result.Success()
                : Result.Failure($"Solution has {(entries == 1 ? "1 entry" : entries + " entries")}, " +
                                 $"but problem has {(blocks == 1 ? "1 block" : blocks + " blocks")}.");
        }
    }

    private sealed class EveryBlockIsSolutionKeyVerifier : MapColouringSolutionVerifier
    {
        internal override Result VerifyCorrect(IReadOnlyDictionary<Block, Colour> solution,
            MapColouringProblem problem) => problem
            .BlockData.Select(datum => datum.Block)
            .Where(block => !solution.ContainsKey(block))
            .Select(block => Result.Failure($"Block {block} is not a key in the solution."))
            .FirstOrDefault(Result.Success());
    }

    private sealed class EveryBlockHasPermittedColourVerifier : MapColouringSolutionVerifier
    {
        internal override Result VerifyCorrect(IReadOnlyDictionary<Block, Colour> solution,
            MapColouringProblem problem) => problem
            .BlockData.Join(solution,
                datum => datum.Block,
                entry => entry.Key,
                (datum, entry) => new CheckingItem(datum.Block, entry.Value, datum.PermittedColours.Contains(entry.Value))
            ).Where(item => item.IsPermitted == false)
            .Select(item => Result.Failure($"Block {item.Block} is assigned the colour '{item.AssignedColour}', " +
                                           $"which is not a member of its permitted colours set."))
            .FirstOrDefault(Result.Success());

        private readonly record struct CheckingItem(Block Block, Colour AssignedColour, bool IsPermitted);
    }

    private sealed class NoAdjacentBlocksSameColourVerifier : MapColouringSolutionVerifier
    {
        internal override Result VerifyCorrect(IReadOnlyDictionary<Block, Colour> solution,
            MapColouringProblem problem) =>
            problem.BlockData.Select(datum => datum.Block).SelectMany((blockAtI, i) =>
                problem.BlockData.Take(i).Select(pastDatum => pastDatum.Block)
                    .Where(pastBlock => pastBlock.AdjacentTo(blockAtI))
                    .Select(pastBlock =>
                        {
                            Colour pastColour = solution[pastBlock];
                            Colour colourAtI = solution[blockAtI];

                            return new CheckingItem(pastBlock, pastColour, blockAtI, colourAtI);
                        }
                    )
                    .Where(item => item.FirstColour == item.SecondColour)
                    .Select(item => Result.Failure($"Adjacent blocks {item.FirstBlock} and {item.SecondBlock} " +
                                                   $"are both assigned the colour '{item.FirstColour}'."))
            ).FirstOrDefault(Result.Success());

        private readonly record struct CheckingItem(
            Block FirstBlock,
            Colour FirstColour,
            Block SecondBlock,
            Colour SecondColour);
    }
}
