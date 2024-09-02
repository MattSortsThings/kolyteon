using Kolyteon.Common;
using Kolyteon.Futoshiki;
using Reqnroll;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal static partial class Transformations
{
    [Binding]
    internal static class FutoshikiProblem
    {
        private const int FourByFourGridSideLength = 4;

        [StepArgumentTransformation]
        internal static Futoshiki.FutoshikiProblem ToFourByFourFutoshikiProblem(string multiLineText)
        {
            char[][] textGrid = multiLineText.Split('\n')
                .Select(line => line.Trim().ToCharArray())
                .ToArray();

            IFutoshikiProblemBuilder.ISignAdder builder = Futoshiki.FutoshikiProblem.Create().FromGrid(ParseGrid(textGrid));

            builder = ParseGreaterThanSigns(textGrid).Aggregate(builder, (currentBuilder, sign) => currentBuilder.AddSign(sign));

            builder = ParseLessThanSigns(textGrid).Aggregate(builder, (currentBuilder, sign) => currentBuilder.AddSign(sign));

            return builder.Build();
        }

        private static int?[,] ParseGrid(char[][] textGrid)
        {
            int?[,] grid = new int?[FourByFourGridSideLength, FourByFourGridSideLength];

            for (int col = 0; col < FourByFourGridSideLength; col++)
            {
                for (int row = 0; row < FourByFourGridSideLength; row++)
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
            for (int col = 0; col < FourByFourGridSideLength; col++)
            {
                for (int row = 0; row < FourByFourGridSideLength; row++)
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
            for (int col = 0; col < FourByFourGridSideLength; col++)
            {
                for (int row = 0; row < FourByFourGridSideLength; row++)
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
    }
}
