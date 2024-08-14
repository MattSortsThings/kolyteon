using System.Collections;
using Kolyteon.Sudoku;

namespace Kolyteon.Tests.Integration.ProblemSolving.TestData;

public static partial class ExampleProblems
{
    public static class Sudoku
    {
        public sealed class Solvable : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return
                [
                    SudokuProblem.FromGrid(new int?[,]
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
                    })
                ];

                yield return
                [
                    SudokuProblem.FromGrid(new int?[,]
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
                    })
                ];

                yield return
                [
                    SudokuProblem.FromGrid(new int?[,]
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
                    })
                ];

                yield return
                [
                    SudokuProblem.FromGrid(new int?[,]
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
                    })
                ];

                yield return
                [
                    SudokuProblem.FromGrid(new int?[,]
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
                    })
                ];

                yield return
                [
                    SudokuProblem.FromGrid(new int?[,]
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
                    })
                ];

                yield return
                [
                    SudokuProblem.FromGrid(new int?[,]
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
                    })
                ];

                yield return
                [
                    SudokuProblem.FromGrid(new int?[,]
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
                    })
                ];

                yield return
                [
                    SudokuProblem.FromGrid(new int?[,]
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
                    })
                ];

                yield return
                [
                    SudokuProblem.FromGrid(new int?[,]
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
                    })
                ];

                yield return
                [
                    SudokuProblem.FromGrid(new int?[,]
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
                    })
                ];

                yield return
                [
                    SudokuProblem.FromGrid(new int?[,]
                    {
                        { null, 0008, null, 0002, null, 0003, 0004, null, null },
                        { 0006, 0005, 0004, null, 0007, 0009, null, 0003, 0002 },
                        { null, 0002, 0001, 0005, 0004, 0006, null, 0009, 0008 },
                        { null, 0009, 0008, null, 0002, 0004, null, null, null },
                        { 0004, null, 0002, null, 0005, null, 0008, 0001, 0009 },
                        { null, null, 0005, null, 0008, 0001, 0002, 0004, 0003 },
                        { 0008, 0007, 0006, null, null, 0002, null, null, 0004 },
                        { null, null, null, 0007, 0006, null, 0009, null, 0001 },
                        { null, 0001, 0009, 0004, 0003, 0005, 0006, null, null }
                    })
                ];

                yield return
                [
                    SudokuProblem.FromGrid(new int?[,]
                    {
                        { null, 0008, null, 0002, null, 0003, 0004, null, null },
                        { 0006, 0005, null, null, 0007, 0009, null, 0003, 0002 },
                        { null, 0002, null, 0005, 0004, 0006, null, 0009, 0008 },
                        { null, 0009, null, null, 0002, 0004, null, null, null },
                        { 0004, null, null, null, 0005, null, 0008, null, 0009 },
                        { null, 0006, null, null, 0008, 0001, 0002, 0004, 0003 },
                        { 0008, 0007, null, null, null, 0002, null, null, 0004 },
                        { null, null, null, 0007, 0006, null, 0009, null, 0001 },
                        { null, 0001, null, null, null, null, 0006, null, null }
                    })
                ];
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public sealed class Unsolvable : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return
                [
                    SudokuProblem.FromGrid(new int?[,]
                    {
                        { 0009, 0008, 0006, 0001, 0005, 0007, 0004, 0003, 0002 },
                        { null, null, null, null, 0002, null, null, null, null },
                        { null, null, null, null, 0003, null, null, null, null },
                        { null, null, null, 0003, 0004, 0005, null, null, null },
                        { 0004, 0005, 0007, 0006, null, 0009, 0003, 0002, 0001 },
                        { null, null, null, 0007, 0008, 0001, null, null, null },
                        { null, null, null, null, 0007, null, null, null, null },
                        { null, null, null, null, 0009, null, null, null, null },
                        { 0002, 0003, 0004, 0005, 0001, 0006, 0007, 0008, 0009 }
                    })
                ];

                yield return
                [
                    SudokuProblem.FromGrid(new int?[,]
                    {
                        { 0001, 0002, 0003, null, null, null, null, null, null },
                        { 0004, null, 0006, 0005, 0007, 0008, null, null, null },
                        { 0007, 0008, 0009, null, null, null, null, null, null },
                        { null, 0005, null, null, null, null, null, null, null },
                        { null, 0004, null, null, null, null, null, null, null },
                        { null, 0003, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null }
                    })
                ];
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
