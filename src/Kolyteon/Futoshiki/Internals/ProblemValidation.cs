using Kolyteon.Common;
using Kolyteon.Common.Internals;

namespace Kolyteon.Futoshiki.Internals;

internal static class ProblemValidation
{
    internal static FutoshikiProblemValidator AtLeastOneEmptySquare => new AtLeastOneEmptySquareValidator();

    internal static FutoshikiProblemValidator AllFilledSquareNumbersInRange => new AllFilledSquareNumbersInRangeValidator();

    internal static FutoshikiProblemValidator NoDuplicateNumbersInSameColumn => new NoDuplicateNumbersInSameColumnValidator();

    internal static FutoshikiProblemValidator NoDuplicateNumbersInSameRow => new NoDuplicateNumbersInSameRowValidator();

    internal static FutoshikiProblemValidator AllGreaterThanSignsInGrid => new AllGreaterThanSignsInGridValidator();

    internal static FutoshikiProblemValidator AllLessThanSignsInGrid => new AllLessThanSignsInGridValidator();

    internal static FutoshikiProblemValidator NoSignsInSameLocation => new NoSignsInSameLocationValidator();

    internal abstract class FutoshikiProblemValidator : ProblemValidator<FutoshikiProblem>;

    private sealed class AtLeastOneEmptySquareValidator : FutoshikiProblemValidator
    {
        internal override CheckingResult Validate(FutoshikiProblem problem) =>
            problem.FilledSquares.Count >= problem.Grid.AreaInSquares
                ? CheckingResult.Failure("Problem has zero empty squares.")
                : CheckingResult.Success();
    }

    private sealed class AllFilledSquareNumbersInRangeValidator : FutoshikiProblemValidator
    {
        internal override CheckingResult Validate(FutoshikiProblem problem)
        {
            const int minNumber = FutoshikiProblem.MinNumber;
            int maxNumber = problem.MaxNumber;

            return problem.FilledSquares.Where(square => square.Number < minNumber || square.Number > maxNumber)
                .Select(filledSquare => CheckingResult.Failure($"Invalid filled square {filledSquare}. " +
                                                               $"Number must be in the range [{minNumber},{maxNumber}]."))
                .FirstOrDefault(CheckingResult.Success());
        }
    }

    private sealed class NoDuplicateNumbersInSameColumnValidator : FutoshikiProblemValidator
    {
        internal override CheckingResult Validate(FutoshikiProblem problem) =>
            problem.FilledSquares
                .GroupBy(square => new CheckingItem(square.Square.Column, square.Number), _ => 1)
                .Where(grouping => grouping.Count() > 1)
                .Select(grouping => CheckingResult.Failure($"Number {grouping.Key.Number} occurs more than once " +
                                                           $"in column {grouping.Key.Column}."))
                .FirstOrDefault(CheckingResult.Success());

        private readonly record struct CheckingItem(int Column, int Number);
    }

    private sealed class NoDuplicateNumbersInSameRowValidator : FutoshikiProblemValidator
    {
        internal override CheckingResult Validate(FutoshikiProblem problem) =>
            problem.FilledSquares
                .GroupBy(square => new CheckingItem(square.Square.Row, square.Number), _ => 1)
                .Where(grouping => grouping.Count() > 1)
                .Select(grouping => CheckingResult.Failure($"Number {grouping.Key.Number} occurs more than once " +
                                                           $"in row {grouping.Key.Row}."))
                .FirstOrDefault(CheckingResult.Success());

        private readonly record struct CheckingItem(int Row, int Number);
    }

    private sealed class AllGreaterThanSignsInGridValidator : FutoshikiProblemValidator
    {
        internal override CheckingResult Validate(FutoshikiProblem problem)
        {
            Block grid = problem.Grid;

            return problem.GreaterThanSigns.Where(sign => !grid.Contains(sign.FirstSquare) || !grid.Contains(sign.SecondSquare))
                .Select(sign => CheckingResult.Failure($"Sign {sign} is not inside grid {grid}."))
                .FirstOrDefault(CheckingResult.Success());
        }
    }

    private sealed class AllLessThanSignsInGridValidator : FutoshikiProblemValidator
    {
        internal override CheckingResult Validate(FutoshikiProblem problem)
        {
            Block grid = problem.Grid;

            return problem.LessThanSigns.Where(sign => !grid.Contains(sign.FirstSquare) || !grid.Contains(sign.SecondSquare))
                .Select(sign => CheckingResult.Failure($"Sign {sign} is not inside grid {grid}."))
                .FirstOrDefault(CheckingResult.Success());
        }
    }

    private sealed class NoSignsInSameLocationValidator : FutoshikiProblemValidator
    {
        internal override CheckingResult Validate(FutoshikiProblem problem) => problem.GreaterThanSigns.Join(
                problem.LessThanSigns,
                greaterThanSign => new { greaterThanSign.FirstSquare, greaterThanSign.SecondSquare },
                lessThanSign => new { lessThanSign.FirstSquare, lessThanSign.SecondSquare },
                (greaterThanSign, lessThanSign) =>
                    CheckingResult.Failure($"Signs {greaterThanSign} and {lessThanSign} have same location.")
            )
            .FirstOrDefault(CheckingResult.Success());
    }
}
