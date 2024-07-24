using Kolyteon.Common;
using Kolyteon.Common.Internals;

namespace Kolyteon.MapColouring.Internals;

internal static class SolutionVerification
{
    internal static ISolutionVerifier<IReadOnlyDictionary<Block, Colour>, MapColouringProblem> OneEntryPerBlock =>
        new OneEntryPerBlockVerifier();

    internal static ISolutionVerifier<IReadOnlyDictionary<Block, Colour>, MapColouringProblem> EveryBlockIsSolutionKey =>
        new EveryBlockIsSolutionKeyVerifier();

    internal static ISolutionVerifier<IReadOnlyDictionary<Block, Colour>, MapColouringProblem> EveryBlockHasPermittedColour =>
        new EveryBlockHasPermittedColourVerifier();

    internal static ISolutionVerifier<IReadOnlyDictionary<Block, Colour>, MapColouringProblem> NoAdjacentBlocksSameColour =>
        new NoAdjacentBlocksSameColourVerifier();

    private sealed class OneEntryPerBlockVerifier
        : ISolutionVerifier<IReadOnlyDictionary<Block, Colour>, MapColouringProblem>
    {
        public CheckingResult VerifyCorrect(IReadOnlyDictionary<Block, Colour> solution, MapColouringProblem problem)
        {
            int entries = solution.Count;
            int blocks = problem.BlockData.Count;

            return entries == blocks
                ? CheckingResult.Success()
                : CheckingResult.Failure($"Solution has {(entries == 1 ? "1 entry" : entries + " entries")}, " +
                                         $"but problem has {(blocks == 1 ? "1 block" : blocks + " blocks")}.");
        }
    }

    private sealed class EveryBlockIsSolutionKeyVerifier
        : ISolutionVerifier<IReadOnlyDictionary<Block, Colour>, MapColouringProblem>
    {
        public CheckingResult VerifyCorrect(IReadOnlyDictionary<Block, Colour> solution, MapColouringProblem problem) => problem
            .BlockData.Select(datum => datum.Block)
            .Where(block => !solution.ContainsKey(block))
            .Select(block => CheckingResult.Failure($"Block {block} is not a key in the solution."))
            .FirstOrDefault(CheckingResult.Success());
    }

    private sealed class EveryBlockHasPermittedColourVerifier
        : ISolutionVerifier<IReadOnlyDictionary<Block, Colour>, MapColouringProblem>
    {
        public CheckingResult VerifyCorrect(IReadOnlyDictionary<Block, Colour> solution, MapColouringProblem problem) => problem
            .BlockData.Join(solution,
                datum => datum.Block,
                entry => entry.Key,
                (datum, entry) => new CheckingItem(datum.Block, entry.Value, datum.PermittedColours.Contains(entry.Value))
            ).Where(item => item.IsPermitted == false)
            .Select(item => CheckingResult.Failure($"Block {item.Block} is assigned the colour '{item.AssignedColour}', " +
                                                   $"which is not a member of its permitted colours set."))
            .FirstOrDefault(CheckingResult.Success());

        private readonly record struct CheckingItem(
            Block Block,
            Colour AssignedColour,
            bool IsPermitted);
    }

    private sealed class NoAdjacentBlocksSameColourVerifier
        : ISolutionVerifier<IReadOnlyDictionary<Block, Colour>, MapColouringProblem>
    {
        public CheckingResult VerifyCorrect(IReadOnlyDictionary<Block, Colour> solution, MapColouringProblem problem) =>
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
                    .Select(item => CheckingResult.Failure($"Adjacent blocks {item.FirstBlock} and {item.SecondBlock} " +
                                                           $"are both assigned the colour '{item.FirstColour}'."))
            ).FirstOrDefault(CheckingResult.Success());

        private readonly record struct CheckingItem(
            Block FirstBlock,
            Colour FirstColour,
            Block SecondBlock,
            Colour SecondColour);
    }
}
