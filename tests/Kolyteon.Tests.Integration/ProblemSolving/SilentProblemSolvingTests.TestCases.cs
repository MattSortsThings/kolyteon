using System.Diagnostics.CodeAnalysis;
using Kolyteon.Solving;

namespace Kolyteon.Tests.Integration.ProblemSolving;

public abstract partial class SilentProblemSolvingTests
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public static class TestCases
    {
        public sealed class BtPlusBz : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.NaiveBacktracking;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.BrelazHeuristic;
        }

        public sealed class BtPlusMc : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.NaiveBacktracking;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxCardinality;
        }

        public sealed class BtPlusMt : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.NaiveBacktracking;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxTightness;
        }

        public sealed class BtPlusNo : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.NaiveBacktracking;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.NaturalOrdering;
        }

        public sealed class BjPlusBz : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.Backjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.BrelazHeuristic;
        }

        public sealed class BjPlusMc : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.Backjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxCardinality;
        }

        public sealed class BjPlusMt : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.Backjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxTightness;
        }

        public sealed class BjPlusNo : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.Backjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.NaturalOrdering;
        }

        public sealed class GbjPlusBz : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.GraphBasedBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.BrelazHeuristic;
        }

        public sealed class GbjPlusMc : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.GraphBasedBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxCardinality;
        }

        public sealed class GbjPlusMt : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.GraphBasedBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxTightness;
        }

        public sealed class GbjPlusNo : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.GraphBasedBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.NaturalOrdering;
        }

        public sealed class CbjPlusBz : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.ConflictDirectedBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.BrelazHeuristic;
        }

        public sealed class CbjPlusMc : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.ConflictDirectedBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxCardinality;
        }

        public sealed class CbjPlusMt : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.ConflictDirectedBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxTightness;
        }

        public sealed class CbjPlusNo : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.ConflictDirectedBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.NaturalOrdering;
        }

        public sealed class FcPlusBz : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.ForwardChecking;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.BrelazHeuristic;
        }

        public sealed class FcPlusMc : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.ForwardChecking;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxCardinality;
        }

        public sealed class FcPlusMt : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.ForwardChecking;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxTightness;
        }

        public sealed class FcPlusNo : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.ForwardChecking;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.NaturalOrdering;
        }

        public sealed class PlaPlusBz : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.PartialLookingAhead;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.BrelazHeuristic;
        }

        public sealed class PlaPlusMc : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.PartialLookingAhead;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxCardinality;
        }

        public sealed class PlaPlusMt : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.PartialLookingAhead;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxTightness;
        }

        public sealed class PlaPlusNo : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.PartialLookingAhead;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.NaturalOrdering;
        }

        public sealed class FlaPlusBz : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.FullLookingAhead;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.BrelazHeuristic;
        }

        public sealed class FlaPlusMc : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.FullLookingAhead;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxCardinality;
        }

        public sealed class FlaPlusMt : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.FullLookingAhead;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxTightness;
        }

        public sealed class FlaPlusNo : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.FullLookingAhead;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.NaturalOrdering;
        }

        public sealed class MacPlusBz : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.MaintainingArcConsistency;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.BrelazHeuristic;
        }

        public sealed class MacPlusMc : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.MaintainingArcConsistency;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxCardinality;
        }

        public sealed class MacPlusMt : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.MaintainingArcConsistency;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxTightness;
        }

        public sealed class MacPlusNo : SilentProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.MaintainingArcConsistency;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.NaturalOrdering;
        }
    }
}
