using Kolyteon.Common;
using Kolyteon.Common.Internals;

namespace Kolyteon.Sudoku.Internals;

internal static class ProblemValidation
{
    internal static SudokuProblemValidator AtLeastOneEmptySquare => new AtLeastOneEmptySquareValidator();

    internal static SudokuProblemValidator AllFilledSquareNumbersInRange => new AllFilledSquareNumbersInRangeValidator();

    internal static SudokuProblemValidator NoDuplicateNumbersInSameColumn => new NoDuplicateNumbersInSameColumnValidator();

    internal static SudokuProblemValidator NoDuplicateNumbersInSameRow => new NoDuplicateNumbersInSameRowValidator();

    internal static SudokuProblemValidator NoDuplicateNumbersInSameSector => new NoDuplicateNumbersInSameSectorValidator();

    internal abstract class SudokuProblemValidator : ProblemValidator<SudokuProblem>;

    private sealed class AtLeastOneEmptySquareValidator : SudokuProblemValidator
    {
        internal override CheckingResult Validate(SudokuProblem problem) =>
            problem.FilledSquares.Count >= problem.Grid.AreaInSquares
                ? CheckingResult.Failure("Problem has zero empty squares.")
                : CheckingResult.Success();
    }

    private sealed class AllFilledSquareNumbersInRangeValidator : SudokuProblemValidator
    {
        internal override CheckingResult Validate(SudokuProblem problem)
        {
            return problem.FilledSquares.Where(NumberNotInRange)
                .Select(filledSquare => CheckingResult.Failure($"Invalid filled square {filledSquare}. " +
                                                               $"Number must be in the range [1,9]."))
                .FirstOrDefault(CheckingResult.Success());

            bool NumberNotInRange(NumberedSquare n) => n.Number is < SudokuProblem.MinNumber or > SudokuProblem.MaxNumber;
        }
    }

    private sealed class NoDuplicateNumbersInSameColumnValidator : SudokuProblemValidator
    {
        internal override CheckingResult Validate(SudokuProblem problem) =>
            problem.FilledSquares
                .GroupBy(square => new CheckingItem(square.Square.Column, square.Number), _ => 1)
                .Where(grouping => grouping.Count() > 1)
                .Select(grouping => CheckingResult.Failure($"Number {grouping.Key.Number} occurs more than once " +
                                                           $"in column {grouping.Key.Column}."))
                .FirstOrDefault(CheckingResult.Success());

        private readonly record struct CheckingItem(int Column, int Number);
    }

    private sealed class NoDuplicateNumbersInSameRowValidator : SudokuProblemValidator
    {
        internal override CheckingResult Validate(SudokuProblem problem) =>
            problem.FilledSquares
                .GroupBy(square => new CheckingItem(square.Square.Row, square.Number), _ => 1)
                .Where(grouping => grouping.Count() > 1)
                .Select(grouping => CheckingResult.Failure($"Number {grouping.Key.Number} occurs more than once " +
                                                           $"in row {grouping.Key.Row}."))
                .FirstOrDefault(CheckingResult.Success());

        private readonly record struct CheckingItem(int Row, int Number);
    }

    private sealed class NoDuplicateNumbersInSameSectorValidator : SudokuProblemValidator
    {
        internal override CheckingResult Validate(SudokuProblem problem) =>
            problem.FilledSquares
                .GroupBy(square => new CheckingItem(square.Square.GetSector(), square.Number), _ => 1)
                .Where(grouping => grouping.Count() > 1)
                .Select(grouping => CheckingResult.Failure($"Number {grouping.Key.Number} occurs more than once " +
                                                           $"in sector {grouping.Key.Sector}."))
                .FirstOrDefault(CheckingResult.Success());

        private readonly record struct CheckingItem(int Sector, int Number);
    }
}