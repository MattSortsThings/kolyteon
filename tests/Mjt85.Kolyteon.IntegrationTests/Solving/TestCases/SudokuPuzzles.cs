using Mjt85.Kolyteon.Sudoku;

namespace Mjt85.Kolyteon.IntegrationTests.Solving.TestCases;

public sealed class SudokuPuzzles
{
    public sealed class Solvable : TheoryData<SudokuPuzzle>
    {
        public Solvable()
        {
            Add(SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                { 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
                { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
                { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
                { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
                { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
                { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
                { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
                { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 }
            }));

            Add(SudokuPuzzle.FromGrid(new int?[,]
            {
                { 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                { 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
                { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
                { 0008, 0009, 0001, null, null, null, 0005, 0006, 0007 },
                { 0002, 0003, 0004, null, null, null, 0008, 0009, 0001 },
                { 0005, 0006, 0007, null, null, null, 0002, 0003, 0004 },
                { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
                { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
                { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 }
            }));

            Add(SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, 0002, null, null, 0005, 0006, 0007, 0008, 0009 },
                { 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
                { null, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
                { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
                { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
                { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
                { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
                { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
                { null, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 }
            }));

            Add(SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, 0002, null, null, 0005, 0006, 0007, 0008, 0009 },
                { null, 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
                { null, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
                { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
                { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
                { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
                { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
                { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
                { null, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 }
            }));

            Add(SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, null, null, 0002, 0007, 0006, 0004, 0005, 0001 },
                { null, null, null, 0004, 0005, 0001, 0008, 0009, 0003 },
                { null, null, null, 0008, 0009, 0003, 0002, 0007, 0006 },
                { 0005, 0001, 0008, null, null, null, 0007, 0006, 0004 },
                { 0009, 0003, 0002, null, null, null, 0005, 0001, 0008 },
                { 0007, 0006, 0004, null, null, null, 0009, 0003, 0002 },
                { 0006, 0004, 0005, 0001, 0008, 0009, null, null, null },
                { 0001, 0008, 0009, 0003, 0002, 0007, null, null, null },
                { 0003, 0002, 0007, 0006, 0004, 0005, null, null, null }
            }));

            Add(SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, 0009, 0003, 0002, 0007, 0006, null, 0005, 0001 },
                { 0002, null, 0006, null, null, null, null, 0009, 0003 },
                { 0004, 0005, 0001, 0008, null, 0003, 0002, null, 0006 },
                { null, 0001, 0008, null, 0003, null, null, 0006, 0004 },
                { 0009, null, null, 0007, null, 0004, 0005, null, 0008 },
                { null, null, null, null, 0001, null, 0009, 0003, 0002 },
                { null, 0004, 0005, 0001, null, 0009, null, 0002, 0007 },
                { 0001, null, null, null, null, 0007, null, null, 0005 },
                { 0003, null, 0007, 0006, null, null, 0001, 0008, null }
            }));

            Add(SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, null, null, null, null, null, null, null, null },
                { 0002, 0007, 0006, null, 0005, 0001, 0008, 0009, 0003 },
                { null, 0005, 0001, null, 0009, 0003, 0002, 0007, 0006 },
                { null, 0001, 0008, 0009, 0003, 0002, 0007, 0006, null },
                { null, null, null, null, null, null, null, null, null },
                { null, 0006, 0004, 0005, 0001, 0008, 0009, 0003, 0002 },
                { 0006, null, 0005, null, 0008, 0009, 0003, 0002, 0007 },
                { 0001, null, 0009, 0003, null, 0007, 0006, null, 0005 },
                { null, 0002, 0007, 0006, null, 0005, 0001, 0008, null }
            }));

            Add(SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, 0009, 0003, 0002, 0007, 0006, null, 0005, 0001 },
                { 0002, null, 0006, null, null, null, null, 0009, 0003 },
                { null, 0005, 0001, 0008, null, 0003, 0002, null, 0006 },
                { null, 0001, 0008, null, null, null, null, 0006, 0004 },
                { 0009, null, null, null, null, null, 0005, null, 0008 },
                { null, null, null, null, null, null, 0009, 0003, 0002 },
                { null, 0004, 0005, 0001, null, 0009, null, 0002, 0007 },
                { 0001, null, null, null, null, 0007, null, null, 0005 },
                { 0003, null, 0007, 0006, null, null, null, 0008, null }
            }));

            Add(SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, 0003, null, null, 0007, 0002, null, null, 0001 },
                { 0004, 0001, 0005, null, null, 0008, null, 0007, 0006 },
                { 0002, 0006, 0007, null, 0005, 0004, 0008, 0009, 0003 },
                { null, 0008, 0001, 0002, 0003, 0009, 0007, 0006, 0004 },
                { null, null, 0003, null, null, null, 0005, null, null },
                { 0007, 0004, 0006, 0008, 0001, 0005, 0009, 0003, null },
                { 0006, 0005, 0004, null, null, 0001, null, 0002, 0007 },
                { 0001, 0009, 0008, 0007, 0002, null, 0006, 0004, 0005 },
                { null, 0007, 0002, 0005, 0004, 0006, 0001, 0008, 0009 }
            }));

            Add(SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, 0003, null, null, 0007, 0002, null, null, 0001 },
                { 0004, 0001, 0005, null, null, 0008, null, 0007, 0006 },
                { 0002, 0006, 0007, null, 0005, 0004, 0008, 0009, 0003 },
                { null, 0008, 0001, 0002, 0003, 0009, 0007, 0006, 0004 },
                { null, null, null, null, null, null, 0005, null, null },
                { 0007, null, null, 0008, 0001, 0005, 0009, 0003, null },
                { 0006, 0005, 0004, null, null, 0001, null, 0002, 0007 },
                { 0001, null, 0008, 0007, 0002, null, 0006, 0004, 0005 },
                { null, 0007, 0002, 0005, 0004, 0006, 0001, 0008, 0009 }
            }));

            Add(SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, 0003, null, null, 0007, 0002, null, null, 0001 },
                { 0004, 0001, 0005, null, null, 0008, null, 0007, 0006 },
                { 0002, 0006, 0007, null, null, null, 0008, 0009, 0003 },
                { null, 0008, 0001, null, 0003, 0009, 0007, 0006, 0004 },
                { null, null, null, null, null, null, 0005, null, null },
                { 0007, null, null, 0008, 0001, 0005, 0009, 0003, null },
                { 0006, 0005, 0004, null, null, 0001, null, 0002, 0007 },
                { 0001, null, 0008, 0007, 0002, null, 0006, 0004, 0005 },
                { null, 0007, 0002, null, null, null, 0001, null, 0009 }
            }));

            Add(SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, 0003, null, null, 0007, 0002, null, null, 0001 },
                { null, null, null, null, null, 0008, null, 0007, 0006 },
                { null, null, null, null, null, null, 0008, 0009, 0003 },
                { null, 0008, 0001, null, 0003, 0009, 0007, 0006, 0004 },
                { null, null, null, null, null, null, 0005, null, null },
                { 0007, null, null, 0008, 0001, 0005, 0009, 0003, null },
                { 0006, 0005, 0004, null, null, 0001, null, 0002, 0007 },
                { 0001, null, 0008, 0007, 0002, null, 0006, 0004, 0005 },
                { null, 0007, 0002, null, null, null, 0001, null, 0009 }
            }));

            Add(SudokuPuzzle.FromGrid(new int?[,]
            {
                { 0009, 0008, null, 0002, null, 0003, 0004, null, 0005 },
                { 0006, 0005, 0004, null, 0007, 0009, null, 0003, 0002 },
                { null, 0002, 0001, 0005, 0004, 0006, null, 0009, 0008 },
                { null, 0009, 0008, null, 0002, 0004, null, null, null },
                { 0004, null, 0002, null, 0005, null, 0008, 0001, 0009 },
                { null, 0006, 0005, null, 0008, 0001, 0002, 0004, 0003 },
                { 0008, 0007, 0006, null, null, 0002, null, null, 0004 },
                { null, null, null, 0007, 0006, null, 0009, null, 0001 },
                { 0002, 0001, 0009, 0004, 0003, 0005, 0006, null, 0007 }
            }));

            Add(SudokuPuzzle.FromGrid(new int?[,]
            {
                { 0009, 0008, null, 0002, null, 0003, 0004, null, 0005 },
                { 0006, 0005, 0004, null, 0007, 0009, null, 0003, 0002 },
                { null, 0002, 0001, 0005, 0004, 0006, null, 0009, 0008 },
                { null, 0009, 0008, null, 0002, 0004, null, null, null },
                { 0004, null, 0002, null, 0005, null, 0008, 0001, 0009 },
                { null, 0006, 0005, null, 0008, 0001, 0002, 0004, 0003 },
                { null, 0007, null, null, null, 0002, null, null, 0004 },
                { null, null, null, 0007, 0006, null, 0009, null, 0001 },
                { null, 0001, 0009, 0004, 0003, 0005, 0006, null, 0007 }
            }));

            Add(SudokuPuzzle.FromGrid(new int?[,]
            {
                { 0009, 0008, null, 0002, null, 0003, 0004, null, 0005 },
                { 0006, 0005, 0004, null, 0007, 0009, null, 0003, 0002 },
                { null, 0002, 0001, 0005, 0004, 0006, null, 0009, 0008 },
                { null, 0009, 0008, null, 0002, 0004, null, null, null },
                { 0004, null, 0002, null, 0005, null, 0008, 0001, 0009 },
                { null, 0006, 0005, null, 0008, 0001, 0002, 0004, 0003 },
                { null, 0007, null, null, null, 0002, null, null, 0004 },
                { null, null, null, null, 0006, null, 0009, null, 0001 },
                { null, 0001, 0009, null, 0003, 0005, 0006, null, 0007 }
            }));

            Add(SudokuPuzzle.FromGrid(new int?[,]
            {
                { 0001, null, null, null, null, null, null, null, null },
                { null, 0002, null, null, null, null, null, null, null },
                { null, null, 0003, null, null, null, null, null, null },
                { null, null, null, 0004, null, null, null, null, null },
                { null, null, null, null, 0005, null, null, null, null },
                { null, null, null, null, null, 0006, null, null, null },
                { null, null, null, null, null, null, 0007, null, null },
                { null, null, null, null, null, null, null, 0008, null },
                { null, null, null, null, null, null, null, null, 0009 }
            }));

            Add(SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            }));
        }
    }

    public sealed class Unsolvable : TheoryData<SudokuPuzzle>
    {
        public Unsolvable()
        {
            Add(SudokuPuzzle.FromGrid(new int?[,]
            {
                { 0009, 0008, 0006, 0001, 0005, 0007, 004, 0003, 0002 },
                { null, null, null, null, 0002, null, null, null, null },
                { null, null, null, null, 0003, null, null, null, null },
                { null, null, null, 0003, 004, 0005, null, null, null },
                { 004, 0005, 0007, 0006, null, 0009, 0003, 0002, 0001 },
                { null, null, null, 0007, 0008, 0001, null, null, null },
                { null, null, null, null, 0007, null, null, null, null },
                { null, null, null, null, 0009, null, null, null, null },
                { 0002, 0003, 004, 0005, 0001, 0006, 0007, 0008, 0009 }
            }));

            Add(SudokuPuzzle.FromGrid(new int?[,]
            {
                { 0001, 0002, 0003, null, null, null, null, null, null },
                { 004, null, 0006, 0005, 0007, 0008, null, null, null },
                { 0007, 0008, 0009, null, null, null, null, null, null },
                { null, 0005, null, null, null, null, null, null, null },
                { null, 004, null, null, null, null, null, null, null },
                { null, 0003, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            }));
        }
    }
}
