using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Kolyteon.Common;
using Kolyteon.Modelling;

namespace Kolyteon.Sudoku;

/// <summary>
///     Models a <see cref="SudokuProblem" /> instance as a binary CSP.
/// </summary>
public sealed class SudokuConstraintGraph : ConstraintGraph<Square, int, SudokuProblem>
{
    private const int GridSideLength = SudokuProblem.MaxNumber;
    private readonly BitArray[] _columnPossibleNumbers = InitializePossibleNumbersIndexedLookup();
    private readonly BitArray _emptySquares = new(GridSideLength * GridSideLength, true);
    private readonly BitArray _presentVariablePossibleNumbers = InitializeSinglePossibleNumbersArray();
    private readonly BitArray[] _rowPossibleNumbers = InitializePossibleNumbersIndexedLookup();
    private readonly BitArray[] _sectorPossibleNumbers = InitializePossibleNumbersIndexedLookup();

    /// <summary>
    ///     Initializes a new <see cref="SudokuConstraintGraph" /> instance with a default initial
    ///     <see cref="ConstraintGraph{TVariable,TDomainValue,TProblem}.Capacity" /> of 0.
    /// </summary>
    public SudokuConstraintGraph() { }

    /// <summary>
    ///     Initializes a new <see cref="SudokuConstraintGraph" /> instance with the specified initial
    ///     <see cref="ConstraintGraph{TVariable,TDomainValue,TProblem}.Capacity" />.
    /// </summary>
    /// <param name="capacity">
    ///     A non-negative integer. The initial capacity of the <see cref="SudokuConstraintGraph" /> instance.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity" /> is negative.</exception>
    public SudokuConstraintGraph(int capacity) : base(capacity) { }

    /// <summary>
    ///     Creates and returns a new <see cref="SudokuConstraintGraph" /> instance that is modelling the specified Sudoku
    ///     problem as a binary CSP.
    /// </summary>
    /// <param name="problem">The problem to be modelled.</param>
    /// <returns>A new <see cref="SudokuConstraintGraph" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="problem" /> is <see langword="null" />.</exception>
    public static SudokuConstraintGraph ModellingProblem(SudokuProblem problem)
    {
        ArgumentNullException.ThrowIfNull(problem);

        SudokuConstraintGraph constraintGraph = new(problem.Grid.AreaInSquares - problem.FilledSquares.Count);
        constraintGraph.Model(problem);

        return constraintGraph;
    }

    private protected override void PopulateProblemData(SudokuProblem problem)
    {
        foreach ((Square square, int number) in problem.FilledSquares)
        {
            (int column, int row, int sector) = (square.Column, square.Row, square.GetSector());
            _emptySquares.Set((column * GridSideLength) + row, false);
            _columnPossibleNumbers[column].Set(number, false);
            _rowPossibleNumbers[row].Set(number, false);
            _sectorPossibleNumbers[sector].Set(number, false);
        }
    }

    private protected override IEnumerable<Square> GetVariables()
    {
        (int column, int row, int upperBound) = (0, 0, _emptySquares.Length);

        for (int i = 0; i < upperBound; i++)
        {
            if (_emptySquares.Get(i))
            {
                yield return new Square(column, row);
            }

            row = row + 1 == GridSideLength ? 0 : row + 1;
            column = row > 0 ? column : column + 1;
        }
    }

    private protected override IEnumerable<int> GetDomainValues(Square presentVariable)
    {
        (int column, int row, int sector) = (presentVariable.Column, presentVariable.Row, presentVariable.GetSector());

        _presentVariablePossibleNumbers
            .And(_columnPossibleNumbers[column])
            .And(_rowPossibleNumbers[row])
            .And(_sectorPossibleNumbers[sector]);

        for (int n = 1; n <= GridSideLength; n++)
        {
            if (_presentVariablePossibleNumbers.Get(n))
            {
                yield return n;
            }
        }

        _presentVariablePossibleNumbers.SetAll(true);
    }

    private protected override bool TryGetBinaryPredicate(Square firstVariable,
        Square secondVariable,
        [NotNullWhen(true)] out Func<int, int, bool>? binaryPredicate)
    {
        binaryPredicate = SameColumnOrRowOrSector(in firstVariable, in secondVariable)
            ? DifferentNumbers
            : null;

        return binaryPredicate is not null;
    }

    private static bool SameColumnOrRowOrSector(in Square squareA, in Square squareB) =>
        squareA.Column == squareB.Column
        || squareA.Row == squareB.Row
        || squareA.GetSector() == squareB.GetSector();

    private protected override void ClearProblemData()
    {
        foreach (BitArray array in _columnPossibleNumbers)
        {
            array.SetAll(true);
        }

        foreach (BitArray array in _rowPossibleNumbers)
        {
            array.SetAll(true);
        }

        foreach (BitArray array in _sectorPossibleNumbers)
        {
            array.SetAll(true);
        }

        _emptySquares.SetAll(true);
    }

    private static BitArray[] InitializePossibleNumbersIndexedLookup() =>
        Enumerable.Range(0, GridSideLength)
            .Select(_ => InitializeSinglePossibleNumbersArray())
            .ToArray();

    private static BitArray InitializeSinglePossibleNumbersArray() => new(GridSideLength + 1, true);

    private static bool DifferentNumbers(int firstNumber, int secondNumber) => firstNumber != secondNumber;
}
