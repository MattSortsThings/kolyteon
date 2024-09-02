using Kolyteon.Common;
using Kolyteon.Common.Internals;

namespace Kolyteon.Sudoku;

/// <summary>
///     Can generate a random, solvable Sudoku problem from parameters specified by the client.
/// </summary>
public sealed class SudokuGenerator : ISudokuGenerator
{
    private const int MinEmptySquares = 1;
    private const int MaxEmptySquares = 80;
    private readonly IRandom _random;

    /// <summary>
    ///     Initializes a new <see cref="SudokuGenerator" /> instance using a default seed value.
    /// </summary>
    public SudokuGenerator()
    {
        _random = new SystemRandom();
    }

    /// <summary>
    ///     Initializes a new <see cref="SudokuGenerator" /> instance using the specified seed value.
    /// </summary>
    /// <param name="seed">
    ///     A number used to calculate a starting value for the pseudo-random number sequence used by the
    ///     generator algorithm. If a negative number is specified, the absolute value of the number is used.
    /// </param>
    public SudokuGenerator(int seed)
    {
        _random = new SystemRandom(seed);
    }

    internal SudokuGenerator(IRandom random)
    {
        _random = random ?? throw new ArgumentNullException(nameof(random));
    }

    /// <inheritdoc />
    public SudokuProblem Generate(int emptySquares)
    {
        ThrowIfInvalidEmptySquares(emptySquares, nameof(emptySquares));

        int?[,] grid = InitializeGrid();
        Shuffle(grid);
        EliminateNumbers(grid, emptySquares);

        return SudokuProblem.FromGrid(grid);
    }

    /// <inheritdoc />
    public void UseSeed(int seed) => _random.UseSeed(seed);

    private void Shuffle(int?[,] grid)
    {
        for (int i = 0; i < 10; i++)
        {
            SelectShuffleAction().Invoke(grid);
        }
    }

    private Action<int?[,]> SelectShuffleAction()
    {
        int i = _random.Next(6);

        return i switch
        {
            0 => SwapColumnsWithinStack,
            1 => SwapRowsWithinBand,
            2 => SwapStacks,
            3 => SwapBands,
            _ => SwapNumbers
        };
    }

    private void EliminateNumbers(int?[,] grid, int emptySquares)
    {
        Square[] squares = (from column in Enumerable.Range(0, grid.GetLength(1))
            from row in Enumerable.Range(0, grid.GetLength(0))
            select Square.FromColumnAndRow(column, row)).ToArray();

        for (int i = 0; i < emptySquares; i++)
        {
            int swap = _random.Next(i, squares.Length);
            (Square squareToEliminate, squares[swap]) = (squares[swap], squares[i]);

            grid.EliminateNumberInSquare(squareToEliminate);
        }
    }

    private void SwapNumbers(int?[,] grid)
    {
        int x = _random.Next(1, 10);
        int y = _random.Next(1, 10);

        grid.SwapNumbers(x, y);
    }

    private void SwapColumnsWithinStack(int?[,] grid)
    {
        int randomNumber = _random.Next(9);
        switch (randomNumber)
        {
            case 0:
                grid.SwapColumns(0, 1);

                break;
            case 1:
                grid.SwapColumns(0, 2);

                break;
            case 2:
                grid.SwapColumns(1, 2);

                break;
            case 3:
                grid.SwapColumns(3, 4);

                break;
            case 4:
                grid.SwapColumns(3, 5);

                break;
            case 5:
                grid.SwapColumns(4, 5);

                break;
            case 6:
                grid.SwapColumns(6, 7);

                break;
            case 7:
                grid.SwapColumns(6, 8);

                break;
            default:
                grid.SwapColumns(7, 8);

                break;
        }
    }

    private void SwapRowsWithinBand(int?[,] grid)
    {
        int randomNumber = _random.Next(9);
        switch (randomNumber)
        {
            case 0:
                grid.SwapRows(0, 1);

                break;
            case 1:
                grid.SwapRows(0, 2);

                break;
            case 2:
                grid.SwapRows(1, 2);

                break;
            case 3:
                grid.SwapRows(3, 4);

                break;
            case 4:
                grid.SwapRows(3, 5);

                break;
            case 5:
                grid.SwapRows(4, 5);

                break;
            case 6:
                grid.SwapRows(6, 7);

                break;
            case 7:
                grid.SwapRows(6, 8);

                break;
            default:
                grid.SwapRows(7, 8);

                break;
        }
    }

    private void SwapStacks(int?[,] grid)
    {
        int randomNumber = _random.Next(3);
        switch (randomNumber)
        {
            case 0:
                grid.SwapColumns(0, 3);
                grid.SwapColumns(1, 4);
                grid.SwapColumns(2, 5);

                break;
            case 1:
                grid.SwapColumns(0, 6);
                grid.SwapColumns(1, 7);
                grid.SwapColumns(2, 8);

                break;
            default:
                grid.SwapColumns(3, 6);
                grid.SwapColumns(4, 7);
                grid.SwapColumns(5, 8);

                break;
        }
    }

    private void SwapBands(int?[,] grid)
    {
        int randomNumber = _random.Next(3);
        switch (randomNumber)
        {
            case 0:
                grid.SwapRows(0, 3);
                grid.SwapRows(1, 4);
                grid.SwapRows(2, 5);

                break;
            case 1:
                grid.SwapRows(0, 6);
                grid.SwapRows(1, 7);
                grid.SwapRows(2, 8);

                break;
            default:
                grid.SwapRows(3, 6);
                grid.SwapRows(4, 7);
                grid.SwapRows(5, 8);

                break;
        }
    }

    private static void ThrowIfInvalidEmptySquares(int emptySquares, string paramName)
    {
        if (emptySquares is < MinEmptySquares or > MaxEmptySquares)
        {
            throw new ArgumentOutOfRangeException(paramName, emptySquares, "Value must be greater than 0 and less than 81.");
        }
    }

    private static int?[,] InitializeGrid() => new int?[,]
    {
        { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
        { 4, 5, 6, 7, 8, 9, 1, 2, 3 },
        { 7, 8, 9, 1, 2, 3, 4, 5, 6 },
        { 2, 3, 4, 5, 6, 7, 8, 9, 1 },
        { 5, 6, 7, 8, 9, 1, 2, 3, 4 },
        { 8, 9, 1, 2, 3, 4, 5, 6, 7 },
        { 3, 4, 5, 6, 7, 8, 9, 1, 2 },
        { 6, 7, 8, 9, 1, 2, 3, 4, 5 },
        { 9, 1, 2, 3, 4, 5, 6, 7, 8 }
    };
}
