using Kolyteon.Common;
using Kolyteon.Common.Internals;

namespace Kolyteon.Shikaku.Internals;

internal static class ProblemValidation
{
    internal static ShikakuProblemValidator ValidGridDimensions => new GridDimensionsValidator();

    internal static ShikakuProblemValidator AtLeastOneHint => new AtLeastOneHintValidator();

    internal static ShikakuProblemValidator NoHintNumberLessThanTwo => new NoHintNumberLessThanTwoValidator();

    internal static ShikakuProblemValidator HintNumbersSumToGridArea => new HintNumbersSumToGridAreaValidator();

    internal abstract class ShikakuProblemValidator : ProblemValidator<ShikakuProblem>;

    private sealed class GridDimensionsValidator : ShikakuProblemValidator
    {
        internal override CheckingResult Validate(ShikakuProblem problem)
        {
            (int width, int height) = problem.Grid.Dimensions;

            if (width == height && width >= ShikakuProblem.MinGridSideLength)
            {
                return CheckingResult.Success();
            }

            return CheckingResult.Failure($"Invalid problem grid dimensions [{problem.Grid.Dimensions}]. " +
                                          $"Grid must be a square no smaller than 5x5.");
        }
    }

    private sealed class AtLeastOneHintValidator : ShikakuProblemValidator
    {
        internal override CheckingResult Validate(ShikakuProblem problem) =>
            problem.Hints.Count == 0
                ? CheckingResult.Failure("Problem has zero hints.")
                : CheckingResult.Success();
    }

    private sealed class NoHintNumberLessThanTwoValidator : ShikakuProblemValidator
    {
        internal override CheckingResult Validate(ShikakuProblem problem) =>
            problem.Hints.Where(hint => hint.Number < ShikakuProblem.MinHintNumber)
                .Select(hint => CheckingResult.Failure($"Invalid hint {hint}. Hint number must be not less than 2."))
                .FirstOrDefault(CheckingResult.Success());
    }

    private sealed class HintNumbersSumToGridAreaValidator : ShikakuProblemValidator
    {
        internal override CheckingResult Validate(ShikakuProblem problem)
        {
            (Block grid, IReadOnlyList<NumberedSquare> hints) = problem;

            int areaInSquares = grid.AreaInSquares;
            int sumHintNumbers = hints.Sum(hint => hint.Number);

            return sumHintNumbers == areaInSquares
                ? CheckingResult.Success()
                : CheckingResult.Failure($"Hint numbers sum to {sumHintNumbers}, but grid area is {areaInSquares}. " +
                                         $"Hint numbers must sum to grid area.");
        }
    }
}
