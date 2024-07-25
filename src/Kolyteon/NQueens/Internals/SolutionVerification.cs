using Kolyteon.Common;
using Kolyteon.Common.Internals;

namespace Kolyteon.NQueens.Internals;

internal static class SolutionVerification
{
    internal static NQueensSolutionVerifier OneSquarePerQueen => new OneSquarePerQueenVerifier();

    internal static NQueensSolutionVerifier AllSquaresInChessBoard => new AllSquaresInChessBoardVerifier();

    internal static NQueensSolutionVerifier AllSquaresUnique => new AllSquaresUniqueVerifier();

    internal static NQueensSolutionVerifier NoCapturingSquares => new NoCapturingSquaresVerifier();

    internal abstract class NQueensSolutionVerifier : SolutionVerifier<IReadOnlyList<Square>, NQueensProblem>;

    private sealed class OneSquarePerQueenVerifier : NQueensSolutionVerifier
    {
        internal override CheckingResult VerifyCorrect(IReadOnlyList<Square> solution, NQueensProblem problem)
        {
            int squares = solution.Count;
            int queens = problem.Queens;

            return squares == queens
                ? CheckingResult.Success()
                : CheckingResult.Failure($"Solution has {(squares == 1 ? "1 square" : squares + " squares")}, " +
                                         $"but problem has {(queens == 1 ? "1 queen" : queens + " queens")}.");
        }
    }

    private sealed class AllSquaresInChessBoardVerifier : NQueensSolutionVerifier
    {
        internal override CheckingResult VerifyCorrect(IReadOnlyList<Square> solution, NQueensProblem problem)
        {
            Block chessBoard = problem.ChessBoard;

            return solution.Where(square =>
                    !chessBoard.Contains(square))
                .Select(square =>
                    CheckingResult.Failure($"Square {square} is not inside chess board {chessBoard}."))
                .FirstOrDefault(CheckingResult.Success());
        }
    }

    private sealed class AllSquaresUniqueVerifier : NQueensSolutionVerifier
    {
        internal override CheckingResult VerifyCorrect(IReadOnlyList<Square> solution, NQueensProblem problem) =>
            solution.SelectMany((squareAtI, i) =>
                    solution.Take(i)
                        .Where(pastSquare => pastSquare.Equals(squareAtI))
                        .Select(_ => squareAtI)
                )
                .Select(duplicateSquare =>
                    CheckingResult.Failure($"Square {duplicateSquare} occurs more than once."))
                .FirstOrDefault(CheckingResult.Success());
    }

    private sealed class NoCapturingSquaresVerifier : NQueensSolutionVerifier
    {
        internal override CheckingResult VerifyCorrect(IReadOnlyList<Square> solution, NQueensProblem problem) =>
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
