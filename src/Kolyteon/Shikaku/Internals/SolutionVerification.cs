using Kolyteon.Common;
using Kolyteon.Common.Internals;

namespace Kolyteon.Shikaku.Internals;

internal static class SolutionVerification
{
    internal static ShikakuSolutionVerifier OneBlockPerHint => new OneBlockPerHintVerifier();

    internal static ShikakuSolutionVerifier BlockAreasSumToGridArea => new BlockAreasSumToGridAreaVerifier();

    internal static ShikakuSolutionVerifier AllBlocksInGrid => new AllBlocksInGridVerifier();

    internal static ShikakuSolutionVerifier NoOverlappingBlocks => new NoOverlappingBlocksVerifier();

    internal static ShikakuSolutionVerifier NoBlockContainsMoreThanOneHint => new NoBlockContainsMoreThanOneHintVerifier();

    internal static ShikakuSolutionVerifier EveryBlockContainsMatchingHint => new EveryBlockContainsMatchingHintVerifier();

    internal abstract class ShikakuSolutionVerifier : SolutionVerifier<IReadOnlyList<Block>, ShikakuProblem>;

    private sealed class OneBlockPerHintVerifier : ShikakuSolutionVerifier
    {
        internal override Result VerifyCorrect(IReadOnlyList<Block> solution, ShikakuProblem problem)
        {
            int blocks = solution.Count;
            int hints = problem.Hints.Count;

            return blocks == hints
                ? Result.Success()
                : Result.Failure($"Solution has {(blocks == 1 ? "1 block" : blocks + " blocks")}, " +
                                 $"but problem has {(hints == 1 ? "1 hint" : hints + " hints")}.");
        }
    }

    private sealed class BlockAreasSumToGridAreaVerifier : ShikakuSolutionVerifier
    {
        internal override Result VerifyCorrect(IReadOnlyList<Block> solution, ShikakuProblem problem)
        {
            int sumBlockAreas = solution.Sum(block => block.AreaInSquares);
            int gridArea = problem.Grid.AreaInSquares;

            return sumBlockAreas == gridArea
                ? Result.Success()
                : Result.Failure($"Block areas sum to {sumBlockAreas}, but grid area is {gridArea}.");
        }
    }

    private sealed class AllBlocksInGridVerifier : ShikakuSolutionVerifier
    {
        internal override Result VerifyCorrect(IReadOnlyList<Block> solution, ShikakuProblem problem)
        {
            Block grid = problem.Grid;

            return solution.Where(block => !grid.Contains(block))
                .Select(block => Result.Failure($"Block {block} is not inside grid {grid}."))
                .FirstOrDefault(Result.Success());
        }
    }

    private sealed class NoOverlappingBlocksVerifier : ShikakuSolutionVerifier
    {
        internal override Result VerifyCorrect(IReadOnlyList<Block> solution, ShikakuProblem problem) =>
            solution.SelectMany((blockAtI, i) =>
                    solution.Take(i).Where(pastBlock => pastBlock.Overlaps(blockAtI))
                        .Select(pastBlock => Result.Failure($"Blocks {pastBlock} and {blockAtI} overlap."))
                )
                .FirstOrDefault(Result.Success());
    }

    private sealed class NoBlockContainsMoreThanOneHintVerifier : ShikakuSolutionVerifier
    {
        internal override Result VerifyCorrect(IReadOnlyList<Block> solution, ShikakuProblem problem)
        {
            IReadOnlyList<NumberedSquare> hints = problem.Hints;

            return solution.Where(block => hints.Count(hint => block.Contains(hint)) > 1)
                .Select(block => Result.Failure($"Block {block} contains more than one hint."))
                .FirstOrDefault(Result.Success());
        }
    }

    private sealed class EveryBlockContainsMatchingHintVerifier : ShikakuSolutionVerifier
    {
        internal override Result VerifyCorrect(IReadOnlyList<Block> solution, ShikakuProblem problem)
        {
            IReadOnlyList<NumberedSquare> hints = problem.Hints;

            return solution.SelectMany(block =>
                    hints.Where(hint => block.Contains(hint) && block.AreaInSquares != hint.Number).Take(1)
                        .Select(hint =>
                            Result.Failure($"Block {block} contains hint {hint} with number not equal to its area.")))
                .FirstOrDefault(Result.Success());
        }
    }
}
