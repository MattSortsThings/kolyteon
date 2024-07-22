using Kolyteon.Shikaku;
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
}
