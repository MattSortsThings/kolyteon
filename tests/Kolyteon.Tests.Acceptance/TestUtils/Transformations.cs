using Kolyteon.Common;
using Kolyteon.Futoshiki;
using Kolyteon.Shikaku;
using Kolyteon.Sudoku;
using Reqnroll;

namespace Kolyteon.Tests.Acceptance.TestUtils;

[Binding]
internal sealed class Transformations
{
    [StepArgumentTransformation]
    internal static FutoshikiProblem ToFiveByFiveFutoshikiProblem(string multiLineText)
    {
        char[][] textGrid = multiLineText.Split('\n')
            .Select(line => line.Trim().ToCharArray())
            .ToArray();

        IFutoshikiProblemBuilder.ISignAdder builder = FutoshikiProblem.Create().FromGrid(ParseGrid(textGrid));

        builder = ParseGreaterThanSigns(textGrid).Aggregate(builder, (currentBuilder, sign) => currentBuilder.AddSign(sign));

        builder = ParseLessThanSigns(textGrid).Aggregate(builder, (currentBuilder, sign) => currentBuilder.AddSign(sign));

        return builder.Build();
    }

    private static int?[,] ParseGrid(char[][] textGrid)
    {
        int?[,] grid = new int?[5, 5];

        for (int col = 0; col < 5; col++)
        {
            for (int row = 0; row < 5; row++)
            {
                int numberCol = (col * 4) + 2;
                int numberRow = (row * 2) + 1;

                if (int.TryParse(textGrid[numberRow][numberCol].ToString(), out int number))
                {
                    grid[row, col] = number;
                }
            }
        }

        return grid;
    }

    private static IEnumerable<GreaterThanSign> ParseGreaterThanSigns(char[][] textGrid)
    {
        for (int col = 0; col < 5; col++)
        {
            for (int row = 0; row < 5; row++)
            {
                int numberCol = (col * 4) + 2;
                int numberRow = (row * 2) + 1;

                char rightSignChar = textGrid[numberRow][numberCol + 2];
                char bottomSignChar = textGrid[numberRow + 1][numberCol];

                if (rightSignChar == '>')
                {
                    yield return GreaterThanSign.Between(Square.FromColumnAndRow(col, row),
                        Square.FromColumnAndRow(col + 1, row));
                }

                if (bottomSignChar == '>')
                {
                    yield return GreaterThanSign.Between(Square.FromColumnAndRow(col, row),
                        Square.FromColumnAndRow(col, row + 1));
                }
            }
        }
    }

    private static IEnumerable<LessThanSign> ParseLessThanSigns(char[][] textGrid)
    {
        for (int col = 0; col < 5; col++)
        {
            for (int row = 0; row < 5; row++)
            {
                int numberCol = (col * 4) + 2;
                int numberRow = (row * 2) + 1;

                char rightSignChar = textGrid[numberRow][numberCol + 2];
                char bottomSignChar = textGrid[numberRow + 1][numberCol];

                if (rightSignChar == '<')
                {
                    yield return LessThanSign.Between(Square.FromColumnAndRow(col, row),
                        Square.FromColumnAndRow(col + 1, row));
                }

                if (bottomSignChar == '<')
                {
                    yield return LessThanSign.Between(Square.FromColumnAndRow(col, row),
                        Square.FromColumnAndRow(col, row + 1));
                }
            }
        }
    }

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
    internal static Dimensions ToDimensions(string text) => Dimensions.Parse(text);
}
