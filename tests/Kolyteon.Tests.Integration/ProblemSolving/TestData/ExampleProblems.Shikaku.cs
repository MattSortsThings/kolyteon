using System.Collections;
using Kolyteon.Shikaku;

namespace Kolyteon.Tests.Integration.ProblemSolving.TestData;

public static partial class ExampleProblems
{
    public static class Shikaku
    {
        public sealed class Solvable : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { 0025, null, null, null, null },
                        { null, null, null, null, null },
                        { null, null, null, null, null },
                        { null, null, null, null, null },
                        { null, null, null, null, null }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { 0005, null, null, null, null },
                        { null, null, null, null, null },
                        { null, null, null, null, null },
                        { null, null, null, null, null },
                        { null, null, null, null, 0020 }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { 0005, null, null, null, null },
                        { null, 0005, null, null, null },
                        { null, null, 0005, null, null },
                        { null, null, null, 0005, null },
                        { null, null, null, null, 0005 }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { null, null, null, null, 0010 },
                        { null, null, null, null, null },
                        { null, null, null, null, null },
                        { null, null, null, null, 0010 },
                        { 0005, null, null, null, null }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { null, null, null, null, null, null, null, 0005, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, 0016, null, null, 0009, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, 0020, null, null, null, null, null, 0045, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { 0002, null, null, 0003, null, null, null, null, null, null }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { 0016, null, null, null, 0009, null, null, null, null, 0005 },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { 0020, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { 0002, null, null, null, 0003, null, null, null, null, 0045 }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { null, null, null, 0010, null, null, null, null, null, null },
                        { 0015, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, 0025, null, null, null, 0010 },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, 0002, null, null, null, null, null, 0012, null, null },
                        { null, 0006, null, null, null, null, null, null, null, 0004 },
                        { null, null, null, null, 0016, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { null, null, null, 0010, null, null, null, null, null, null },
                        { 0006, null, null, null, null, 0010, null, null, null, 0004 },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, 0010, null, null, null, null, null, null },
                        { null, null, null, null, null, null, 0010, null, null, 0004 },
                        { 0006, null, null, null, null, null, null, null, null, null },
                        { null, 0002, null, null, null, null, null, 0012, null, null },
                        { null, 0006, null, null, null, null, null, null, null, 0004 },
                        { null, null, null, null, 0016, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, 0025, null, null, 0015, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, 0002, 0012, null, null },
                        { null, null, null, null, 0010, 0004, 0002, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, 0030, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, 0015, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, 0012, null, null },
                        { null, null, null, 0020, 0005, null, 0002, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, 0010, 0004, 0002, null, null, null },
                        { 0003, 0027, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { null, null, null, null, null, null, 0010, null, null, null },
                        { null, null, null, 0010, null, null, null, null, null, null },
                        { null, 0010, null, null, null, null, null, null, 0010, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, 0042, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { 0004, null, null, null, null, null, null, null, null, 0004 },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, 0002, null, null, 0006, null, null, null, null, 0002 }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, 0016, null, null, null, null, null, null },
                        { null, 0010, null, null, null, null, null, 0004, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, 0020, null, null, null, null, null, null },
                        { 0004, null, null, null, null, null, null, null, 0020, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, 0006, null, null, null, null },
                        { null, null, null, null, null, null, 0020, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { 0008, null, null, null, null, null, null, null, null, 0016 },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, 0008, 0004, null, null, null },
                        { null, null, null, null, null, null, 0004, null, null, null },
                        { null, null, null, null, null, null, 0010, null, null, null },
                        { 0004, null, null, null, null, 0008, 0012, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, 0006, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, 0020, null, null, null, null, null }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { null, null, null, null, null, null, null, null, null, 0010 },
                        { 0002, null, null, null, null, null, null, null, null, 0008 },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, 0064, null, null, null, null, null, null },
                        { 0002, null, null, null, null, null, null, null, null, null },
                        { 0005, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, 0004, null, 0002, 0003, null, null }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, 0021, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, 0009, null },
                        { null, null, null, null, null, null, null, null, 0003, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { 0021, null, null, null, null, null, null, 0006, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, 0040, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, 0012, null, null, null, null, 0009, null },
                        { null, null, null, 0012, null, null, null, null, 0003, null },
                        { null, null, null, null, null, 0030, null, null, null, null },
                        { null, null, null, null, null, null, null, 0006, null, null },
                        { null, null, null, 0016, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, 0012, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { null, null, null, null, 0015, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, 0018, null },
                        { null, null, 0009, null, null, null, null, null, null, null },
                        { null, null, 0006, 0005, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, 0007, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, null, null, null, null },
                        { null, null, null, null, null, null, 0040, null, null, null }
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
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { null, null, 0010, null, null },
                        { null, null, null, 0006, null },
                        { null, null, null, null, null },
                        { null, null, null, null, null },
                        { null, null, null, null, 0009 }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { 0002, null, null, 0010, null },
                        { null, null, null, null, null },
                        { null, null, null, null, 0003 },
                        { null, null, null, null, null },
                        { null, 0010, null, null, null }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { 0004, 0004, 0004, null, 0005 },
                        { null, null, null, null, null },
                        { null, null, null, 0004, null },
                        { null, null, null, 0004, null },
                        { null, null, null, null, null }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { 0004, null, null, null, null },
                        { null, null, null, null, null },
                        { null, null, null, null, null },
                        { null, null, null, null, null },
                        { null, null, null, null, 0021 }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { 0004, null, null, null, null },
                        { null, null, null, null, null },
                        { null, null, null, null, null },
                        { 0005, 0006, null, null, null },
                        { null, null, null, null, 0010 }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { null, 0004, 0005, null, null },
                        { null, null, null, null, null },
                        { null, null, null, null, 0006 },
                        { null, null, null, null, 0010 },
                        { null, null, null, null, null }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { null, 0002, null, null, null },
                        { null, null, null, null, null },
                        { null, null, null, null, 0010 },
                        { null, null, null, null, 0008 },
                        { 0005, null, null, null, null }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { null, 0002, null, null, null },
                        { null, null, null, null, null },
                        { null, null, null, null, 0010 },
                        { null, null, null, null, 0008 },
                        { 0005, null, null, null, null }
                    })
                ];

                yield return
                [
                    ShikakuProblem.FromGrid(new int?[,]
                    {
                        { 0004, null, null, null, null },
                        { null, 0005, null, null, null },
                        { null, null, 0005, null, null },
                        { null, null, null, 0005, null },
                        { null, null, null, null, 0006 }
                    })
                ];
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
