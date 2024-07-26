using Kolyteon.Common;
using Kolyteon.Shikaku;
using Kolyteon.Sudoku;
using Reqnroll;

namespace Kolyteon.Tests.Acceptance.TestUtils;

[Binding]
internal sealed class Transformations
{
    [StepArgumentTransformation]
    internal static ShikakuProblem ToShikakuProblem(string multiLineText)
    {
        string[][] textGrid = multiLineText.Split('\n')
            .Select(line => line.Trim().Split(' ').ToArray())
            .ToArray();

        int rows = textGrid.Length;
        int columns = textGrid[0].Length;

        int?[,] grid = new int?[rows, columns];

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                if (int.TryParse(textGrid[row][column], out int number))
                {
                    grid[row, column] = number;
                }
            }
        }

        return ShikakuProblem.FromGrid(grid);
    }

    [StepArgumentTransformation]
    internal static SudokuProblem ToSudokuProblem(string multiLineText)
    {
        string[][] textGrid = multiLineText.Split('\n')
            .Select(line => line.Trim().Split(' ').ToArray())
            .ToArray();

        int rows = textGrid.Length;
        int columns = textGrid[0].Length;

        int?[,] grid = new int?[rows, columns];

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                if (int.TryParse(textGrid[row][column], out int number))
                {
                    grid[row, column] = number;
                }
            }
        }

        return SudokuProblem.FromGrid(grid);
    }

    [StepArgumentTransformation]
    internal static Dimensions ToDimensions(string text)
    {
        string[] items = text.Split('x');

        int width = int.Parse(items[0]);
        int height = int.Parse(items[1]);

        return Dimensions.FromWidthAndHeight(width, height);
    }
}
