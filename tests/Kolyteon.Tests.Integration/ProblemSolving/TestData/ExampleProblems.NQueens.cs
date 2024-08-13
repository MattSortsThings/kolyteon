using System.Collections;
using Kolyteon.NQueens;

namespace Kolyteon.Tests.Integration.ProblemSolving.TestData;

public static partial class ExampleProblems
{
    public static class NQueens
    {
        public sealed class Solvable : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return [NQueensProblem.FromN(1)];
                yield return [NQueensProblem.FromN(4)];
                yield return [NQueensProblem.FromN(5)];
                yield return [NQueensProblem.FromN(6)];
                yield return [NQueensProblem.FromN(7)];
                yield return [NQueensProblem.FromN(8)];
                yield return [NQueensProblem.FromN(9)];
                yield return [NQueensProblem.FromN(10)];
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public sealed class Unsolvable : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return [NQueensProblem.FromN(2)];
                yield return [NQueensProblem.FromN(3)];
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
