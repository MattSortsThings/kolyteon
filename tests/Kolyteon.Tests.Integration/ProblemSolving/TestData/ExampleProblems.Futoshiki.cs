using System.Collections;
using Kolyteon.Futoshiki;

namespace Kolyteon.Tests.Integration.ProblemSolving.TestData;

public static partial class ExampleProblems
{
    public static class Futoshiki
    {
        public sealed class Solvable : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return
                [
                    FutoshikiProblem.Create()
                        .FromGrid(new int?[,]
                        {
                            { null, 0002, 0003, 0004 },
                            { 0002, 0003, 0004, 0001 },
                            { 0003, 0004, 0001, 0002 },
                            { 0004, 0001, 0002, 0003 }
                        }).AddSign(LessThanSign.Parse("(0,0)<(1,0)"))
                        .Build()
                ];

                yield return
                [
                    FutoshikiProblem.Create()
                        .FromGrid(new int?[,]
                        {
                            { null, null, null, null },
                            { 0002, 0003, 0004, 0001 },
                            { 0003, 0004, 0001, 0002 },
                            { 0004, 0001, 0002, 0003 }
                        }).AddSign(LessThanSign.Parse("(0,0)<(1,0)"))
                        .AddSign(LessThanSign.Parse("(1,0)<(2,0)"))
                        .Build()
                ];

                yield return
                [
                    FutoshikiProblem.Create()
                        .FromGrid(new int?[,]
                        {
                            { 0003, null, null, null },
                            { null, null, null, null },
                            { null, null, null, null },
                            { null, null, null, null }
                        }).AddSign(LessThanSign.Parse("(0,0)<(0,1)"))
                        .AddSign(GreaterThanSign.Parse("(0,3)>(1,3)"))
                        .AddSign(GreaterThanSign.Parse("(1,1)>(2,1)"))
                        .AddSign(GreaterThanSign.Parse("(2,1)>(3,1)"))
                        .AddSign(LessThanSign.Parse("(2,2)<(3,2)"))
                        .Build()
                ];

                yield return
                [
                    FutoshikiProblem.Create()
                        .FromGrid(new int?[,]
                        {
                            { 0003, null, null, null },
                            { null, null, 0001, null },
                            { null, null, null, 0003 },
                            { null, null, null, 0002 }
                        }).AddSign(GreaterThanSign.Parse("(0,3)>(1,3)"))
                        .AddSign(LessThanSign.Parse("(1,0)<(1,1)"))
                        .Build()
                ];

                yield return
                [
                    FutoshikiProblem.Create()
                        .FromGrid(new int?[,]
                        {
                            { null, null, null, null },
                            { null, null, null, null },
                            { null, null, null, null },
                            { null, null, null, 0002 }
                        }).Build()
                ];

                yield return
                [
                    FutoshikiProblem.Create()
                        .FromGrid(new int?[,]
                        {
                            { null, null, null, null },
                            { null, null, null, null },
                            { null, null, null, null },
                            { null, null, null, 0002 }
                        }).AddSign(GreaterThanSign.Parse("(0,0)>(0,1)"))
                        .AddSign(GreaterThanSign.Parse("(0,2)>(0,3)"))
                        .AddSign(GreaterThanSign.Parse("(0,3)>(1,3)"))
                        .AddSign(LessThanSign.Parse("(2,2)<(3,2)"))
                        .Build()
                ];

                yield return
                [
                    FutoshikiProblem.Create()
                        .FromGrid(new int?[,]
                        {
                            { null, null, null, null },
                            { null, null, null, null },
                            { null, null, null, 0002 },
                            { null, null, null, null }
                        })
                        .AddSign(GreaterThanSign.Parse("(0,0)>(0,1)"))
                        .AddSign(GreaterThanSign.Parse("(0,2)>(0,3)"))
                        .AddSign(GreaterThanSign.Parse("(1,0)>(1,1)"))
                        .AddSign(GreaterThanSign.Parse("(1,0)>(2,0)"))
                        .AddSign(GreaterThanSign.Parse("(1,1)>(1,2)"))
                        .AddSign(GreaterThanSign.Parse("(2,0)>(2,1)"))
                        .Build()
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
                    FutoshikiProblem.Create()
                        .FromGrid(new int?[,]
                        {
                            { null, null, 0003, 0004 },
                            { 0002, 0003, 0004, 0001 },
                            { 0003, 0004, 0001, 0002 },
                            { 0004, 0001, 0002, 0003 }
                        }).AddSign(GreaterThanSign.Parse("(0,0)>(1,0)"))
                        .Build()
                ];

                yield return
                [
                    FutoshikiProblem.Create()
                        .FromGrid(new int?[,]
                        {
                            { 0003, 0002, null, null },
                            { null, null, 0002, null },
                            { null, 0001, null, null },
                            { null, null, 0001, 0003 }
                        }).AddSign(LessThanSign.Parse("(2,0)<(2,1)"))
                        .AddSign(GreaterThanSign.Parse("(2,0)>(3,0)"))
                        .Build()
                ];

                yield return
                [
                    FutoshikiProblem.Create()
                        .FromGrid(new int?[,]
                        {
                            { 0003, null, null, 0004 },
                            { null, null, 0001, null },
                            { null, null, null, 0003 },
                            { null, null, null, 0002 }
                        }).AddSign(GreaterThanSign.Parse("(0,3)>(1,3)"))
                        .AddSign(LessThanSign.Parse("(1,0)<(1,1)"))
                        .Build()
                ];
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
