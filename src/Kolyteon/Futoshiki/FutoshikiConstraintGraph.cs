using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Kolyteon.Common;
using Kolyteon.Modelling;

namespace Kolyteon.Futoshiki;

/// <summary>
///     Models a <see cref="FutoshikiProblem" /> instance as a generic binary CSP.
/// </summary>
public sealed class FutoshikiConstraintGraph : ConstraintGraph<Square, int, FutoshikiProblem>
{
    private readonly ProblemSquare[,] _problemGrid = InitializeProblemGrid();
    private int _problemSize = FutoshikiProblem.MinGridSideLength;

    /// <summary>
    ///     Initializes a new <see cref="FutoshikiConstraintGraph" /> instance with a default initial
    ///     <see cref="ConstraintGraph{TVariable,TDomainValue,TProblem}.Capacity" /> of 0.
    /// </summary>
    public FutoshikiConstraintGraph() { }

    /// <summary>
    ///     Initializes a new <see cref="FutoshikiConstraintGraph" /> instance with the specified initial
    ///     <see cref="ConstraintGraph{TVariable,TDomainValue,TProblem}.Capacity" />.
    /// </summary>
    /// <param name="capacity">
    ///     A non-negative integer. The initial capacity of the <see cref="FutoshikiConstraintGraph" /> instance.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity" /> is negative.</exception>
    public FutoshikiConstraintGraph(int capacity) : base(capacity) { }

    /// <summary>
    ///     Creates and returns a new <see cref="FutoshikiConstraintGraph" /> instance that is modelling the specified
    ///     Futoshiki problem as a binary CSP.
    /// </summary>
    /// <param name="problem">The problem to be modelled.</param>
    /// <returns>A new <see cref="FutoshikiConstraintGraph" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="problem" /> is <see langword="null" />.</exception>
    public static FutoshikiConstraintGraph ModellingProblem(FutoshikiProblem problem)
    {
        ArgumentNullException.ThrowIfNull(problem);

        FutoshikiConstraintGraph constraintGraph = new(problem.Grid.AreaInSquares - problem.FilledSquares.Count);
        constraintGraph.Model(problem);

        return constraintGraph;
    }

    /// <inheritdoc />
    protected override void PopulateProblemData(FutoshikiProblem problem)
    {
        ((_, (int problemSize, _)),
            IReadOnlyList<NumberedSquare> filledSquares,
            IReadOnlyList<GreaterThanSign> greaterThanSigns,
            IReadOnlyList<LessThanSign> lessThanSigns) = problem;

        _problemSize = problemSize;
        PopulateFilledSquares(filledSquares);
        PopulateGreaterThanSigns(greaterThanSigns);
        PopulateLessThanSigns(lessThanSigns);
    }

    /// <inheritdoc />
    protected override IEnumerable<Square> GetVariables()
    {
        for (int row = 0; row < _problemSize; row++)
        {
            for (int column = 0; column < _problemSize; column++)
            {
                if (!_problemGrid[row, column].FixedNumber.HasValue)
                {
                    yield return Square.FromColumnAndRow(column, row);
                }
            }
        }
    }

    /// <inheritdoc />
    protected override IEnumerable<int> GetDomainValues(Square presentVariable) =>
        _problemGrid[presentVariable.Row, presentVariable.Column].GetPossibleNumbers(_problemSize);

    /// <inheritdoc />
    protected override bool TryGetBinaryPredicate(Square firstVariable,
        Square secondVariable,
        [NotNullWhen(true)] out Func<int, int, bool>? binaryPredicate)
    {
        (int firstColumn, int firstRow) = firstVariable;
        (int secondColumn, int secondRow) = secondVariable;

        binaryPredicate = null;

        if (firstColumn == secondColumn)
        {
            binaryPredicate = firstRow + 1 == secondRow
                ? _problemGrid[firstRow, firstColumn].BottomPredicate
                : UnequalNumbers;
        }

        if (firstRow == secondRow)
        {
            binaryPredicate = firstColumn + 1 == secondColumn
                ? _problemGrid[firstRow, firstColumn].RightPredicate
                : UnequalNumbers;
        }

        return binaryPredicate is not null;
    }

    /// <inheritdoc />
    protected override void ClearProblemData()
    {
        for (int row = 0; row < _problemSize; row++)
        {
            for (int column = 0; column < _problemSize; column++)
            {
                _problemGrid[row, column].Reset();
            }
        }

        _problemSize = FutoshikiProblem.MinGridSideLength;
    }

    private void PopulateFilledSquares(IReadOnlyList<NumberedSquare> filledSquares)
    {
        foreach (((int column, int row), int number) in filledSquares)
        {
            _problemGrid[row, column].SetFixedNumber(number);

            for (int i = 0; i < _problemSize; i++)
            {
                _problemGrid[i, column].EliminateNumber(number);
                _problemGrid[row, i].EliminateNumber(number);
            }
        }
    }

    private void PopulateGreaterThanSigns(IReadOnlyList<GreaterThanSign> greaterThanSigns)
    {
        foreach (((int firstColumn, int firstRow), (int secondColumn, int secondRow)) in greaterThanSigns)
        {
            ProblemSquare firstSquare = _problemGrid[firstRow, firstColumn];
            ProblemSquare secondSquare = _problemGrid[secondRow, secondColumn];

            if (firstColumn == secondColumn)
            {
                firstSquare.BottomPredicate = FirstNumberGreaterThanSecondNumber;
            }
            else
            {
                firstSquare.RightPredicate = FirstNumberGreaterThanSecondNumber;
            }

            if (firstSquare.FixedNumber.GetValueOrDefault() is var firstSquareFixedNumber and > 0)
            {
                secondSquare.EliminateRange(firstSquareFixedNumber, _problemSize);
            }

            if (secondSquare.FixedNumber.GetValueOrDefault() is var secondSquareFixedNumber and > 0)
            {
                firstSquare.EliminateRange(1, secondSquareFixedNumber);
            }
        }
    }

    private void PopulateLessThanSigns(IReadOnlyList<LessThanSign> lessThanSigns)
    {
        foreach (((int firstColumn, int firstRow), (int secondColumn, int secondRow)) in lessThanSigns)
        {
            ProblemSquare firstSquare = _problemGrid[firstRow, firstColumn];
            ProblemSquare secondSquare = _problemGrid[secondRow, secondColumn];

            if (firstColumn == secondColumn)
            {
                firstSquare.BottomPredicate = FirstNumberLessThanSecondNumber;
            }
            else
            {
                firstSquare.RightPredicate = FirstNumberLessThanSecondNumber;
            }

            if (firstSquare.FixedNumber.GetValueOrDefault() is var firstSquareFixedNumber and > 0)
            {
                secondSquare.EliminateRange(1, firstSquareFixedNumber);
            }

            if (secondSquare.FixedNumber.GetValueOrDefault() is var secondSquareFixedNumber and > 0)
            {
                firstSquare.EliminateRange(secondSquareFixedNumber, _problemSize);
            }
        }
    }

    private static ProblemSquare[,] InitializeProblemGrid()
    {
        ProblemSquare[,] grid = new ProblemSquare[FutoshikiProblem.MaxGridSideLength, FutoshikiProblem.MaxGridSideLength];

        for (int row = FutoshikiProblem.MaxGridSideLength - 1; row >= 0; row--)
        {
            for (int column = FutoshikiProblem.MaxGridSideLength - 1; column >= 0; column--)
            {
                grid[row, column] = new ProblemSquare();
            }
        }

        return grid;
    }

    private static bool UnequalNumbers(int firstNumber, int secondNumber) => firstNumber != secondNumber;

    private static bool FirstNumberGreaterThanSecondNumber(int firstNumber, int secondNumber) => firstNumber > secondNumber;

    private static bool FirstNumberLessThanSecondNumber(int firstNumber, int secondNumber) => firstNumber < secondNumber;

    private sealed record ProblemSquare
    {
        private BitArray PossibleNumbers { get; } = new(FutoshikiProblem.MaxGridSideLength + 1, true);

        internal int? FixedNumber { get; private set; }

        internal Func<int, int, bool> RightPredicate { get; set; } = UnequalNumbers;

        internal Func<int, int, bool> BottomPredicate { get; set; } = UnequalNumbers;

        internal void SetFixedNumber(int number)
        {
            FixedNumber = number;
            PossibleNumbers.SetAll(false);
        }

        internal void EliminateNumber(int number) => PossibleNumbers.Set(number, false);

        internal void EliminateRange(int inclusiveMin, int inclusiveMax)
        {
            for (int number = inclusiveMin; number <= inclusiveMax; number++)
            {
                PossibleNumbers.Set(number, false);
            }
        }

        internal IEnumerable<int> GetPossibleNumbers(int problemSize)
        {
            for (int number = 1; number <= problemSize; number++)
            {
                if (PossibleNumbers[number])
                {
                    yield return number;
                }
            }
        }

        internal void Reset()
        {
            FixedNumber = null;
            RightPredicate = UnequalNumbers;
            BottomPredicate = UnequalNumbers;
            PossibleNumbers.SetAll(true);
        }
    }
}
