using Kolyteon.Solving;

namespace Kolyteon.Tests.Integration.ProblemSolving;

public abstract partial class ProblemSolvingTests
{
    public sealed class BtPlusNo : ProblemSolvingTests
    {
        private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.NaiveBacktracking;

        private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.NaturalOrdering;
    }
}
