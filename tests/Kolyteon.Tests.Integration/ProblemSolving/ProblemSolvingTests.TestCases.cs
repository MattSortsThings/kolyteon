using System.Diagnostics.CodeAnalysis;
using Kolyteon.Solving;

namespace Kolyteon.Tests.Integration.ProblemSolving;

public abstract partial class ProblemSolvingTests
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public static class TestCases
    {
        public sealed class BtPlusBz : ProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.NaiveBacktracking;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.BrelazHeuristic;
        }

        public sealed class BtPlusMc : ProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.NaiveBacktracking;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxCardinality;
        }

        public sealed class BtPlusMt : ProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.NaiveBacktracking;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxTightness;
        }

        public sealed class BtPlusNo : ProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.NaiveBacktracking;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.NaturalOrdering;
        }

        public sealed class BjPlusBz : ProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.Backjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.BrelazHeuristic;
        }

        public sealed class BjPlusMc : ProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.Backjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxCardinality;
        }

        public sealed class BjPlusMt : ProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.Backjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxTightness;
        }

        public sealed class BjPlusNo : ProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.Backjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.NaturalOrdering;
        }

        public sealed class GbjPlusBz : ProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.GraphBasedBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.BrelazHeuristic;
        }

        public sealed class GbjPlusMc : ProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.GraphBasedBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxCardinality;
        }

        public sealed class GbjPlusMt : ProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.GraphBasedBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxTightness;
        }

        public sealed class GbjPlusNo : ProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.GraphBasedBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.NaturalOrdering;
        }

        public sealed class CbjPlusBz : ProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.ConflictDirectedBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.BrelazHeuristic;
        }

        public sealed class CbjPlusMc : ProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.ConflictDirectedBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxCardinality;
        }

        public sealed class CbjPlusMt : ProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.ConflictDirectedBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxTightness;
        }

        public sealed class CbjPlusNo : ProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.ConflictDirectedBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.NaturalOrdering;
        }
    }
}
