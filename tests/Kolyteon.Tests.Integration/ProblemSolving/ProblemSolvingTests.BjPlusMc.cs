using System.Diagnostics.CodeAnalysis;
using Kolyteon.Solving;

namespace Kolyteon.Tests.Integration.ProblemSolving;

public abstract partial class ProblemSolvingTests
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public sealed class BjPlusMc : ProblemSolvingTests
    {
        private protected override CheckingStrategy CheckingStrategy => CheckingStrategy.Backjumping;

        private protected override OrderingStrategy OrderingStrategy => OrderingStrategy.MaxCardinality;
    }
}
