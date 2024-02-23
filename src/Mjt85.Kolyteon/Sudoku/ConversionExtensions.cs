using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.Sudoku;

/// <summary>
///     Contains a single conversion extension method.
/// </summary>
public static class ConversionExtensions
{
    /// <summary>
    ///     Converts this enumerable into a Sudoku puzzle solution list.
    /// </summary>
    /// <remarks>
    ///     The returned list can be used as the argument to a <see cref="SudokuPuzzle.ValidSolution" /> method invocation on
    ///     a <see cref="SudokuPuzzle" /> instance.
    /// </remarks>
    /// <param name="assignments">The assignments to be converted.</param>
    /// <returns>
    ///     A new read-only list of <see cref="FilledCell" /> instances, containing one instance for every assignment in this
    ///     enumerable.
    /// </returns>
    public static IReadOnlyList<FilledCell> ToPuzzleSolution(this IEnumerable<Assignment<EmptyCell, int>> assignments)
    {
        return assignments.Select(a =>
        {
            var ((col, row, _), num) = a;

            return new FilledCell(col, row, num);
        }).ToArray();
    }
}
