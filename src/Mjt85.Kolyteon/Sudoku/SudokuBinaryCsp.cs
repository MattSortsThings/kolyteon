using System.Collections;
using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.Sudoku;

/// <summary>
///     Can model any Sudoku puzzle as a binary CSP.
/// </summary>
public sealed class SudokuBinaryCsp : BinaryCsp<SudokuPuzzle, EmptyCell, int>
{
    private const int GridSideLength = 9;
    private static readonly DifferentNumbersPredicate DifferentNumbers = new();

    private readonly BitArray _emptyCells = new(GridSideLength * GridSideLength, true);
    private readonly BitArray[] _freeNumbersByColumn = InitializeFreeNumbersLookup();
    private readonly BitArray[] _freeNumbersByRow = InitializeFreeNumbersLookup();
    private readonly BitArray[] _freeNumbersBySector = InitializeFreeNumbersLookup();

    /// <summary>
    ///     Initializes a new <see cref="SudokuBinaryCsp" /> instance that is not modelling a problem and has the specified
    ///     initial capacity.
    /// </summary>
    /// <param name="capacity">
    ///     The number of binary CSP variables the new <see cref="SudokuBinaryCsp" /> instance can initially store.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity" /> is negative.</exception>
    public SudokuBinaryCsp(int capacity) : base(capacity)
    {
    }

    protected override void PopulateProblemData(SudokuPuzzle problem)
    {
        foreach (var (column, row, sector, number) in problem.FilledCells)
        {
            _emptyCells.Set(column * GridSideLength + row, false);
            _freeNumbersByColumn[column].Set(number, false);
            _freeNumbersByRow[row].Set(number, false);
            _freeNumbersBySector[sector].Set(number, false);
        }
    }

    protected override void ClearProblemData()
    {
        foreach (BitArray arr in _freeNumbersByColumn.Concat(_freeNumbersByRow).Concat(_freeNumbersBySector).Append(_emptyCells))
        {
            arr.SetAll(true);
        }
    }

    protected override IEnumerable<EmptyCell> GetVariables()
    {
        var column = 0;
        var row = 0;
        for (var i = 0; i < _emptyCells.Length; i++)
        {
            if (_emptyCells.Get(i))
            {
                yield return new EmptyCell(column, row);
            }

            row = row + 1 == GridSideLength ? 0 : row + 1;
            column = row > 0 ? column : column + 1;
        }
    }

    protected override IEnumerable<int> GetDomainOf(EmptyCell variable)
    {
        var (column, row, sector) = variable;
        BitArray array = InitializeFreeNumbersArray()
            .And(_freeNumbersByColumn[column])
            .And(_freeNumbersByRow[row])
            .And(_freeNumbersBySector[sector]);

        for (var n = 1; n <= 9; n++)
        {
            if (array.Get(n))
            {
                yield return n;
            }
        }
    }

    protected override IBinaryPredicate<int> GetBinaryPredicateFor(EmptyCell variable1, EmptyCell variable2) =>
        variable1.Column == variable2.Column
        || variable1.Row == variable2.Row
        || variable1.Sector == variable2.Sector
            ? DifferentNumbers
            : NotAdjacent;

    private static BitArray[] InitializeFreeNumbersLookup()
    {
        return Enumerable.Range(0, GridSideLength)
            .Select(_ => InitializeFreeNumbersArray())
            .ToArray();
    }

    private static BitArray InitializeFreeNumbersArray() => new(GridSideLength + 1, true);

    private sealed class DifferentNumbersPredicate : IBinaryPredicate<int>
    {
        public bool CanAssign(in int firstDomainValue, in int secondDomainValue) => firstDomainValue != secondDomainValue;
    }
}
