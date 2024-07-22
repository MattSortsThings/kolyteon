using Kolyteon.Common;
using Kolyteon.Common.Internals;

namespace Kolyteon.Shikaku.Internals;

internal static class ProblemValidation
{
    internal static IProblemValidator<ShikakuProblem> ValidGridDimensions =>
        new GridDimensionsValidator();

    internal static IProblemValidator<ShikakuProblem> AtLeastOneHint =>
        new AtLeastOneHintValidator();

    internal static IProblemValidator<ShikakuProblem> NoHintNumberLessThanTwo =>
        new NoHintNumberLessThanTwoValidator();

    internal static IProblemValidator<ShikakuProblem> HintNumbersSumToGridArea =>
        new HintNumbersSumToGridAreaValidator();

    private sealed class GridDimensionsValidator : IProblemValidator<ShikakuProblem>
    {
        public CheckingResult Validate(ShikakuProblem problem)
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

    private sealed class AtLeastOneHintValidator : IProblemValidator<ShikakuProblem>
    {
        public CheckingResult Validate(ShikakuProblem problem) => problem.Hints.Count == 0
            ? CheckingResult.Failure("Problem has zero hints.")
            : CheckingResult.Success();
    }

    private sealed class NoHintNumberLessThanTwoValidator : IProblemValidator<ShikakuProblem>
    {
        public CheckingResult Validate(ShikakuProblem problem)
        {
            foreach (NumberedSquare hint in problem.Hints)
            {
                if (hint.Number >= ShikakuProblem.MinHintNumber)
                {
                    continue;
                }

                return CheckingResult.Failure($"Invalid hint {hint}. Hint number must be not less than 2.");
            }

            return CheckingResult.Success();
        }
    }

    private sealed class HintNumbersSumToGridAreaValidator : IProblemValidator<ShikakuProblem>
    {
        public CheckingResult Validate(ShikakuProblem problem)
        {
            ((_, (int width, int height)), IReadOnlyList<NumberedSquare> hints) = problem;

            int areaInSquares = width * height;
            int sumHintNumbers = hints.Sum(hint => hint.Number);

            return sumHintNumbers == areaInSquares
                ? CheckingResult.Success()
                : CheckingResult.Failure($"Hint numbers sum to {sumHintNumbers}, but grid area is {areaInSquares}. " +
                                         $"Hint numbers must sum to grid area.");
        }
    }
}
