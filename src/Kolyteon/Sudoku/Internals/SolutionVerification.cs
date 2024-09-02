using Kolyteon.Common;
using Kolyteon.Common.Internals;

namespace Kolyteon.Sudoku.Internals;

internal sealed class SolutionVerification
{
    internal static SudokuSolutionVerifier OneFilledSquarePerEmptySquare => new OneFilledSquarePerEmptySquareVerifier();

    internal static SudokuSolutionVerifier AllFilledSquaresInGrid => new AllFilledSquaresInGridVerifier();

    internal static SudokuSolutionVerifier AllFilledSquareNumbersInRange => new AllFilledSquareNumbersInRangeVerifier();

    internal static SudokuSolutionVerifier NoSquareFilledMoreThanOnce => new NoSquareFilledMoreThanOnceVerifier();

    internal static SudokuSolutionVerifier NoDuplicateNumbersInSameColumn => new NoDuplicateNumbersInSameColumnValidator();

    internal static SudokuSolutionVerifier NoDuplicateNumbersInSameRow => new NoDuplicateNumbersInSameRowValidator();

    internal static SudokuSolutionVerifier NoDuplicateNumbersInSameSector => new NoDuplicateNumbersInSameSectorValidator();

    internal abstract class SudokuSolutionVerifier : SolutionVerifier<IReadOnlyList<NumberedSquare>, SudokuProblem>;

    private sealed class OneFilledSquarePerEmptySquareVerifier : SudokuSolutionVerifier
    {
        internal override Result VerifyCorrect(IReadOnlyList<NumberedSquare> solution, SudokuProblem problem)
        {
            int expected = problem.Grid.AreaInSquares - problem.FilledSquares.Count;
            int actual = solution.Count;

            return actual == expected
                ? Result.Success()
                : Result.Failure($"Solution has {(actual == 1 ? "1 filled square" : actual + " filled squares")}, " +
                                 $"but problem has {(expected == 1 ? "1 empty square" : expected + " empty squares")}.");
        }
    }

    private sealed class AllFilledSquaresInGridVerifier : SudokuSolutionVerifier
    {
        internal override Result VerifyCorrect(IReadOnlyList<NumberedSquare> solution, SudokuProblem problem)
        {
            Block grid = problem.Grid;

            return solution.Where(NotInsideGrid)
                .Select(filledSquare => Result.Failure($"Filled square {filledSquare} is not inside grid {grid}."))
                .FirstOrDefault(Result.Success());

            bool NotInsideGrid(NumberedSquare filledSquare) => !grid.Contains(filledSquare.Square);
        }
    }

    private sealed class AllFilledSquareNumbersInRangeVerifier : SudokuSolutionVerifier
    {
        private static bool NumberNotInRange(NumberedSquare n) =>
            n.Number is < SudokuProblem.MinNumber or > SudokuProblem.MaxNumber;

        internal override Result VerifyCorrect(IReadOnlyList<NumberedSquare> solution, SudokuProblem problem) =>
            solution.Where(NumberNotInRange)
                .Select(filledSquare => Result.Failure($"Filled square {filledSquare} " +
                                                       $"has number outside permitted range [1,9]."))
                .FirstOrDefault(Result.Success());
    }

    private sealed class NoSquareFilledMoreThanOnceVerifier : SudokuSolutionVerifier
    {
        internal override Result VerifyCorrect(IReadOnlyList<NumberedSquare> solution, SudokuProblem problem) => problem
            .FilledSquares.Concat(solution)
            .GroupBy(filledSquare => filledSquare.Square, _ => 1)
            .Where(grouping => grouping.Count() > 1)
            .Select(grouping => Result.Failure($"Square {grouping.Key} is filled more than once."))
            .FirstOrDefault(Result.Success());
    }

    private sealed class NoDuplicateNumbersInSameColumnValidator : SudokuSolutionVerifier
    {
        internal override Result VerifyCorrect(IReadOnlyList<NumberedSquare> solution, SudokuProblem problem) =>
            problem.FilledSquares.Concat(solution)
                .GroupBy(square => new CheckingItem(square.Square.Column, square.Number), _ => 1)
                .Where(grouping => grouping.Count() > 1)
                .Select(grouping => Result.Failure($"Number {grouping.Key.Number} occurs more than once " +
                                                   $"in column {grouping.Key.Column}."))
                .FirstOrDefault(Result.Success());

        private readonly record struct CheckingItem(int Column, int Number);
    }

    private sealed class NoDuplicateNumbersInSameRowValidator : SudokuSolutionVerifier
    {
        internal override Result VerifyCorrect(IReadOnlyList<NumberedSquare> solution, SudokuProblem problem) =>
            problem.FilledSquares.Concat(solution)
                .GroupBy(square => new CheckingItem(square.Square.Row, square.Number), _ => 1)
                .Where(grouping => grouping.Count() > 1)
                .Select(grouping => Result.Failure($"Number {grouping.Key.Number} occurs more than once " +
                                                   $"in row {grouping.Key.Row}."))
                .FirstOrDefault(Result.Success());

        private readonly record struct CheckingItem(int Row, int Number);
    }

    private sealed class NoDuplicateNumbersInSameSectorValidator : SudokuSolutionVerifier
    {
        internal override Result VerifyCorrect(IReadOnlyList<NumberedSquare> solution, SudokuProblem problem) =>
            problem.FilledSquares.Concat(solution)
                .GroupBy(square => new CheckingItem(square.Square.GetSector(), square.Number), _ => 1)
                .Where(grouping => grouping.Count() > 1)
                .Select(grouping => Result.Failure($"Number {grouping.Key.Number} occurs more than once " +
                                                   $"in sector {grouping.Key.Sector}."))
                .FirstOrDefault(Result.Success());

        private readonly record struct CheckingItem(int Sector, int Number);
    }
}
