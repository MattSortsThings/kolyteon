using System.Diagnostics.CodeAnalysis;
using Kolyteon.Solving;

namespace Kolyteon.Tests.Integration.ProblemSolving;

public abstract partial class VerboseProblemSolvingTests
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public static class TestCases
    {
        public sealed class BtPlusBz : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.NaiveBacktracking;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.BrelazHeuristic;
        }

        public sealed class BtPlusMc : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.NaiveBacktracking;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxCardinality;
        }

        public sealed class BtPlusMt : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.NaiveBacktracking;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxTightness;
        }

        public sealed class BtPlusNo : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.NaiveBacktracking;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.NaturalOrdering;
        }

        public sealed class BjPlusBz : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.Backjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.BrelazHeuristic;
        }

        public sealed class BjPlusMc : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.Backjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxCardinality;
        }

        public sealed class BjPlusMt : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.Backjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxTightness;
        }

        public sealed class BjPlusNo : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.Backjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.NaturalOrdering;
        }

        public sealed class GbjPlusBz : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.GraphBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.BrelazHeuristic;
        }

        public sealed class GbjPlusMc : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.GraphBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxCardinality;
        }

        public sealed class GbjPlusMt : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.GraphBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxTightness;
        }

        public sealed class GbjPlusNo : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.GraphBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.NaturalOrdering;
        }

        public sealed class CbjPlusBz : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.ConflictBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.BrelazHeuristic;
        }

        public sealed class CbjPlusMc : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.ConflictBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxCardinality;
        }

        public sealed class CbjPlusMt : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.ConflictBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxTightness;
        }

        public sealed class CbjPlusNo : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.ConflictBackjumping;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.NaturalOrdering;
        }

        public sealed class FcPlusBz : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.ForwardChecking;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.BrelazHeuristic;
        }

        public sealed class FcPlusMc : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.ForwardChecking;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxCardinality;
        }

        public sealed class FcPlusMt : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.ForwardChecking;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxTightness;
        }

        public sealed class FcPlusNo : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.ForwardChecking;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.NaturalOrdering;
        }

        public sealed class PlaPlusBz : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.PartialLookingAhead;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.BrelazHeuristic;
        }

        public sealed class PlaPlusMc : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.PartialLookingAhead;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxCardinality;
        }

        public sealed class PlaPlusMt : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.PartialLookingAhead;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxTightness;
        }

        public sealed class PlaPlusNo : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.PartialLookingAhead;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.NaturalOrdering;
        }

        public sealed class FlaPlusBz : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.FullLookingAhead;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.BrelazHeuristic;
        }

        public sealed class FlaPlusMc : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.FullLookingAhead;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxCardinality;
        }

        public sealed class FlaPlusMt : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.FullLookingAhead;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxTightness;
        }

        public sealed class FlaPlusNo : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.FullLookingAhead;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.NaturalOrdering;
        }

        public sealed class MacPlusBz : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.MaintainingArcConsistency;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.BrelazHeuristic;
        }

        public sealed class MacPlusMc : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.MaintainingArcConsistency;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxCardinality;
        }

        public sealed class MacPlusMt : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.MaintainingArcConsistency;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxTightness;
        }

        public sealed class MacPlusNo : VerboseProblemSolvingTests
        {
            private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.MaintainingArcConsistency;

            private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.NaturalOrdering;
        }
    }
}
