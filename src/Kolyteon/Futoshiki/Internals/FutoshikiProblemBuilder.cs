using Kolyteon.Common;

namespace Kolyteon.Futoshiki.Internals;

internal sealed class FutoshikiProblemBuilder : IFutoshikiProblemBuilder, IFutoshikiProblemBuilder.ISignAdder
{
    private readonly HashSet<GreaterThanSign> _greaterThanSigns = new(4);
    private readonly HashSet<LessThanSign> _lessThanSigns = new(4);
    private NumberedSquare[]? _filledSquares;
    private Block _problemGrid;

    /// <inheritdoc />
    public IFutoshikiProblemBuilder.ISignAdder FromGrid(int?[,] grid)
    {
        ArgumentNullException.ThrowIfNull(grid);
        ThrowIfInvalidGridDimensions(grid);

        _problemGrid = grid.ToProblemGrid();
        _filledSquares = grid.ToFilledSquares();

        return this;
    }

    /// <inheritdoc />
    public FutoshikiProblem Build()
    {
        FutoshikiProblem problem = new(_problemGrid,
            _filledSquares!,
            _greaterThanSigns.OrderBy(sign => sign).ToArray(),
            _lessThanSigns.OrderBy(sign => sign).ToArray());

        ThrowIfInvalidProblem(problem);

        return problem;
    }

    /// <inheritdoc />
    public IFutoshikiProblemBuilder.ISignAdder AddSign(GreaterThanSign sign)
    {
        ArgumentNullException.ThrowIfNull(sign);

        _greaterThanSigns.Add(sign);

        return this;
    }

    /// <inheritdoc />
    public IFutoshikiProblemBuilder.ISignAdder AddSign(LessThanSign sign)
    {
        ArgumentNullException.ThrowIfNull(sign);

        _lessThanSigns.Add(sign);

        return this;
    }

    /// <inheritdoc />
    public IFutoshikiProblemBuilder.ISignAdder AddSigns(IEnumerable<GreaterThanSign> signs)
    {
        ArgumentNullException.ThrowIfNull(signs);
        foreach (GreaterThanSign sign in signs)
        {
            _greaterThanSigns.Add(sign);
        }

        return this;
    }

    /// <inheritdoc />
    public IFutoshikiProblemBuilder.ISignAdder AddSigns(IEnumerable<LessThanSign> signs)
    {
        ArgumentNullException.ThrowIfNull(signs);
        foreach (LessThanSign sign in signs)
        {
            _lessThanSigns.Add(sign);
        }

        return this;
    }

    private static void ThrowIfInvalidProblem(FutoshikiProblem problem)
    {
        CheckingResult validationResult = ProblemValidation.AtLeastOneEmptySquare
            .Then(ProblemValidation.AllFilledSquareNumbersInRange)
            .Then(ProblemValidation.NoDuplicateNumbersInSameColumn)
            .Then(ProblemValidation.NoDuplicateNumbersInSameRow)
            .Then(ProblemValidation.AllGreaterThanSignsInGrid)
            .Then(ProblemValidation.AllLessThanSignsInGrid)
            .Then(ProblemValidation.NoSignsInSameLocation).Validate(problem);

        if (validationResult is { IsSuccessful: false, FirstError: not null })
        {
            throw new InvalidProblemException(validationResult.FirstError);
        }
    }

    private static void ThrowIfInvalidGridDimensions(int?[,] grid)
    {
        if (!IsSquareWithSideLengthBetweenFourAndNine(grid))
        {
            throw new ArgumentException("Invalid grid dimensions. Rank-0 and rank-1 lengths must be equal to each other, " +
                                        "not less than 4, and not greater than 9.");
        }
    }

    private static bool IsSquareWithSideLengthBetweenFourAndNine(int?[,] grid) =>
        grid.GetLength(0) == grid.GetLength(1)
        && grid.GetLength(0) is >= FutoshikiProblem.MinGridSideLength and <= FutoshikiProblem.MaxGridSideLength
        && grid.GetLength(1) is >= FutoshikiProblem.MinGridSideLength and <= FutoshikiProblem.MaxGridSideLength;
}
