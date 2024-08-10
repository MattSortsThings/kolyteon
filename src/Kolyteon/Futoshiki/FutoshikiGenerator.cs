using Kolyteon.Common;
using Kolyteon.Common.Internals;
using Kolyteon.Futoshiki.Internals;

namespace Kolyteon.Futoshiki;

/// <summary>
///     Can generate a random, solvable Futoshiki problem from parameters specified by the client.
/// </summary>
public sealed class FutoshikiGenerator : IFutoshikiGenerator
{
    private const int MinEmptySquares = 1;
    private readonly IRandom _random;

    /// <summary>
    ///     Initializes a new <see cref="FutoshikiGenerator" /> instance using a default seed value.
    /// </summary>
    public FutoshikiGenerator()
    {
        _random = new SystemRandom();
    }

    /// <summary>
    ///     Initializes a new <see cref="FutoshikiGenerator" /> instance using the specified seed value.
    /// </summary>
    /// <param name="seed">
    ///     A number used to calculate a starting value for the pseudo-random number sequence used by the
    ///     generator algorithm. If a negative number is specified, the absolute value of the number is used.
    /// </param>
    public FutoshikiGenerator(int seed)
    {
        _random = new SystemRandom(seed);
    }

    internal FutoshikiGenerator(IRandom random)
    {
        _random = random ?? throw new ArgumentNullException(nameof(random));
    }

    /// <inheritdoc />
    public FutoshikiProblem Generate(int gridSideLength, int emptySquares)
    {
        ThrowIfInvalidGridSideLength(gridSideLength, nameof(gridSideLength));
        ThrowIfInvalidEmptySquares(emptySquares, nameof(emptySquares), gridSideLength);

        GeneratedProblem problem = InitializeGeneratedProblem(gridSideLength, emptySquares);

        ShuffleGrid(problem);
        AddSigns(problem);
        EliminateNumbers(problem);

        IFutoshikiProblemBuilder.ISignAdder builder = FutoshikiProblem.Create()
            .FromGrid(problem.Grid);

        foreach (GreaterThanSign sign in problem.GreaterThanSigns)
        {
            builder.AddSign(sign);
        }

        foreach (LessThanSign sign in problem.LessThanSigns)
        {
            builder.AddSign(sign);
        }

        return builder.Build();
    }


    /// <inheritdoc />
    public void UseSeed(int seed) => _random.UseSeed(seed);

    private void ShuffleGrid(GeneratedProblem problem)
    {
        for (int i = 0; i < 10; i++)
        {
            SelectShuffleAction().Invoke(problem.Grid);
        }
    }

    private void AddSigns(GeneratedProblem problem)
    {
        int width = problem.Grid.GetLength(1);
        int height = problem.Grid.GetLength(0);

        for (int attempts = 20; problem.CanAddAnotherSign && attempts > 0; attempts--)
        {
            (Square firstSquare, Square secondSquare) = SelectTwoAdjacentSquares(width, height);

            problem.AddSignBetween(firstSquare, secondSquare);
        }
    }

    private void EliminateNumbers(GeneratedProblem problem)
    {
        foreach (Square square in problem.GetFirstSquaresOfAllSigns())
        {
            problem.TryAddEmptySquare(square);
        }

        Square[] squares = (from column in Enumerable.Range(0, problem.Grid.GetLength(1))
            from row in Enumerable.Range(0, problem.Grid.GetLength(0))
            select Square.FromColumnAndRow(column, row)).ToArray();

        for (int i = 0; i < squares.Length && problem.CanAddAnotherEmptySquare; i++)
        {
            int swap = _random.Next(i, squares.Length);
            (Square squareToEliminate, squares[swap]) = (squares[swap], squares[i]);
            problem.TryAddEmptySquare(squareToEliminate);
        }
    }

    private (Square, Square) SelectTwoAdjacentSquares(int gridWidth, int gridHeight)
    {
        int column = _random.Next(gridWidth);
        int row = _random.Next(gridHeight);

        return _random.Next() % 2 == 0
            ? row > 0
                ? (Square.FromColumnAndRow(column, row - 1), Square.FromColumnAndRow(column, row))
                : (Square.FromColumnAndRow(column, row), Square.FromColumnAndRow(column, row + 1))
            : column > 0
                ? (Square.FromColumnAndRow(column - 1, row), Square.FromColumnAndRow(column, row))
                : (Square.FromColumnAndRow(column, row), Square.FromColumnAndRow(column + 1, row));
    }

    private GeneratedProblem InitializeGeneratedProblem(int gridSideLength, int emptySquares) => new()
    {
        Grid = InitializeGrid(gridSideLength),
        GreaterThanSigns = new HashSet<GreaterThanSign>(emptySquares),
        LessThanSigns = new HashSet<LessThanSign>(emptySquares),
        RequiredSigns = Math.Min(emptySquares, _random.Next(gridSideLength, (int)Math.Pow(gridSideLength, 1.5))),
        RequiredEmptySquares = emptySquares
    };

    private Action<int?[,]> SelectShuffleAction()
    {
        int i = _random.Next(4);

        return i switch
        {
            0 => SwapColumns,
            1 => SwapRows,
            _ => SwapNumbers
        };
    }

    private void SwapRows(int?[,] grid)
    {
        int rowX = _random.Next(grid.GetLength(0));
        int rowY = _random.Next(grid.GetLength(0));

        grid.SwapRows(rowX, rowY);
    }


    private void SwapColumns(int?[,] grid)
    {
        int columnX = _random.Next(grid.GetLength(1));
        int columnY = _random.Next(grid.GetLength(1));

        grid.SwapColumns(columnX, columnY);
    }

    private void SwapNumbers(int?[,] grid)
    {
        int x = _random.Next(1, grid.GetLength(1) + 1);
        int y = _random.Next(1, grid.GetLength(1) + 1);

        grid.SwapNumbers(x, y);
    }

    private static int?[,] InitializeGrid(int sideLength) => sideLength switch
    {
        4 => new int?[,] { { 1, 2, 3, 4 }, { 2, 3, 4, 1 }, { 3, 4, 1, 2 }, { 4, 1, 2, 3 } },
        5 => new int?[,] { { 1, 2, 3, 4, 5 }, { 2, 3, 4, 5, 1 }, { 3, 4, 5, 1, 2 }, { 4, 5, 1, 2, 3 }, { 5, 1, 2, 3, 4 } },
        6 => new int?[,]
        {
            { 1, 2, 3, 4, 5, 6 },
            { 2, 3, 4, 5, 6, 1 },
            { 3, 4, 5, 6, 1, 2 },
            { 4, 5, 6, 1, 2, 3 },
            { 5, 6, 1, 2, 3, 4 },
            { 6, 1, 2, 3, 4, 5 }
        },
        7 => new int?[,]
        {
            { 1, 2, 3, 4, 5, 6, 7 },
            { 2, 3, 4, 5, 6, 7, 1 },
            { 3, 4, 5, 6, 7, 1, 2 },
            { 4, 5, 6, 7, 1, 2, 3 },
            { 5, 6, 7, 1, 2, 3, 4 },
            { 6, 7, 1, 2, 3, 4, 5 },
            { 7, 1, 2, 3, 4, 5, 6 }
        },
        8 => new int?[,]
        {
            { 1, 2, 3, 4, 5, 6, 7, 8 },
            { 2, 3, 4, 5, 6, 7, 8, 1 },
            { 3, 4, 5, 6, 7, 8, 1, 2 },
            { 4, 5, 6, 7, 8, 1, 2, 3 },
            { 5, 6, 7, 8, 1, 2, 3, 4 },
            { 6, 7, 8, 1, 2, 3, 4, 5 },
            { 7, 8, 1, 2, 3, 4, 5, 6 },
            { 8, 1, 2, 3, 4, 5, 6, 7 }
        },
        _ => new int?[,]
        {
            { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
            { 2, 3, 4, 5, 6, 7, 8, 9, 1 },
            { 3, 4, 5, 6, 7, 8, 9, 1, 2 },
            { 4, 5, 6, 7, 8, 9, 1, 2, 3 },
            { 5, 6, 7, 8, 9, 1, 2, 3, 4 },
            { 6, 7, 8, 9, 1, 2, 3, 4, 5 },
            { 7, 8, 9, 1, 2, 3, 4, 5, 6 },
            { 8, 9, 1, 2, 3, 4, 5, 6, 7 },
            { 9, 1, 2, 3, 4, 5, 6, 7, 8 }
        }
    };

    private static void ThrowIfInvalidGridSideLength(int gridSideLength, string paramName)
    {
        if (gridSideLength is < FutoshikiProblem.MinGridSideLength or > FutoshikiProblem.MaxGridSideLength)
        {
            throw new ArgumentOutOfRangeException(paramName, gridSideLength,
                "Value must be not less than 4 and not greater than 9.");
        }
    }

    private static void ThrowIfInvalidEmptySquares(int emptySquares, string paramName, int gridSideLength)
    {
        if (emptySquares < MinEmptySquares || emptySquares >= gridSideLength * gridSideLength)
        {
            throw new ArgumentOutOfRangeException(paramName, emptySquares,
                "Value must be greater than 0 and less than the square of the specified grid side length.");
        }
    }

    private sealed record GeneratedProblem
    {
        public required int?[,] Grid { get; init; }

        public required HashSet<GreaterThanSign> GreaterThanSigns { get; init; }

        public required HashSet<LessThanSign> LessThanSigns { get; init; }

        public required int RequiredSigns { get; init; }

        private int EmptySquares { get; set; }

        public int RequiredEmptySquares { get; init; }

        public bool CanAddAnotherSign => GreaterThanSigns.Count + LessThanSigns.Count < RequiredSigns;

        public bool CanAddAnotherEmptySquare => EmptySquares < RequiredEmptySquares;

        public IEnumerable<Square> GetFirstSquaresOfAllSigns() =>
            GreaterThanSigns.Select(sign => sign.FirstSquare)
                .Concat(LessThanSigns.Select(sign => sign.FirstSquare));

        public void TryAddEmptySquare(in Square square)
        {
            if (Grid.EliminateNumberInSquare(in square))
            {
                EmptySquares++;
            }
        }

        public void AddSignBetween(in Square firstSquare, in Square secondSquare)
        {
            if (Grid.FirstIsGreaterThanSecond(in firstSquare, in secondSquare))
            {
                GreaterThanSigns.Add(GreaterThanSign.Between(firstSquare, secondSquare));
            }
            else
            {
                LessThanSigns.Add(LessThanSign.Between(firstSquare, secondSquare));
            }
        }
    }
}
