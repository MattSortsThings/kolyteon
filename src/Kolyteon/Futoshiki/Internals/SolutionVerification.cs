using Kolyteon.Common;
using Kolyteon.Common.Internals;

namespace Kolyteon.Futoshiki.Internals;

internal static class SolutionVerification
{
    internal static FutoshikiSolutionVerifier OneFilledSquarePerEmptySquare => new OneFilledSquarePerEmptySquareVerifier();

    internal static FutoshikiSolutionVerifier AllFilledSquaresInGrid => new AllFilledSquaresInGridVerifier();

    internal static FutoshikiSolutionVerifier AllFilledSquareNumbersInRange => new AllFilledSquareNumbersInRangeVerifier();

    internal static FutoshikiSolutionVerifier NoSquareFilledMoreThanOnce => new NoSquareFilledMoreThanOnceVerifier();

    internal static FutoshikiSolutionVerifier NoDuplicateNumbersInSameColumn => new NoDuplicateNumbersInSameColumnVerifier();

    internal static FutoshikiSolutionVerifier NoDuplicateNumbersInSameRow => new NoDuplicateNumbersInSameRowVerifier();

    internal static FutoshikiSolutionVerifier AllSignsSatisfied => new AllSignsSatisfiedVerifier();

    internal abstract class FutoshikiSolutionVerifier : SolutionVerifier<IReadOnlyList<NumberedSquare>, FutoshikiProblem>;

    private sealed class OneFilledSquarePerEmptySquareVerifier : FutoshikiSolutionVerifier
    {
        internal override CheckingResult VerifyCorrect(IReadOnlyList<NumberedSquare> solution, FutoshikiProblem problem)
        {
            int expected = problem.Grid.AreaInSquares - problem.FilledSquares.Count;
            int actual = solution.Count;

            return actual == expected
                ? CheckingResult.Success()
                : CheckingResult.Failure($"Solution has {(actual == 1 ? "1 filled square" : actual + " filled squares")}, " +
                                         $"but problem has {(expected == 1 ? "1 empty square" : expected + " empty squares")}.");
        }
    }

    private sealed class AllFilledSquaresInGridVerifier : FutoshikiSolutionVerifier
    {
        internal override CheckingResult VerifyCorrect(IReadOnlyList<NumberedSquare> solution, FutoshikiProblem problem)
        {
            Block grid = problem.Grid;

            return solution.Where(NotInsideGrid)
                .Select(filledSquare => CheckingResult.Failure($"Filled square {filledSquare} is not inside grid {grid}."))
                .FirstOrDefault(CheckingResult.Success());

            bool NotInsideGrid(NumberedSquare filledSquare) => !grid.Contains(filledSquare.Square);
        }
    }

    private sealed class AllFilledSquareNumbersInRangeVerifier : FutoshikiSolutionVerifier
    {
        internal override CheckingResult VerifyCorrect(IReadOnlyList<NumberedSquare> solution, FutoshikiProblem problem)
        {
            const int minNumber = FutoshikiProblem.MinNumber;
            int maxNumber = problem.MaxNumber;

            CheckingResult result = solution.Where(square => square.Number < minNumber || square.Number > maxNumber)
                .Select(filledSquare => CheckingResult.Failure($"Filled square {filledSquare} " +
                                                               $"has number outside permitted range [{minNumber},{maxNumber}]."))
                .FirstOrDefault(CheckingResult.Success());

            return result;
        }
    }

    private sealed class NoSquareFilledMoreThanOnceVerifier : FutoshikiSolutionVerifier
    {
        internal override CheckingResult VerifyCorrect(IReadOnlyList<NumberedSquare> solution, FutoshikiProblem problem) =>
            solution.Concat(problem.FilledSquares)
                .GroupBy(filledSquare => filledSquare.Square, _ => 1)
                .Where(grouping => grouping.Count() > 1)
                .Select(grouping => CheckingResult.Failure($"Square {grouping.Key} is filled more than once."))
                .FirstOrDefault(CheckingResult.Success());
    }

    private sealed class NoDuplicateNumbersInSameColumnVerifier : FutoshikiSolutionVerifier
    {
        internal override CheckingResult VerifyCorrect(IReadOnlyList<NumberedSquare> solution, FutoshikiProblem problem) =>
            solution.Concat(problem.FilledSquares)
                .GroupBy(filledSquare => new CheckingItem(filledSquare.Square.Column, filledSquare.Number), _ => 1)
                .Where(grouping => grouping.Count() > 1)
                .Select(grouping =>
                    CheckingResult.Failure(
                        $"Number {grouping.Key.Number} occurs more than once in column {grouping.Key.Column}."))
                .FirstOrDefault(CheckingResult.Success());

        private readonly record struct CheckingItem(int Column, int Number);
    }

    private sealed class NoDuplicateNumbersInSameRowVerifier : FutoshikiSolutionVerifier
    {
        internal override CheckingResult VerifyCorrect(IReadOnlyList<NumberedSquare> solution, FutoshikiProblem problem) =>
            solution.Concat(problem.FilledSquares)
                .GroupBy(filledSquare => new CheckingItem(filledSquare.Square.Row, filledSquare.Number), _ => 1)
                .Where(grouping => grouping.Count() > 1)
                .Select(grouping =>
                    CheckingResult.Failure($"Number {grouping.Key.Number} occurs more than once in row {grouping.Key.Row}."))
                .FirstOrDefault(CheckingResult.Success());

        private readonly record struct CheckingItem(int Row, int Number);
    }

    private sealed class AllSignsSatisfiedVerifier : FutoshikiSolutionVerifier
    {
        internal override CheckingResult VerifyCorrect(IReadOnlyList<NumberedSquare> solution, FutoshikiProblem problem)
        {
            Dictionary<Square, int> combined = solution.Concat(problem.FilledSquares)
                .ToDictionary(filledSquare => filledSquare.Square, filledSquare => filledSquare.Number);

            IEnumerable<CheckingResult> greaterThanSignQuery = problem.GreaterThanSigns
                .Where(sign => combined[sign.FirstSquare] <= combined[sign.SecondSquare])
                .Select(sign => CheckingResult.Failure($"Sign {sign} is not satisfied."));

            IEnumerable<CheckingResult> lessThanSignQuery = problem.LessThanSigns
                .Where(sign => combined[sign.FirstSquare] >= combined[sign.SecondSquare])
                .Select(sign => CheckingResult.Failure($"Sign {sign} is not satisfied."));

            return greaterThanSignQuery.Concat(lessThanSignQuery).FirstOrDefault(CheckingResult.Success());
        }
    }
}
