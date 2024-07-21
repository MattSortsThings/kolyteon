using Kolyteon.Common;
using Kolyteon.Common.Internals;

namespace Kolyteon.NQueens.Internals;

internal static class SolutionVerification
{
    internal static ISolutionVerifier<IReadOnlyList<Square>, NQueensProblem> OneSquarePerQueen =>
        new OneSquarePerQueenVerifier();

    internal static ISolutionVerifier<IReadOnlyList<Square>, NQueensProblem> AllSquaresInChessBoard =>
        new AllSquaresInChessBoardVerifier();

    internal static ISolutionVerifier<IReadOnlyList<Square>, NQueensProblem> AllSquaresUnique =>
        new AllSquaresUniqueVerifier();

    internal static ISolutionVerifier<IReadOnlyList<Square>, NQueensProblem> NoCapturingSquares =>
        new NoCapturingSquaresVerifier();

    private sealed class OneSquarePerQueenVerifier : ISolutionVerifier<IReadOnlyList<Square>, NQueensProblem>
    {
        public CheckingResult VerifyCorrect(IReadOnlyList<Square> solution, NQueensProblem problem)
        {
            int squares = solution.Count;
            int queens = problem.Queens;

            return squares == queens
                ? CheckingResult.Success()
                : CheckingResult.Failure($"Solution has {(squares == 1 ? "1 square" : squares + " squares")}, " +
                                         $"but problem has {(queens == 1 ? "1 queen" : queens + " queens")}.");
        }
    }

    private sealed class AllSquaresInChessBoardVerifier : ISolutionVerifier<IReadOnlyList<Square>, NQueensProblem>
    {
        public CheckingResult VerifyCorrect(IReadOnlyList<Square> solution, NQueensProblem problem)
        {
            Block chessBoard = problem.ChessBoard;

            return solution.Where(square =>
                    !chessBoard.Contains(square))
                .Select(square =>
                    CheckingResult.Failure($"Square {square} is not inside chess board {chessBoard}."))
                .FirstOrDefault(CheckingResult.Success());
        }
    }

    private sealed class AllSquaresUniqueVerifier : ISolutionVerifier<IReadOnlyList<Square>, NQueensProblem>
    {
        public CheckingResult VerifyCorrect(IReadOnlyList<Square> solution, NQueensProblem problem) =>
            solution.SelectMany((squareAtI, i) =>
                    solution.Take(i)
                        .Where(pastSquare => pastSquare.Equals(squareAtI))
                        .Select(_ => squareAtI)
                )
                .Select(duplicateSquare =>
                    CheckingResult.Failure($"Square {duplicateSquare} occurs more than once."))
                .FirstOrDefault(CheckingResult.Success());
    }

    private sealed class NoCapturingSquaresVerifier : ISolutionVerifier<IReadOnlyList<Square>, NQueensProblem>
    {
        public CheckingResult VerifyCorrect(IReadOnlyList<Square> solution, NQueensProblem problem) =>
            solution.SelectMany(
                    (squareAtI, i) =>
                        solution.Take(i)
                            .Where(pastSquare => pastSquare.Captures(squareAtI))
                            .Select(pastSquare => new { pastSquare, squareAtI })
                )
                .Select(squares =>
                    CheckingResult.Failure($"Squares {squares.pastSquare} and {squares.squareAtI} capture each other."))
                .FirstOrDefault(CheckingResult.Success());
    }
}
