using Kolyteon.Solving;

namespace Kolyteon.Tests.Integration.ProblemSolving.TestUtils;

public sealed class TestSolvingProgressReporter<TVariable, TDomainValue> : SolvingProgressReporter<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    protected override void OnReset() { }

    protected override void OnReport() { }
}
