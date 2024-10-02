using Kolyteon.Common;
using Kolyteon.Futoshiki;
using Kolyteon.GraphColouring;
using Kolyteon.MapColouring;
using Kolyteon.NQueens;
using Kolyteon.Shikaku;
using Kolyteon.Solving;
using Kolyteon.Sudoku;
using Kolyteon.Tests.Utils.TestAssertions;

namespace Kolyteon.Tests.Integration.ProblemSolving.TestUtils;

public static class SolvingResultExtensions
{
    public static void VerifyCorrectSolution(this SolvingResult<Square, int> result, FutoshikiProblem problem) =>
        problem.VerifyCorrect(result.Solution.ToFutoshikiSolution()).Should().BeSuccessful();

    public static void VerifyCorrectSolution(this SolvingResult<Node, Colour> result, GraphColouringProblem problem) =>
        problem.VerifyCorrect(result.Solution.ToGraphColouringSolution()).Should().BeSuccessful();

    public static void VerifyCorrectSolution(this SolvingResult<Block, Colour> result, MapColouringProblem problem) =>
        problem.VerifyCorrect(result.Solution.ToMapColouringSolution()).Should().BeSuccessful();

    public static void VerifyCorrectSolution(this SolvingResult<int, Square> result, NQueensProblem problem) =>
        problem.VerifyCorrect(result.Solution.ToNQueensSolution()).Should().BeSuccessful();

    public static void VerifyCorrectSolution(this SolvingResult<NumberedSquare, Block> result, ShikakuProblem problem) =>
        problem.VerifyCorrect(result.Solution.ToShikakuSolution()).Should().BeSuccessful();

    public static void VerifyCorrectSolution(this SolvingResult<Square, int> result, SudokuProblem problem) =>
        problem.VerifyCorrect(result.Solution.ToSudokuSolution()).Should().BeSuccessful();
}
